using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitbox : SceneElement
{
    public List<Hurtbox> exemptHurtboxes = new();
    public string damageTag = "world";
    public int power = 1;
    public float maxSpinoutTime = 3f;
    public UnityEvent onHitSuccess;
}
