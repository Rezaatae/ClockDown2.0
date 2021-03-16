using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Guy : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float jumpHeight = 5f;
    public bool canMove = true;
    public Transform groundCheck;
    public float groundCheckRadious = 0.2f;
    public LayerMask mouseAimMask;
    public LayerMask groundMask;
    public GameObject bulletPrefab;
    public Transform muzzleTransform;
    public Transform targetTransform;
    public AnimationCurve recoilCurve;
    public float recoilDuration = 0.25f;
    public float recoilMaxRotation = 45f;
    public Transform rightLowerArm;
    public Transform rightHand;

    private float inputMovement;
    private Animator animator;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    private Camera mainCamera;
    private float recoilTimer;
    private int superJumpToken;

    [SerializeField]
    private PhotonView photonView;
    

    private int facingSign
    {
        // this is for getting the correct sign for the runnnig direction
        get{
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0f ? 1 : 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = GameObject.Find("targetTransform").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidbodyComponent = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            inputMovement = Input.GetAxis("Horizontal");
        
            if (photonView.IsMine)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
                {
                    targetTransform.position = hit.point;
                }
            }

            if (canMove && Input.GetButtonDown("Jump") && isGrounded){

                float jumpPower = 1f;
                if(superJumpToken > 0){
                    jumpPower *= 1.5f;
                    superJumpToken -= 1;
                }

            
                rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, 0, 0);
                rigidbodyComponent.AddForce(Vector3.up * jumpPower * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.VelocityChange);

            }

            if (Input.GetButtonDown("Fire1")){
                Fire();
            }

            if (transform.position.y < -20){
                Lives.life --;
                FindObjectOfType<GameManager>().Respawn();
            
            }
        }
        
    }


    private void Fire(){

        recoilTimer = Time.time;

        var go = Instantiate(bulletPrefab);
        go.transform.position = muzzleTransform.position;
        var bullet = go.GetComponent<Bullet>();
        bullet.Fire(go.transform.position, muzzleTransform.eulerAngles, gameObject.layer);

    }

    private void LateUpdate()
    {
        // recoil animation here
        // if (photonView.IsMine)
        // {
            if (recoilTimer < 0){
                return;
            }

            float curveTime = (Time.time - recoilTimer) / recoilDuration;

            if (curveTime > 1f)
            {
            recoilTimer = -1;
            }
            else
            {
                rightLowerArm.Rotate(Vector3.forward, recoilCurve.Evaluate(curveTime) * recoilMaxRotation, Space.Self);
            }
        // }
        
    }

    private void FixedUpdate(){

        // if (photonView.IsMine)
        // {
            // movement
            rigidbodyComponent.velocity = new Vector3(inputMovement * walkSpeed, rigidbodyComponent.velocity.y, 0);
            animator.SetFloat("Speed", (facingSign * rigidbodyComponent.velocity.x) / walkSpeed);

            if (photonView.IsMine)
            {
                // facing the right direction and following curser
                rigidbodyComponent.MoveRotation(Quaternion.Euler(new Vector3(0, 90* Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));

            }
            
            // ground check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadious, groundMask, QueryTriggerInteraction.Ignore);
            animator.SetBool("isGrounded", isGrounded);
        
            // game over check
            if(Lives.life == 0)
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        // }
        
    }

    private void OnAnimatorIK(){
        if (photonView.IsMine)
        {
            // aim at target 
            // animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            // animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

            // // look at target
            // animator.SetLookAtWeight(1);
            // animator.SetLookAtPosition(targetTransform.position);
        }

    }

    private void OnTriggerEnter(Collider other){

        // jump token trigger
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpToken += 1;
        }

        // virus collisoon trigger
        if (other.gameObject.layer == 12)
        {
            Lives.life --;
            Destroy(other.gameObject);
            
            StartCoroutine(Freeze());
        
        }

        // wormhole collission trigger
        if (other.gameObject.layer == 14){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }  

    IEnumerator Freeze(){
        canMove = false;
        walkSpeed = 0.01f;
        yield return new WaitForSeconds(5f);
        walkSpeed = 2.5f;
        canMove = true;
    }
}
