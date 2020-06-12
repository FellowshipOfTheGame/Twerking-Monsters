using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {
    public float vida = 100;

    void Update() {
        if (vida <= 0)
            Destroy(gameObject);
    }
}
