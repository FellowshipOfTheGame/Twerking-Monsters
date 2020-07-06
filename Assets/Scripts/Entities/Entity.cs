using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    [Header("Base stats")]
    public float baseHealth;
    public float baseMana;
    public float baseArmor;
    public float baseSpeed;

    public float attackSpeedMultiplier;
    public float moveSpeedMultiplier;

    protected float currentHealth;
    protected float currentMana;

    protected float healthModifier;

    protected float extraHealthModifier;
    protected float healthRegenModifier;
    protected float manaRegenModifier;
    protected float armorModifier;
    protected float speedModifier;

    public enum Stat {
        EXTRA_HEALTH,
        HEALTH_REGEN,
        MANA_REGEN,
        ARMOR,
        SPEED,
    }

    protected void Start() {
        currentHealth = baseHealth;
    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.K))
            Damage(10f);
    }

    public void Damage(float damage) {
        float armorDefense = (100f - Mathf.Clamp(baseArmor + armorModifier, 0f, 100f)) / 100f;

        healthModifier -= damage * armorDefense;

        if (healthModifier < 0) {
            currentHealth += healthModifier * armorDefense;
            healthModifier = 0;
        }
    }

    public float GetAttackTimer() {
        return attackSpeedMultiplier / (baseSpeed + speedModifier);
    }

    public float GetMoveSpeed() {
        return (baseSpeed + speedModifier) * moveSpeedMultiplier;
    }

    public void AddToModifier(Stat stat, float value) {
        switch (stat) {
            case Stat.EXTRA_HEALTH:
                healthModifier += value;
                break;
            case Stat.HEALTH_REGEN:
                healthModifier += value;
                break;
            case Stat.MANA_REGEN:
                healthModifier += value;
                break;
            case Stat.ARMOR:
                healthModifier += value;
                break;
            case Stat.SPEED:
                healthModifier += value;
                break;
        }
    }

    // buff manager stuff
    public void AddBuff(BaseBuff buff) {
        foreach (BaseBuff.Modifier modifier in buff.modifiers)
            AddToModifier(modifier.stat, modifier.value);

        transform.localScale *= 1f + buff.sizeEffect;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
            renderer.color = Color.Lerp(renderer.color, buff.colorEffect, 0.5f);
    }

    public void RemoveBuff(BaseBuff buff) {
        foreach (BaseBuff.Modifier modifier in buff.modifiers)
            AddToModifier(modifier.stat, -modifier.value);

        transform.localScale /= 1f + buff.sizeEffect;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
            renderer.color = (renderer.color * 2f) - buff.colorEffect;
    }

    public void AddTemporaryBuff(BaseBuff buff, float time) {
        StartCoroutine(TemporaryBuff(buff, time));
    }

    IEnumerator TemporaryBuff(BaseBuff buff, float time) {
        AddBuff(buff);
        yield return new WaitForSeconds(time);
        RemoveBuff(buff);
    }

}
