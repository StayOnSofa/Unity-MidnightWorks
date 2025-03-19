using System;
using CityBuilder.SaveStorage;

namespace CityBuilder.Inventory
{
    public class UserInventory
    {
        private const string InventorySaveKey = "Inventory";

        public event Action<Item, int> OnItemAmountChanged;
        
        private readonly ILocalStorage _localStorage;
        private readonly SerializeDictionary<string, int> _itemStorage;
        
        public UserInventory(AppCoreDependencies dependencies)
        {
            _localStorage = dependencies.LocalStorage;
            _itemStorage = LoadInventory() ?? new SerializeDictionary<string, int>();
        }

        private void SaveInventory()
        {
            _localStorage.SaveValue(InventorySaveKey, _itemStorage);
        }

        private SerializeDictionary<string, int> LoadInventory()
        {
            return _localStorage.TryGetValue(InventorySaveKey, out SerializeDictionary<string, int> inventory) 
                ? inventory 
                : new SerializeDictionary<string, int>();
        }
        
        public void AddItem(Item item, int amount)
        {
            if (amount <= 0) return;
            
            _itemStorage[item.GUID] = _itemStorage.GetValueOrDefault(item.GUID, 0) + amount;
            OnItemAmountChanged?.Invoke(item, _itemStorage[item.GUID]);
            SaveInventory();
        }
        
        public bool TryRemoveItem(Item item, int amount)
        {
            if (amount <= 0 || !_itemStorage.TryGetValue(item.GUID, out int currentAmount) || currentAmount < amount)
                return false;
            
            _itemStorage[item.GUID] -= amount;
            OnItemAmountChanged?.Invoke(item, _itemStorage[item.GUID]);
            SaveInventory();
            return true;
        }

        public int GetCurrentAmount(Item item) 
            => _itemStorage.GetValueOrDefault(item.GUID, 0);

        public void Clear()
            => _itemStorage.Clear();
    }
}
