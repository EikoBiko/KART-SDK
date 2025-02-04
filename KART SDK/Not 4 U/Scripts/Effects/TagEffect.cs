using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Tag")]
public class TagEffect : Effect
{
    [Header("Display Options")]
    public bool displayOnScreen = false;
    public bool showValue = true;
    public Sprite displayIcon;
    public enum Mode{
        SetTag,
        AdjustTag,
        RemoveTag
    }
    [Header("Settings")]
    public bool addTagIfNotPresent = true;
    [Tooltip("If this is enabled, players are tagged when an item they use is tagged. For instance, if a missile is tagged, its user will be tagged too.")]
    public bool canTagByProxy = false;
    public Mode mode = Mode.SetTag;
    public string tagName = "New Tag";
    public int value = 0;
}

public class Tag{
    public bool displayOnScreen = false;
    public bool showValue = true;
    public Sprite displayIcon;
    public int value;
    public Tag(bool display, bool valueDisplay, Sprite sprite, int amt){
        displayOnScreen = display;
        showValue = valueDisplay;
        displayIcon = sprite;
        value = amt;
    }
}