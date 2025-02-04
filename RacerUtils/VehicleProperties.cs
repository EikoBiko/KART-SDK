using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleProperties : MonoBehaviour
{
    [Header("FOR REFERENCE -- DO NOT ALTER")]
    public Mesh kartSeatMesh;
    public Mesh bikeSeatMesh;

    [Header("Kart Settings")]
    public Vehicle.VehicleType vehicleType;

    [Header("Locators")]
    public Renderer mainRenderer;
    public Vector3 seatPosition;
    public Animator kartAnimator;
    public Transform wheelsBone;
    public Transform chassisBone;
    public Transform driftSparks1;
    public Transform driftSmoke1;
    public Transform driftSparks2;
    public Transform driftSmoke2;
    public Transform chassisFront;
    public Transform chassisCenter;
    public Transform chassisBottom;
    public Transform chassisBack;
    public Transform[] skidPositions;
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Mesh mesh;
        if (vehicleType == Vehicle.VehicleType.Kart){
            mesh = kartSeatMesh;
        }else{
            mesh = bikeSeatMesh;
        }
        if(mesh == null){
            return;
        }
        Gizmos.DrawMesh(mesh, transform.position + seatPosition);
    }
}