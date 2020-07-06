using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour {

    public BaseItem item;
    protected new Collider2D collider2D;
    protected SpriteRenderer spriteRenderer;

    void Start() {
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player")
            return;
        spriteRenderer.color = Color.gray;
        collider.GetComponent<Player>().temp = gameObject;
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag != "Player")
            return;
        spriteRenderer.color = Color.white;
        collider.GetComponent<Player>().temp = null;
    }

}
