using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "skll/cape/sword")]
public class CapeSword : BaseSkill {

    public float distance;
    public float time;

    protected override void OnTrigger(Transform parent, Vector2 direction, LayerMask layerMask) {
        Player player = parent.GetComponent<Player>();
        player.GetComponent<Rigidbody2D>().velocity = distance * direction;
        player.StartCoroutine(Stop(time, player));
    }

    IEnumerator Stop(float time, Player player) {
        yield return new WaitForSeconds(time);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    
}
