using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuffSkill : BaseSkill {

    public BaseBuff[] buffList;
    public float time;

    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        Entity entity = parent.GetComponent<Entity>();

        if (entity == null)
            return;

        foreach (BaseBuff buff in buffList)
            entity.AddTemporaryBuff(buff, time);
    }
}
