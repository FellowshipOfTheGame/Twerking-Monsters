using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : BaseProjectile {

    public float damage;
    public float radius;

    public float fireDamage;
    public float fireDuration;

    public GameObject explosion;

    public override void OnTargetHit(Collider2D targetCollider, LayerMask laykerMask) {
        Enemy enemy = targetCollider.GetComponent<Enemy>();

        if (!enemy)
            return;


        GameObject particle = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(particle, 0.3f);

        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach(Collider2D col in results) {
            Entity entity = col.GetComponent<Entity>();
            if (entity != null) {
                entity.Damage(damage);
            }
        }

        Destroy(gameObject);
    }
}
