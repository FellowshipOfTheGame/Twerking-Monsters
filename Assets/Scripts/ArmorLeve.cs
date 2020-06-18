using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorLeve : MonoBehaviour
{
    public Armor leve;
    // Start is called before the first frame update
    void Start()
    {
        leve.defense = 5;
        leve.deb_speed = 5;
        leve.life_wave = 15;
        leve.Magic = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
