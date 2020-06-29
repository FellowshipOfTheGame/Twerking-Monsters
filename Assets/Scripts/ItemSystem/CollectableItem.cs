using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour {

    public Item item;

    void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.temp = item;
    }

    void OnTriggerExit2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>();

        if (player == null)
            return;

        player.temp = null;
    }

}