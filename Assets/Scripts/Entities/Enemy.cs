using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public BaseSkill attack;
    public float range;
    float attackBuffer;

    protected Animator animator;
    protected CharacterAppearance characterAppearance;

    // AI components
    protected Pathfinding.AIDestinationSetter destinationSetter;
    protected Pathfinding.AIPath path;

    bool dead;

    protected new void Start() {
        base.Start();

        dead = false;

        animator = GetComponent<Animator>();
        characterAppearance = GetComponent<CharacterAppearance>();

        destinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        path = GetComponent<Pathfinding.AIPath>();

        Player player = FindObjectOfType<Player>();
        if (player)
            destinationSetter.target = player.transform;

        path.maxSpeed = GetMoveSpeed();
    }

    protected new void Update() {
        base.Update();

        if (currentHealth <= 0 && !dead) {
            dead = true;
            animator.SetTrigger("defeat");
            DisableMovement(2f);
            Destroy(gameObject, 0.3f);
        }

        Vector2 direction = new Vector2(path.destination.x - transform.position.x, path.destination.y - transform.position.y).normalized;

        characterAppearance.weaponDirection = new Vector2(path.target.position.x - transform.position.x, path.target.transform.position.y - transform.position.y).normalized;

        if (Vector2.Distance(transform.position, destinationSetter.target.position) <= range) {
            characterAppearance.weaponObject.GetComponent<Animator>().SetTrigger("attack");
        }

        animator.SetFloat("horizontalDir", path.velocity.x);
        animator.SetFloat("verticalDir", path.velocity.y);

        if (Vector2.Distance(transform.position, path.destination) <= range && attackBuffer <= 0) {
            DisableMovement(GetAttackTimer() * 1.5f);
            attackBuffer = GetAttackTimer();
            attack.Trigger(transform, direction, 1 << 8);
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
