using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BaseProjectile {

    public float damage;
    public bool piercing;

    public override void OnTargetHit(Collider2D targetCollider, LayerMask laykerMask) {
       
       if (1 << targetCollider.gameObject.layer != layerMask.value) {
            return;
        } 
        Entity entity = targetCollider.GetComponent<Entity>();

        if (!entity)
            return;

        entity.Damage(damage);

        if (!piercing)
            Destroy(gameObject);
    }

}
