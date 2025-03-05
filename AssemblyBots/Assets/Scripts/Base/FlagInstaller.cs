using UnityEngine;

public class FlagInstaller : MonoBehaviour
{
    [SerializeField] private FlagController _flagPrefab;

    public FlagController CurrentFlag { get; private set; }

    public void Create(Vector3 position)
    {
        if (CurrentFlag != null)
        {
            CurrentFlag.transform.position = position;
            CurrentFlag.gameObject.SetActive(true);
        }
        else
        {
            CurrentFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
        }
    }
}
