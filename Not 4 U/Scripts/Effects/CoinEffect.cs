using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Coin")]
public class CoinEffect : Effect
{
    public enum Mode{
        Give,
        Take,
        Set
    }
    public Mode mode;

    public enum Trigger{
        Always,
        Once,
        OncePerPoint
    }
    public Trigger trigger;

    public bool destroyDroppedCoins = false;

    public int amount = 1;
}
