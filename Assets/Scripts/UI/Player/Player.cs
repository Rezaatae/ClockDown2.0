using System.Collections;
using UnityEngine;
using Photon.Pun;
using Gravitons.UI.Modal;

public class Player : MonoBehaviour
{

    [SerializeField]
    public float walkSpeed = 2.5f;

    [SerializeField]
    public float jumpHeight = 5f;

    [SerializeField]
    public bool invincible = false;

    [SerializeField]
    public bool playerDead = false;

    [SerializeField]
    private bool canMove;

    [SerializeField]
    private float lockDownTime = 5f;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadious = 0.2f;

    [SerializeField]
    private LayerMask mouseAimMask;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform muzzleTransform;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private AnimationCurve recoilCurve;

    [SerializeField]
    private float recoilDuration = 0.25f;

    [SerializeField]
    private float recoilMaxRotation = 45f;

    [SerializeField]
    private Transform rightLowerArm;

    [SerializeField]
    private Transform rightHand;
    
    [SerializeField]
    public PhotonView photonView;

    private float inputMovement;
    private Animator animator;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    private Camera mainCamera;
    private float recoilTimer;
    private Lives playerLives;
    public float minHeightForDeath = -50;
    public float duration = 10.0f;


    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerTagPrefab;

    private int facingSign
    {
        // this is for getting the correct sign for the runnnig direction
        get
        {
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0f ? 1 : 0;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {

        targetTransform = GameObject.Find(Constants.Scenes.Game.Objects.TargetTransform).GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidbodyComponent = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        
        playerLives = GameObject.Find(Constants.Scenes.Game.Objects.LifeRemainingText).GetComponent<Lives>();

        if (playerTagPrefab != null)
        {
            GameObject _uiGo = Instantiate(playerTagPrefab);
            _uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
        
    }

    

    // Update is called once per frame
    private void Update()
    {
        ProcessInputs();
    }


    private void Fire()
    {

        recoilTimer = Time.time;

        var gameObject = PhotonNetwork.Instantiate(bulletPrefab.name, muzzleTransform.position, Quaternion.identity);
        var bullet = gameObject.GetComponent<Bullet>();
        bullet.SetTrajectory(gameObject.transform.position, muzzleTransform.eulerAngles, base.gameObject.layer);

    }

    private void LateUpdate()
    {
        // recoil animation here
        Recoil();
        
    }

    private void FixedUpdate()
    {

        if (photonView.IsMine)
        {
                // movement
            rigidbodyComponent.velocity = new Vector3(inputMovement * walkSpeed, rigidbodyComponent.velocity.y, 0);
            animator.SetFloat("Speed", (facingSign * rigidbodyComponent.velocity.x) / walkSpeed);
                // facing the right direction and following curser
            rigidbodyComponent.MoveRotation(Quaternion.Euler(new Vector3(0, 90* Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));

            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadious, groundMask, QueryTriggerInteraction.Ignore);        
            animator.SetBool("isGrounded", isGrounded);
        }
            
    }

    private void OnAnimatorIK()
    {
        if (photonView.IsMine)
        {
            // aim at target 
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

            // look at target
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(targetTransform.position);
        }

    }

    /* 
    These methods activate when the player collides with a certain entity in the game equipped with a
    3D box collider.
    */
    private void OnTriggerEnter(Collider other)
    {

        JumpBoost(other.gameObject);
        gainLife(other);
        VirusCollision(other);
        
    }  

    /*
    When the player prefab collides with a virus game object of tag 'Enemy', and is not invincible,
    if the player is contolled by the local user then the player has one life deducted, 
    the gameobject is then destroyed, and the freeze coroutine will begin (explained below).
    */
    public void VirusCollision(Collider other)
    {
        if (!invincible && other.CompareTag("Enemy"))
        {
            if (photonView.IsMine)
                playerLives.Deduct();
            PhotonView.Get(other.gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
            StartCoroutine(Freeze());  
        }
    }

    /*
    When the player collides with a game object with the tag "Vaccine" and the player is contolled by the
    local user, then one life will be added on, and the game object will be destroyed. Then the
    "Invincibility" coroutine will begin (explained below).
    */
    public void gainLife(Collider other)
    {
        if (other.CompareTag("Vaccine"))
        {
            if (photonView.IsMine)
                playerLives.Increase();
            PhotonView.Get(other.gameObject).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
            StartCoroutine(Invincibility());
        }
    } 

    private void JumpBoost(GameObject go)
    {
        if (go.layer.Equals(Constants.Scenes.Game.Objects.ToiletRoll))
        {
            if (photonView.IsMine)
                FindObjectOfType<Score>().Increment();
            PhotonView.Get(go).RPC(Constants.RPC.Destroy, RpcTarget.AllBuffered);
        }
    }

    /*
    The coroutine Freeze() accounts for the disabling of movement after collision with tag "Enemy". 
    Here for lockDownTime = 5f, the player is unable to move.
    */
    private IEnumerator Freeze()
    {
        canMove = false;
        walkSpeed = 0.01f;
        yield return new WaitForSeconds(lockDownTime);
        walkSpeed = 2.5f;
        canMove = true;
    }

    /*
    The coroutine Invincibility() accounts for disabling colliders with the viruses for duration = 10f.
    */
    IEnumerator Invincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;    
    }

    private void Recoil()
    {
        if (recoilTimer < 0)
            return;
    
        float curveTime = (Time.time - recoilTimer) / recoilDuration;

        if (curveTime > 1f)
        {
            recoilTimer = -1;
        } else
        {
            rightLowerArm.Rotate(Vector3.forward, recoilCurve.Evaluate(curveTime) * recoilMaxRotation, Space.Self);
        }
    }

    private void ProcessInputs()
    {
        if (photonView.IsMine)
        {
            inputMovement = Input.GetAxis(Constants.GameControls.Movement.Horizontal);
        
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            {
                targetTransform.position = hit.point;
            }

            if (canMove && Input.GetButtonDown(Constants.GameControls.Movement.Jump) && isGrounded)
            {
                float jumpPower = 1.2f;

                rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, 0, 0);
                rigidbodyComponent.AddForce(Vector3.up * jumpPower * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.VelocityChange);

            }

            if (Input.GetButtonDown(Constants.GameControls.Movement.Fire))
            {
                Fire();
            }

            Respawn();
    
        }
    }

    // When a player falls below a certain y coordinate under the map, they will respawn at designated area
    private void Respawn()
    {
        if (transform.position.y < minHeightForDeath)
        {
            transform.position = new Vector3(15, 2, 0);
            playerLives.Deduct();
        }
    }

}
