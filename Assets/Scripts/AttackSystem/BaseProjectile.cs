using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseProjectile : MonoBehaviour {

    // Temporário até eu conseguir pensar em uma maneira melhor de detectar quando ele chega na distância máxima.
    bool hasMaxDistance = false;
    Vector2 origin;
    float maxDistance;

    // Ajustar a rotação do objeto com base na velocidade.
    public bool rotateToMatchVelocity = true;

    protected Rigidbody2D rb2d;

    public abstract void OnTargetHit(Collider2D targetCollider, BaseProjectile baseProjectile);

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

    void OnTriggerEnter2D(Collider2D targetCollider) {
        OnTargetHit(targetCollider, this);
    }

    protected void QueueDestruction() {
        // Espaço para chamar uma animação antes do objeto ser destruído.
        Destroy(gameObject);
    }

    public GameObject InstantiateProjectile(Vector2 origin, Vector2 direction, float speed, float maxDistance) {
        GameObject obj = Instantiate(gameObject, origin, Quaternion.identity);
        if (maxDistance != 0) {
            obj.GetComponent<BaseProjectile>().origin = origin;
            obj.GetComponent<BaseProjectile>().maxDistance = maxDistance;
            obj.GetComponent<BaseProjectile>().hasMaxDistance = true;
        }
        obj.GetComponent<BaseProjectile>().rotateToMatchVelocity = true;
        obj.GetComponent<Rigidbody2D>().velocity = direction * speed;

        return obj;
    }

}
