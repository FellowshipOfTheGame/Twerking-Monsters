using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagarTMP : MonoBehaviour
{
    public int dano;

    //void OnCollisionEnter2D(Collision2D other)
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           other.gameObject.GetComponent<Player>().currentHealth -= dano;
        }
    }
}
