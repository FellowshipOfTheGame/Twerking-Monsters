using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Entity {

    public Weapon weapon;
    public float range;

    float attackBuffer;

    AIPath path;
    Animator animator;
    Rigidbody2D rb2d;

    new void Start() {
        base.Start();

        path = gameObject.GetComponent<AIPath>();
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    new void Update() {
        base.Update();

        if (currentHealth <= 0f) {
            Destroy(gameObject);
            return;
        }

        path.maxSpeed = GetMoveSpeed();
        animator.SetFloat("horizontalVelocity", path.velocity.x);
        animator.SetFloat("verticalVelocity", path.velocity.y);

        if (Vector2.Distance(transform.position, path.destination) <= range && attackBuffer <= 0) {
            Vector2 direction = new Vector2(path.destination.x - transform.position.x, path.destination.y - transform.position.y).normalized;
            DisableMovement(GetAttackTimer() * 1.5f);
            attackBuffer = GetAttackTimer();
            weapon.Attack(transform, direction, new ContactFilter2D() { useLayerMask = true, layerMask = 1 << 8 }, false);
        }


        if (attackBuffer > 0f)
            attackBuffer -= Time.deltaTime;
    }

    public void DisableMovement(float time) {
        StartCoroutine(DisablePath(time));
    }

    IEnumerator DisablePath(float time) {
        path.enabled = false;
        yield return new WaitForSeconds(time);
        path.enabled = true;
    }

}
