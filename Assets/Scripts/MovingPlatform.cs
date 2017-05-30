using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Vector3 MoveBy;
    public float PauseTime;
    public float speed;
    public bool StartPause;

    float time_to_wait;

    Vector3 pointA;
    Vector3 pointB;

    bool going_to_a;
    bool inMove;


    // Use this for initialization
    void Start () {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;

        this.going_to_a = false;
        this.time_to_wait = PauseTime;
        this.inMove = !StartPause;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        loopMoving();
    }

  
    void loopMoving() {
        Vector3 my_pos = this.transform.position;
        Vector3 target;

        if (going_to_a)
        {
            target = this.pointA;
        }
        else
        {
            target = this.pointB;
        }

        Vector3 destination = target - my_pos;
        destination.z = 0;


        bool arrived = isArrived(my_pos, target);

        if (arrived) {
            going_to_a = !going_to_a;
            inMove = false;
        }

  

        if (inMove)
        {
            float deltaSpeed = speed * Time.deltaTime;         
            transform.position = Vector3.MoveTowards(this.transform.position, target, deltaSpeed); 
        }
        else
        {
            time_to_wait -= Time.deltaTime;
            if (time_to_wait <= 0)
            {
                inMove = true;
                time_to_wait = PauseTime;
            }
        }


    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }
}
