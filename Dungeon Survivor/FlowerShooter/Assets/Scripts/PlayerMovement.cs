using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float turnRate = 40f;

    //Inputs
    float vertical;
    float horizontal;


    //State
    PlayerStates state;
    //Components
    Rigidbody rb;
    Animator anim;


    //Raycast Rays

    [Header("Raycasts")]
    [SerializeField]
    float rayLength = 5;
    [SerializeField]
    LayerMask rayMask;
    [SerializeField]
    Transform raycastOrigin;

    Ray[] rays;
    RaycastHit hit;
    bool isGrounded = false;
    bool canGround = true;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 5f;
    bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<PlayerStates>();
        state.PlayerState = PlayerState.idle;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        rays = new Ray[5];
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetState() == GameState.play) 
        {
            PlayerInput();

            if(canGround)//Just check if we are falling
                LaunchRaycast();

            CanJump();
            Animate();
            Move();
            Rotate();
        
        }


    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetState() == GameState.play)
        {
            Jump();
            CheckFalling();

        }

    }

    private void CheckFalling()
    {
        if (rb.velocity.y < 0) //we are falling
        {
            canGround = true; 
            

        }
    }

    void PlayerInput() 
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");        

    }

    void Move() 
    {
        if(state.PlayerState != PlayerState.attacking) //We can move if we are NOT walking
        {
            transform.Translate(Vector3.forward * speed * vertical * Time.deltaTime);
            if(vertical!= 0)
                state.SetWalking();
        
        }
        
    }

    void Rotate() 
    {
        transform.Rotate(Vector3.up, turnRate * horizontal * Time.deltaTime);
    }

    void Animate() 
    {
        //Walking
        if(state.PlayerState == PlayerState.walking)
        {
            if (vertical != 0)
            {
                //state.SetWalking();
                SetWalkingAnim(true);
            }
            else 
            {
                state.SetIdle();
                SetWalkingAnim(false);
            }
        }
        
       
    }

    void SetWalkingAnim(bool isWalking) 
    {
        anim.SetBool("isWalking", isWalking);

        //if (vertical > 0) 
        //{
        //    anim.SetBool("isWalkingForward", true);
        //    anim.SetBool("isWalkingBackward", false);

        //}
        //else 
        //{
        //    anim.SetBool("isWalkingForward", false);
        //    anim.SetBool("isWalkingBackward", true);
        //}

        //Esto es lo mismo que lo de arriba
        anim.SetBool("isWalkingForward", vertical > 0);
        anim.SetBool("isWalkingBackward", !(vertical >= 0));
    }

    void LaunchRaycast() 
    {
        
        rays[0].origin = raycastOrigin.position;//Center
        rays[1].origin = raycastOrigin.position + Vector3.forward / 3;//Foward
        rays[2].origin = raycastOrigin.position - Vector3.forward / 3;//Back
        rays[3].origin = raycastOrigin.position + Vector3.right / 3;//Right
        rays[4].origin = raycastOrigin.position - Vector3.right / 3;//Left

        for (int i = 0; i < rays.Length; i++)//Set up all the rays and draw them to debug
        {
            rays[i].direction = -raycastOrigin.up;
            Debug.DrawLine(rays[i].origin, rays[i].origin + rayLength * rays[i].direction, Color.blue);
        }

        
        CheckAllRaycasts();//Check if we are grounded

    }
    void CanJump() 
    {
        if(Input.GetKey(KeyCode.Space) && isGrounded) 
        {
            canJump = true;
        }
    }

    void Jump() 
    {
        if (canJump) 
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetBool("isJumping", true);
            state.SetJump();            
        }
    
    }

    void CheckAllRaycasts() 
    {
        isGrounded = false;
        for (int i = 0; i < rays.Length && !isGrounded; i++)
        {
            if (Physics.Raycast(rays[0], out hit, rayLength, rayMask)) 
            {
                isGrounded = true;
                Ground();
            }
        }
    }

    void Ground() 
    {
        anim.SetBool("isJumping", false);
        if(state.PlayerState == PlayerState.jumping)
            state.SetIdle();

    }
}
