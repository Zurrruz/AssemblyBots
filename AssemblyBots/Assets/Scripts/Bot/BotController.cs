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

    private IEnumerator VerifyDistance(FlagController flag, BaseController baseController)
    {
        while(Vector3.Distance(transform.position, flag.transform.position) > 3f)
        {
            yield return null;
        }

        _botMover.ResetPath();

        CreateBase(flag, baseController);       
    }

    private void CreateBase(FlagController flag, BaseController baseController)
    {
        var newBase = Instantiate(baseController, flag.transform.position, Quaternion.identity);

        newBase.AddBotAvailable(this);
        _baseController = newBase;
        _baseController.DefaultBot();

        Destroy(flag.gameObject);
    }

    public void CreateNewBase(FlagController flag, BaseController baseController)
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
}