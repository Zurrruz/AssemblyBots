using UnityEngine;

public class BotResourceCollector : MonoBehaviour
{
    [SerializeField] private Transform _box;

    private bool _isCarryingResource = false;    

    public bool IsCarryingResource => _isCarryingResource;

    public void CollectResource(Resource resource)
    {
        resource.transform.SetParent(transform);
        resource.transform.localPosition = _box.localPosition;
        resource.ChangesLayer();
        _isCarryingResource = true;
    }

    public void DeliverResource(Resource resource)
    {
        resource.transform.parent = null;
        resource.UploadBase();
        _isCarryingResource = false;
    }
}
