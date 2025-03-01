using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _obstacleAvoidanceDistance = 2f;
    [SerializeField] private LayerMask _obstacleLayer;

    private GameObject _ignoreTarget;

    private Rigidbody _rigidbody;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        if (_targetPosition != Vector3.zero)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;

            if (LooksObstacles(direction))
                direction = FindNewDirection();

            RotateTowards(direction);

            MoveForward();
        }
    }

    private bool LooksObstacles(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, _obstacleAvoidanceDistance, _obstacleLayer))
            if (hit.collider.gameObject != _ignoreTarget)
                return true;

        return false;
    }

    private Vector3 FindNewDirection()
    {
        Vector3[] directions = {
            transform.right,
            -transform.right
        };

        foreach (Vector3 direction in directions)
            if (LooksObstacles(direction) == false)
                return direction;

        return -transform.forward;
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void MoveForward()
    {
        _rigidbody.velocity = transform.forward * _moveSpeed;
    }

    public void AssignTarget(GameObject target)
    {
        _ignoreTarget = target;

        _targetPosition = target.transform.position;
    }    

    public void MoveStop()
    {
        _targetPosition = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
    }
}
