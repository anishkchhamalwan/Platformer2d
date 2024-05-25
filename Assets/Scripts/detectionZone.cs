using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColiders = new List<Collider2D>();
    Collider2D col;

    void Awake(){
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        detectedColiders.Add(other);
    }
    private void OnTriggerExit2D(Collider2D other) {
        detectedColiders.Remove(other);
    }
}
