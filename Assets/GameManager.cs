using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject door;

    public bool end;

    void Start() {
        end = false;
        door.SetActive(false);
    }

    void Update() {
        if (end) {
            door.SetActive(true);
            door.GetComponent<DoorController>().isOpen = true;
        }
    }

}
