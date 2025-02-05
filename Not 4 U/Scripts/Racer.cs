using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Racer", menuName = "KART SDK/Racer")]
public class Racer : ScriptableObject
{
    [Tooltip("This is the actual model of the racer, with animations and such.")]
    public GameObject racerPrefab;
    [Tooltip("This is what the racer will be called in-game.")]
    public string racerName;
    [Tooltip("This is what \"franchise\" the character is from. Be creative!")]
    public string racerOrigin;
    [Tooltip("This a short backstory of the character. Character limit is 500.")]
    [TextArea(3,5)]
    public string racerBackstory;
    [Tooltip("This is the icon used to represent the racer in menus and the map.")]
    public Icon icon;
    [Tooltip("These are the default colors for this character. This is the main color.")]
    public Color defaultPrimaryColor = Color.red;
    [Tooltip("These are the default colors for this character. This is the secondary color, typically for secondary details.")]
    public Color defaultSecondaryColor = Color.white;
    public Texture2D adSheet;
}