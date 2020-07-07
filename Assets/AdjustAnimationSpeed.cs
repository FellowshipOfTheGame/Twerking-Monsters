using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustAnimationSpeed : MonoBehaviour {

    Animator animator;
    Player player;

    void Start() {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.SetFloat("attackSpeed", 1f / player.GetAttackTimer());
    }
}
