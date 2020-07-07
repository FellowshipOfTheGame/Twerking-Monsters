using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "skll/cape/staff")]
public class CapeStaff : BaseSkill {

    public BaseBuff buffs;
    public float time;

    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        Player player = parent.GetComponent<Player>();
        player.AddTemporaryBuff(buffs, time);
    }
}
