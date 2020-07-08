using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBuff", menuName = "Skill/Buff")]
public class BaseBuff : ScriptableObject {

    public float sizeEffect;
    public Color colorEffect;
    public Modifier[] modifiers;

    [System.Serializable]
    public struct Modifier {
        public Entity.Stat stat;
        public float value;
    }

}
