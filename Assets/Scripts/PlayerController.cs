using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;

    TouchingDirections touchingDirections;
    Damagable damagable;
  

    public float CurrentmoveSpeed{
        get{
            if(CanMove){
                 if(IsMoving && !touchingDirections.IsOnWall){
                if(touchingDirections.IsGrounded){
                    if(isRunning){
                        return runSpeed;
                    }
                else return walkSpeed;
                }

            else{ 
                return airWalkSpeed;
                }   
            }
            else return 0f;
            }
            else return 0f;
           
        }
    }

    Rigidbody2D rb;

    private bool _isMoving =false;

    public bool IsMoving { get
    {
        return _isMoving;
    } private set{
        _isMoving = value;
        animator.SetBool(AnimationStrings.isMoving,value);
    } }

    private bool _isRunning = false;

    private bool isRunning{
        get{
            return _isRunning;
        }
        set{
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get{return _isFacingRight;} private set{
        if(_isFacingRight != value){
            transform.localScale *= new Vector2(-1,1);
        }
        _isFacingRight = value;
    } }

    public bool CanMove{get{return animator.GetBool(AnimationStrings.canMove);}}

    public bool IsAlive{get{return animator.GetBool(AnimationStrings.isAlive);}}

   

    Animator animator;
    public float jumpImpulse= 10f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damagable = GetComponent<Damagable>();
       
    }
  

     private void FixedUpdate() {
        if(!damagable.LockVelocity)
             rb.velocity = new Vector2(moveInput.x *CurrentmoveSpeed ,rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity,rb.velocity.y);

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x >0 && !IsFacingRight){
            IsFacingRight = true;
        }
        else if(moveInput.x <0){
            IsFacingRight = false;
        }
    }


    public void OnMove(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive){
            
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
        }
        else{
            IsMoving = false;
        }

    }

    
    public void OnRun(InputAction.CallbackContext context ){
        if(context.started){
            isRunning = true;
        }
        else if(context.canceled){
            isRunning = false;
        }

    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started && touchingDirections.IsGrounded){
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x,jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            animator.SetTrigger("attack");
        }
    }

    public void onHit(int damage, Vector2 knockback){
      //  LockVelocity =true;
        rb.velocity = new Vector2(knockback.x,rb.velocity.y+knockback.y);
    }

}
