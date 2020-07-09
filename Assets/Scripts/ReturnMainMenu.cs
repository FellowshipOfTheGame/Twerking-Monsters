using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public float timeToMainMenu = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MainMenu");
    }

    public IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(timeToMainMenu);
          SceneManager.LoadScene("Menu");
    }
}
