using UnityEngine;
using UnityEngine.AI;

public class Resource : PooledObject
{
    [SerializeField] private Collider collider;

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
