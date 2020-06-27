using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public enum Stat {
        HEALTH,
        MANA,
        ARMOR,
        SPEED,
    }

    // attack stuff
    public Weapon baseAttack;
    public Vector2 target;

    // stats stuff
    [Space]
    [Header("Stats")]
    protected float currentHealth;
    protected float currentMana;

    public float maximumHealth;
    public float maximumMana;

    public float healthRegeneration;
    public float manaRegeneration;

    public float extraHealth;

    public float baseSpeed;
    protected float speedModifier;

    public float moveSpeed;
    public float attackSpeed;

    public float baseArmor;
    protected float armorModifier;

    // buff system
    protected Dictionary<string, Buff> activeBuffs;

    protected float modHealth;
    protected float modMana;
    protected float modSpeed;
    protected float modArmor;

    protected struct Buff {
        public float value;
        public float time;
    }

    // Monobehaviour lifecycle
    protected void Start() {
        currentHealth = maximumHealth;
        currentMana = maximumMana;
    }

    protected void Update() {
        if (currentMana < maximumMana)
            currentMana = Mathf.Clamp(currentMana + (manaRegeneration * Time.deltaTime), 0f, maximumMana);

        foreach (KeyValuePair<string, Buff> buff in activeBuffs) {
            switch (buff.Key) {
                case "damagePerSecond":
                    if (buff.Value.time <= 0)
                        activeBuffs.Remove(buff.Key);
                    else
                        currentHealth -= buff.Value.value * Time.deltaTime;
                    break;
                
            }
            activeBuffs[buff.Key] = new Buff() { value = buff.Value.value, time = buff.Value.time - Time.deltaTime };
        }
    }

    // useful functions
    public bool Damage(float rawDamage) {
        currentHealth -= rawDamage * (1f - (baseArmor + armorModifier)) / 100;
        return currentHealth > 0;
    }

    public void DamageOverTime(float rawDamagePerSecond, float totalSeconds) {
        if (activeBuffs.ContainsKey("damagePerSecond"))
            activeBuffs["damagePerSecond"] = new Buff() {
                time = Mathf.Max(totalSeconds, activeBuffs["damagePerSecond"].time),
                value = Mathf.Max(rawDamagePerSecond, activeBuffs["damagePerSecond"].value),
            };
        else
            activeBuffs.Add("damagePerSecond", new Buff() { time = totalSeconds, value = rawDamagePerSecond });
    }

    public bool TemporaryBuff(Stat stat, float value, float totalSeconds) {
        switch (stat) {
            case Stat.HEALTH:
                break;
            case Stat.MANA:
                break;
            case Stat.SPEED:
                break;
            case Stat.ARMOR:
                break;
        }
    }

    public bool UseMana(float ammount) {
        if (currentMana < ammount)
            return false;

        currentMana -= ammount;
        return true;
    }

    public float GetMoveSpeed() {
        return (baseSpeed + speedModifier) * moveSpeed;
    }

    public float GetAttackSpeed() {
        return (baseSpeed + speedModifier) * attackSpeed;
    }

}
