using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Só existe para mostrar a arma como um objeto, não guarda informações sobre os ataques.
/// </summary>
public class WeaponObject : MonoBehaviour {

    public Vector2 direction;
    public float distanceFromCenter;

    protected Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        transform.localPosition = direction * distanceFromCenter;

        float weaponAngle = Vector2.Angle(Vector2.right, direction);
        if (direction.y < 0)
            weaponAngle = 360 - weaponAngle;
        weaponAngle = SumAngles(weaponAngle, -45f);

        transform.rotation = Quaternion.Euler(0f, 0f, weaponAngle);
    }
    
    public void TriggerAttack() {
        animator.SetTrigger("attack");
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

}
