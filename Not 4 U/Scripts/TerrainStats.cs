using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Terrain Preset")]
public class TerrainStats : ScriptableObject{
    public bool canRespawnHere = true;
    public enum TerrainType{
        Stone,
        Grass,
        Dirt,
        Mud,
        Liquid
    }
    public TerrainType terrainType;
    public Color particleColor = Color.white;
    [Range(0.25f, 1.25f)]
    [Tooltip("How fast you'll go while moving over this terrain; if the racer is boosting, this value will not be used.")]
    public float speedModifier = 1f;
    [Range(0, 2)]
    [Tooltip("This is a control for whether a racer can boost through this to avoid speed penalty. \n0 = all boosts avoid speed penalty. \n1 = only non-drift boosts avoid speed penalty. \n2 = no boosts avoid the speed penalty.")]
    public int boostResistence = 1;
    [Range(0f, 2f)]
    [Tooltip("How much grip this terrain has. 0 is no grip, 2 is extra grippy.")]
    public float tractionModifier = 1f;
}