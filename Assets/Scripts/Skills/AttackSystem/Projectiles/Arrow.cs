using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BaseProjectile {

    public float damage;
    public bool piercing;

    public override void OnTargetHit(Collider2D targetCollider, LayerMask laykerMask) {
        Enemy entity = targetCollider.GetComponent<Enemy>();

        if (!entity)
            return;

        entity.Damage(damage);

        if (!piercing)
            Destroy(gameObject);
    }

}
