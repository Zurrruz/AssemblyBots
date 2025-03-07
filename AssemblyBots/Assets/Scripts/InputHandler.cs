using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _raycastTarget = 7;

    public event Action<RaycastHit> ButtonPressed;

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

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _raycastTarget))
            ButtonPressed?.Invoke(hit);
    }
}