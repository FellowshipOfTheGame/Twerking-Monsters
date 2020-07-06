using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class CharacterAppearance : MonoBehaviour {

    [Header("Stuff")]
    public Texture2D skin;
    public Texture2D armor;

    [Header("Sprite info")]
    public int referencePixelsPerUnit;
    public int spriteWidth;
    public int spriteHeight;

    public GameObject armorObject;
    public GameObject weaponObject;

    [Header("Position in sheet")]
    [Tooltip("Y position, bottom to top")]
    public int pose;
    [Tooltip("X position, left to right")]
    public int frame;

    // Components
    protected SpriteRenderer spriteRenderer;
    protected SpriteRenderer armorSpriteRenderer;
    protected Animator weaponAnimator;

    public Vector2 weaponDirection;
    public Vector2 weaponUpDirection;
    public float weaponDistanceFromCenter;

    public GameObject defaultWeapon;

    // MonoBehaviour lifecycle
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (armorObject)
            armorSpriteRenderer = armorObject.GetComponent<SpriteRenderer>();

        if (defaultWeapon)
            ChangeWeapon(defaultWeapon);
    }

    void Update() {
        spriteRenderer.sprite = GetSpriteFromSheet(skin, frame, pose, spriteWidth, spriteHeight);

        if (weaponObject) {
            weaponObject.transform.localPosition = weaponDirection * weaponDistanceFromCenter;
            weaponObject.transform.localRotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(weaponUpDirection, weaponDirection));
        }

        if (armor)
            armorSpriteRenderer.sprite = GetSpriteFromSheet(armor, frame, pose, spriteWidth, spriteHeight);
    }

    // Other functions
    public void ChangeWeapon(GameObject newWeapon) {
        Destroy(weaponObject);
        weaponObject = Instantiate(newWeapon, transform);
        weaponAnimator = weaponObject.GetComponent<Animator>();
    }

    protected Sprite GetSpriteFromSheet(Texture2D sheet, int x, int y, int width, int height) {
        Rect rect = new Rect(x * width, y * height, width, height);
        Sprite sprite = Sprite.Create(sheet, rect, new Vector2(0.5f / (width / 32f), 0.5f), referencePixelsPerUnit);
        return sprite;
    }

}
