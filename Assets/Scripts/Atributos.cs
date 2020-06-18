using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Armor
{
        public int defense;
        public int deb_speed;
        public int life_wave;
        public int Magic;
}
public class Atributos : MonoBehaviour
{//Essa classe contem todos os atributos e caracteristicas de inimigos, player e cenario do jogo
    public bool wave;//Define se é ou nao uma wave
    public bool fire;//define se o inimigo está ou não em chamas
    public float life_player = 100;//vida max do jogador
    public float mana_max = 100;// mana max do jogdor
    public float speed = 1;// velocidade do jogador
    public Armor Current;// weapondura atual do jogador
    public float LifeCurrent;//Vida atual do Jogador
    public float Life_Enemy;// vida do inimigo
    public static Atributos instance;
    private void Awake() {
        if(instance == null) instance = this;
        else if(instance != this) Destroy(this);
    }

    public float PlayerDamage(int En_weapon, Armor Current){//calculo de dano recebido pelo player e pode ser utilizado tambem para o dano que o player der em inimigos mais fortes
        float damage;
        damage = En_weapon * (1- Current.defense);
        return damage;
    }
    public void OnFire(Armor Current, float Life_Enemy){// dano recebido pelo inimigo enqunto ele estiver pegando fogo
            Life_Enemy = Life_Enemy * (1-(Current.Magic/50));
        }
    void CallFire(){// chama o dano de fogo e é responsavel por esse dano ser ao longo do tempo
        if(!fire){
            InvokeRepeating("OnFire", 0f, 0.7f);
            fire = true;// assim que o inimigo comeca a pegar fogo, essa variavel vira true para impedir que outro dando de fogo seja aplicado nele
            Invoke(nameof(CancelFire), 3.0f);
        }
    }
    public float LifeWave(Armor Current, float LifeCurrent){// cura por  wave
            LifeCurrent = LifeCurrent + (life_player * (Current.life_wave/100));// a cura é na vida maxima por isso usa-se life player
            return LifeCurrent;
        }
    public void CancelFire(){//cancela o dano de fogo e seta fire pra false, ou seja o inimigo nao está mais em chamas
        CancelInvoke(nameof(OnFire));
        fire = false;
    }
    public void ManaRegen(float current_mana, Armor Current){
        current_mana += Current.Magic;
    }
}