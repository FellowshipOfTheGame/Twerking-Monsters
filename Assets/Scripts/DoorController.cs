using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public bool isOpen;
    private Animator ani;
    void Start()
    {
        isOpen = false;//devife a ver como false ao iniciar a cena
        ani = gameObject.GetComponent<Animator>();//pega por paremtro o animator da porta
        GameController.OnWaveEnd += OpenDoor; //adiciona o metodo que sera chamado
    }
    public void OpenDoor()
    {
        if (!isOpen)// se não tiver aberta 
        {
            isOpen = true; // defina como aberta
            ani.SetBool("isOpen", true); // e mude a animação pra aberta
        }
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isOpen)// ve se o objeto tem a tag Player
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);// muda a fase segundo a config
        }
    }

}
