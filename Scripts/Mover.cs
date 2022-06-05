using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;


    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdatedMotor(Vector3 input) {
        // Reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);


        //Swap sprite direction, wheterh you're going right or left
        if (moveDelta.x > 0) {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }    

        //Add push vector if any
        moveDelta += pushDirection;

        // Reduce force every frame based of recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverSpeed);


        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null) {
            //move player
            //move left and right 
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); //must run equally fast on all devices 
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null) {
            //move player
            //move left and right 
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0); //must run equally fast on all devices 
        }
    }
}
