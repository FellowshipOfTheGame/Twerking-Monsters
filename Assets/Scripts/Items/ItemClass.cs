using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newClassItem", menuName = "Item/ClassItem")]
public class ItemClass : BaseItem {

    public BaseSkill swordSkill;
    public BaseSkill bowSkill;
    public BaseSkill staffSkill;

    public BaseBuff passive;

    public void TriggerSkill(ItemWeapon.Type weaponType, Transform parent, Vector2 target, LayerMask layerMask) {
        switch (weaponType) {
            case ItemWeapon.Type.SWORD:
                swordSkill.Trigger(parent, target, layerMask);
                break;

            case ItemWeapon.Type.BOW:
                bowSkill.Trigger(parent, target, layerMask);
                break;

            case ItemWeapon.Type.STAFF:
                staffSkill.Trigger(parent, target, layerMask);
                break;
        }
    }

}
