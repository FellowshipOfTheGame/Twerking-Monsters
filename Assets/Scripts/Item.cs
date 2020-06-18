using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IdItem
{
    EMPTY,//0
    SWORD,//1
    BOW,//2
    WAND,//3
    CAPE,//4
    SHIELD,//5
    GRIMOIRE,//6
    LIGHT_ARMOR,//7
    MEDIUM_ARMOR,//8
    HEAVY_ARMOR//9
}
public enum TypeItem
{
    EMPTY,//0
    PRIMARY_WEAPON,//1
    SECONDARY_WEAPON,//2
    ARMOR//3
}

public class Item : MonoBehaviour
{
    public string nameItem;
    public IdItem idItem;
    public TypeItem typeItem;
    private SpriteRenderer itemSpriteRenderer;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //pega a referencia do sprite render do item
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        //chama a função de deixar o item apagado, "não selecinado"
        UnSelected();
    }
    /// <summary>
    ///Contem detalhes dos items
    ///</summary>
    /// <param name="name">string que vai representar o nome do item.</param>
    /// <param name="id">int que tem o valor unico do item, não pode ser repetir.</param>
    /// <param name="type">int que representa o tipo do item 1 arma, 2 segundaria, 3 armadura.</param>
    public Item(string name, IdItem id, TypeItem type)
    {
        this.nameItem = name;
        this.idItem = id;
        this.typeItem = type;
    }
    /// <summary>
    ///deixa o item com a cor e o alpha normal
    ///</summary>
    public void IsSelected()
    {
        itemSpriteRenderer.color = Color.white;
    }
    /// <summary>
    ///remove parte da cor e do apha do item dando efeito de apagado
    ///</summary>
    public void UnSelected()
    {
        itemSpriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
    }
}