using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "skll/cape/bow")]
public class CapeBow : BaseSkill {

    public BaseBuff invisibilidade;
    public float tempo;

    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        Player player = parent.GetComponent<Player>();
        player.AddTemporaryBuff(invisibilidade, tempo);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            enemy.GetComponent<Enemy>().DisableMovement(tempo);
        }
    }
}
