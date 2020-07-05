using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public float attackRange;

    protected Animator animator;
    protected CharacterAppearance characterAppearance;

    // AI components
    protected Pathfinding.AIDestinationSetter destinationSetter;
    protected Pathfinding.AIPath path;

    protected new void Start() {
        base.Start();

        animator = GetComponent<Animator>();
        characterAppearance = GetComponent<CharacterAppearance>();

        destinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        path = GetComponent<Pathfinding.AIPath>();

        Player player = FindObjectOfType<Player>();
        if (player)
            destinationSetter.target = player.transform;
    }

    protected new void Update() {
        base.Update();

        characterAppearance.weaponDirection = new Vector2(path.target.position.x - transform.position.x, path.target.transform.position.y - transform.position.y).normalized;

        if (currentHealth <= 0f) {
            animator.SetTrigger("defeat");
            Destroy(gameObject, 0.3f);
        }

        if (Vector2.Distance(transform.position, destinationSetter.target.position) <= attackRange) {
            characterAppearance.weaponObject.GetComponent<Animator>().SetTrigger("attack");
        }

        animator.SetFloat("horizontalDir", path.velocity.x);
        animator.SetFloat("verticalDir", path.velocity.y);

    }
}
