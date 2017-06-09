using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : MonoBehaviour {
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;

    bool dead;
    bool isWalking;

    Animator animator;
    BoxCollider2D body;
    SpriteRenderer sprite;
    Rigidbody2D rigidBody;
    Mode currentMode;

    public enum Mode
    {
        GoToA,
        GoToB,
        Hitting,
        Attack,
        Idle     
    }

    public AudioClip attSound = null;
    AudioSource attSource = null;

    void Start () {
                 
        animator = this.transform.GetComponent<Animator>();
        body = this.transform.GetComponent<BoxCollider2D>();
        sprite = this.transform.GetComponent<SpriteRenderer>();
        rigidBody = this.transform.GetComponent<Rigidbody2D>();

        currentMode = Mode.GoToB;
        dead = false;
        isWalking = false;

        attSource = gameObject.AddComponent<AudioSource>();
        attSource.clip = attSound;
    }
	
	
	void Update () {     

        if (this.isWalking)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            aliveController();
        }

    }


    void aliveController()
    {

        updateMode();
        float value = this.getDirection();   
        if (Mathf.Abs(value) > 0)
        {
            Vector2 velocity = rigidBody.velocity;
            velocity.x = value * speed;          
            rigidBody.velocity = velocity;
            isWalking = true;
        }
        else
        {         
            isWalking = false;
        }



        if (value < 0)
        {
            sprite.flipX = false;
        }
        else if (value > 0)
        {
            sprite.flipX = true;
        }
    }


    float getDirection()
    {

        if (currentMode == Mode.Attack)
        {
          
            if (this.transform.localPosition.x < Rabbit.Hero.transform.localPosition.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        if (currentMode == Mode.GoToA)
        {
            return -1; 
        }
        else if (currentMode == Mode.GoToB)
        {
            return 1; 
        }
        return 0;
    }

    void updateMode() {


        if (isRabbitHere(Rabbit.Hero.transform.localPosition)&&!Rabbit.Hero.isDead())
        {
            currentMode = Mode.Attack;
            return;
        }


        if (currentMode == Mode.GoToA)
        {
            if (isArrived(this.transform.localPosition, pointA))
            {
                currentMode = Mode.GoToB;
                return;
            }
        }
        else if (currentMode == Mode.GoToB)
        {
            if (isArrived(this.transform.localPosition, pointB))
            {
                currentMode = Mode.GoToA;
                return;
            }
        }
        else if (currentMode == Mode.Attack) {
            if (sprite.flipX)
            {
                currentMode = Mode.GoToB;
            }
            else
            {
                currentMode = Mode.GoToA;
            }
        }
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        pos.y = 0;

        target.z = 0;
        target.y = 0;


        return Vector3.Distance(pos, target) < 0.02f;
    }

    bool isRabbitHere(Vector3 rabit_pos) {
        Vector3 pos = this.transform.localPosition;
     
        return rabit_pos.x > Mathf.Min(pointA.x, pointB.x) 
            && rabit_pos.x < Mathf.Max(pointA.x, pointB.x)
            && Mathf.Abs(pos.y - rabit_pos.y) < 0.8f;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Rabbit rabit = collision.gameObject.GetComponent<Rabbit>();
        if (rabit != null)
        {
            hit(rabit);
            rabit.hitted();
        }
    }



    public void die()
    {
        Destroy(rigidBody);
        dead = true;
        body.enabled = false;
        animator.SetBool("die", true);
        StartCoroutine(afterDead(0.65f));
    }

    public void hit(Rabbit rabbit) {
        if (rabbit.isDead())
            return;

        currentMode = Mode.Hitting;
        rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        this.animator.SetTrigger("attack");
        if (SoundManager.Instance.isSoundOn())
            attSource.Play();

        StartCoroutine(afterHit(0.65f));
    }


    IEnumerator afterDead(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

    IEnumerator afterHit(float duration)
    {
        yield return new WaitForSeconds(duration);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (sprite.flipX)
        {
            currentMode = Mode.GoToA;
        }
        else
        {
            currentMode = Mode.GoToB;
        }
    }
}
