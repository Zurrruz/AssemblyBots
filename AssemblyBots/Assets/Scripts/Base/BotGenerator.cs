using System;
using UnityEngine;

public class BotGenerator : MonoBehaviour
{
    [SerializeField] private BotController _botPrefab;
    [SerializeField] private Transform _box;
    [SerializeField] private BaseController _baseController;
    [SerializeField] private int _numberResourcesPerUnit;

    public event Action<int, BotController> Created;

    private void OnEnable()
    {
        _baseController.ResourceChanged += TryCreate;
    }

    private void OnDisable()
    {
        _baseController.ResourceChanged -= TryCreate;
    }

    private void TryCreate(int resource)
    {
        if (resource >= _numberResourcesPerUnit && _baseController.IsBuildingNewBase == false)
        {
            BotController bot = Instantiate(_botPrefab, _box.position, Quaternion.identity);
            bot.AssignBase(_baseController);

            Created?.Invoke(_numberResourcesPerUnit, bot);
        }
    }
}
