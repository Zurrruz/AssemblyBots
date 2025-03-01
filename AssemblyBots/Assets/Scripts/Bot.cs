using UnityEngine;

public class Bot : MonoBehaviour
{
    private GameController gameManager;
    private Vector3 targetPosition;
    private bool isCollecting = false;

    public void Initialize(GameController manager)
    {
        gameManager = manager; // Получаем ссылку на GameManager
        InvokeRepeating(nameof(ScanForResources), 0f, 3f); // Сканирование ресурса каждые 3 секунды
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

        // Проверка достижения ресурса
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            gameManager.AddResource(); // Добавляем ресурс к базе
            targetPosition = Vector3.zero; // Возвращаемся на базу
            isCollecting = false;
            // Вернуться на базу
            MoveToBase();
        }
    }

    private void MoveToBase()
    {
        targetPosition = Vector3.zero; // Позиция базы
        isCollecting = false; // Ожидаем указаний
    }

    public bool IsIdle()
    {
        return !isCollecting; // Проверяем, свободен ли юнит
    }

    private void ScanForResources()
    {
        gameManager.ResourceScanned(); // Запускаем сканирование ресурсов в базе
    }
}
