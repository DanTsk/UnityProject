using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrc : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public GameObject prefabCarrot;

    public float speed = 2f;
    public float radius = 1f;
    

    float last_carrot = 0;

    bool dead;
    bool isWalking;
    bool startShooting;
    bool hitting;


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



    void Start()
    {

        animator = this.transform.GetComponent<Animator>();
        body = this.transform.GetComponent<BoxCollider2D>();
        sprite = this.transform.GetComponent<SpriteRenderer>();
        rigidBody = this.transform.GetComponent<Rigidbody2D>();

        currentMode = Mode.GoToB;
        dead = false;
        isWalking = false;
    }


    void Update()
    {
        if (hitting) {
            isWalking = false;
        }


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

        Debug.Log(currentMode+"-mode,"+value+"-direction");
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



        if (currentMode == Mode.Hitting && !dead)
        {
            isWalking = false;
            hitting = true;
          
            if (Time.time - last_carrot > 2.0f)
            {
                hit(Rabbit.Hero);
            }
        }
        else {
            hitting = false;
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

    float getCarrotDirection() {
            if (this.transform.localPosition.x < Rabbit.Hero.transform.localPosition.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }      
    }

    void updateMode()
    {

        if (isRabbitIn(Rabbit.Hero.transform.localPosition))
        {
            currentMode = Mode.Hitting;
            return;
        }
        else {
            rigidBody.constraints = RigidbodyConstraints2D.None;
        }



        if (isRabbitHere(Rabbit.Hero.transform.localPosition) && !Rabbit.Hero.isDead())
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
        else if (currentMode == Mode.Attack || currentMode == Mode.Hitting)
        {
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

    bool isRabbitIn(Vector3 rabit_pos)
    {
        Vector3 pos = this.transform.localPosition;
        return Mathf.Abs(rabit_pos.x - pos.x) < radius && Mathf.Abs(pos.y - rabit_pos.y) < 0.8f;
    }

    bool isRabbitHere(Vector3 rabit_pos)
    {
        Vector3 pos = this.transform.localPosition;
        return rabit_pos.x > Mathf.Min(pointA.x, pointB.x)
            && rabit_pos.x < Mathf.Max(pointA.x, pointB.x)
            && Mathf.Abs(pos.y - rabit_pos.y) < 0.8f;
    }
    


    void launchCarrot(float direction)
    {

        GameObject obj = GameObject.Instantiate(this.prefabCarrot);
        obj.transform.SetParent(LevelController.objects.transform);

        var vector3 = this.transform.localPosition;
        vector3.y += 0.8f;

        obj.transform.localPosition = vector3;      
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.launch(direction);
    }




    public void die()
    {
        Destroy(rigidBody);
        dead = true;
        body.enabled = false;
        animator.SetBool("die", true);
        StartCoroutine(afterDead(0.65f));
    }

    public void hit(Rabbit rabbit)
    {

        if (rabbit.isDead())
            return;

        last_carrot = Time.time;
        rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;

        float direction = getCarrotDirection();

        if (direction < 0)
        {
            sprite.flipX = false;
        }
        else {
            sprite.flipX = true;
        }

        launchCarrot(direction);     
        this.animator.SetTrigger("attack");   
    }


    IEnumerator afterDead(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

   
}
