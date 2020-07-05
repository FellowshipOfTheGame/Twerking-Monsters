Skip to content
Search or jump to…

Pull requests
Issues
Marketplace
Explore
 
@DioUSP 
FellowshipOfTheGame
/
treinamento2020-grupo1
0
00
Code
Issues
Pull requests
Actions
Projects
Wiki
Security
Insights
Settings
treinamento2020-grupo1/Treinamento/Assets/Scripts/WaveSpawner.cs
@DioUSP
DioUSP WaveSpawn
Latest commit dae02a6 5 days ago
 History
 1 contributor
107 lines (83 sloc)  2.78 KB
  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    //Assim podemos mudar as variáveis dentro da própria Unity  
    [System.Serializable]
    public class Wave {

        public string name; //Nome da wave
        public Transform enemy; //Referência para o primero inimigo
        public Transform enemy2; //Referência para o segundo inimigo
        public int enemyamount; //Contagem de inimigos para o inimigo 1
        public int enemy2amount; //Contagem de inimigos para o inimigo 2 
    }

    public Wave[] waves;
    public Transform[] spawnPoint;
    public Rigidbody2D player;
    public Chest chestScript;
    private int waveCount = 0;
    private float searchCountdown = 1f;
    private bool isAlive = true;
    private bool endWaves = false;
    public bool isSpawn = false;
    private bool alredyOnFight = false;

    private void Start() {
        player = gameObject.GetComponent<Rigidbody2D>();
        if (spawnPoint.Length == 0) {
            Debug.LogError("Não tem ponto para spawnar!");
        }
        chestScript = GetComponent<Chest>();
        chestScript.enabled = false;
    }

    private void Update() {
        
        if (isSpawn) {
            if (!alredyOnFight) {
                //Spawn inimigos
                Spawn(waves);
                Debug.Log("Spawnando");
                isSpawn = false;
                alredyOnFight = true;
            }
        }   

        isAlive = EnemyIsAlive();

        //Testando caso não haja mais inimigos
        if (isAlive) {
            if (waveCount < waves.Length) {
                waveCount++; //Troca a wave
                isSpawn = true;
                alredyOnFight = false;
            }
            else if(waveCount == waves.Length) {
                //Libera o báu e libera a próxima fase
                chestScript.enabled = true;
            }
        }

    }

    //Spawnando inimigos
    void Spawn(Wave _wave) { 

        //Spawnando a quantidade de inimigos do tipo 1 escolhida no inspector
        for (int i = 0; i < _wave.enemyamount; i++)
        {
            SpawnEnemy(_wave.enemy);
        }

        //Spawnando a quantidade de inimigos do tipo 2 escolhida no inspector
        for (int i = 0; i < _wave.enemy2amount; i++)
        {
            SpawnEnemy(_wave.enemy2);
        }
    }

    void SpawnEnemy(Transform _enemy) {

        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);

    }

    //Checando caso tenha inimigos vivos
    bool EnemyIsAlive() {

        //Checando a cada segundo
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f) {
            
            searchCountdown = 1f;
            
            //Testando caso haja inimigos vivos, se sim retorna falso se não retorna verdadeiro
            //Lembrar de colocar a tag Enemy nos inimigos
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return true;
            }
        }
        
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            isSpawn = true;
        }
    }

}