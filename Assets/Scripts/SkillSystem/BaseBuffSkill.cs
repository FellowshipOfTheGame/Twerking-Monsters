using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBuffSkill", menuName = "Skill/BuffSkill")]
public class BaseBuffSkill : Skill {

    public Entity.Stat statToBuff;
    public float modifierValue;
    public float buffTime;
    public Color color;
    public GameObject buff;

    protected override void OnTrigger(Transform parent, Vector2 target, ContactFilter2D filter, bool hasFireDamage) {
        Entity entity = parent.GetComponent<Entity>();
        if (entity == null)
            return;
        entity.TemporaryBuff(statToBuff, modifierValue, buffTime);

        GameObject effect = Instantiate(buff, parent);
        effect.GetComponent<SpriteRenderer>().color = color;
        Destroy(effect, buffTime);
    }

    IEnumerator BuffStat(Entity entity, float speedModifier, float buffTime) {
        entity.baseSpeed += speedModifier;
        yield return new WaitForSeconds(1f);
    }

}
