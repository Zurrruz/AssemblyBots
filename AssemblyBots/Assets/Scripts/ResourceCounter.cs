using System;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] BaseController _baseController;

    private int _numberResource = 0;

    public event Action<int> CountChanged;

    private void OnEnable()
    {
        _baseController.ResourceDelivered += Increase;
    }

    private void OnDisable()
    {
        _baseController.ResourceDelivered += Increase;
    }

    private void Increase()
    {
        _numberResource++;

        CountChanged?.Invoke(_numberResource);
    }
}
