using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Entity {

    public Skill attack;
    public float range;

    public int enemy;

    public GameObject weapon;

    float attackBuffer;

    AIPath path;
    Animator animator;
    Rigidbody2D rb2d;
    CharacterAnimation characterAnimation;

    bool defeated = false;

    new void Start() {
        base.Start();

        characterAnimation = GetComponent<CharacterAnimation>();
        characterAnimation.ChangeWeapon(weapon);
        path = gameObject.GetComponent<AIPath>();
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        animator.SetInteger("enemy", enemy);

        Player player = FindObjectOfType<Player>();
        if (player != null)
            path.target = player.transform;
    }

    new void Update() {
        base.Update();

        if (defeated)
            return;

        Vector2 direction = new Vector2(path.destination.x - transform.position.x, path.destination.y - transform.position.y).normalized;
        characterAnimation.weaponDirection = direction;

        if (currentHealth <= 0f) {
            characterAnimation.enabled = false;
            defeated = true;
            animator.SetBool("defeated", true);
            Destroy(gameObject, 0.5f);
            return;
        }

        path.maxSpeed = GetMoveSpeed();
        animator.SetFloat("horizontalVelocity", path.velocity.x);
        animator.SetFloat("verticalVelocity", path.velocity.y);

        if (Vector2.Distance(transform.position, path.destination) <= range && attackBuffer <= 0) {
            DisableMovement(GetAttackTimer() * 1.5f);
            attackBuffer = GetAttackTimer();
            attack.Trigger(transform, direction, new ContactFilter2D() { useLayerMask = true, layerMask = 1 << 8 }, false);
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
