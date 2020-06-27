using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// O BaseProjectileAttack é um tipo de ataque que lança um GameObject, o Projectile,
// em uma direção com determinada velocidade. O projectile por si só tem um comportamento
// pré definido que não depende desse ataque. Sendo assim, o BaseProjectileAttack define
// como o projectile vai ser lançado, e não o que ele faz.
[CreateAssetMenu(fileName = "newProjectileAttack", menuName = "Attack/BaseProjectileAttack")]
public class BaseProjectileAttack : Skill {

    [Tooltip("Prefab to be used as the projectile")]
    public BaseProjectile projectile;

    public float speed;
    public float maxDistance;

    protected override void OnTrigger(Player parent, Vector2 target, ContactFilter2D contactFilter) {
        projectile.InstantiateProjectile(parent.transform.position, target, speed, maxDistance, contactFilter);
    }

}
