using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mod", menuName = "KART SDK/Mod")]
public class Mod : ScriptableObject
{
    [Tooltip("The name of the mod.")]
    public string modName;
    [Tooltip("The author of the mod. (That's you!)")]
    public string modAuthor;

    public List<UnlockableRacer> racers = new List<UnlockableRacer>();
    public List<UnlockableVehicle> vehicles = new List<UnlockableVehicle>();
    public List<UnlockableTrack> tracks = new List<UnlockableTrack>();
    public List<UnlockableHorn> horns = new List<UnlockableHorn>();
    public List<UnlockableFigurine> figurines = new List<UnlockableFigurine>();

    public override string ToString()
    {
        return $"{modName} by {modAuthor}";
    }

    public bool CheckModValidity(){
        if(!RacersValid() || !VehiclesValid() || !TracksValid() || !FigurinesValid()){
            return false;
        }
        return true;
    }

    public bool RacersValid(){
        foreach(UnlockableRacer unlockableRacer in racers){
            if(!IsRacerValid(unlockableRacer)){
                return false;
            }
        }
        return true;
    }

    public bool IsRacerValid(UnlockableRacer unlockableRacer){
        Racer racer = unlockableRacer.racer;
        if (racer.icon == null)
        {
            GiveError(racer.name + " has no icon!", racer);
            return false;
        }
        if (racer.racerName == "")
        {
            GiveError(racer.name + " has no name!", racer);
            return false;
        }
        if (racer.racerPrefab == null)
        {
            GiveError(racer.name + " has no racer prefab!", racer);
            return false;
        }
        CharacterProperties properties = racer.racerPrefab.GetComponent<CharacterProperties>();
        if (properties == null)
        {
            GiveError(racer.name + " has no CharacterProperties script attached to their prefab!", racer.racerPrefab);
            return false;
        }
        if (properties.characterAnimator == null)
        {
            GiveError(racer.name + " has no animator attached to their CharacterProperties!", racer.racerPrefab);
            return false;
        }
        return true;
    }

    public bool VehiclesValid(){
        foreach(UnlockableVehicle unlockableVehicle in vehicles){
            if(!IsVehicleValid(unlockableVehicle)){
                return false;
            }
        }
        return true;
    }

    public bool IsVehicleValid(UnlockableVehicle unlockableVehicle){
        Vehicle vehicle = unlockableVehicle.vehicle;
        if (vehicle.vehicleName == "")
        {
            GiveError(vehicle.name + " has no name!", vehicle);
            return false;
        }
        if (vehicle.vehiclePrefab == null)
        {
            GiveError(vehicle.name + " has no vehicle prefab!", vehicle);
            return false;
        }
        VehicleProperties properties = vehicle.vehiclePrefab.GetComponent<VehicleProperties>();
        if (properties == null)
        {
            GiveError(vehicle.name + " has no VehicleProperties!", vehicle.vehiclePrefab);
            return false;
        }
        if (
            properties.wheelsBone == null ||
            properties.chassisBone == null ||
            properties.driftSparks1 == null ||
            properties.driftSparks2 == null ||
            properties.driftSmoke1 == null ||
            properties.driftSmoke2 == null ||
            properties.chassisFront == null ||
            properties.chassisCenter == null ||
            properties.chassisBack == null ||
            properties.chassisBottom == null
        )
        {
            GiveError(vehicle.name + " is missing locators!", vehicle.vehiclePrefab);
            return false;
        }
        if (properties.kartAnimator == null)
        {
            GiveError(vehicle.name + " is missing an animator in its VehicleProperties!", vehicle.vehiclePrefab);
            return false;
        }
        return true;
    }

    public bool TracksValid(){
        foreach(UnlockableTrack unlockableTrack in tracks){
            if(!IsTrackValid(unlockableTrack)){
                return false;
            }
        }
        return true;
    }

    public bool IsTrackValid(UnlockableTrack unlockableTrack){
        Track track = unlockableTrack.track;
        if (track.trackTitle == "")
        {
            GiveError(track.name + " is missing a name!", track);
            return false;
        }
        if (track.trackPrefab == null)
        {
            GiveError(track.name + " has no track prefab!", track);
            return false;
        }
        StartingLine startingLine = track.trackPrefab.GetComponentInChildren<StartingLine>();
        if (startingLine == null)
        {
            GiveError(track.name + " has no Starting Line within its track prefab!", track.trackPrefab);
            return false;
        }
        LapPoint lapPoint = track.trackPrefab.GetComponentInChildren<LapPoint>();
        if (track.gameMode == Track.GameMode.Race && lapPoint == null)
        {
            GiveError(track.name + " is a race track, but has no lap markers!", track.trackPrefab);
            return false;
        }
        return true;
    }

