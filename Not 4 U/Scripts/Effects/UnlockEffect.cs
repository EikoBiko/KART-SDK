using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Unlock Collectable")]
public class UnlockEffect : Effect
{
    public Collectable unlockedCollectable;

    public override string ToString()
    {
        string message = name + " will unlock " + unlockedCollectable.name + ".";
        return message;
    }
}
