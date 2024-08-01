using UnityEngine;
using UnityEditor;
using BezierSolution;
using System.Collections.Generic;

[RequireComponent(typeof(BezierSpline))]
public class SplineDistributor : MonoBehaviour
{
    private BezierSpline spline;
    public GameObject prefab;
    public Transform sceneParentObject;
    public int numberToPlace;
    public bool alignToSpline = false;
    public bool deletePreviouslyPlacedObjects = false;
    public List<GameObject> placedObjects;


    private void OnValidate()
    {
        spline = GetComponent<BezierSpline>();
    }

    public void PlaceObjectsAlongSpline(){
        // Calculate the step size based on the number of instances
        float stepSize = spline.loop ? 1f / numberToPlace : 1f / (numberToPlace - 1);

        if(deletePreviouslyPlacedObjects){
            DeletePlacedObjects();
        }

        for (int i = 0; i < numberToPlace; i++)
        {
            float t = i * stepSize; // Calculate t value
            Vector3 pointOnSpline = spline.GetPoint(t); // Get point on spline
            Quaternion newQuaternion = Quaternion.identity;
            if(alignToSpline){
                newQuaternion = Quaternion.LookRotation(spline.GetTangent(t), spline.GetNormal(t));
            }
            if(sceneParentObject != null){
                placedObjects.Add(Instantiate(prefab, pointOnSpline, newQuaternion, sceneParentObject));
            }else{
                placedObjects.Add(Instantiate(prefab, pointOnSpline, newQuaternion));
            }
            
        }
    }

    public void DeletePlacedObjects()
    {
        foreach (GameObject obj in placedObjects)
        {
            DestroyImmediate(obj);
        }
        placedObjects.Clear();
    }

    // Add your distribution logic here
}

#if UNITY_EDITOR
[CustomEditor(typeof(SplineDistributor))]
public class SplineDistributorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SplineDistributor splineDistributor = (SplineDistributor)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Place Objects Along Spline"))
        {
            splineDistributor.PlaceObjectsAlongSpline();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Delete Placed Objects"))
        {
            splineDistributor.DeletePlacedObjects();
        }
    }
}
#endif