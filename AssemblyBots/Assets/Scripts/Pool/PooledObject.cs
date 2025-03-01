using System;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public event Action<PooledObject> Finished;

    protected void Back()
    {
        Finished?.Invoke(this);
    }
}
