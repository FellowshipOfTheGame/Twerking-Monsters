using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPesada : MonoBehaviour
{
    public Armor pesada;
    // Start is called before the first frame update
    void Start()
    {
        pesada.defense = 20;
        pesada.deb_speed = 20;
        pesada.life_wave = 40;
        pesada.Magic = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
