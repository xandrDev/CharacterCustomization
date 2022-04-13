using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriprableObjects/InventoryItem", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField]
    private string _name = "Item";
    [SerializeField]
    private Sprite _icon = null;
    [SerializeField]
    private int _price = 0;
    [SerializeField]
    public Material _material = null;
    [SerializeField]
    public bool _isLocked = true;

    public string Name => _name;
    public Sprite Icon => _icon;
    public int Price => _price;
    public Material Material => _material;

    public bool IsLocked => _isLocked;

    public void Unclock() => _isLocked = false;
}
