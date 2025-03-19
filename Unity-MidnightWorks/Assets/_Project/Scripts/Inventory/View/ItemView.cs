using CityBuilder.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.Views
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private Image _image;

        private UserInventory _userInventory;
        
        public void Constructor(UserInventory userInventory)
        {
            _userInventory = userInventory;
            _userInventory.OnItemAmountChanged += OnRefresh;
            
            OnRefresh(_item, _userInventory.GetCurrentAmount(_item));
        }

        private void OnDestroy()
        {
            if (_userInventory != null)
                _userInventory.OnItemAmountChanged -= OnRefresh;
        }

        private void OnRefresh(Item item, int amount)
        {
            if (item != _item)
                return;
            
            _image.sprite = item.Icon;
            _amount.text = amount.FormatNumber();
        }
    }
}