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

    public GameObject armorObject;
    public GameObject weaponObject;

    [Space]
    [Header("Animation Movement and Armor")]
    public PlayerState state;
    public int frame;

    [Space]
    [Header("Animation Movement and Armor")]
    public float weaponDistanceFromCenter;

    protected SpriteRenderer spriteRenderer;
    protected SpriteRenderer armorRenderer;

    public Vector2 weaponDirection;

    // De baixo para cima na sheet
    public enum PlayerState {
        MOVING_RIGHT = 0,
        MOVING_LEFT = 1,
        MOVING_UP = 2,
        MOVING_DOWN = 3,
        IDLE_RIGHT = 4,
        IDLE_LEFT = 5,
        IDLE_UP = 6,
        IDLE_DOWN = 7,
    }

    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (armorObject != null)
            armorRenderer = armorObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        spriteRenderer.sprite = GetSpriteFromSheet(playerSheet, frame, (int) state);

        if (armorSheet != null)
            armorRenderer.sprite = GetSpriteFromSheet(armorSheet, frame, (int) state);

        if (weaponObject != null) {
            weaponObject.transform.localPosition = weaponDirection * weaponDistanceFromCenter;
            
            float weaponAngle = Vector2.Angle(Vector2.right, weaponDirection);
            if (weaponDirection.y < 0)
                weaponAngle = 360 - weaponAngle;
            weaponAngle = SumAngles(weaponAngle, -135f);

            weaponObject.transform.rotation = Quaternion.Euler(0f, 0f, weaponAngle);
        }
    }

    public void ChangeWeapon(GameObject newWeapon) {
        Destroy(weaponObject);
        weaponObject = Instantiate(newWeapon, transform);
    }

    public void Attack() {
        weaponObject.GetComponent<Animator>().SetTrigger("attack");
    }

    float SumAngles(params float[] angles) {
        float sum = 0;
        float result;

        for (int i = 0; i < angles.Length; i++)
            sum += angles[i];

        result = sum % 360;

        if (result < 0)
            result = 360 + result;

        return result;
    }

    protected Sprite GetSpriteFromSheet(Texture2D spriteSheet, int x, int y) {
        Rect rect = new Rect(x * spriteWidth, y * spriteHeight, spriteWidth, spriteHeight);
        Sprite spr = Sprite.Create(spriteSheet, rect, new Vector2(0.5f, 0.5f), referencePixelsPerUnit);
        return spr;
    }

}
