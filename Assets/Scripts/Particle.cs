using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem particulasHit;
    public ParticleSystemRenderer particulaRender;
    // Start is called before the first frame update
    void Start()
    {
        //desliga por segurança
        particulasHit.Stop(true);
    }

    // Update is called once per frame
    void Update()
    {
        //deve ser ajustado cada vez que o player muda a direção (x,y,z)
        particulaRender.flip = new Vector3(1, 0, 0);
        //manda tocar, não deve ser loop
        particulasHit.Play(true);
    }
}