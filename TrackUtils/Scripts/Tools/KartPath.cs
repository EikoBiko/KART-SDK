using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using UnityEngine;

[RequireComponent(typeof(BezierSpline))]
public class KartPath : SceneElement
{
    public Road road;

    /// <summary>
    /// The spline representing this path.
    /// </summary>
    public BezierSpline pathSpline;

    /// <summary>
    /// The length of this path.
    /// </summary>
    public float approxLength;

    /// <summary>
    /// What percentage of this path is 1 unit.
    /// </summary>
    public float meterPercentage;

    /// <summary>
    /// Segments along the path that may lead to other paths.
    /// </summary>
    [SerializeField]
    public List<Branch> branches;

    [Serializable]
    public struct Branch
    {
        public Vector2 progress;
        public KartPath path;
        public int chance;
    }

    /// <summary>
    /// The path this one leads to.
    /// </summary>
    public KartPath exit;

    public float exitToPoint;

    new private void OnValidate() {
        base.OnValidate();
        pathSpline = GetComponent<BezierSpline>();
        if(pathSpline.Count > 0){
            approxLength = pathSpline.GetLengthApproximately(0, 1);
            meterPercentage = 1 / approxLength;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach(Branch branch in branches){
            if(branch.path != null){
                Vector3 startZone = pathSpline.GetPoint(branch.progress.x);
                Vector3 endZone = pathSpline.GetPoint(branch.progress.y);
                Vector3 exit = branch.path.pathSpline.GetPoint(0);
                float increments = (branch.progress.y - branch.progress.x) / 7;
                Vector3[] points = new Vector3[10]
                {
                    startZone,
                    pathSpline.GetPoint(branch.progress.x + (increments * 1)),
                    pathSpline.GetPoint(branch.progress.x + (increments * 2)),
                    pathSpline.GetPoint(branch.progress.x + (increments * 3)),
                    pathSpline.GetPoint(branch.progress.x + (increments * 4)),
                    pathSpline.GetPoint(branch.progress.x + (increments * 5)),
                    pathSpline.GetPoint(branch.progress.x + (increments * 6)),
                    pathSpline.GetPoint(branch.progress.x + (increments * 7)),
                    endZone,
                    exit
                };
                Gizmos.DrawLineStrip(points, true);

                Gizmos.DrawSphere(startZone, 0.25f);
                Gizmos.DrawSphere(endZone, 0.25f);
            }
        }

        if(exit != null){
            Gizmos.DrawLine(pathSpline.GetPoint(1), exit.pathSpline.GetPoint(exitToPoint));
        }
    }

    public KartPath CheckForBranch(float currentLocation){

        if(currentLocation > 1 && exit != null){
            return exit;
        }
        foreach (Branch branch in branches)
        {
            if(branch.progress.x <= currentLocation && branch.progress.y >= currentLocation){
                if(UnityEngine.Random.Range(0, 101) < branch.chance){
                    return branch.path;
                }
            }
        }
        return null;
    }
}
