using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BaseProjectile {

    public float damage;
    public float fireDamage;
    public float fireDuration;

    public override void OnTargetHit(Collider2D targetCollider, ContactFilter2D contactFilter, bool hasFireDamage) {
        Entity enemy = targetCollider.GetComponent<Entity>();

        if (enemy == null || contactFilter.layerMask.value != 1 << targetCollider.gameObject.layer)
            return;

        enemy.Damage(damage);

        if (hasFireDamage)
            enemy.DamageOverTime(fireDamage, fireDuration);
    }

}
