using System;
using UnityEngine;

public class SelectionBehaviour : MonoBehaviour
{
    private HighlightBehaviour _highlightBehaviour;

    public event Action<SelectionBehaviour> OnMouseEntered;
    public event Action<SelectionBehaviour> OnMouseClicked;
    public event Action<SelectionBehaviour> OnMouseLeaved;

    private void Start()
    {
        _highlightBehaviour = new HighlightBehaviour(gameObject);
    }

    public void Highlight(Color color)
    {
        _highlightBehaviour.ChangeColor(color);
    }

    public void Unhighlight()
    {
        _highlightBehaviour.SetOriginalColor();
    }

    public void ChangeMaterial(Material newMaterial)
    {
        _highlightBehaviour.ChangeOriginalMaterial(newMaterial);
    }

    private void OnMouseEnter()
    {
        OnMouseEntered?.Invoke(this);
    }

    private void OnMouseExit()
    {
        OnMouseLeaved?.Invoke(this);
    }

    private void OnMouseUp()
    {
        OnMouseClicked?.Invoke(this);
    }
}
