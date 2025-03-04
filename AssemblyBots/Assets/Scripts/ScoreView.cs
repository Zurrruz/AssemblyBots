using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private BaseController _baseController;

    private void OnEnable()
    {
        _baseController.ResourceChanged += ShowCount;
    }

    private void OnDisable()
    {
        _baseController.ResourceChanged -= ShowCount;
    }

    private void ShowCount(int count)
    {
        _countText.text = count.ToString();
    }
}
