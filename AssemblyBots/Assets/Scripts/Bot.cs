using UnityEngine;

public class Bot : MonoBehaviour
{
    private GameController gameManager;
    private Vector3 targetPosition;
    private bool isCollecting = false;

    public void Initialize(GameController manager)
    {
        gameManager = manager; // �������� ������ �� GameManager
        InvokeRepeating(nameof(ScanForResources), 0f, 3f); // ������������ ������� ������ 3 �������
    }

    private void Update()
    {
        if (isCollecting)
        {
            MoveToResource();
        }
    }

    public void CollectResource(Vector3 resourcePosition)
    {
        targetPosition = resourcePosition;
        isCollecting = true;
    }

    private void MoveToResource()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2f);

        // �������� ���������� �������
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            gameManager.AddResource(); // ��������� ������ � ����
            targetPosition = Vector3.zero; // ������������ �� ����
            isCollecting = false;
            // ��������� �� ����
            MoveToBase();
        }
    }

    private void MoveToBase()
    {
        targetPosition = Vector3.zero; // ������� ����
        isCollecting = false; // ������� ��������
    }

    public bool IsIdle()
    {
        return !isCollecting; // ���������, �������� �� ����
    }

    private void ScanForResources()
    {
        gameManager.ResourceScanned(); // ��������� ������������ �������� � ����
    }
}
