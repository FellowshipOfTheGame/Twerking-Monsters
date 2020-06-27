using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    new void Start() {
        base.Start();
    }

    new void Update() {
        base.Update();

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
