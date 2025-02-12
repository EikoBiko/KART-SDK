using UnityEngine;
public class Collectable : ScriptableObject
{
    [HideInInspector]
    public Hash128 unlockableID = new();
}