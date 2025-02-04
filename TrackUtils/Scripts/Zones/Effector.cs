using System.Collections.Generic;
using UnityEngine;

public class Effector : SceneElement
{
    [Header("Effects")]
    [Tooltip("Effects put here will apply to entities that touch this.")]
    public List<Effect> effects = new List<Effect>();
    [Tooltip("You can use this to apply the effects to only specific targets.")]
    public List<Filter> filters = new List<Filter>();
    public DirectionSetter directionReference;

    private void OnDrawGizmosSelected() {
        foreach (Effect effect in effects) {
            if(effect != null){
                effect.DrawGizmos(transform, directionReference);
            }
        }
    }

    public void DescribeFunctions(){
        Debug.Log("<color=red>--- REPORT START ---");
        foreach(Effect effect in effects){
            Debug.Log("<color=orange>" + effect.ToString(), effect);
        }
        Debug.Log("<color=red>--- REPORT END ---");
    }

    public override void OnValidate() {
        base.OnValidate();
        if (directionReference == null && effects.Count > 0){
            directionReference = Instantiate(new GameObject(), transform).AddComponent<DirectionSetter>();
            directionReference.gameObject.name = "Effector Direction Reference";
        }
    }
}


public abstract class Effect : ScriptableObject
{
    public enum TriggerMode{
        Enter,
        Exit,
        Stay
    }
    public enum TriggerBehavior{
        WithinBounds,
        UponTouching
    }
    public delegate void EffectTrigger(Collider other, Effect effect, TriggerMode triggerMode, Transform origin, DirectionSetter directionSetter, List<Filter> filters);
    public EffectTrigger triggerEnter;
    public EffectTrigger triggerStay;
    public EffectTrigger triggerExit;
    public delegate void EffectCollision(Collision other, Effect effect, TriggerMode triggerMode, Transform origin, DirectionSetter directionSetter, List<Filter> filters);
    public EffectCollision collisionEnter;
    public EffectCollision collisionStay;
    public EffectCollision collisionExit;
    public void TriggerEnterEvent(Collider other, Transform origin, DirectionSetter directionSetter, List<Filter> filters){
        triggerEnter?.Invoke(other, this, TriggerMode.Enter, origin, directionSetter, filters);
    }
    public void TriggerStayEvent(Collider other, Transform origin, DirectionSetter directionSetter, List<Filter> filters){
        triggerStay?.Invoke(other, this, TriggerMode.Stay, origin, directionSetter, filters);
    }
    public void TriggerExitEvent(Collider other, Transform origin, DirectionSetter directionSetter, List<Filter> filters){
        triggerExit?.Invoke(other, this, TriggerMode.Exit, origin, directionSetter, filters);
    }
    public void CollisionEnterEvent(Collision other, Transform origin, DirectionSetter directionSetter, List<Filter> filters){
        if(triggerEnter != null){
            collisionEnter(other, this, TriggerMode.Enter, origin, directionSetter, filters);
        }
    }
    public void CollisionStayEvent(Collision other, Transform origin, DirectionSetter directionSetter, List<Filter> filters){
        if(triggerStay != null){
            collisionStay(other, this, TriggerMode.Stay, origin, directionSetter, filters);
        }
    }
    public void CollisionExitEvent(Collision other, Transform origin, DirectionSetter directionSetter, List<Filter> filters){
        if(triggerExit != null){
            collisionExit(other, this, TriggerMode.Exit, origin, directionSetter, filters);
        }
    }

    public virtual void DrawGizmos(Transform transform, DirectionSetter directionSetter){

    }

    public void DrawLabel(Vector3 position, string text, Color color){
        #if UNITY_EDITOR
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        UnityEditor.Handles.Label(position, text, style);
        #endif
    }
}

public abstract class Filter : ScriptableObject
{
    public virtual string Info(){
        return "";
    }

    public virtual string Warning(){
        return "";
    }
}

public abstract class NumericalFilter : Filter
{
    public enum PreciseOperator{
        Equals,
        AtLeast,
        LessThan
    }

    public enum ImpreciseOperator{
        AtLeast,
        LessThan
    }
}