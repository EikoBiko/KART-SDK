using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KART SDK/Filters/Tag")]
public class TagFilter : NumericalFilter
{
    public override string Info()
    {
        return "This will apply effects to entities that have recieved tags that match the parameters below. You can tag entities with a Tag Effect.";
    }
    public string tagName;
    public enum FilterCheckMethod{
        TagExists,
        TagDoesNotExist,
        TagValue
    }
    [Tooltip("How the tag will be checked.")]
    public FilterCheckMethod checkMethod = FilterCheckMethod.TagExists;
    [Tooltip("If the check method if set to Tag Value, it will be compared this way. This is unused if not checking Tag Value. If the tag doesn't exist, it's the same as the tag value being equal to 0.")]
    public PreciseOperator valueCheck = PreciseOperator.Equals;
    public int value = 1;
}
