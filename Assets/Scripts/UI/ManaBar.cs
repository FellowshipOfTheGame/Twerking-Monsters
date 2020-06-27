using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {

    RectTransform rect;

    void Start() {
        rect = GetComponent<RectTransform>();
    }

    public void SetMana(float percentage) {
        rect.offsetMax = new Vector2(Mathf.Lerp(-69, -2, percentage), -10);
    }
}
