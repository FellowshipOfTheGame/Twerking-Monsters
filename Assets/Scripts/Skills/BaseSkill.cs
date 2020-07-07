using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : ScriptableObject {

    public float cost = 0f;
    public float cooldown;

    [Header("Special Effects")]
    public GameObject effect;
    public Vector2 whichDirectionIsUp;
    public float distanceFromCenter;

    protected abstract void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask);

    public void Trigger(Transform parent, Vector2 direction, LayerMask layerMask) {

        Player player = parent.GetComponent<Player>();

        if (player) {
            if (!player.UseMana(cost))
                return;
        }

        if (effect) {
            Vector3 position = parent.transform.position + (Vector3)(direction * distanceFromCenter);
            Quaternion rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(whichDirectionIsUp, direction));

            GameObject effectInstance = Instantiate(effect, position, rotation, parent.transform);
            Destroy(effectInstance, cooldown);
        }

        OnTrigger(parent, direction, layerMask);
    }

}
