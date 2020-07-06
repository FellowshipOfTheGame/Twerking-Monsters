using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "skll/grimoire/bow")]
public class GrimoiraBow : BaseSkill {
    [Header("Projectile Attack")]
    public BaseProjectile projectile;
    public float speed;
    public float maxDistance;

    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        projectile.InstantiateProjectile(parent.position + (Vector3)direction, direction, speed, maxDistance, layerMask);
    }
}
