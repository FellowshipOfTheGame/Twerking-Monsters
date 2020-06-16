using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour {

    public Entity entity;
    public List<Buff> buffList;

    void Start() {
        entity = GetComponent<Entity>();
    }

    void Update() {
        foreach (Buff buff in buffList) {
            if (!buff.Tick(entity))
                buffList.Remove(buff);
        }
    }

    public void AddBuff(Buff newBuff) {
        Buff buff = Object.Instantiate(newBuff);
        buffList.Add(buff);
    }

}
