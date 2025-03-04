using UnityEngine;

public class Resource : PooledObject
{
    [SerializeField] private new Collider collider;

    private void OnEnable()
    {
        collider.enabled = true;
    }

    public void DisableObstacleParameter()
    {
        collider.enabled = false;
    }

    public void UploadBase()
    {
        Back();
    }
}
