using UnityEngine;

public class CollectableUnlocker : SceneElement
{
    public Collectable unlockedCollectable;
    public delegate void UnlockCollectable(Collectable collectable);
    public UnlockCollectable unlockedEvent;
    public void TriggerUnlock()
    {
        if (unlockedCollectable != null)
        {
            unlockedEvent?.Invoke(unlockedCollectable);
        }
    }
}
