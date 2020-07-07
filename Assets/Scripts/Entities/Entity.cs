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

    public float currentHealth;
    public float currentMana;

    public float extraHealthModifier;
    public float healthRegenModifier;
    public float manaRegenModifier;
    public float armorModifier;
    public float speedModifier;

    private float damageOverTime;
    private float timeRemaining;

    public enum Stat {
        EXTRA_HEALTH,
        HEALTH_REGEN,
        MANA_REGEN,
        ARMOR,
        SPEED,
    }

    protected void Start() {
        currentHealth = baseHealth;
        currentMana = baseMana;
    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.K))
            Damage(10f);

        if (timeRemaining > 0 && damageOverTime > 0)
            Damage(damageOverTime * Time.deltaTime);

        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;

        if (currentMana < baseMana)
            currentMana += manaRegenModifier * Time.deltaTime;
    }

    public void Damage(float damage) {
        float armorDefense = (100f - Mathf.Clamp(baseArmor + armorModifier, 0f, 100f)) / 100f;

        extraHealthModifier -= damage * armorDefense;

        if (extraHealthModifier < 0) {
            currentHealth += extraHealthModifier * armorDefense;
            extraHealthModifier = 0;
        }
    }

    public bool UseMana(float mana) {
        if (currentMana >= mana) {
            currentMana -= mana;
            return true;
        }

        return false;
    }

    public void DamageOverTime(float dps, float time) {
        damageOverTime = dps;
        timeRemaining = time;
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
                extraHealthModifier += value;
                break;
            case Stat.HEALTH_REGEN:
                healthRegenModifier += value;
                break;
            case Stat.MANA_REGEN:
                manaRegenModifier += value;
                break;
            case Stat.ARMOR:
                armorModifier += value;
                break;
            case Stat.SPEED:
                speedModifier += value;
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
