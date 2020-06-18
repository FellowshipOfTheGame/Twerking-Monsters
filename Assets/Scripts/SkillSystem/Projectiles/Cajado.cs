using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cajado : BaseProjectile {

    public float explosionDamage;
    public float explosionRadius;

    public override void OnTargetHit(Collider2D targetCollider, ContactFilter2D contactFilter) {
        Collider2D[] result = Physics2D.OverlapCircleAll(targetCollider.transform.position, explosionRadius, contactFilter.layerMask);
        foreach (Collider2D col in result) {
            Entity entity = col.GetComponent<Entity>();
            if (entity != null)
                entity.stats.lifePoints -= explosionDamage;
        }
        QueueDestruction();
    }
}
