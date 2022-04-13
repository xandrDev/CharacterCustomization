using System;
using UnityEngine;

public class CustomizationSystem : MonoBehaviour
{
    [SerializeField]
    private SelectionInspector _selectionInspector = null;
    [SerializeField]
    private Inventory _inventory = null;
    [SerializeField]
    private Canvas _buyCoinsWindow = null;
    [SerializeField]
    private Coins _playerCoins = null;


    private void Start()
    {
        if (_selectionInspector == null)
            throw new Exception("SelectionInspector not defined");

        if (_inventory == null)
            throw new Exception("Inventory not defined");

        if (_playerCoins == null)
            throw new Exception("PlayerCoins not defined");

        if (_buyCoinsWindow == null)
            throw new Exception("Buy coins panel not defined");

        if (_playerCoins == null)
            throw new Exception("Player coins not defined");
    }

    private void OnEnable()
    {
        _selectionInspector.OnObjectSelected += SelectionInspector_OnObjectSelected;
        _inventory.OnItemSelected += Inventory_OnItemSelected;
        _inventory.OnItemTryBuy += Inventory_OnItemTryBuy;
        _inventory.OnInventoryClosed += Inventory_OnInventoryClosed;
    }

    private void OnDisable()
    {
        _selectionInspector.OnObjectSelected -= SelectionInspector_OnObjectSelected;
        _inventory.OnItemSelected -= Inventory_OnItemSelected;
        _inventory.OnItemTryBuy -= Inventory_OnItemTryBuy;
        _inventory.OnInventoryClosed -= Inventory_OnInventoryClosed;
    }

    private void Inventory_OnInventoryClosed()
    {
        _selectionInspector.UnselectObject();
    }

    private void SelectionInspector_OnObjectSelected()
    {
        _inventory.Open();
    }

    private void Inventory_OnItemTryBuy(Item item)
    {
        if (_playerCoins.BuyItem(item))
        {
            _inventory.UnlockClickedItem();
            ChangeSelectedObjOriginalMaterial(item.Material);
        }
        else
            OpenBuyCoinsWindow();
    }

    private void OpenBuyCoinsWindow()
    {
        _buyCoinsWindow.gameObject.SetActive(true);
    }

    private void Inventory_OnItemSelected(Material itemMaterial)
    {
        ChangeSelectedObjOriginalMaterial(itemMaterial);
    }

    private void ChangeSelectedObjOriginalMaterial(Material newMaterial)
    {
        _selectionInspector.ChangeSelectedObjOriginalMaterial(newMaterial);
    }

    public void OnButtonBuyCoinsClick()
    {
        _playerCoins.IncreaseCoins(200);
        _buyCoinsWindow.gameObject.SetActive(false);
    }
}
