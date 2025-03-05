using UnityEngine;

public class Resource : PooledObject
{
    [SerializeField] private Collider _collider;

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    public void DisableObstacleParameter()
    {
        _collider.enabled = false;
    }

    public void UploadBase()
    {
        Back();
    }
}
