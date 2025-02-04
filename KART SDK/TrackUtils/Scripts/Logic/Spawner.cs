using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : SceneElement
{
    public bool inheritRotation = true;
    public GameObject spawnedObject;

    public delegate void SpawnEvent();
    public SpawnEvent spawnEvent;
    public delegate void SpawnAtEvent(Transform transform);
    public SpawnAtEvent spawnAtEvent;

    public void Spawn(){
        spawnEvent?.Invoke();
    }
    public void SpawnAt(Transform transform){
        spawnAtEvent?.Invoke(transform);
    }
}
