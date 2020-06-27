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

    // stats stuff
    [Space]
    [Header("Stats")]
    protected float currentHealth;
    protected float currentMana;

    public float maximumHealth;
    public float maximumMana;

    protected float healthRegeneration;
    protected float manaRegeneration;

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
        activeBuffs = new Dictionary<string, Buff>();

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
                case "modHealth":
                    if (buff.Value.time <= 0) {
                        modHealth -= buff.Value.value;
                        activeBuffs.Remove(buff.Key);
                    }
                    break;
                case "modMana":
                    if (buff.Value.time <= 0) {
                        modMana -= buff.Value.value;
                        activeBuffs.Remove(buff.Key);
                    }
                    break;
                case "modSpeed":
                    if (buff.Value.time <= 0) {
                        modSpeed -= buff.Value.value;
                        activeBuffs.Remove(buff.Key);
                    }
                    break;
                case "modArmor":
                    if (buff.Value.time <= 0) {
                        modArmor -= buff.Value.value;
                        activeBuffs.Remove(buff.Key);
                    }
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

    public void TemporaryBuff(Stat stat, float value, float totalSeconds) {
        switch (stat) {
            case Stat.HEALTH:
                if (activeBuffs.ContainsKey("modHealth")) {
                    modHealth = Mathf.Max(value, activeBuffs["modHealth"].value);
                    activeBuffs["modHealth"] = new Buff() {
                        time = Mathf.Max(totalSeconds, activeBuffs["modHealth"].time),
                        value = Mathf.Max(value, activeBuffs["modHealth"].value),
                    };
                }
                else {
                    modHealth = value;
                    activeBuffs.Add("modHealth", new Buff() { time = totalSeconds, value = value });
                }
                break;
            case Stat.MANA:
                if (activeBuffs.ContainsKey("modMana")) {
                    modMana = Mathf.Max(value, activeBuffs["modMana"].value);
                    activeBuffs["modMana"] = new Buff() {
                        time = Mathf.Max(totalSeconds, activeBuffs["modMana"].time),
                        value = Mathf.Max(value, activeBuffs["modMana"].value),
                    };
                } else {
                    modMana = value;
                    activeBuffs.Add("modMana", new Buff() { time = totalSeconds, value = value });
                }
                break;
            case Stat.SPEED:
                if (activeBuffs.ContainsKey("modSpeed")) {
                    modSpeed = Mathf.Max(value, activeBuffs["modSpeed"].value);
                    activeBuffs["modSpeed"] = new Buff() {
                        time = Mathf.Max(totalSeconds, activeBuffs["modSpeed"].time),
                        value = Mathf.Max(value, activeBuffs["modSpeed"].value),
                    };
                } else {
                    modSpeed = value;
                    activeBuffs.Add("modSpeed", new Buff() { time = totalSeconds, value = value });
                }
                break;
            case Stat.ARMOR:
                if (activeBuffs.ContainsKey("modArmor")) {
                    modArmor = Mathf.Max(value, activeBuffs["modArmor"].value);
                    activeBuffs["modArmor"] = new Buff() {
                        time = Mathf.Max(totalSeconds, activeBuffs["modArmor"].time),
                        value = Mathf.Max(value, activeBuffs["modArmor"].value),
                    };
                } else {
                    modArmor = value;
                    activeBuffs.Add("modArmor", new Buff() { time = totalSeconds, value = value });
                }
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
