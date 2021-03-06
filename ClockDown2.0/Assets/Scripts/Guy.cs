using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundCheckRadious = 0.2f;

    private float inputMovement;
    private Animator animator;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;

    public Transform targetTransform;
    private Camera mainCamera;
    public LayerMask mouseAimMask;

    public LayerMask groundMask;

    private int facingSign{
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
        animator = GetComponent<Animator>();
        rigidbodyComponent = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        inputMovement = Input.GetAxis("Horizontal");

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask)){
            targetTransform.position = hit.point;
        }

        if (Input.GetButtonDown("Jump") && isGrounded){
            rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, 0, 0);
            rigidbodyComponent.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.VelocityChange);

        }
    }

    private void FixedUpdate(){
        // movement
        rigidbodyComponent.velocity = new Vector3(inputMovement * walkSpeed, rigidbodyComponent.velocity.y, 0);
        animator.SetFloat("Speed", (facingSign * rigidbodyComponent.velocity.x) / walkSpeed);
        // facing the right direction and following curser
        rigidbodyComponent.MoveRotation(Quaternion.Euler(new Vector3(0, 90* Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));

        // ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadious, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnAnimatorIK(){
        // aim at target 
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

        // look at target
        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);


    }
}
