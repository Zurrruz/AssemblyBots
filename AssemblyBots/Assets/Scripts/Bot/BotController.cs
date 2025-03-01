using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private BaseController _baseController;
    [SerializeField] private BotMover _botMover;
    [SerializeField] private BotResourceCollector _botResourceCollector;
    [SerializeField] private CollisionHandler _collisionHandler; 

    private Resource _targetResource;

    private void Start()
    {
        _baseController.AddBotAvailable(this);
    }

    private void OnEnable()
    {
        _collisionHandler.ResourceReached += CollectResource;
        _collisionHandler.BaseReached += UploadResource;
    }

    private void OnDisable()
    {
        _collisionHandler.ResourceReached -= CollectResource;
        _collisionHandler.BaseReached -= UploadResource;
    }
    
    private void UploadResource()
    {
        if (_botResourceCollector.IsCarryingResource)
        {
            _botResourceCollector.DeliverResource(_targetResource);
            _baseController.RemoveAssignedResource(_targetResource);
            _botMover.MoveStop();
            _baseController.AddBotAvailable(this);
        }
    }

    private void CollectResource(Resource resource)
    {
        if (resource == _targetResource)
        {
            _botResourceCollector.CollectResource(_targetResource);

            _botMover.AssignTarget(_baseController.gameObject);
        }
    }

    public void ObtainResource(Resource resource)
    {
        _targetResource = resource;

        _botMover.AssignTarget(resource.gameObject);
    }
}