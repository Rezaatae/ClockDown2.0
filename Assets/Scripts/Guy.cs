﻿using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Guy : MonoBehaviour, IPunObservable
{

    [SerializeField]
    private float walkSpeed = 2.5f;

    [SerializeField]
    private float jumpHeight = 5f;

    [SerializeField]
    private bool canMove = true;

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
    private PhotonView photonView;

    private float inputMovement;
    private Animator animator;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    private Camera mainCamera;
    private float recoilTimer;
    private int superJumpToken;
    private Lives playerLives = new Lives();
    private Score playerScore = new Score();

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
        targetTransform = GameObject.Find("targetTransform").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidbodyComponent = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        playerScore.SetPlayer(photonView);

    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.IsMine)
        {
            inputMovement = Input.GetAxis("Horizontal");
        
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            {
                targetTransform.position = hit.point;
            }

            if (canMove && Input.GetButtonDown("Jump") && isGrounded)
            {

                float jumpPower = 1.2f;
                if(superJumpToken > 0)
                {
                    jumpPower *= 1.5f;
                    superJumpToken -= 1;
                }

                rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, 0, 0);
                rigidbodyComponent.AddForce(Vector3.up * jumpPower * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.VelocityChange);

            }

            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }

            if (transform.position.y < -20)
            {
                playerLives.Deduct();
                FindObjectOfType<GameManager>().Respawn(); 
            }

        }
        
    }


    private void Fire()
    {

        recoilTimer = Time.time;

        var gameObject = PhotonNetwork.Instantiate(bulletPrefab.name, muzzleTransform.position, Quaternion.identity);// Instantiate(bulletPrefab);
        var bullet = gameObject.GetComponent<Bullet>();
        bullet.SetCurrentPlayerScore(playerScore);
        bullet.Fire(gameObject.transform.position, muzzleTransform.eulerAngles, base.gameObject.layer);

    }

    private void LateUpdate()
    {
        // recoil animation here
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
        
    }

    private void FixedUpdate()
    {

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
            if(playerLives.GetRemainingLives() == 0)
            {
                FindObjectOfType<GameManager>().EndGame();
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

    private void OnTriggerEnter(Collider other)
    {

        // jump token trigger
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpToken += 1;
        }

        // virus collisoon trigger
        if (other.gameObject.layer == 12)
        {
            playerLives.Deduct();
            Destroy(other.gameObject);
            StartCoroutine(Freeze());            
        }

        
    }  

    private IEnumerator Freeze()
    {
        canMove = false;
        walkSpeed = 0.01f;
        yield return new WaitForSeconds(lockDownTime);
        walkSpeed = 2.5f;
        canMove = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

}
