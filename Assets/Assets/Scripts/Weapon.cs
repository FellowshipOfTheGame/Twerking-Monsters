using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Vector2 direction;
    public float distanceFromCenter;
    public Vector2 center;
    
    void Start() {
        
    }

    void Update() {
        transform.position = center + (direction * distanceFromCenter);
        float angle = Vector2.Angle(Vector2.up, direction);
        if (direction.x < 0)
            angle = 360 - angle;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
