using System.Collections;
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

    public void CreateNewBase(Transform flag, BaseController baseController)
    {
        _baseController = baseController;

        _botMover.AssignTarget(flag.gameObject);

        StartCoroutine(VerifyDistance(flag, baseController));
    }

    public void ObtainResource(Resource resource)
    {
        _targetResource = resource;

        _botMover.AssignTarget(resource.gameObject);
    }

    public void AssignBase(BaseController baseController)
    {
        _baseController = baseController;
    }

    private void UploadResource()
    {
        if (_botResourceCollector.IsCarryingResource)
        {
            _targetResource.UploadBase();

            _botResourceCollector.DeliverResource(_targetResource);
            _baseController.RemoveAssignedResource(_targetResource);
            _baseController.AddBotAvailable(this);
            _baseController.IncreaseNumberResources();
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

    private IEnumerator VerifyDistance(Transform flag, BaseController baseController)
    {
        while(Vector3.Distance(transform.position, flag.position) > 1.5f)
        {
            yield return null;
        }

        CreateBase(flag, baseController);       

        _botMover.ResetPath();
    }

    private void CreateBase(Transform flag, BaseController baseController)
    {
        BaseController newBase = Instantiate(baseController, flag.position, Quaternion.identity);

        newBase.AddBotAvailable(this);
        _baseController = newBase;
        _baseController.DefaultBot();
    }    
}