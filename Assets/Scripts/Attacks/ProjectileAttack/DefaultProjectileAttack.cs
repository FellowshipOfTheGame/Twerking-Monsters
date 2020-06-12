using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/DefaultProjectileAttack")]
public class DefaultProjectileAttack : BaseProjectileAttack {

    public float maxDistance = 1f;

    [Tooltip("The initial speed at which the projectile is launched")]
    public float speed;

    public override void OnTrigger(Transform parent, Vector2 target, ContactFilter2D contactFilter, BaseProjectile projectile) {
        projectile.InstantiateProjectile(parent.position, target, speed, maxDistance);
    }

}
