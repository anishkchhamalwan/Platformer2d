using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;

    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float distanceFromTarget => transform.position.z - followTarget.position.z;

    float parallaxFactor => Mathf.Abs(distanceFromTarget) / clippingPlane;

    float clippingPlane => cam.transform.position.z +(distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane);


    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;

    }

    void FixedUpdate(){
        Vector2 newPostion = startingPosition + camMoveSinceStart*parallaxFactor;
        transform.position = new Vector3(newPostion.x, newPostion.y,startingZ);

    }

}
