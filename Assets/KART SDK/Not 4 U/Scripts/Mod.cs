using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mod", menuName = "KART SDK/Mod")]
public class Mod : ScriptableObject
{
    [Tooltip("The name of the mod.")]
    public string modName;
    [Tooltip("The version of the mod. Please update this when you make a change to your mod; it is used to confirm players are using the same version in multiplayer. This doesn't have to be numbers.")]
    public string modVersion;
    [Tooltip("The author of the mod. (That's you!)")]
    public string modAuthor;

    public List<UnlockableRacer> racers = new List<UnlockableRacer>();
    public List<UnlockableTrack> tracks = new List<UnlockableTrack>();
    public List<UnlockableHorn> horns = new List<UnlockableHorn>();

    public override string ToString()
    {
        return $"{modName}-{modVersion}-{modAuthor}";
    }
}

[System.Serializable]
public class UnlockableRacer {
    public Racer racer;
    public bool unlockedByDefault;
}

[System.Serializable]
public class UnlockableTrack {
    public Track track;
    public bool unlockedByDefault = true;
}

[System.Serializable]
public class UnlockableHorn {
    public Horn horn;
    public bool unlockedByDefault;
}