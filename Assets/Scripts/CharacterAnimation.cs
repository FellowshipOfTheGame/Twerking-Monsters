using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    [Space]
    [Header("Source Sheets")]
    public Texture2D playerSheet;
    public Texture2D armorSheet;

    [Space]
    [Header("Sprite config")]
    public float referencePixelsPerUnit;
    public float spriteWidth;
    public float spriteHeight;
    public GameObject armor;

    [Space]
    [Header("Animation")]
    public PlayerState state;
    public int frame;

    protected SpriteRenderer spriteRenderer;
    protected SpriteRenderer armorRenderer;

    // De baixo para cima na sheet
    public enum PlayerState {
        MOVING_RIGHT = 0,
        MOVING_LEFT = 1,
        MOVING_UP = 2,
        MOVING_DOWN = 3,
        IDLE = 4,
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        armorRenderer = armor.GetComponent<SpriteRenderer>();
    }

    void Update() {
        spriteRenderer.sprite = GetSpriteFromSheet(playerSheet, frame, (int) state);
        if (armorSheet != null)
            armorRenderer.sprite = GetSpriteFromSheet(armorSheet, frame, (int) state);
    }

    protected Sprite GetSpriteFromSheet(Texture2D spriteSheet, int x, int y) {
        Rect rect = new Rect(x * spriteWidth, y * spriteHeight, spriteWidth, spriteHeight);
        Sprite spr = Sprite.Create(spriteSheet, rect, new Vector2(0.5f, 0.5f), referencePixelsPerUnit);
        return spr;
    }

}
