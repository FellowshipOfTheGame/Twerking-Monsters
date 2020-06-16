using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    public Skill mouse1;
    public Skill mouse2;

    void Update() {

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.position += new Vector3(hor * Time.deltaTime * 5f, ver * Time.deltaTime * 5f);
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = mousePos - (Vector2) transform.position;

        if (Input.GetButtonDown("Fire1"))
            mouse1.Trigger(this, mouseDirection, new ContactFilter2D() { useLayerMask = true, layerMask = 1 << 9 });

        if (Input.GetButtonDown("Fire2"))
            mouse2.Trigger(this, mouseDirection, new ContactFilter2D() { useLayerMask = true, layerMask = 1 << 9 });
    }

}
