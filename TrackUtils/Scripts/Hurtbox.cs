using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    public List<string> whitelistedDamageTags = new();
    public List<string> ineffectiveDamageTags = new();
    public int defense = 0;
    public FloatEvent onHit;
    public UnityEvent immediatelyAfterHit;
    public void Hit(int hitPower, string damageTag, float spinoutTime){
        if(HitSuccess(ineffectiveDamageTags, defense, damageTag, hitPower)){
            onHit.Invoke(spinoutTime);
            hit = true;
        }
    }

    private bool hit = false;

    private void LateUpdate() {
        if(hit){
            immediatelyAfterHit.Invoke();
            hit = false;
        }
    }

    public bool HitSuccess(List<string> defenderTags, int def, string attackerTag, int atk){
        // If we have a damage whitelist and it doesn't contain the tag, don't register the hit.
        if(whitelistedDamageTags.Count > 0 && !whitelistedDamageTags.Contains(attackerTag)){
            return false;
        }else if(whitelistedDamageTags.Count > 0){
            // If the whitelist does contain it, register the hit
            return true;
        }

        // If we have no defense, and the attack power is high enough, register the hit.
        if(defenderTags.Count < 1 && atk >= def){
            return true;
        }

        // If our attacker is weak, we block all damage, or our defense tags contain the tag, don't register the hit.
        if(atk < def ||  defenderTags.Contains("all") ||  defenderTags.Contains(attackerTag)){
            return false;
        }
        // If nothing blocked it, register the hit.
        return true;
    }

    [HideInInspector]
    public GameObject hurtableObject;
}

[Serializable]
public class FloatEvent : UnityEvent<float> { }