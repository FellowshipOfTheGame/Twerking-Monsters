using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : BaseProjectile {

    public float damage;
    
    public override void OnTargetHit(Collider2D targetCollider, BaseProjectile baseProjectile) {
        StatManager statManager = targetCollider.gameObject.GetComponent<StatManager>();
        if (statManager != null) {
            statManager.vida -= damage;
            QueueDestruction();
        }
    }
}
