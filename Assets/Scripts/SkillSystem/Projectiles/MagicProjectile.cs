using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : BaseProjectile {

    public float damage;
    public float radius;

    public float fireDamage;
    public float fireDuration;

    public GameObject explosion;

    public override void OnTargetHit(Collider2D targetCollider, ContactFilter2D contactFilter, bool hasFireDamage) {
        if (1 << targetCollider.gameObject.layer != contactFilter.layerMask.value)
            return;

        GameObject particle = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(particle, 0.3f);

        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, radius, contactFilter.layerMask);

        foreach(Collider2D col in results) {
            Entity entity = col.GetComponent<Entity>();
            if (entity != null) {
                entity.Damage(damage);
                if (hasFireDamage)
                    entity.DamageOverTime(fireDamage, fireDuration);
            }
        }

        Destroy(gameObject);
    }
}
