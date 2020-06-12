using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/MultipleProjectileAttack")]
public class MultipleProjectileAttack : BaseProjectileAttack {

    public int numProjectiles;
    [Range(1, 360)] public float arcAngle;
    public float maxDistance = 1f;

    [Tooltip("The initial speed at which the projectile is launched")]
    public float speed;

    public override void OnTrigger(Transform parent, Vector2 target, ContactFilter2D contactFilter, BaseProjectile projectile) {

        float angleBetweenProjectiles = arcAngle / (numProjectiles + 1);

        float minPointAngle;
        float directionAngle;

        if (target.y > 0)
            directionAngle = Vector2.Angle(Vector2.right, target);
        else
            directionAngle = 360 - Vector2.Angle(Vector2.right, target);

        minPointAngle = SumAngles(directionAngle, -1 * (arcAngle / 2));

        for (int i = 1; i <= numProjectiles; i++) {
            float curr_angle = SumAngles(minPointAngle, i * angleBetweenProjectiles);
            Vector2 direction = new Vector2(Mathf.Cos(curr_angle * Mathf.Deg2Rad), Mathf.Sin(curr_angle * Mathf.Deg2Rad));
            projectile.InstantiateProjectile(parent.position, direction, speed, maxDistance);
        }
    }

    float SumAngles(params float[] angles) {
        float sum = 0;
        float result;

        for (int i = 0; i < angles.Length; i++)
            sum += angles[i];

        result = sum % 360;

        if (result < 0)
            result = 360 + result;

        return result;
    }

}
