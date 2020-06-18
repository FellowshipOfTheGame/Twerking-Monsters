using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Space]
    [Header("Drops")]
    public GameObject[] fWeapon;
    public GameObject[] sWeapon;
    public GameObject[] armor;
    private SpriteRenderer chestSpriteRenderer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //pega a referencia do spriterender ligado ao script
        chestSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        //verefica se foi o player que tocou no item"bau"
        if (other.gameObject.tag == "Player")
        {
            //cria os itens
            CreateRamdonLoots();
        }
    }
    /// <summary>
    /// Apos ser chamado ela cria um item random de cada tipo, primary, sub e armor
    // Que deve ser previamente configurado, apos criar ele destrooi o item que chamou a function
    /// </summary>
    private void CreateRamdonLoots()
    {
        //cria uma var temp
        int temp;
        //cria a var x com o a pos.x do gameobject
        float x = gameObject.transform.position.x;
        //cria a var y com o a pos.y do gameobject
        float y = gameObject.transform.position.y;
        //verefica se todos os vetor tem pelo menos um item;
        if (fWeapon != null || sWeapon != null || armor != null)
        {
            //gera um numero de 0 a 2
            temp = Random.Range(0, fWeapon.Length);

            //cria um item usando o valor temp como pos do array
            GameObject fWeaponLoot = Instantiate(fWeapon[temp], new Vector2(x - 0.40f, y), gameObject.transform.rotation);
            //coloca ele como filho do chest
            SetParent(fWeaponLoot);

            //gera um numero de 0 a 2
            temp = Random.Range(0, sWeapon.Length);
            //cria um item usando o valor temp como pos do array
            GameObject SWeaponLoot = Instantiate(sWeapon[temp], new Vector2(x, y), gameObject.transform.rotation);
            //coloca ele como filho do chest
            SetParent(SWeaponLoot);

            //gera um numero de 0 a 2
            temp = Random.Range(0, armor.Length);
            //cria um item usando o valor temp como pos do array
            GameObject armorloot = Instantiate(armor[temp], new Vector2(x + 0.40f, y), gameObject.transform.rotation);
            //coloca ele como filho do chest
            SetParent(armorloot);

            //torna o item invisivel
            chestSpriteRenderer.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            //remove o collider do item
            Destroy(gameObject.GetComponent<Collider2D>());
        }
        else
        {
            //avisa caso a config estive errada
            Debug.Log("Falta configurar os itens no vetor de gameObject");
        }

    }
    /// <summary>
    /// recebe um gameobject e torna ele filho"chinld" do gameObject que contem o script
    /// </summary>
    public void SetParent(GameObject child)
    {
        child.transform.parent = gameObject.transform;
    }
}