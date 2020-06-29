using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Item/Weapon")]
public class Weapon : Item {

    public Skill attack;
    public Weapon.Type weaponType;
    public GameObject weaponObject;

    public void Attack(Transform parent, Vector2 target, ContactFilter2D filter, bool hasFireDamage) {
        attack.Trigger(parent, target, filter, hasFireDamage);
    }

    public enum Type {
        SWORD, BOW, STAFF,
    }

}