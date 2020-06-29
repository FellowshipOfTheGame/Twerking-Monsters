using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {

    RectTransform rect;
    Player player;

    void Start() {
        rect = GetComponent<RectTransform>();
        player = (Player)FindObjectOfType<Player>();
    }

    void Update() {
        if (player != null)
            SetMana(player.currentMana / player.maximumMana);
    }

    public void SetMana(float percentage) {
        rect.offsetMax = new Vector2(Mathf.Lerp(-69, -2, percentage), -10);
    }
}
