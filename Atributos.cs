using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Armor
{
        int defesa;
        int deb_velocidade;
        int life_wave;
        int sabedoria;
}
public class Atributos : MonoBehaviour
{
    public static bool fogo;
    public float life = 100;
    public float mana = 100;
    public float velocidade = 1;
    public Armor leve;
    public Armor media;
    public Armor pesada;
    public Armor Atual;
    public float LifeAtual;
    public float VidaInimigo;
    // Start is called before the first frame update
    void Start()
    {
        //Caracteristicas Armor
        leve.defesa = 5;
        media.defesa = 10;
        pesada.defesa = 20;
        leve.deb_velocidade = 5;
        media.deb_velocidade = 10;
        pesada.deb_velocidade = 20;
        leve.life_wave = 15;
        media.life_wave = 20;
        pesada.life_wave = 40;
        leve.sabedoria = 10;
        media.sabedoria = 5;
        pesada.sabedoria = 2;
    }

    // Update is called once per frame
    public int Dano(int arma, Armor Atual){
        float dano;
        dano = arma * (1- Atual.defesa);
        return dano;
    }
    public int Fogo(Armor Armor_Atual, float VidaInimigo){
            VidaInimigo = VidaInimigo * (1-(Armor_Atual.sabedoria/50));
        }
    void CallFogo(){
        CancelInvoke(nameof(Fogo));
    }
    public float LifeWave(Armor Atual, float LifeAtual){
        if (!wave){
            LifeAtual = LifeAtual (1 + (Atual.life_wave/100));
            return LifeAtual;
        }
    }
    void Update()
    {
        InvokeReapeating(nameof(Fogo), 0f, 1.0f);
        invoke(nameof(CallFogo), 5.0f);
    }
}

