using System;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public event Action<PooledObject> Finished;

    public void Back()
    {
        Finished?.Invoke(this);
    }
}
