using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseProjectile : MonoBehaviour {

    // """Temporário""" até eu conseguir pensar em uma maneira melhor de detectar quando ele chega na distância máxima.
    bool hasMaxDistance;
    Vector2 origin;
    float maxDistance;
    bool hasFireDamage;

    // Ajustar a rotação do objeto com base na velocidade.
    [HideInInspector] public bool rotateToMatchVelocity = true;

    protected Rigidbody2D rb2d;
    [HideInInspector] public ContactFilter2D contactFilter;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();;

        if (rotateToMatchVelocity) {
            float angle;

            if (rb2d.velocity.x < 0)
                angle = Vector2.Angle(Vector2.up, rb2d.velocity);
            else
                angle = 360 - Vector2.Angle(Vector2.up, rb2d.velocity);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

    }

    void Update() {
        if (hasMaxDistance) {
            if (Vector2.Distance(transform.position, origin) >= maxDistance)
                QueueDestruction();
        }
    }

    public abstract void OnTargetHit(Collider2D targetCollider, ContactFilter2D contactFilter, bool hasFireDamage);

    void OnTriggerEnter2D(Collider2D targetCollider) {
        OnTargetHit(targetCollider, contactFilter, hasFireDamage);
    }

    protected void QueueDestruction() {
        // Espaço para chamar uma animação ou algo do tipo antes do objeto ser destruído.
        Destroy(gameObject);
    }

    public GameObject InstantiateProjectile(Vector2 origin, Vector2 direction, float speed, float maxDistance, ContactFilter2D contactFilter, bool hasFireDamage) {
        GameObject obj = Instantiate(gameObject, origin, Quaternion.identity);
        if (maxDistance > 0) {
            obj.GetComponent<BaseProjectile>().origin = origin;
            obj.GetComponent<BaseProjectile>().maxDistance = maxDistance;
            obj.GetComponent<BaseProjectile>().hasMaxDistance = true;
            obj.GetComponent<BaseProjectile>().contactFilter = contactFilter;
        }
        obj.GetComponent<BaseProjectile>().rotateToMatchVelocity = true;
        obj.GetComponent<Rigidbody2D>().velocity = direction * speed;

        return obj;
    }

}
