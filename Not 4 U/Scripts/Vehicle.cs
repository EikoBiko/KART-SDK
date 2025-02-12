using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Racer", menuName = "KART SDK/Vehicle")]
public class Vehicle : Collectable{
    public GameObject vehiclePrefab;
    public string vehicleName;
    public enum VehicleType{
        Kart,
        Bike
    }
    public VehicleType vehicleType;
    public AudioClip engineNoise;
}