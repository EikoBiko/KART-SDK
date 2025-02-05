using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Track", menuName = "KART SDK/Track")]
public class Track : ScriptableObject
{
    [Header("Assets")]
    [Tooltip("This is the display name of your track!")]
    public GameObject trackPrefab;
    [Tooltip("This is the skybox that will be applied when the track is loaded.")]
    public Material skyboxMaterial;

    [Header("Track Selection")]
    [Tooltip("This is the display name of your track!")]
    public string trackTitle = "Custom Track";
    [Tooltip("What world is your track from?")]
    public string trackOrigin = "Unknown Origin";
    [Tooltip("This is the image used in the track selection screen.")]
    public Texture2D trackPreview;
    public enum GameMode{
        Race,
        Battle
    }
    [Tooltip("This is the intended play mode for the track.")]
    public GameMode gameMode;

    [Header("Game Settings")]
    [Tooltip("For races, this indicates how many laps are needed to complete the race. For battles, this is how many hits you can take.")]
    public int pointsRequired = 3;
    [Tooltip("This is the music setting for this track. This can be created in right click > Create > KART SDK > Music Setting.")]
    public Music musicSetting;
    [Tooltip("For the minimap; this color represents higher elevation.")]
    public Color mapColorHigh = Color.white;
    [Tooltip("For the minimap; this color represents lower elevation.")]
    public Color mapColorLow = Color.gray;

    [Header("Race Settings")]
    [Tooltip("The time needed to earn a bronze medal; measured in minutes, seconds, and milliseconds.")]
    public Vector3 bronzeTime;
    [Tooltip("The time needed to earn a silver medal; measured in minutes, seconds, and milliseconds.")]
    public Vector3 silverTime;
    [Tooltip("The time needed to earn a gold medal; measured in minutes, seconds, and milliseconds.")]
    public Vector3 goldTime;
}
