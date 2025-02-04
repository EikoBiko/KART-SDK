using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Effects/ScreenShake")]
public class ScreenShakeEffect : Effect
{
    public enum ShakeMode{
        Zone,
        Time
    }
    public ShakeMode shakeMode = ShakeMode.Zone;
    public float timeShakeDuration = 0;
    [Range(0f, 1f)]
    public float shakeIntensity = 0.2f;
}
