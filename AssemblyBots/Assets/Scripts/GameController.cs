using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject basePrefab; // ������ ����
    public GameObject unitPrefab; // ������ �����
    public GameObject resourcePrefab; // ������ �������

    private GameObject baseObject;
    private List<GameObject> units = new List<GameObject>();
    private List<GameObject> resources = new List<GameObject>();
    private int resourceCount = 0;
    private int maxUnits = 3;

    private void Start()
    {
        // �������� ����
        baseObject = Instantiate(basePrefab, Vector3.zero, Quaternion.identity);
        // �������� ������
        for (int i = 0; i < maxUnits; i++)
        {
            GameObject unit = Instantiate(unitPrefab, new Vector3(i * 5.0f, 0, 0), Quaternion.identity);
            unit.AddComponent<Bot>().Initialize(this);
            units.Add(unit);
        }

        // ��������� ��������
        InvokeRepeating(nameof(GenerateResource), 2f, 5f); // ������������ ������ ������ 5 ������
    }

    private void GenerateResource()
    {
        // ��������� ������� � ��������� �������
        Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        GameObject resource = Instantiate(resourcePrefab, randomPosition, Quaternion.identity);
        resources.Add(resource);
    }

    public void ResourceScanned()
    {
        // �������� ������� ��������� ������
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
                        return; // ���������� ������������ ����� �������� �����
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

