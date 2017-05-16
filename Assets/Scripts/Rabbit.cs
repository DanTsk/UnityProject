using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {
    public float speed = 1.5F;

    Rigidbody2D rabbitBody;
    SpriteRenderer rabbitSprite;

    // Use this for initialization
    void Start () {
        rabbitBody = this.GetComponent<Rigidbody2D>();
        rabbitSprite = this.GetComponent<SpriteRenderer>();
    }   
	
	// Update is called once per frame
	void Update () {
        
    }
    

    void FixedUpdate()
    {
       
        float value = Input.GetAxis("Horizontal");

        controllRun(value);



    }


    void controllRun(float value) {
        bool inMove = false;

        if (Mathf.Abs(value) > 0)
        {
            inMove = true;
            if (inMove)
            {
                speed += 0.05f;
            }

            Vector2 velocity = rabbitBody.velocity;
            velocity.x = value * speed;
            rabbitBody.velocity = velocity;
        }
        else
        {
            inMove = false;
            speed = 1.5f;
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


}
