using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "KART SDK/Music")]
public class Music : ScriptableObject
{
    [Header("Description")]
    [Tooltip("This is the audio that will be played.")]
    public AudioClip musicFile;
    [Tooltip("The name of the song.")]
    public string songTitle;
    [Tooltip("The song's author.")]
    public string artist;
    [Tooltip("If this song could get content creators in trouble, this should be false.")]
    public bool streamerFriendly = true;
    [Header("Mood")]
    [Tooltip("If enabled, will play before races.")]
    public bool preRace;
    [Tooltip("If enabled, will play after races.")]
    public bool results;
    [Header("Configuration")]
    [Tooltip("How many beats per minute this song is. For best effect in-game, ensure the song starts on a beat.")]
    public float bpm;
    [Tooltip("Enabling this allows the settings below to function; this allows you to set a zone within the song that will loop.")]
    public bool usesCustomLoop;
    [Tooltip("The point (in samples) at which the loop zone begins. Click `Show Useful Links` for more details.")]
    public int loopStart;
    [Tooltip("The point (in samples) at which the loop zone ends. Click `Show Useful Links` for more details.")]
    public int loopEnd;
}
