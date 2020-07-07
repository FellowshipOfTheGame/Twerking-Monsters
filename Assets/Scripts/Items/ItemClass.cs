using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newClassItem", menuName = "Item/ClassItem")]
public class ItemClass : BaseItem {

    public BaseSkill swordSkill;
    public BaseSkill bowSkill;
    public BaseSkill staffSkill;

    public BaseBuff passive;

    public float GetCooldown(ItemWeapon.Type weaponType) {
        switch (weaponType) {
            case ItemWeapon.Type.SWORD:
                return swordSkill.cooldown;

            case ItemWeapon.Type.BOW:
                return bowSkill.cooldown;

            case ItemWeapon.Type.STAFF:
                return staffSkill.cooldown;
        }

        return 0f;
    }

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
