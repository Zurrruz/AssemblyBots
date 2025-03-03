using UnityEngine;
using UnityEngine.AI;

public class BotMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    public void AssignTarget(GameObject target)
    {
        _agent.SetDestination(target.transform.position);
    }  
}
