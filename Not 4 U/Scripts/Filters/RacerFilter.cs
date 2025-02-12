using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "KART SDK/Filters/Racer")]
public class RacerFilter : Filter
{
    [Tooltip("If NonRacers is selected, anything that isn't a racer will fail to pass the filter. \nIf Racers is selected, all racers will fail to pass the filter.")]
    public BlockType blocking;

    [Tooltip("This is ignored if blocking is set to Racers. \nIf set to LocalPlayer, it only players who playing on the system can pass the filter. \nIf set to NonLocalPlayer, only CPU and networked players can pass through.")]
    public RacerType racerType;

    public enum RacerType{
        LocalPlayer,
        NonLocalPlayer
    }

    public enum BlockType{
        NonRacers,
        Racers
    }


}
