using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            PlayerPrefs.SetInt("sex", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKey(KeyCode.Alpha2)) {
            PlayerPrefs.SetInt("sex", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

    }
}
