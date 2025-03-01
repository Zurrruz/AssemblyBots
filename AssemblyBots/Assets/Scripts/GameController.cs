using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject basePrefab; // Префаб базы
    public GameObject unitPrefab; // Префаб юнита
    public GameObject resourcePrefab; // Префаб ресурса

    private GameObject baseObject;
    private List<GameObject> units = new List<GameObject>();
    private List<GameObject> resources = new List<GameObject>();
    private int resourceCount = 0;
    private int maxUnits = 3;

    private void Start()
    {
        // Создание базы
        baseObject = Instantiate(basePrefab, Vector3.zero, Quaternion.identity);
        // Создание юнитов
        for (int i = 0; i < maxUnits; i++)
        {
            GameObject unit = Instantiate(unitPrefab, new Vector3(i * 5.0f, 0, 0), Quaternion.identity);
            unit.AddComponent<Bot>().Initialize(this);
            units.Add(unit);
        }

        // Генерация ресурсов
        InvokeRepeating(nameof(GenerateResource), 2f, 5f); // Генерировать ресурс каждые 5 секунд
    }

    private void GenerateResource()
    {
        // Генерация ресурса в случайной позиции
        Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        GameObject resource = Instantiate(resourcePrefab, randomPosition, Quaternion.identity);
        resources.Add(resource);
    }

    public void ResourceScanned()
    {
        // Проверка наличия свободных юнитов
        foreach (GameObject resource in resources)
        {
            if (resource != null)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    Bot unit = units[i].GetComponent<Bot>();
                    if (unit.IsIdle())
                    {
                        unit.CollectResource(resource.transform.position);
                        resources.Remove(resource);
                        return; // Прекратить сканирование после отправки юнита
                    }
                }
            }
        }
    }

    public void AddResource()
    {
        resourceCount++;
        Debug.Log("Resources collected: " + resourceCount);
    }
}

