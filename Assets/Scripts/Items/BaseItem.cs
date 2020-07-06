using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject {

    public string itemName;
    [Multiline] public string itemDescription;
    public Sprite icon;

    public GameObject CreateCollectible(Vector2 worldPosition) {
        GameObject itemObject = new GameObject(itemName, typeof(ObjectItem), typeof(BoxCollider2D), typeof(SpriteRenderer));

        SpriteRenderer objSpriteRenderer = itemObject.GetComponent<SpriteRenderer>();
        BoxCollider2D objBoxCollider = itemObject.GetComponent<BoxCollider2D>();

        objSpriteRenderer.sprite = icon;
        objBoxCollider.size = icon.rect.size / icon.pixelsPerUnit;
        objBoxCollider.isTrigger = true;

        itemObject.transform.position = worldPosition;
        itemObject.GetComponent<ObjectItem>().item = this;

        itemObject.tag = "Item";

        return itemObject;
    }

}
