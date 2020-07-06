using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : BaseProjectile {

    public float damage;

    public override void OnTargetHit(Collider2D targetCollider, LayerMask laykerMask) {
        if (!targetCollider.IsTouchingLayers(layerMask))
            return;

        Entity entity = targetCollider.GetComponent<Entity>();

        if (!entity)
            return;

        entity.Damage(damage);
        Destroy(gameObject);
    }
}
