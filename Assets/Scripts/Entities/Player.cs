using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    // Equipment
    [Space]
    [Header("Equipment")]
    public Weapon weapon;
    public Armor armor;
    public ClassItem classItem;

    public GameObject temp;

    // Components
    Animator playerAnimator;
    Rigidbody2D playerRigidbody2D;
    CharacterAnimation characterAnimation;

    // timers n stuff
    protected float attackBuffer;

    new void Start() {
        base.Start();

        playerAnimator = gameObject.GetComponent<Animator>();
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        characterAnimation = gameObject.GetComponent<CharacterAnimation>();

        characterAnimation.ChangeWeapon(weapon.weaponObject);
    }

    new void Update() {
        base.Update();

        HandleMovement();
        HandleAttack();
        
        if (temp != null && Input.GetKeyDown(KeyCode.E)) {
            ChangeItem(temp.GetComponent<CollectableItem>().item);
            Destroy(temp);
        }
    }

    public void ChangeItem(Item item) {
        if (item is Weapon) {
            weapon = item as Weapon;
            characterAnimation.ChangeWeapon(((Weapon)item).weaponObject);
        } else if (item is ClassItem) {
            classItem = item as ClassItem;
            switch (((ClassItem)item).statModifier) {
                case Stat.HEALTH:
                    healthModifier = ((ClassItem)item).statModifierValue;
                    manaModifier = 0f;
                    speedModifier = 0f;
                    armorModifier = 0f;
                    break;
                case Stat.MANA:
                    healthModifier = 0f;
                    manaModifier = ((ClassItem)item).statModifierValue;
                    speedModifier = 0f;
                    armorModifier = 0f;
                    break;
                case Stat.SPEED:
                    healthModifier = 0f;
                    manaModifier = 0f;
                    speedModifier = ((ClassItem)item).statModifierValue;
                    armorModifier = 0f;
                    break;
                case Stat.ARMOR:
                    healthModifier = 0f;
                    manaModifier = 0f;
                    speedModifier = 0f;
                    armorModifier = ((ClassItem)item).statModifierValue;
                    break;
            }
        } else if (item is Armor) {
            // set stats
            baseArmor = ((Armor)item).defense;
            baseSpeed = ((Armor)item).speedModifier;
            baseHealthRegeneration = ((Armor)item).healthRegen;
            baseManaRegeneration = ((Armor)item).manaRegen;

            // change sheet
            characterAnimation.armorSheet = ((Armor)item).appearance;
        }
    }

    void HandleAttack() {
        Vector2 mouseWolrdPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = new Vector2(mouseWolrdPos.x - transform.position.x, mouseWolrdPos.y - transform.position.y).normalized;
        characterAnimation.weaponDirection = mouseDirection;

        if (Input.GetButton("Fire1") && attackBuffer == 0f && weapon != null) {

            characterAnimation.Attack();
            weapon.Attack(transform, mouseDirection.normalized, new ContactFilter2D() { useLayerMask = true, layerMask = 1 << 9 }, (classItem ? classItem.name.Equals("Grimoire") : false));
            attackBuffer = GetAttackTimer();
        }

        if (Input.GetButton("Fire2") && attackBuffer == 0f && classItem != null) {
            classItem.TriggerSkill(weapon.weaponType, transform, mouseDirection.normalized, new ContactFilter2D() { useLayerMask = true, layerMask = 1 << 9 });
            attackBuffer = GetAttackTimer();
        }

        if (attackBuffer > 0f)
            attackBuffer = Mathf.Clamp(attackBuffer - Time.deltaTime, 0f, attackBuffer);
    }

    void HandleMovement() {
        float dirHorizontal = Input.GetAxisRaw("Horizontal");
        float dirVertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(dirHorizontal, dirVertical).normalized;

        playerAnimator.SetFloat("horizontalVelocity", dirHorizontal);
        playerAnimator.SetFloat("verticalVelocity", dirVertical);

        playerRigidbody2D.velocity = direction * GetMoveSpeed();
    }
}
