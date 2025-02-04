using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerrainModifier : Effector
{

    public override void OnValidate() {
        gameObject.tag = "Loader Element Terrain";
        if(info == ""){
            info = Info();
        }
        if (directionReference == null && effects.Count > 0){
            directionReference = Instantiate(new GameObject(), transform).AddComponent<DirectionSetter>();
            directionReference.gameObject.name = "Terrain Effect Direction Reference";
        }
        if(effects.Count > 0){
            mode = GroundTriggerMode.All;
        }
    }

    [Header("Terrain Stats")]
    [Tooltip("This is how much your speed will be adjusted while on this terrain.")]
    public TerrainStats statsPreset;

    private List<Collider> currentEntities = new List<Collider>();
    private List<Collider> previousEntities = new List<Collider>();
    public enum GroundTriggerMode{
        OnlyOne,
        All
    }
    [Header("Triggers")]
    public GroundTriggerMode mode;
    public bool kartsOnly;
    public UnityEvent onEnter;
    public UnityEvent onStay;
    public UnityEvent onExit;

    private void Start() {
        StartCoroutine(FixedUpdateWaiter());
    }

    public TerrainStats DeclarePresence(Collider entity){
        return DeclarePresence(entity, false);
    }


    /// <summary>
    /// This is called by other scripts on Update whenever passing over this terrain; all it does is add itself to the list.
    /// </summary>
    public TerrainStats DeclarePresence(Collider entity, bool isKart)
    {
        if(!kartsOnly){
            if (!currentEntities.Contains(entity)){
                currentEntities.Add(entity);
            }
        }else if(isKart){
            if (!currentEntities.Contains(entity)){
                currentEntities.Add(entity);
            }
        }
        return statsPreset;
    }

    public delegate void EffectTrigger(Collider other, Transform origin, DirectionSetter directionSetter, List<Filter> filters);
    public EffectTrigger triggerEnter;
    public EffectTrigger triggerStay;
    public EffectTrigger triggerExit;

    public virtual void EntityArrived(Collider entity)
    {
        onEnter.Invoke();
        if(effects.Count > 0 && entity != null){
            triggerEnter?.Invoke(entity, transform, directionReference, filters);
        }
    }
    public virtual void EntityStayed(Collider entity)
    {
        onStay.Invoke();
        if(effects.Count > 0 && entity != null){
            triggerStay?.Invoke(entity, transform, directionReference, filters);
        }
    }

    public virtual void EntityLeft(Collider entity)
    {
        onExit.Invoke();
        if (effects.Count > 0 && entity != null){
            triggerExit?.Invoke(entity, transform, directionReference, filters);
        }
    }

    IEnumerator FixedUpdateWaiter(){
        yield return new WaitForFixedUpdate();
        // Check for changes in entity presence
        if(mode == GroundTriggerMode.OnlyOne){
            OnlyOneMode();
        }else if(mode == GroundTriggerMode.All){
            AllMode();
        }
        StartCoroutine(FixedUpdateWaiter());
    }

    /// <summary>
    /// Triggers arrival and leave functions each time an entity arrives or leaves.
    /// </summary>
    private void AllMode()
    {
        // Check for entities that arrived
        foreach (Collider entity in currentEntities)
        {
            if (!previousEntities.Contains(entity))
            {
                EntityArrived(entity);
            }
        }

        // Check for entities that stayed
        foreach(Collider entity in previousEntities){
            if(currentEntities.Contains(entity)){
                EntityStayed(entity);
            }
        }

        // Check for entities that left
        foreach(Collider entity in previousEntities)
        {
            if (!currentEntities.Contains(entity))
            {
                EntityLeft(entity);
            }
        }

        // Update previousEntities list for next frame
        previousEntities.Clear();
        previousEntities.AddRange(currentEntities);
        currentEntities.Clear();
    }

    /// <summary>
    /// Triggers arrival and leave functions when one entity arrives, or all entities have left.
    /// </summary>
    void OnlyOneMode(){
        if(currentEntities.Count > 0 && previousEntities.Count == 0){
            EntityArrived(null);
        }

        if(previousEntities.Count > 0 && currentEntities.Count > 0){
            EntityStayed(null);
        }

        if(currentEntities.Count == 0 && previousEntities.Count > 0){
            EntityLeft(null);
        }

        // Update previousTrackEntities list for next frame
        previousEntities.Clear();
        previousEntities.AddRange(currentEntities);
        currentEntities.Clear();
    }

}