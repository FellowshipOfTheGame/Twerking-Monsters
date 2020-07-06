using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemCollectible : MonoBehaviour {

    protected new Collider2D collider2D;

    protected void Start() {
        collider2D = GetComponent<Collider2D>();

        if (!collider2D.isTrigger)
            collider2D.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider) {

    }

    void OnTriggerExit2D(Collider2D collider) {

    }
    
}
