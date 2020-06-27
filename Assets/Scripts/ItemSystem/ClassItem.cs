using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newClassItem", menuName = "Item/ClassItem")]
public class ClassItem : Item {

    [Space]
    [Header("Skills")]
    public Skill swordSkill;
    public Skill bowSkill;
    public Skill staffSkill;

    public void TriggerSkill(Weapon.Type weaponType, Transform parent, Vector2 target, ContactFilter2D filter) {
        switch (weaponType) {
            case Weapon.Type.SWORD:
                swordSkill.Trigger(parent, target, filter, false);
                break;
            
            case Weapon.Type.BOW:
                bowSkill.Trigger(parent, target, filter, false);
                break;
            
            case Weapon.Type.STAFF:
                staffSkill.Trigger(parent, target, filter, false);
                break;
        }
    }

}
