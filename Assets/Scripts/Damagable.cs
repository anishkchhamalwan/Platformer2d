using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<int,Vector2> damagableHit;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;

    public int  MaxHealth{
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive{
        get { return _isAlive; }
        set { _isAlive = value; animator.SetBool(AnimationStrings.isAlive,false); Debug.Log("Is Alive" + value); }
    }

    [SerializeField]
    private int _health = 100;
    
    [SerializeField]
    private bool isInvincible = false;
    public float invinciblityTime = 0.25f;
    private float timeSinceHit = 0f;

    public int Health{
        get { return _health; }
        set { _health = value; if(_health <= 0) IsAlive = false; }
    }

     public bool LockVelocity { get{
        return animator.GetBool(AnimationStrings.lockVelocity);
    }set{
        animator.SetBool(AnimationStrings.lockVelocity,value);
    }}


    void Awake(){
        animator = GetComponent<Animator>();
    }

    public void Update(){
        if(isInvincible){
            if(timeSinceHit > invinciblityTime){
                isInvincible = false;
                timeSinceHit =0;
            }
            timeSinceHit += Time.deltaTime;
        }
        
    }

    public void hit(int damage,Vector2 knockback){
        if(IsAlive && !isInvincible){
            Health -= damage;
            isInvincible = true;
            LockVelocity =true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            damagableHit?.Invoke(damage, knockback);
        }
    }
    
}
