using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// O BaseProjectileAttack é um tipo de ataque que lança um GameObject, o Projectile,
// em uma direção com determinada velocidade. O projectile por si só tem um comportamento
// pré definido que não depende desse ataque. Sendo assim, o BaseProjectileAttack define
// como o projectile vai ser lançado, e não o que ele faz.
public abstract class BaseProjectileAttack : Attack {

    [Tooltip("Prefab to be used as the projectile")]
    public BaseProjectile projectile;

    public abstract void OnTrigger(Transform parent, Vector2 target, ContactFilter2D contactFilter, BaseProjectile projectile);

    public override void Trigger(Transform parent, Vector2 target, ContactFilter2D contactFilter) {
        OnTrigger(parent, target, contactFilter, projectile);
    }

}
