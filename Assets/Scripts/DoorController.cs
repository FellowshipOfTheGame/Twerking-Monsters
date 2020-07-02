using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public bool isOpen;
    private Animator ani;
    public string nextLevelName;
    void Start()
    {
        isOpen = false;
        ani = gameObject.GetComponent<Animator>();
        GameController.OnWaveEnd += OpenDoor;
    }
    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            ani.SetBool("isOpen", true);
        }
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player" && isOpen)
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

}
