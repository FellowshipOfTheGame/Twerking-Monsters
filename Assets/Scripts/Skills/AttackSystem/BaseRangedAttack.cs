using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newProjectileAttack", menuName = "Attack/BaseProjectileAttack")]
public class BaseRangedAttack : BaseSkill {

    [Header("Projectile Attack")]
    public BaseProjectile projectile;
    public float speed;
    public float maxDistance;

    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        projectile.InstantiateProjectile(parent.position + (Vector3) direction, direction, speed, maxDistance, layerMask);
    }

}
