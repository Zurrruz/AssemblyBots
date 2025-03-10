using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action BaseReached;
    public event Action<Resource> ResourceReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseController _))
        {
            BaseReached?.Invoke();
        }
        
        if (other.TryGetComponent(out Resource resource))
        {
            ResourceReached?.Invoke(resource);
        }

        if(other.TryGetComponent(out FlagController flag))
        {
            flag.Deactivate();
        }
    }
}
