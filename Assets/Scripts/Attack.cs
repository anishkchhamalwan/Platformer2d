using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
//using Vector2 = UnityEngine.Vector2;
using UnityEngine;


public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision){
        Damagable damagable = collision.GetComponent<Damagable>();
        Vector2 deliveredKnockback = transform.parent.localScale.x>0 ?knockback: new Vector2(-1*knockback.x, knockback.y);
        if(damagable != null){
            damagable.hit(attackDamage,deliveredKnockback);
           // Debug.Log(collision.name + " "+ damagable.Health);
        }
    }
}
