using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Filters/Coin")]
public class CoinFilter : NumericalFilter
{
    [Range(0,10)]
    public int coins = 5;
    public PreciseOperator operation;
}
