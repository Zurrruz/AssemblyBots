using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private ResourceCounter _resourceCounter;

    private void OnEnable()
    {
        _resourceCounter.CountChanged += ShowCount;
    }

    private void OnDisable()
    {
        _resourceCounter.CountChanged -= ShowCount;
    }

    private void ShowCount(int count)
    {
        _countText.text = count.ToString();
    }
}
