using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newArmor", menuName = "Item/Armor")]
public class Armor : ScriptableObject {
    public int defense;
    public int deb_speed;
    public int life_wave;
    public int Magic;
}
