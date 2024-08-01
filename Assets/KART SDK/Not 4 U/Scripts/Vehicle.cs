using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Racer", menuName = "KART SDK/Vehicle")]
public class Vehicle : ScriptableObject{
    public GameObject vehicle;
    public string vehicleName;
    public enum VehicleType{
        Kart,
        Bike
    }
    public VehicleType vehicleType;
}