using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    RectTransform rect;
    Player player;

    void Start() {
        rect = GetComponent<RectTransform>();
        player = (Player) FindObjectOfType<Player>();
    }

    void Update() {
        if (player != null)
            SetHealth(player.currentHealth / player.maximumHealth);
    }

    public void SetHealth(float percentage) {
        rect.offsetMax = new Vector2(Mathf.Lerp(-70, -1, percentage), -2);
    }
}
