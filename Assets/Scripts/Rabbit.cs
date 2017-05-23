using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {
    public float speed = 1.5f;

    Rigidbody2D rabbitBody;
    SpriteRenderer rabbitSprite;
    Animator animator;

    bool isGrounded = false;
    bool JumpActive = false;

    float JumpTime = 0f;

    public float MaxJumpTime = 2f;
    public float JumpSpeed = 5f;

    public float MaxRunSped = 3f;
    public float RunSpeed = 2f;
    



    void Start () {
        rabbitBody = this.GetComponent<Rigidbody2D>();
        rabbitSprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();

        LevelController.current.setStartPosition(transform.position);
    }   
	
	void Update () {
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
    }

    void FixedUpdate()
    {              
        controllRun();
        controllJump();
    }


    void controllRun() {
        float value = Input.GetAxis("Horizontal");
        bool inMove = false; 

        if (Mathf.Abs(value) > 0)
        {
            inMove = true;
            if (inMove && speed < 3.2f)
            {
                speed += 0.8f;
            }

            Vector2 velocity = rabbitBody.velocity;
            velocity.x = value * speed;
            rabbitBody.velocity = velocity;

            animator.SetBool("run", true);
        }
        else{
            inMove = false;
            speed = 2f;

            animator.SetBool("run", false);
        }
        


        if (value < 0)
        {
            rabbitSprite.flipX = true;
        }
        else if (value > 0)
        {
            rabbitSprite.flipX = false;
        }

    }

    void controllJump() {

        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.2f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
     
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;         
        }
        else
        {
            isGrounded = false;
        }
       
        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
            Debug.Log("Jump pressed, is grounded = " + isGrounded);
        }
        else if(Input.GetButtonDown("Jump")){
            Debug.Log("Jump pressed, is grounded = " + isGrounded);
        }

        if (this.JumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = rabbitBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    rabbitBody.velocity = vel;                   
                }
             }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }

        }


    }
}
