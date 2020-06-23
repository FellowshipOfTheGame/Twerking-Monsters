using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public float life = 100;//vida max do inimigo
    public bool fire;//define se o inimigo está ou não em chamas
    public Transform target;
    public AIPath script;

    public void OnFire(Armor Current, float Life_Enemy) {// dano recebido pelo inimigo enqunto ele estiver pegando fogo
        Life_Enemy = Life_Enemy * (1 - (Current.Magic / 50));
    }
    
    void CallFire() {// chama o dano de fogo e é responsavel por esse dano ser ao longo do tempo
        if (!fire) {
            InvokeRepeating("OnFire", 0f, 0.7f);
            fire = true;// assim que o inimigo comeca a pegar fogo, essa variavel vira true para impedir que outro dando de fogo seja aplicado nele
            Invoke(nameof(CancelFire), 3.0f);
        }
    }

    void Awake() {
        target.GetComponent<Transform>();
        script = GetComponent<AIPath>(); 
    }

    private void Update() {
        float attackRange = 0.4f;
        if (Vector3.Distance(transform.position, target.position) < attackRange) {
            script.enabled = false;
            Debug.Log("Atacando o inimigo");
        } 
        else {
            script.enabled = true;
        }
    }

    public void CancelFire() {//cancela o dano de fogo e seta fire pra false, ou seja o inimigo nao está mais em chamas
        CancelInvoke(nameof(OnFire));
        fire = false;
    }

}
