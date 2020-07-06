using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "skll/shield/bow")]
public class ShieldBow : BaseSkill {

    public GameObject wall;
    public float time;

    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        float rotation = Vector2.SignedAngle(Vector2.up, direction);

        GameObject wallinst = Instantiate(wall, parent.position + (Vector3) direction, Quaternion.Euler(0f, 0f, rotation));
        Destroy(wallinst, time);
    }
}
