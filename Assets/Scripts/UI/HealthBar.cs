using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    RectTransform rect;

    void Start() {
        rect = GetComponent<RectTransform>();
    }

    public void SetHealth(float percentage) {
        rect.offsetMax = new Vector2(Mathf.Lerp(-70, -1, percentage), -2);
    }
}
