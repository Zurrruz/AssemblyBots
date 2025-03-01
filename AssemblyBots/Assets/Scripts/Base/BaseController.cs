using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private ResourceScanner _resourceScanner;

    private Queue<BotController> _availableBots;
    private HashSet<Resource> assignedResources;

    private void Awake()
    {
        _availableBots = new Queue<BotController>();
        assignedResources = new HashSet<Resource>();
    }

    private void OnEnable()
    {
        _resourceScanner.DiscoveredResource += HandleResourceFound;
    }

    private void OnDisable()
    {
        _resourceScanner.DiscoveredResource -= HandleResourceFound;
    }

    private void HandleResourceFound(Resource resource)
    {
        if (assignedResources.Contains(resource) == false)
            SendBotResource(resource);
    }

    private void SendBotResource(Resource resource)
    {
        if (_availableBots.Count > 0)
        {
            BotController bot = _availableBots.Dequeue();

            assignedResources.Add(resource);

            bot.ObtainResource(resource);
        }
    }
    
    public void AddBotAvailable(BotController bot)
    {
        if (_availableBots.Contains(bot) == false)
            _availableBots.Enqueue(bot);
    }

    public void RemoveAssignedResource(Resource resource)
    {
        if (assignedResources.Contains(resource))
            assignedResources.Remove(resource);
    }
}
