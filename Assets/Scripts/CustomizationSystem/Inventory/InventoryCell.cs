using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    private Text _priceTxt;

    public event Action<InventoryCell> OnClicked;
    
    private void Awake()
    {
        _image = GetComponentsInChildren<Image>()[1];
        _priceTxt = GetComponentsInChildren<Text>()[0];
    }

    public void UpdateCell(Item item)
    {
        if(item && item.Icon)
        {
            _image.sprite = item.Icon;
            _image.color = Color.white;

            if(item.IsLocked)
                _priceTxt.text = item.Price.ToString();
            else
                _priceTxt.text = "Unlock";
        }
        else
        {
            _image.sprite = null;
            _image.color = Color.clear;
            _priceTxt.text = "???";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
    }
}
