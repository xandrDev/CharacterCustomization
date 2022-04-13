using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _cellPrafab = null;
    [SerializeField]
    private Transform _cellContainer = null;
    [SerializeField]
    private List<Item> _items = new List<Item>();

    private Dictionary<InventoryCell, Item> _inventoryMap = new Dictionary<InventoryCell, Item>();
    private InventoryCell _selectedCell;

    public event Action<Item> OnItemTryBuy;
    public event Action OnInventoryClosed;
    public event Action<Material> OnItemSelected;

    private void Awake()
    {
        foreach (var item in _items)
        {
            InventoryCell cell = Instantiate(_cellPrafab, _cellContainer).GetComponent<InventoryCell>();
            var itemClone = Instantiate(item);
            cell.UpdateCell(itemClone);
            _inventoryMap.Add(cell, itemClone);
        }
    }

    private void OnEnable()
    {
        foreach(var pair in _inventoryMap)
            pair.Key.OnClicked += Cell_OnClicked;
    }

    private void OnDisable()
    {
        foreach (var pair in _inventoryMap)
            pair.Key.OnClicked -= Cell_OnClicked;
    }

     private void Cell_OnClicked(InventoryCell cell)
    {
        _selectedCell = cell;

        if (_inventoryMap[cell].IsLocked)
            OnItemTryBuy?.Invoke(_inventoryMap[cell]);
        else
            OnItemSelected?.Invoke(_inventoryMap[cell].Material);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        OnInventoryClosed?.Invoke();
        gameObject.SetActive(false);
    }

    public void UnlockClickedItem()
    {
        _inventoryMap[_selectedCell].Unclock();
        _selectedCell.UpdateCell(_inventoryMap[_selectedCell]);
    }
}
