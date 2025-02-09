using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate= 0.05f;
    public Rigidbody2D rb;

    public TouchingDirections touchingDirections;
    public detectionZone attackZone;
    Animator animator;
    Damagable damagable;

    public enum WalkableDirection { Right,Left}

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get{ return _walkDirection; }
        set{ 
            if(_walkDirection != value){
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x*-1,gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right){
                    walkDirectionVector = Vector2.right;
                }
                if(value == WalkableDirection.Left){
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;}
    }

    public bool _hasTarget = false;

    public bool HasTarget { 
        get{
            return _hasTarget;
        } 
         set{
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget,value);
        } 
    }

    public bool CanMove{
        get{
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        touchingDirections  = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damagable = GetComponent<Damagable>();
    }

    private void FixedUpdate(){
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirections();
            Debug.Log(touchingDirections.IsOnWall);
        }

        if(!damagable.LockVelocity){
            if(CanMove)
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x,0,walkStopRate),rb.velocity.y);
        }
        
        
    }

    private void Update() {
        HasTarget = attackZone.detectedColiders.Count >0;
    }

    private void FlipDirections()
    {
        if(WalkDirection == WalkableDirection.Left){
            WalkDirection = WalkableDirection.Right;
        }
        else if(WalkDirection == WalkableDirection.Right){
            WalkDirection = WalkableDirection.Left;
        }
        
        //throw new NotImplementedException();
    }

    public void onHIt(int damage ,Vector2 knockback){
        rb.velocity = new Vector2(knockback.x,rb.velocity.y+knockback.y);
    }

}
