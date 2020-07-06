using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    [Header("Drops")]
    public ItemWeapon[] weaponItems;
    public ItemClass[] classItems;
    public ItemArmor[] armorItems;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player")
            return;

        // Create random loot
        int weaponIndex = Random.Range(0, weaponItems.Length);
        int armorIndex = Random.Range(0, armorItems.Length);
        int classIndex = Random.Range(0, classItems.Length);

        GameObject weapon = weaponItems[weaponIndex].CreateCollectible((Vector2)transform.position + new Vector2(-1.5f, 0f));
        GameObject classItem = classItems[classIndex].CreateCollectible((Vector2)transform.position + new Vector2(0f, 0f));
        GameObject armor = armorItems[armorIndex].CreateCollectible((Vector2)transform.position + new Vector2(1.5f, 0f));

        Destroy(gameObject);
    }

}
