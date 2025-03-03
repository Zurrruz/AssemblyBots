using System;
using System.Collections;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 20f; 
    [SerializeField] private float _scanInterval = 5f;

    private WaitForSeconds _delay;

    public event Action<Resource> DiscoveredResource;

    private void Awake()
    {       
        _delay = new WaitForSeconds(_scanInterval);
    }

    void Start()
    {
        StartCoroutine(ScanForResources());
    }

    private IEnumerator ScanForResources()
    {
        while (enabled)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius);

            foreach (var hitCollider in hitColliders)
                if (hitCollider.TryGetComponent(out Resource resource))
                    DiscoveredResource?.Invoke(resource);

            yield return _delay;
        }
    }    
}
