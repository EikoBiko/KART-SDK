using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using UnityEngine;

public class Road : SceneElement
{
    public bool startingRoad;
    public List<KartPath> paths;
    public Road defaultExit;

    new private void OnValidate() {
        base.OnValidate();
        foreach(KartPath path in paths){
            path.road = this;
        }
    }
}
