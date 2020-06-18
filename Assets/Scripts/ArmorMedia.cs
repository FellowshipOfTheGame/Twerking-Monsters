using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorMedia : MonoBehaviour
{
    public Armor media;
    // Start is called before the first frame update
    void Start()
    {
        media.defense = 10;
        media.deb_speed = 10;
        media.life_wave = 20;
        media.Magic = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
