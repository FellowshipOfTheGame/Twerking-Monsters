using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public int Jogador;
/*
1- Homem
2-Mulher
*/
    void Start()
    {
       Jogador = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)){
            SceneManager.LoadScene("Game");
            Jogador=1;
        }
        else if (Input.GetKey(KeyCode.Alpha2)){
            SceneManager.LoadScene("Game");
            Jogador=2;
        }
        else if (Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
        
    }
}
