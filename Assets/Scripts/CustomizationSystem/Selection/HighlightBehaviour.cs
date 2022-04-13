using System.Collections.Generic;
using UnityEngine;

public class HighlightBehaviour
{
    private List<Material> _materials = new List<Material>();
    private List<Color> _originalColors = new List<Color>();

    private GameObject _highlightObject;

    public HighlightBehaviour(GameObject highlightObject)
    {
        _highlightObject = highlightObject;

        foreach (Renderer objectRenderer in highlightObject.gameObject.GetComponentsInChildren<Renderer>())
        {
            _materials.Add(objectRenderer.material);
            _originalColors.Add(objectRenderer.material.color);
        }
    }

    public void ChangeColor(Color color)
    {
        foreach (var material in _materials)
            material.color = color;
    }

    public void SetOriginalColor()
    {
        for (int i = 0; i < _materials.Count; i++)
            _materials[i].color = _originalColors[i];
    }

    public void ChangeOriginalMaterial(Material newMaterial)
    {
        _materials.Clear();
        _originalColors.Clear();
        
        foreach (Renderer objectRenderer in _highlightObject.gameObject.GetComponentsInChildren<Renderer>())
        {
            objectRenderer.material = newMaterial;

            _materials.Add(objectRenderer.material);
            _originalColors.Add(objectRenderer.material.color);
        }
    }
}
