using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private BaseController _basePrefab;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private BotGenerator _botGenerator;
    [SerializeField] private FlagInstaller _flaInstaller;

    [SerializeField] private Renderer _renderer;

    private int _resourcesRequired = 5;
    private int _resourceCount = 0;
    private int _botCount = 3;
    private int _minNumberBot = 1;

    private Coroutine _currentCoroutine;

    private Queue<BotController> _availableBots;
    private HashSet<Resource> assignedResources;

    public bool IsBuildingNewBase { get; private set; } = false;

    public event Action<int> ResourceChanged;

    private void Awake()
    {
        _availableBots = new Queue<BotController>();
        assignedResources = new HashSet<Resource>();
    }

    private void OnEnable()
    {
        _resourceScanner.DiscoveredResource += HandleResourceFound;
        _botGenerator.Created += ExpendResource;
    }

    private void OnDisable()
    {
        _resourceScanner.DiscoveredResource -= HandleResourceFound;
        _botGenerator.Created -= ExpendResource;
    }      

    public void PlaceFlag(Vector3 position)
    {
        _flaInstaller.Create(position);

        IsBuildingNewBase = true;

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(CreateNewBase());
    }

    public bool CanPlaceFlag()
    {
        return _botCount > _minNumberBot;
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

    public void IncreaseNumberResources()
    {
        _resourceCount++;

        ResourceChanged?.Invoke(_resourceCount);
    }

    public void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void DefaultBot()
    {
        _botCount = 1;
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

    private void ExpendResource(int numberResource)
    {
        _resourceCount -= numberResource;

        _botCount++;

        ResourceChanged?.Invoke(_resourceCount);
    }

    private IEnumerator CreateNewBase()
    {
        while (IsBuildingNewBase == true)
        {
            if (_resourceCount >= _resourcesRequired && _botCount > 1)
            {
                if (_availableBots.Count > 0)
                {
                    IsBuildingNewBase = false;

                    SendUnitBuildNewBase();

                    _resourceCount -= _resourcesRequired;
                    ResourceChanged?.Invoke(_resourceCount);
                }
            }

            yield return null;
        }
    }

    private void SendUnitBuildNewBase()
    {
        BotController bot = _availableBots.Dequeue();

        bot.CreateNewBase(_flaInstaller.CurrentFlag.transform, _basePrefab);
    }
}
