using UnityEngine;

public class Looker : MonoBehaviour
{
    public Transform head; // Reference to the head bone
    public bool invertLookDirection = false; // Toggle to invert the look direction
    [HideInInspector]
    public Transform pointOfInterest; // The object or position to look at
    public bool canLook = true; // Whether the character can look
    private float lookWeight = 0f; // Blending factor (0 = Animator, 1 = Fully look at target)
    private float transitionSpeed = 2.5f; // Speed of blending transitions
    private Quaternion originalRotation; // The Animator's current rotation for the head bone

    private Vector3 lookDirection = Vector3.forward; // Current look direction

    // Constraints for rotation in degrees
    public float maxYaw = 90f; // Maximum horizontal rotation (left-right)
    public float maxPitch = 30f; // Maximum vertical rotation (up-down)
    public float maxRoll = 10f; // Maximum roll rotation (tilting head)

    void LateUpdate()
    {
        if (!head)
            return;

        originalRotation = head.rotation;

        if (pointOfInterest != null)
        {
            // Calculate direction to the point of interest
            lookDirection = pointOfInterest.position - head.position;

            // Adjust for backward head bone if necessary
            if (invertLookDirection)
            {
                lookDirection = -lookDirection;
            }
        }

        // Calculate target rotation
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, transform.up);

        // Apply constraints
        targetRotation = ApplyConstraints(originalRotation, targetRotation);

        // Blend between the original and target rotations
        Quaternion blendedRotation = Quaternion.Slerp(originalRotation, targetRotation, lookWeight);

        // Apply the blended rotation to the head
        head.rotation = blendedRotation;

        // Adjust the look weight for smooth transitions
        AdjustLookWeight();
    }

    private Quaternion ApplyConstraints(Quaternion original, Quaternion target)
    {
        // Convert rotations to local space relative to the original rotation
        Quaternion localTarget = Quaternion.Inverse(original) * target;

        // Convert the local target rotation to Euler angles
        Vector3 localEuler = localTarget.eulerAngles;

        // Normalize angles to the range [-180, 180]
        localEuler.x = Mathf.DeltaAngle(0, localEuler.x);
        localEuler.y = Mathf.DeltaAngle(0, localEuler.y);
        localEuler.z = Mathf.DeltaAngle(0, localEuler.z);

        // Clamp the rotation angles to the constraints
        localEuler.x = Mathf.Clamp(localEuler.x, -maxPitch, maxPitch);
        localEuler.y = Mathf.Clamp(localEuler.y, -maxYaw, maxYaw);
        localEuler.z = Mathf.Clamp(localEuler.z, -maxRoll, maxRoll);

        // Convert back to a constrained local rotation
        localTarget = Quaternion.Euler(localEuler);

        // Convert back to world space
        return original * localTarget;
    }

    public void EnableLooking()
    {
        canLook = true;
    }

    public void DisableLooking()
    {
        canLook = false;
    }

    public void SetPointOfInterest(Transform newPOI)
    {
        pointOfInterest = newPOI;
    }

    private void AdjustLookWeight()
    {
        float targetWeight = (pointOfInterest != null) && canLook ? 1f : 0f;
        lookWeight = Mathf.MoveTowards(lookWeight, targetWeight, transitionSpeed * Time.deltaTime);
    }
}
