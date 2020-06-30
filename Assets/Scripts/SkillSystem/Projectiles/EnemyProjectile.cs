using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : BaseProjectile {

    public float damage;

    public override void OnTargetHit(Collider2D targetCollider, ContactFilter2D contactFilter, bool hasFireDamage) {
        Player player = targetCollider.GetComponent<Player>();

        if (player == null || contactFilter.layerMask.value != 1 << targetCollider.gameObject.layer)
            return;

        player.Damage(damage);
        Destroy(gameObject);
    }
}
