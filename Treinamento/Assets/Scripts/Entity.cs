using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public Stats stats;

    [System.Serializable]
    public struct Stats {
        public float lifePoints;
        public float energyPoints;
    }

}
