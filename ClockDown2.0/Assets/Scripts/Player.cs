using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpToken;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate(){
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 4, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheck.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyPressed)
        {
            float jumpPower = 5f;
            if(superJumpToken > 0){
                jumpPower *= 2;
                superJumpToken -= 1;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
            
        }

    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpToken += 1;
        }
    }
}
