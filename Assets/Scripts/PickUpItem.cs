using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private IdItem idItem = IdItem.EMPTY;
    private TypeItem typeItem = TypeItem.EMPTY;
    private Chest roomChest;

    public IdItem idPrimaryWeapon;
    public IdItem idSecondaryWeapon;
    public IdItem idArmor;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //pega o unico gameObject que contem o script chest
        roomChest = FindObjectOfType(typeof(Chest)) as Chest;


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //verefica se esta com um item selecinado 
            if (idItem != IdItem.EMPTY)
            {
                switch (typeItem)
                {
                    case TypeItem.PRIMARY_WEAPON://arma
                        idPrimaryWeapon = idItem;
                        break;

                    case TypeItem.SECONDARY_WEAPON://segundario
                        idSecondaryWeapon = idItem;
                        break;

                    case TypeItem.ARMOR://armadura
                        idArmor = idItem;
                        break;

                    default:
                        Debug.Log("deu algo errado mo switch do PickUpItem");
                        break;
                }
                //remove o bau invisivel e os seus children
                Destroy(roomChest.gameObject);
            }
            else
            {
                //feedback temporario
                print("Desculpe não pode fazer esta ação no momento.");
            }
        }
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //verefica se foi o player que tocou no item"bau"
        if (other.gameObject.tag == "Item")
        {
            //pega o  id do item do chão
            idItem = other.gameObject.GetComponent<Item>().idItem;
            typeItem = other.gameObject.GetComponent<Item>().typeItem;
            //marca o item como selecinado
            other.gameObject.GetComponent<Item>().IsSelected();
        }
    }
    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        //reseta o id do item 
        idItem = IdItem.EMPTY;
        typeItem = TypeItem.EMPTY;
        //desmarca o item como selecinado
        other.gameObject.GetComponent<Item>().UnSelected();
    }
}