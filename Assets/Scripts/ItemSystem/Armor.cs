using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newArmor", menuName = "Item/Armor")]
public class Armor : Item {

    public Texture2D appearance;

    [Tooltip("Percentage of damage blocked")]
    public float defense;

    public int speedModifier;

    [Tooltip("Ammount regenerated per wave")]
    public int healthRegen;
    
    [Tooltip("Ammount regenerated per second")]
    public int manaRegen;
}
