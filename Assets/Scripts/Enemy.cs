using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Entity {

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

        path.maxSpeed = GetMoveSpeed();
        animator.SetFloat("dirHorizontal", path.velocity.x);
        animator.SetFloat("dirVertical", path.velocity.y);

        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    public void Stun(Vector2 dirAway, float velocity) {
        TemporaryBuff(Entity.Stat.SPEED, -1000, 0.5f);
        StartCoroutine(DisablePath(0.5f));
        rb2d.velocity = dirAway * velocity;
    }

    IEnumerator DisablePath(float time) {
        path.enabled = false;
        yield return new WaitForSeconds(time);
        path.enabled = true;
    }

}
