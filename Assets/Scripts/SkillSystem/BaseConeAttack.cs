using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newConeAttack", menuName = "Attack/BaseConeAttack")]
public class BaseConeAttack : Skill {

    [Tooltip("O ângulo em graus do cone do ataque")]
    [Range(1, 360)] public float arcAngle;

    [Tooltip("O raio do cone do ataque")]
    public float arcRadius;

    [Tooltip("Quantidade de dano infligido")]
    public float damage;

    public float fireDamage;
    public float fireDuration;

    [Tooltip("Módulo da aceleração a ser aplicada nos alvos")]
    public float knockbackIntensity;

    public Entity.Stat statToBuff;
    public float modifierValue;
    public float buffTime;

    private static readonly float Precision = 30f;

    protected override void OnTrigger(Transform parent, Vector2 target, ContactFilter2D contactFilter, bool hasFireDamage) {
        PolygonCollider2D hitbox;
        hitbox = GenerateConeHitbox(parent, arcAngle, arcRadius, target);

        Collider2D[] results = new Collider2D[16];
        hitbox.OverlapCollider(contactFilter, results);

        foreach (Collider2D collider in results) {
            if (collider == null)
                continue;

            Entity entity = collider.GetComponent<Entity>();

            if (entity != null) {
                entity.Damage(damage);
                if (hasFireDamage)
                    entity.DamageOverTime(fireDamage, fireDuration);
            }

            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null) {
                Vector2 dir_away = collider.transform.position - hitbox.transform.position;
                collider.attachedRigidbody.velocity = dir_away * knockbackIntensity;
                enemy.DisableMovement(0.5f);
            }

            if (collider.attachedRigidbody != null) {
                Vector2 dir_away = collider.transform.position - hitbox.transform.position;
                collider.attachedRigidbody.velocity = dir_away.normalized * knockbackIntensity;
            }


            if (modifierValue != 0f) {
                entity.TemporaryBuff(statToBuff, modifierValue, buffTime);
            }
        }

        Destroy(hitbox);
    }

    PolygonCollider2D GenerateConeHitbox(Transform parent, float angle, float radius, Vector2 direction) {
        Vector2[] points;
        int numPoints = Mathf.FloorToInt(angle / Precision);
        float angleBetweenPoints = angle / (numPoints + 1);
        
        // A forma que os pontos do collider são calculados é:
        // A partir da direção, gerar o "ponto mínimo" (o ângulo da direção - metade do ângulo do arco)
        // e o máximo (direção + a outra metade do ângulo). E, por fim, colocar os pontos adicionais entre
        // os dois, somando valores ao ângulo ponto mínimo (varia conforme o número de pontos).
        float minPointAngle;
        float maxPointAngle;
        float directionAngle;

        if (direction.y > 0)
            directionAngle = Vector2.Angle(Vector2.right, direction);
        else
            directionAngle = 360 - Vector2.Angle(Vector2.right, direction);

        minPointAngle = SumAngles(directionAngle, -1 * (angle / 2));
        maxPointAngle = SumAngles(directionAngle, (angle / 2));

        // Se o ângulo do arco for 360 então é um círculo, e os pontos mínimo e máximo serão o mesmo,
        // então adiciona apenas um deles, e o centro não precisa ser um ponto do collider.
        if (angle == 360)
            points = new Vector2[numPoints + 1];
        else {
            points = new Vector2[numPoints + 3];
            points[numPoints + 1] = new Vector3(Mathf.Cos(maxPointAngle * Mathf.Deg2Rad), Mathf.Sin(maxPointAngle * Mathf.Deg2Rad)) * radius;
            points[numPoints + 2] = Vector3.zero;
        }

        points[0] = new Vector3(Mathf.Cos(minPointAngle * Mathf.Deg2Rad), Mathf.Sin(minPointAngle * Mathf.Deg2Rad)) * radius;

        for (int i = 1; i <= numPoints; i++) {
            float curr_angle = SumAngles(minPointAngle, i * angleBetweenPoints);
            points[i] = new Vector3(Mathf.Cos(curr_angle * Mathf.Deg2Rad), Mathf.Sin(curr_angle * Mathf.Deg2Rad)) * radius;
        }

        // Adiciona o collider no parent.
        PolygonCollider2D collider = parent.gameObject.AddComponent<PolygonCollider2D>();
        
        collider.isTrigger = true;
        collider.points = points;
        
        return collider;
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