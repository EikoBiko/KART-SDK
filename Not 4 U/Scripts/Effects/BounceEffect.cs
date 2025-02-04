using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/Bounce")]
public class BounceEffect : Effect
{
    public enum Direction{
        DirectionReference,
        Up,
        Down
    }
    public Direction direction = Direction.Up;
    public float power = 5f;
}
