using UnityEngine;

public class BotResourceCollector : MonoBehaviour
{
    [SerializeField] private Transform _box;

    public bool IsCarryingResource { get; private set; } = false;

    public void CollectResource(Resource resource)
    {
        resource.DisableObstacleParameter();
        resource.transform.SetParent(transform);
        resource.transform.localPosition = _box.localPosition;
        IsCarryingResource = true;
    }

    public void DeliverResource(Resource resource)
    {
        resource.transform.parent = null;
        IsCarryingResource = false;
    }
}
