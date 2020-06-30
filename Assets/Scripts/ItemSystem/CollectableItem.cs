using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class CollectableItem : MonoBehaviour {

    public Item item;
    
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = item.icon;
        boxCollider2D.size = item.icon.rect.size / item.icon.pixelsPerUnit;
        boxCollider2D.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.temp = gameObject;
    }

    void OnTriggerExit2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.temp = null;
    }

}