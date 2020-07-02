using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagarTMP : MonoBehaviour
{
    public float speed;
    private Rigidbody2D playerRigidbody2D;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    void Start()
    {
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //pega o eixo x do movimento sendo  0 ou 1
        float x = Input.GetAxisRaw("Horizontal");
        //pega o eixo y do movimento sendo  0 ou 1
        float y = Input.GetAxisRaw("Vertical");


        playerRigidbody2D.velocity = (new Vector2(x, y).normalized) * speed;
    }
}
