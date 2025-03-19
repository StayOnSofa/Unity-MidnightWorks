using System;
using CityBuilder.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.Crafting.View
{
    public class CraftingRecipeView : MonoBehaviour
    {
        public event Action<CraftingRecipe> OnCraft;

        [SerializeField] private string _amountFormat = "x{0}";
        [SerializeField] private Image _icon1;
        [SerializeField] private Image _icon2;
        [Space] 
        [SerializeField] private TextMeshProUGUI _amount1;
        [SerializeField] private TextMeshProUGUI _amount2;
        [Space]
        [SerializeField] private Button _craftButton;

        private CraftingRecipe _data;
        private UserInventory _userInventory;
        
        public void Constructor(UserInventory inventory, CraftingRecipe recipe)
        {
            _userInventory = inventory;
            _data = recipe;
            
            var need = recipe.Need;
            var give = recipe.Give;

            _icon1.sprite = need.Item.Icon;
            _icon2.sprite = give.Item.Icon;

            _amount1.text = String.Format(_amountFormat, need.Amount);
            _amount2.text = String.Format(_amountFormat, give.Amount);

            _userInventory.OnItemAmountChanged += OnItemAmountChanged;
            Refresh();
        }

        private void OnEnable()
            => _craftButton.onClick.AddListener(Craft);
        
        private void OnDisable()
            => _craftButton.onClick.RemoveListener(Craft);
        
        private void Craft()
            => OnCraft?.Invoke(_data);

        private void OnItemAmountChanged(Item item, int _)
        {
            if (item == _data.Need.Item || item == _data.Give.Item)
                Refresh();
        }

        private void Refresh()
        {
            var needItem = _data.Need.Item;
            var needAmount = _data.Need.Amount;
            
            int currentAmount = _userInventory.GetCurrentAmount(needItem);
            _craftButton.interactable = currentAmount >= needAmount;
        }

        private void OnDestroy()
        {
            OnCraft = null;
            
            if (_userInventory != null)
                _userInventory.OnItemAmountChanged -= OnItemAmountChanged;
        }
    }
}