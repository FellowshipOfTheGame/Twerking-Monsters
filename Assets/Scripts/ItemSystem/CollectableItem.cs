using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour {

    public Item item;

    void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>();

        if (player == null)
            return;

        if (item is Weapon)
            player.weapon = item as Weapon;
        else if (item is Armor)
            player.armor = item as Armor;
        else if (item is ClassItem)
            player.classItem = item as ClassItem;

        Destroy(gameObject);
    }

}