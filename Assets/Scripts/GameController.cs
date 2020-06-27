using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth <= 0 && !player.isDead)
        {
            player.isDead = true;
            StartCoroutine("GameOver");
        }
    }

    public void btnPause()
    {
        Time.timeScale = 0f;
    }
    public void btnResume()
    {
        Time.timeScale = 1f;
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleSceneAmauri", LoadSceneMode.Single);//implementar
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3.0f);
        ///mostra o botão com a opção ou muda direto
        GoMenu();
    }
}