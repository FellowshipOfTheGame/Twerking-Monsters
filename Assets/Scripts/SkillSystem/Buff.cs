using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Buff : ScriptableObject {

    public float activeTime;
    [HideInInspector] public float currTime = 0f;

    protected abstract void OnFirstTick(Entity entity);
    protected abstract void OnTick(Entity entity);
    protected abstract void OnDestroy(Entity entity);

    public bool Tick(Entity entity) {
        if (currTime == 0f) {
            OnFirstTick(entity);
            currTime += Time.deltaTime;
            return true;
        }
        
        if (currTime >= activeTime) {
            OnDestroy(entity);
            return false;
        }
        
        OnTick(entity);
        currTime += Time.deltaTime;
        return true;
    }

}
