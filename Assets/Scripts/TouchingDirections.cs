using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
   // Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D touchingCol;

    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded { 
        get{
            return _isGrounded;
        } 
        set{
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded,value);
        } }
         [SerializeField]
    private bool _isOnWall = false;
    public bool IsOnWall { 
        get{
            return _isOnWall;
        } 
        set{
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall,value);
        } }

         [SerializeField]
    private bool _isOnCeling = false;
    public bool IsOnCeiling { 
        get{
            return _isOnCeling;
        } 
        set{
            _isOnCeling = value;
            animator.SetBool(AnimationStrings.isOnCeiling,value);
        } }

        private Vector2 wallCheckDirection => gameObject.transform.localScale.x >0 ? Vector2.right: Vector2.left ;


    private void Awake() {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }
    private void FixedUpdate() {
        IsGrounded = touchingCol.Cast(Vector2.down,castFilter,groundHits,groundDistance) >0 ;
        IsOnWall = touchingCol.Cast(wallCheckDirection,castFilter,wallHits,wallDistance) > 0;
       IsOnCeiling = touchingCol.Cast(Vector2.up,castFilter,ceilingHits,ceilingDistance) > 0;
    }
    
}
