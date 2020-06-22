using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Weapon weapon;

    // ===== MOVIMENTAÇÃO =====
    [Space]
    [Header("PlayerConfig")]
    public float speed;
    private int lookingDir;//0 up, 1 right, 2 down, 3 left
    private bool isMirrored;
    [Space]
    [Header("Componentes")]
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidbody2D;
    private float x;
    private float y;
    [Space]
    [Header("Bool da Anaimação")]
    private string anniWalkUp = "UPwalk";
    private string anniWalkDown = "DOWNwalk";
    private string anniIdleUp = "UPidle";
    private string anniIdleDown = "DOWNidle";
    private string anniWalkHorizontal = "Hwalk";
    private string anniIdleHorizontal = "Hidle";

    // ===== ATRIBUTOS =====

    //Essa classe contem todos os atributos e caracteristicas de inimigos, player e cenario do jogo
    public float life = 100;//vida max do jogador
    public float mana = 100;// mana max do jogdor
    public float manaMax;
    public Armor current;// weapondura atual do jogador
    public float lifeCurrent;//Vida atual do Jogador

    // ===== INVENTÁRIO =====

    private IdItem idItem = IdItem.EMPTY;
    private TypeItem typeItem = TypeItem.EMPTY;
    private Chest roomChest;

    public Skill selectedSkill;

    public Skill tempSelectedSkill;//cria um local temp para guarda o skill;

    public IdItem idPrimaryWeapon;
    public IdItem idSecondaryWeapon;
    public IdItem idArmor;
    //==============new passives
    public int passiveDefense = 2;
    public float passiveSpeed = 0.5f;
    private float speedBase = 1;
    public bool passiveFire = false;



    void Start()
    {
        roomChest = FindObjectOfType(typeof(Chest)) as Chest;

        //pega a referencia do SpriteRenderer do gameObject que contem o script
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //pega a referencia do Animator do gameObject que contem o script
        playerAnimator = gameObject.GetComponent<Animator>();
        //pega a referencia do Rigidbody2D do gameObject que contem o script
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        InvokeRepeating("ManaRegen", 0f, 1f);
        //define valor da velocidade base do player
        //  speedBase = speed;
    }

    void Update()
    {
        ManageMovement();
        Bau();
        Atack();

        weapon.center = transform.position;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = new Vector2(mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y).normalized;

        weapon.direction = mouseDirection;
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
            ///coloca o skill no local temporario
            tempSelectedSkill = other.gameObject.GetComponent<Item>().skill;

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

        ///remove o skill no local temporario
        tempSelectedSkill = null;
    }

    public float PlayerDamage(int en_weapon)
    {//calculo de dano recebido pelo player e pode ser utilizado tambem para o dano que o player der em inimigos mais fortes
        float damage;
        damage = en_weapon * (1 - current.defense);
        return damage;
    }

    public void ManaRegen()
    {
        mana = Mathf.Clamp(mana + current.Magic, 0, manaMax);
    }

    public float LifeWave(Armor Current, float LifeCurrent)
    {// cura por  wave
        LifeCurrent = LifeCurrent + (life * (Current.life_wave / 100));// a cura é na vida maxima por isso usa-se life player
        return LifeCurrent;
    }

    void Bau()
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
                        resetPassive();
                        ///////////// sub arma
                        switch (idItem)
                        {
                            case IdItem.CAPE://capa
                                speed += passiveSpeed;
                                break;
                            case IdItem.GRIMOIRE://grimorio
                                passiveFire = true;
                                break;
                            case IdItem.SHIELD://escudo
                                               //onde fica defesa  = current.defense + passiveDefense;
                                break;

                            default:
                                Debug.Log("deu algo errado mo switch do player/sub arma");
                                break;
                        }
                        break;

                    case TypeItem.ARMOR://armadura
                        idArmor = idItem;
                        break;

                    default:
                        Debug.Log("deu algo errado mo switch do player/ tipo item");
                        break;
                }
                //remove o bau invisivel e os seus children
                Destroy(roomChest.gameObject);
                //coloca o skill em temporario como definitivo
                selectedSkill = tempSelectedSkill;
            }
            else
            {
                //feedback temporario
                print("Desculpe não pode fazer esta ação no momento.");
            }
        }
    }
    void Atack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedSkill.Trigger(gameObject,weapon.gameObject.transform.position,);
        }

    }

    void ManageMovement()
    {
        //pega o eixo x do movimento sendo  0 ou 1
        x = Input.GetAxisRaw("Horizontal");
        //pega o eixo y do movimento sendo  0 ou 1
        y = Input.GetAxisRaw("Vertical");

        switch (x)
        {
            case 1://direita
                resetAll();
                playerAnimator.SetBool(anniWalkHorizontal, true);
                lookingDir = 1;
                playerSpriteRenderer.flipX = false;
                break;
            case -1://esquerda
                resetAll();
                playerAnimator.SetBool(anniWalkHorizontal, true);
                lookingDir = 3;
                playerSpriteRenderer.flipX = true;
                break;
            default:
                switch (y)
                {
                    case 1://cima
                        lookingDir = 0;
                        resetAll();
                        playerAnimator.SetBool(anniWalkUp, true);
                        break;
                    case -1:
                        lookingDir = 2;//baixo
                        resetAll();
                        playerAnimator.SetBool(anniWalkDown, true);
                        break;
                    default:
                        resetAll();
                        switch (lookingDir)
                        {
                            case 0://up idle
                                playerAnimator.SetBool(anniIdleUp, true);
                                break;
                            case 1://right idle
                                playerAnimator.SetBool(anniIdleHorizontal, true);
                                break;
                            case 2://down idle
                                playerAnimator.SetBool(anniIdleDown, true);
                                break;
                            case 3://left idle
                                playerAnimator.SetBool(anniIdleHorizontal, true);
                                break;
                            default://por padrão se não for config vem idle olhando para baixo
                                playerAnimator.SetBool(anniIdleDown, true);
                                break;
                        }
                        break;
                }
                break;
        }
        /*
        define a direção e normaliza ela para as diagonais e
        aplica a velocidade ao vetor de direção
        */
        playerRigidbody2D.velocity = (new Vector2(x, y).normalized) * speed;
    }

    /// <summary>
    /// Quando chamado ela limpa todos os bool da animator do player
    /// </summary>
    private void resetAll()
    {
        playerAnimator.SetBool(anniWalkUp, false);
        playerAnimator.SetBool(anniWalkDown, false);
        playerAnimator.SetBool(anniIdleUp, false);
        playerAnimator.SetBool(anniIdleDown, false);
        playerAnimator.SetBool(anniWalkHorizontal, false);
        playerAnimator.SetBool(anniIdleHorizontal, false);
    }
    private void resetPassive()
    {
        passiveFire = false;
        speed = speedBase;
        //onde fica defesa  = current.defense
    }

}
