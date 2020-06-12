using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Um ataque básico que só dá dano (pra exemplificar o uso do BaseConeAttack)

[CreateAssetMenu(menuName = "Attacks/DefaultConeAttack")]
public class DefaultConeAttack : BaseConeAttack {

    public float damage;

    public override void OnTrigger(PolygonCollider2D hitbox, Vector2 direction, ContactFilter2D contactFilter) {
        Collider2D[] results = GetOverlapColliders(hitbox, contactFilter);

        foreach (Collider2D collider in results) {
            if (collider == null)
                continue;

            StatManager statManager = collider.GetComponent<StatManager>();
            statManager.vida -= damage;
        }
    }
}
