using System;
using CityBuilder.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.City.View
{
    public class BuyBuildingView : MonoBehaviour
    {
        public event Action<BuildingEntity> OnBuyBuilding;

        [SerializeField] private Image _icon;
        [Space]
        [SerializeField] private Image _priceIcon;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private string _priceFormat = "{0}";
        [Space]
        [SerializeField] private Image _bringsIcon;
        [SerializeField] private TextMeshProUGUI _bringsText;
        [SerializeField] private string _bringsFormat = "+{0}";
        [Space]
        [SerializeField] private Button _buyButton;

        private BuildingEntity _building;
        
        public void Constructor(UserInventory inventory, BuildingEntity buildingEntity)
        {
            _building = buildingEntity;
            
            _icon.sprite = _building.Icon;
            
            _priceIcon.sprite = _building.Price.Item.Icon;
            _priceText.text = String.Format(_priceFormat, _building.Price.Amount);
            
            _bringsIcon.sprite = _building.Brings.Item.Icon;
            _bringsText.text = String.Format(_bringsFormat, _building.Brings.Amount);
            
            _buyButton.interactable = inventory.GetCurrentAmount(_building.Price.Item) >= _building.Price.Amount;;
        }

        private void OnEnable()
            => _buyButton.onClick.AddListener(OnClick);
        
        private void OnDisable()
            => _buyButton.onClick.RemoveListener(OnClick);
        
        private void OnClick()
            => OnBuyBuilding?.Invoke(_building);

        private void OnDestroy()
            => OnBuyBuilding = null;
    }
}