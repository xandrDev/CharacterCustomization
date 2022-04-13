using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionInspector : MonoBehaviour
{
    [SerializeField]
    private Color _mouseOverColor = Color.blue;
    [SerializeField]
    private Color _selectibleColor = Color.red;
    [SerializeField]
    private List<GameObject> _selectionObjects = new List<GameObject>();

    private List<SelectionBehaviour> _selectionBehaviours = new List<SelectionBehaviour>();

    private bool _isObjectSelected;
    private SelectionBehaviour _selectedObj;

    public event Action OnObjectSelected;

    private void Awake()
    {
        foreach (var obj in _selectionObjects)
            _selectionBehaviours.Add(obj.AddComponent<SelectionBehaviour>());
    }


    private void OnEnable()
    {
        foreach (var behaviour in _selectionBehaviours)
        {
            behaviour.OnMouseClicked += SelectionObj_OnMouseClicked;
            behaviour.OnMouseEntered += SelectionObj_OnMouseEntered;
            behaviour.OnMouseLeaved += SelectionObj_OnMouseLeaved;
        }
    }

    private void OnDisable()
    {
        foreach (var behaviour in _selectionBehaviours)
        {
            behaviour.OnMouseClicked -= SelectionObj_OnMouseClicked;
            behaviour.OnMouseEntered -= SelectionObj_OnMouseEntered;
            behaviour.OnMouseLeaved -= SelectionObj_OnMouseLeaved;
        }
    }

    private void SelectionObj_OnMouseEntered(SelectionBehaviour selectedObj)
    {
        if (_isObjectSelected) return;

        selectedObj.Highlight(_mouseOverColor);
    }

    private void SelectionObj_OnMouseClicked(SelectionBehaviour selectedObj)
    {
        if (!_isObjectSelected)
            SelectObject(selectedObj);
    }

    private void SelectionObj_OnMouseLeaved(SelectionBehaviour selectedObj)
    {
        if (_isObjectSelected) return;

        selectedObj.Unhighlight();
    }

    private void SelectObject(SelectionBehaviour selectedObj)
    {
        _isObjectSelected = true;
        _selectedObj = selectedObj;
        selectedObj.Highlight(_selectibleColor);
        OnObjectSelected?.Invoke();
    }

    public void UnselectObject()
    {
        _isObjectSelected = false;

        if(_selectedObj != null)
            _selectedObj.Unhighlight();
    }

    public void ChangeSelectedObjOriginalMaterial(Material newMaterial)
    {
        _selectedObj.ChangeMaterial(newMaterial);
    }
}
