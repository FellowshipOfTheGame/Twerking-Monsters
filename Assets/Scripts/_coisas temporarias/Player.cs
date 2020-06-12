using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Attack primaryAttack;
    public GameObject swingEffect;

    void Update() {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.position += new Vector3(hor * Time.deltaTime * 5f, ver * Time.deltaTime * 5f);

        if (Input.GetButtonDown("Fire1")) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDirection = mousePos - (Vector2) transform.position;
            primaryAttack.Trigger(transform, mouseDirection, new ContactFilter2D() { useLayerMask = true, layerMask = 1 << 9 });
        }
    }

}
