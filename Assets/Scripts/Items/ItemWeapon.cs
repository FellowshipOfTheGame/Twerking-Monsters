using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Item/Weapon")]
public class ItemWeapon : BaseItem {

    public BaseSkill attack;
    public GameObject weaponObject;
    public Type type;

    public void Attack(Transform parent, Vector2 target, LayerMask layerMask) {
        attack.Trigger(parent, target, layerMask);
    }

    public enum Type {
        SWORD, STAFF, BOW,
    }

}
