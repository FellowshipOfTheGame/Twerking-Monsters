using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

    public Vector2 whichDirectionIsUp = Vector2.up;

    protected LayerMask layerMask;
    protected new Rigidbody2D rigidbody2D;

    protected void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void Update() {
        transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(whichDirectionIsUp, rigidbody2D.velocity));
    }

    public void OnTargetHit(GameObject obj, Collider2D target) {
        
    }

    public GameObject InstantiateProjectile(Vector2 origin, Vector2 direction, float speed, LayerMask layerMask) {
        GameObject projectile = Instantiate(gameObject, origin, Quaternion.identity);

        BaseProjectile projectileBaseProjectile = projectile.GetComponent<BaseProjectile>();
        Rigidbody2D projectileRigidbody2D = projectile.GetComponent<Rigidbody2D>();

        projectileRigidbody2D.velocity = direction * speed;
        projectileBaseProjectile.layerMask = layerMask;

        return projectile;
    }

}
