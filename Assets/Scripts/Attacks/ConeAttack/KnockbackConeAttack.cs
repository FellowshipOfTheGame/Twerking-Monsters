using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Um ataque básico que que dá dano e empurra o alvo na direção oposta do gameObject que cria o ataque (pra exemplificar o uso do BaseConeAttack)
[CreateAssetMenu(menuName = "Attacks/KnockbackAttack")]
public class KnockbackConeAttack : BaseConeAttack {

    public float damage;
    public float knockbackIntensity;

    public override void OnTrigger(PolygonCollider2D hitbox, Vector2 direction, ContactFilter2D contactFilter) {
        Collider2D[] results = GetOverlapColliders(hitbox, contactFilter);

        foreach (Collider2D collider in results) {
            if (collider == null)
                continue;

            StatManager statManager = collider.GetComponent<StatManager>();
            statManager.vida -= damage;

            Rigidbody2D rb2d = collider.GetComponent<Rigidbody2D>();
            if (rb2d != null) {
                Vector2 dir_away = collider.transform.position - hitbox.transform.position;
                rb2d.velocity = dir_away.normalized * knockbackIntensity;
            }
        }
    }
}
