using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class basicMove : MonoBehaviour
{
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
    private string AnniWalkUp = "UPwalk";
    private string AnniWalkDown = "DOWNwalk";
    private string AnniIdleUp = "UPidle";
    private string AnniIdleDown = "DOWNidle";
    private string AnniWalkHorizontal = "Hwalk";
    private string AnniIdleHorizontal = "Hidle";
    void Start()
    {
        //pega a referencia do SpriteRenderer do gameObject que contem o script
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //pega a referencia do Animator do gameObject que contem o script
        playerAnimator = gameObject.GetComponent<Animator>();
        //pega a referencia do Rigidbody2D do gameObject que contem o script
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //pega o eixo x do movimento sendo  0 ou 1
        x = Input.GetAxisRaw("Horizontal");
        //pega o eixo y do movimento sendo  0 ou 1
        y = Input.GetAxisRaw("Vertical");
        
        switch (x)
        {
            case 1://direita
                resetAll();
                playerAnimator.SetBool(AnniWalkHorizontal, true);
                lookingDir = 1;
                playerSpriteRenderer.flipX = false;
                break;
            case -1://esquerda
                resetAll();
                playerAnimator.SetBool(AnniWalkHorizontal, true);
                lookingDir = 3;
                playerSpriteRenderer.flipX = true;
                break;
            default:
                switch (y)
                {
                    case 1://cima
                        lookingDir = 0;
                        resetAll();
                        playerAnimator.SetBool(AnniWalkUp, true);
                        break;
                    case -1:
                        lookingDir = 2;//baixo
                        resetAll();
                        playerAnimator.SetBool(AnniWalkDown, true);
                        break;
                    default:
                        resetAll();
                        switch (lookingDir)
                        {
                            case 0://up idle
                                playerAnimator.SetBool(AnniIdleUp, true);
                                break;
                            case 1://right idle
                                playerAnimator.SetBool(AnniIdleHorizontal, true);
                                break;
                            case 2://down idle
                                playerAnimator.SetBool(AnniIdleDown, true);
                                break;
                            case 3://left idle
                                playerAnimator.SetBool(AnniIdleHorizontal, true);
                                break;
                            default://por padrão se não for config vem idle olhando para baixo
                                playerAnimator.SetBool(AnniIdleDown, true);
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
        playerAnimator.SetBool(AnniWalkUp, false);
        playerAnimator.SetBool(AnniWalkDown, false);
        playerAnimator.SetBool(AnniIdleUp, false);
        playerAnimator.SetBool(AnniIdleDown, false);
        playerAnimator.SetBool(AnniWalkHorizontal, false);
        playerAnimator.SetBool(AnniIdleHorizontal, false);
    }
}