    public bool FigurinesValid(){
        foreach(UnlockableFigurine unlockableFigurine in figurines){
            if(!IsFigurineValid(unlockableFigurine)){
                return false;
            }
        }
        return true;
    }

    public bool IsFigurineValid(UnlockableFigurine unlockableFigure){
        Figurine figurine = unlockableFigure.figurine;
        if (figurine.figurinePrefab == null)
        {
            GiveError(figurine.name + " is missing a prefab!", figurine);
            return false;
        }
        if (figurine.figurineName == "")
        {
            GiveError(figurine.name + " is missing a name!", figurine);
            return false;
        }
        if (figurine.figurineDescription == "")
        {
            GiveError(figurine.name + " is missing a description!", figurine);
            return false;
        }
        return true;
    }

    public bool HornsValid(){
        foreach(UnlockableHorn unlockableHorn in horns){
            if(!IsHornValid(unlockableHorn)){
                return false;
            }
        }
        return true;
    }

    public bool IsHornValid(UnlockableHorn unlockableHorn){
        Horn horn = unlockableHorn.horn;
        if (horn.hornName == "")
        {
            GiveError(horn.name + " is missing a name!", horn);
            return false;
        }
        if (horn.audio == null)
        {
            GiveError(horn.name + " is missing an audio file!", horn);
            return false;
        }
        return true;
    }

    public void GiveError(string errorMessage, Object problem){
        Debug.Log("<color=red>" + modName + " has an issue: </color>" + errorMessage, problem);
    }
}

[System.Serializable]
public abstract class Unlockable{
    [Tooltip("If this is toggled, the content will be unlocked the moment it's added to the player's game.")]
    public bool unlockedByDefault = false;
    [Tooltip("If this is toggled, this content can be unlocked from random gacha capsules.")]
    public bool randomUnlockable = true;
    [Tooltip("If this is toggled, this content will always be hidden from the player until unlocked.")]
    public bool secretUnlockable = false;
    [Tooltip("Enter text here to make this content unlockable via a password. If left blank, it cannot be unlocked this way.")]
    public string unlockPassword = "";
    [HideInInspector]
    public Hash128 itemID = new Hash128();
    public abstract void GenerateID();
}

[System.Serializable]
public class UnlockableRacer : Unlockable {
    public Racer racer;
    public override void GenerateID()
    {
        if(itemID != new Hash128()){
            return;
        }else{
            string textconversion = JsonUtility.ToJson(racer) + System.DateTime.Now.ToString();
            itemID = Hash128.Compute(textconversion);
        }
    }
    public override string ToString()
    {
        if(racer == null){
            return "INVALID RACER -- NO DATA FOUND";
        }else{
            return racer.racerName;
        }
    }
}

[System.Serializable]
public class UnlockableVehicle : Unlockable {
    public Vehicle vehicle;
    public override void GenerateID()
    {
        if(itemID != new Hash128()){
            return;
        }else{
            string textconversion = JsonUtility.ToJson(vehicle) + System.DateTime.Now.ToString();
            itemID = Hash128.Compute(textconversion);
        }
    }
    public override string ToString()
    {
        if(vehicle == null){
            return "INVALID VEHICLE -- NO DATA FOUND";
        }else{
            return vehicle.vehicleName;
        }
    }
}

[System.Serializable]
public class UnlockableTrack : Unlockable {
    public Track track;
    public override void GenerateID()
    {
        if(itemID != new Hash128()){
            return;
        }else{
            string textconversion = JsonUtility.ToJson(track) + System.DateTime.Now.ToString();
            itemID = Hash128.Compute(textconversion);
        }
    }
    public override string ToString()
    {
        if(track == null){
            return "INVALID TRACK -- NO DATA FOUND";
        }else{
            return track.trackTitle;
        }
    }
}

[System.Serializable]
public class UnlockableHorn : Unlockable {
    public Horn horn;
    public override void GenerateID()
    {
        if(itemID != new Hash128()){
            return;
        }else{
            string textconversion = JsonUtility.ToJson(horn) + System.DateTime.Now.ToString();
            itemID = Hash128.Compute(textconversion);
        }
    }
    public override string ToString()
    {
        if(horn == null){
            return "INVALID HORN -- NO DATA FOUND";
        }else{
            return horn.hornName;
        }
    }
}

[System.Serializable]
public class UnlockableFigurine : Unlockable {
    public Figurine figurine;
    public override void GenerateID()
    {
        if(itemID != new Hash128()){
            return;
        }else{
            string textconversion = JsonUtility.ToJson(figurine) + System.DateTime.Now.ToString();
            itemID = Hash128.Compute(textconversion);
        }
    }
}