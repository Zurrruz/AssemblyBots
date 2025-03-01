using UnityEngine;

public class Resource : PooledObject
{
    [SerializeField] int _obstacleLayer = 6;
    [SerializeField] int _defaultLeyer = 0;

    private void OnEnable()
    {
        gameObject.layer = _obstacleLayer;
    }

    public void UploadBase()
    {
        Back();
    }

    public void ChangesLayer()
    {
        gameObject.layer = _defaultLeyer;
    }
}
