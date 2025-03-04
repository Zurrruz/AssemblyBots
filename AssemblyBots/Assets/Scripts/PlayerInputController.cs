using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;

    [SerializeField] private LayerMask _baseLayer = 7;
    [SerializeField] private LayerMask _groundLayer = 8;

    private BaseController _selectedBase;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(_selectedBase !=null)
        {
            _selectedBase.ChangeColor(_defaultColor);
        }

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _baseLayer))
        {
            _selectedBase = hit.collider.GetComponent<BaseController>();

            _selectedBase.ChangeColor(_selectedColor);
        }

        else if (_selectedBase != null && Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
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