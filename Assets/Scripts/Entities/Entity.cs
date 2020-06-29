using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    // base stats that you can modify
    public enum Stat {
        HEALTH,
        MANA,
        ARMOR,
        SPEED,
    }

    // stats stuff
    [Space]
    [Header("Stats")]

    // consumables
    public float maximumHealth;
    public float maximumMana;

    [Tooltip("Percentage of health regained at the end of every wave")]
    public float baseHealthRegeneration;
    [Tooltip("Ammout regenerated per second")]
    public float baseManaRegeneration;

    [Tooltip("Used for move speed and attack speed. Both are directly proportional to base speed, so increasing it also increases both.")]
    [HideInInspector] public float baseSpeed = 1f;
    public float moveSpeed;
    public float attackSpeed;

    [Tooltip("Percentage of damage blocked")]
    [HideInInspector] public float baseArmor = 0f;

    // modified by Damage() and UseMana() functions
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentMana;
    
    // buff system and stat modifiers. only way to change a stat value is to change its modifier.
    protected Dictionary<string, Buff> activeBuffs;

    [HideInInspector] public float healthModifier = 0f;
    [HideInInspector] public float manaModifier = 0f;
    [HideInInspector] public float speedModifier = 0f;
    [HideInInspector] public float armorModifier = 0f;

    protected struct Buff {
        public float value;
        public float time;
    }

    // monobehaviours lifecycle
    protected void Start() {
        activeBuffs = new Dictionary<string, Buff>();

        currentHealth = maximumHealth;
        currentMana = maximumMana;
    }

    protected void Update() {
        // manaRegen
        if (currentHealth < maximumMana)
            currentHealth = Mathf.Clamp(currentHealth + ((baseManaRegeneration + manaModifier) * Time.deltaTime), 0f, maximumMana);

        // update buffs (yes this is pure hardcore, who needs optimization amiright)
        if (activeBuffs.ContainsKey("damageOverTime")) {
            Damage(activeBuffs["damageOverTime"].value);
            activeBuffs["damageOverTime"] = new Buff() { value = activeBuffs["damageOverTime"].value, time = activeBuffs["damageOverTime"].time - Time.deltaTime };

            if (activeBuffs["damageOverTime"].time <= 0f)
                activeBuffs.Remove("damageOverTime");
        }

        if (activeBuffs.ContainsKey("healthModifier")) {
            activeBuffs["healthModifier"] = new Buff() { value = activeBuffs["healthModifier"].value, time = activeBuffs["healthModifier"].time - Time.deltaTime };

            if (activeBuffs["healthModifier"].time <= 0f) {
                healthModifier = Mathf.Clamp(healthModifier - activeBuffs["healthModifier"].value, 0f, activeBuffs["healthModifier"].value);
                activeBuffs.Remove("healthModifier");
            }
        }

        if (activeBuffs.ContainsKey("manaModifier")) {
            activeBuffs["manaModifier"] = new Buff() { value = activeBuffs["manaModifier"].value, time = activeBuffs["manaModifier"].time - Time.deltaTime };

            if (activeBuffs["manaModifier"].time <= 0f) {
                manaModifier = Mathf.Clamp(manaModifier - activeBuffs["manaModifier"].value, 0f, activeBuffs["manaModifier"].value);
                activeBuffs.Remove("manaModifier");
            }
        }

        if (activeBuffs.ContainsKey("speedModifier")) {
            activeBuffs["speedModifier"] = new Buff() { value = activeBuffs["speedModifier"].value, time = activeBuffs["speedModifier"].time - Time.deltaTime };

            if (activeBuffs["speedModifier"].time <= 0f) {
                speedModifier = Mathf.Clamp(speedModifier - activeBuffs["speedModifier"].value, 0f, activeBuffs["speedModifier"].value);
                activeBuffs.Remove("speedModifier");
            }
        }

        if (activeBuffs.ContainsKey("armorModifier")) {
            activeBuffs["armorModifier"] = new Buff() { value = activeBuffs["armorModifier"].value, time = activeBuffs["armorModifier"].time - Time.deltaTime };

            if (activeBuffs["armorModifier"].time <= 0f) {
                armorModifier = Mathf.Clamp(armorModifier - activeBuffs["armorModifier"].value, 0f, activeBuffs["armorModifier"].value);
                activeBuffs.Remove("armorModifier");
            }
        }
    }

    // other functions
    public void Damage(float rawDamage) {
        currentHealth -= rawDamage * GetArmorProtection();
    }

    public void DamageOverTime(float rawDamagePerSecond, float totalSeconds) {
        AddBuff("damageOverTime", new Buff() { value = rawDamagePerSecond, time = totalSeconds });
    }

    public bool UseMana(float ammount) {
        if (currentMana < ammount)
            return false;

        currentMana -= ammount;
        return true;
    }

    public float GetArmorProtection() {
        return (100f - Mathf.Clamp(baseArmor + armorModifier, 0f, 100f)) / 100f;
    }

    public float GetMoveSpeed() {
        return (baseSpeed + speedModifier) * moveSpeed;
    }

    public float GetAttackTimer() {
        return attackSpeed / (baseSpeed + speedModifier);
    }

    public void TemporaryBuff(Stat stat, float value, float totalSeconds) {
        switch (stat) {
            case Stat.HEALTH:
                if (activeBuffs.ContainsKey("healthModifier"))
                    healthModifier = Mathf.Max(value, activeBuffs["healthModifier"].value);
                else
                    healthModifier = value;
                AddBuff("healthModifier", new Buff() { value = value, time = totalSeconds });
                break;
            case Stat.MANA:
                if (activeBuffs.ContainsKey("manaModifier"))
                    manaModifier = Mathf.Max(value, activeBuffs["manaModifier"].value);
                else
                    manaModifier = value;
                AddBuff("manaModifier", new Buff() { value = value, time = totalSeconds });
                break;
            case Stat.SPEED:
                if (activeBuffs.ContainsKey("speedModifier"))
                    speedModifier = Mathf.Max(value, activeBuffs["speedModifier"].value);
                else
                    speedModifier = value;
                AddBuff("speedModifier", new Buff() { value = value, time = totalSeconds });
                break;
            case Stat.ARMOR:
                if (activeBuffs.ContainsKey("armorModifier"))
                    armorModifier = Mathf.Max(value, activeBuffs["armorModifier"].value);
                else
                    armorModifier = value;
                AddBuff("armorModifier", new Buff() { value = value, time = totalSeconds });
                break;
        }
    }

    protected void AddBuff(string key, Buff buff) {
        if (activeBuffs.ContainsKey(key)) {
            activeBuffs[key] = new Buff() {
                value = Mathf.Max(buff.value, activeBuffs[key].value),
                time = Mathf.Max(buff.time, activeBuffs[key].time),
            };
        } else {
            activeBuffs.Add(key, buff);
        }
    }

}
