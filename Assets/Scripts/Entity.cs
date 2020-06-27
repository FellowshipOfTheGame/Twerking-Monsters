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
    public float currentHealth;
    public float currentMana;

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

    public float modHealth;
    public float modMana;
    public float modSpeed;
    public float modArmor;

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

        List<string> keys = new List<string>();
        foreach (string key in activeBuffs.Keys)
            keys.Add(key);

        foreach (string key in keys) {
            Buff buff = activeBuffs[key];
            buff.time = activeBuffs[key].time - Time.deltaTime;
            buff.value = activeBuffs[key].value;
            activeBuffs[key] = buff;


            switch (key) {
                case "damagePerSecond":
                    if (buff.time > 0)
                        currentHealth -= buff.value * Time.deltaTime;
                    else
                        activeBuffs.Remove(key);
                    break;
                case "modHealth":
                    if (buff.time <= 0) {
                        modHealth -= buff.value;
                        activeBuffs.Remove(key);
                    }
                    break;
                case "modMana":
                    if (buff.time <= 0) {
                        modMana -= buff.value;
                        activeBuffs.Remove(key);
                    }
                    break;
                case "modSpeed":
                    if (buff.time <= 0) {
                        modSpeed -= buff.value;
                        activeBuffs.Remove(key);
                    }
                    break;
                case "modArmor":
                    if (buff.time <= 0) {
                        modArmor -= buff.value;
                        activeBuffs.Remove(key);
                    }
                    break;
            }
        }

    }

    // useful functions
    public bool Damage(float rawDamage) {
        currentHealth -= rawDamage * (100f - (baseArmor + armorModifier)) / 100f;
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
