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
        GameObject item = new GameObject(itemName, typeof(CollectableItem));

        item.GetComponent<CollectableItem>().item = this;
        item.transform.position = origin;

        return item;
    }

}
