using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject  {

    [Space]
    [Header("Item info")]
    public string itemName;
    public string description;
    public Sprite icon;

    public GameObject CreateGameObject(Vector2 origin) {
        GameObject item = new GameObject("name", typeof(SpriteRenderer), typeof(CollectableItem), typeof(BoxCollider2D));

        item.GetComponent<SpriteRenderer>().sprite = icon;
        item.GetComponent<CollectableItem>().item = this;
        item.GetComponent<BoxCollider2D>().size = icon.rect.size / icon.pixelsPerUnit;
        item.GetComponent<BoxCollider2D >().isTrigger = true;
        item.transform.position = origin;

        return item;
    }

}
