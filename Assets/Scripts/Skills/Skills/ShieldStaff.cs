using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "skll/shield/staff")]
public class ShieldStaff : BaseSkill {

    public BaseBuff vida;
    
    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        parent.GetComponent<Player>().AddBuff(vida);
    }

}
