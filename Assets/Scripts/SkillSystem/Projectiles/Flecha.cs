using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : BaseProjectile {

    public float damage;

    public override void OnTargetHit(Collider2D targetCollider, ContactFilter2D contactFilter) {
        Entity entity = targetCollider.GetComponent<Entity>();
        if (entity != null)
            entity.stats.lifePoints -= damage;
    }
}

