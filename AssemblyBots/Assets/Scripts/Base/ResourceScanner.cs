using System;
using System.Collections;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float scanRadius = 20f; 
    [SerializeField] private float scanInterval = 5f;

    private WaitForSeconds _delay;

    public event Action<Resource> DiscoveredResource;

    private void Awake()
    {       
        _delay = new WaitForSeconds(scanInterval);
    }

    void Start()
    {
        StartCoroutine(ScanForResources());
    }

    private IEnumerator ScanForResources()
    {
        while (enabled)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, scanRadius);

            foreach (var hitCollider in hitColliders)
                if (hitCollider.TryGetComponent(out Resource resourse))
                    DiscoveredResource?.Invoke(resourse);

            yield return _delay;
        }
    }    
}
