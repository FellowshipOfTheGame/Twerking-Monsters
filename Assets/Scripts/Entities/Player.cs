using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : Entity {

    protected Animator animator;
    protected new Rigidbody2D rigidbody2D;
    protected CharacterAppearance characterAppearance;

    [Header("Inventário")]
    public ItemWeapon weapon;
    protected float mainAttackCooldown;

    public ItemClass classItem;
    protected float specialAttackCooldown;

    public ItemArmor armor;

    public GameObject temp;

    // MonoBehaviour lifecycle
    new protected void Start() {
        base.Start();

        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        characterAppearance = GetComponent<CharacterAppearance>();

        mainAttackCooldown = 0f;
    }

    new protected void Update() {
        base.Update();

        Vector2 mouseWolrdPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = new Vector2(mouseWolrdPos.x - transform.position.x, mouseWolrdPos.y - transform.position.y).normalized;

        characterAppearance.weaponDirection = mouseDirection;

        if (Input.GetButtonDown("Fire1") && mainAttackCooldown <= 0f) {
            characterAppearance.weaponObject.GetComponent<Animator>().SetTrigger("attack");
            //mainAttackCooldown = GetAttackTimer();
            //weapon.Attack(transform, mouseDirection, 1 << 9);
        }

        if (Input.GetButtonDown("Fire2") && specialAttackCooldown <= 0f) {
            classItem.TriggerSkill(weapon.type, transform, mouseDirection, 1 << 9);
        }

        HandleMovement();

        if (mainAttackCooldown > 0f)
            mainAttackCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (temp != null) {
                ChangeItem(temp.GetComponent<ObjectItem>().item);
                Destroy(temp);
            }

            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item")) {
                Destroy(item);
            }
        }
    }

    public void ChangeItem(BaseItem item) {
        if (item is ItemWeapon) {
            weapon = item as ItemWeapon;
            characterAppearance.ChangeWeapon(weapon.weaponObject);
        } else if (item is ItemArmor) {
            armor = item as ItemArmor;
            characterAppearance.armor = armor.appearance;
        } else if (item is ItemClass) {

        }
    }

    // Other functions
    protected void HandleMovement() {
        float hDir = Input.GetAxisRaw("Horizontal");
        float vDir = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontalSpeed", hDir);
        animator.SetFloat("verticalSpeed", vDir);

        Vector2 dir = new Vector2(hDir, vDir).normalized;
        rigidbody2D.velocity = dir * GetMoveSpeed();
    }

}
