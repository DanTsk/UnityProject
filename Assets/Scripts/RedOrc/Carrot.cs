using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour {

    bool launched;
    float direction;
    float speed;

    Rigidbody2D rigidBody;
    SpriteRenderer sprite;
    
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        

        speed = 4f;
        StartCoroutine(destroyLater());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rabbit rab = collision.GetComponent<Rabbit>();
        if (rab != null) {
            rab.hitted();
            Destroy(this.gameObject);
        }
    }

    void Update () {
        if (launched) {
            Vector2 velocity = rigidBody.velocity;
            velocity.x = direction * speed;
            rigidBody.velocity = velocity;                   
               
        }
	}

    public void launch(float direction) {
        this.launched = true;
        this.direction = direction;
        sprite = this.GetComponent<SpriteRenderer>();

        if (direction < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }


    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
