using UnityEngine;

public class InputService : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private InputHandler _inputHandler;

    private BaseController _selectedBase;

    private void OnEnable()
    {
        _inputHandler.ButtonPressed += ProcessClick;   
    }

    private void OnDisable()
    {
        _inputHandler.ButtonPressed -= ProcessClick;
    }

    private void ProcessClick(RaycastHit hit)
    {
        if (_selectedBase != null)
            _selectedBase.ChangeColor(_defaultColor);

        if (hit.collider.TryGetComponent(out BaseController baseController))
        {
            _selectedBase = baseController;
            _selectedBase.ChangeColor(_selectedColor);
        }

        else if (_selectedBase != null && hit.collider.TryGetComponent(out Ground ground))
        {
            _selectedBase.ChangeColor(_defaultColor);

            if (_selectedBase.CanPlaceFlag())
            {
                _selectedBase.PlaceFlag(hit.point);
                _selectedBase = null;
            }
        }
    }
}
