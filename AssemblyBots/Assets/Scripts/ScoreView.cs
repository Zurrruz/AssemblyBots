using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private BaseController _baseController;

    private void OnEnable()
    {
        _baseController.ResourceDelivered += ShowCount;
    }

    private void OnDisable()
    {
        _baseController.ResourceDelivered -= ShowCount;
    }

    private void ShowCount(int count)
    {
        _countText.text = count.ToString();
    }
}
