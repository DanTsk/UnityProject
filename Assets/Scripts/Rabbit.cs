using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rabbit : MonoBehaviour {
    public static Rabbit Hero;
    public float speed = 1.5f;
    public string currentScene;

    Animator animator;
    Rigidbody2D rabbitBody;
    BoxCollider2D boxCollider;
    SpriteRenderer rabbitSprite;
    Transform heroParent = null;
 
    bool isGrounded = false;
    bool JumpActive = false;
    bool isRunning = false;

    float JumpTime = 0f;

    public float MaxJumpTime = 2f;
    public float JumpSpeed = 5f;

    public float MaxRunSped = 3f;
    public float RunSpeed = 2f;

    public float GodSeconds = 4f;

    bool affectedByMushroom;
    bool dead;
    bool rabbitIsGod;

    private void Awake()
    {
        Hero = this;
    }

    void Start () {
        rabbitBody = this.GetComponent<Rigidbody2D>();
        rabbitSprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        heroParent = this.transform.parent;
        currentScene = SceneManager.GetActiveScene().name;

        affectedByMushroom = false;
        rabbitIsGod = false;
        dead = false;

        LevelController.current.setStartPosition(transform.position);
    }   
	

	void Update () {
        if (!dead && currentScene != "MainMenu" )
        {
          aliveUpdate();
        }                  
    }

    void FixedUpdate()
    {   
        if(!dead && currentScene != "MainMenu")
        {
          controllRun();
          controllHits();
        }           
         
    }


    void aliveUpdate() {
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

        if (this.isRunning)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
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
            isRunning = true;
        }
        else{
            inMove = false;
            speed = 2f;
            isRunning = false;            
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

    void controllHits() {

        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.2f;
        int layer_id1 = 1 << LayerMask.NameToLayer("Ground");
        int layer_id2 = 1 << LayerMask.NameToLayer("MovingBox");

        jumpController(from,to,layer_id1);
        platfromController(from,to,layer_id2);

    }



    void jumpController(Vector3 from, Vector3 to, int layer_id) {
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
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

    void platfromController(Vector3 from, Vector3 to, int layer_id) {
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {               
                LevelController.SetNewParent(this.transform, hit.transform);
                isGrounded = true;
            }
        }
        else
        {
            LevelController.SetNewParent(this.transform, this.heroParent);
        }
    }



    public void collectMushroom() {

        if (!affectedByMushroom)
        {
            affectedByMushroom = true;
            this.transform.localScale += new Vector3(0.6f,0.6f);       
        }

    }

    public void collectBomb() {
        if (affectedByMushroom)
        {
            affectedByMushroom = false;
            this.transform.localScale -= new Vector3(0.6f, 0.6f);
            becomeGod();           
        }
        else if(!rabbitIsGod)
        {
            die();
        }
            
    }
    
    public void removeBuffes() {
        if (affectedByMushroom)
        {
          affectedByMushroom = false;
          this.transform.localScale -= new Vector3(0.6f, 0.6f);
        }
    }

    public void die()
    {
        if (!isDead())
        {
            dead = true;
            animator.SetBool("die", true);
            StartCoroutine(afterDead(1.3f));
        }
    }

    public void hitted() {

        if (rabbitIsGod)
            return;

        if (affectedByMushroom)
        {
            removeBuffes();
            return;
        }
            

        dead = true;
        animator.SetBool("die", true);
        StartCoroutine(afterDead(1.3f));
    }

    public bool isDead() {
        return dead;
    }


    void becomeGod() {
        rabbitIsGod = true;
        rabbitSprite.color = new Color(1f, 0.8f, 0.8f, 1f);
        StartCoroutine(goneGod(GodSeconds));
    }

    IEnumerator goneGod(float duration) {
        yield return new WaitForSeconds(duration);
        rabbitIsGod = false;
        rabbitSprite.color = new Color(255f, 255f, 255f, 1f);

    }

    IEnumerator afterDead(float duration)
    {       
        yield return new WaitForSeconds(duration);
        LevelController.current.onRabitDeath(this);      
        dead = false;     
        animator.SetBool("die", false);
    }

}
