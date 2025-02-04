using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KART/Track Elements/Kill Zone")]
public class KillZone : SceneElement
{
    public override string Info()
    {
        return "This component will destroy karts that enter a trigger collider. It can also trigger events when a kart is destroyed.";
    }

    public UnityEvent onKill;
    public bool onExit;

    private void OnDrawGizmosSelected() {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);    
    }
}