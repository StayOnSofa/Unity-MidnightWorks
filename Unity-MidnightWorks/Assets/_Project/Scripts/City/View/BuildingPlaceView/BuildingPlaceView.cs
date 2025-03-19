using CityBuilder.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.City.View
{
    public class BuildingPlaceView : UIView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _demolishButton;
        [Space] 
        [SerializeField] private BuyBuildingView _prefab;
        [SerializeField] private Transform _content;
        [Space] 
        [SerializeField] private GameObject _buildLayer;
        [SerializeField] private GameObject _demolishLayer;
        
        private CityPresenter _cityPresenter => Dependencies.CityPresenter;
        private UserInventory _inventory => Dependencies.UserInventory;
        
        private IReadonlyBuildingPlace _place;
        
        private void SetBuildingLayer(bool state)
        {
            _buildLayer.SetActive(state);
            _demolishLayer.SetActive(!state);
        }

        private void Start()
        {
            _place = _cityPresenter.GetSelectedPlace();
            CreateContent();
            
            SetBuildingLayer(_place.GetCurrentBuilding() == null);
        }

        private void CreateContent()
        {
            foreach (var building in _place.GetAvailableBuildings())
            {
                var slide = Instantiate(_prefab, _content);
                
                slide.Constructor(_inventory, building);
                slide.OnBuyBuilding += OnBuyBuilding;
            }
        }

        private void OnBuyBuilding(BuildingEntity entity)
        {
            if (_cityPresenter.TryBuyBuildingFor(_place, entity))
                Close();
        }

        private void OnDemolish()
        {
            if (_cityPresenter.TryDemolishBuilding(_place))
                Close();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
            _demolishButton.onClick.AddListener(OnDemolish);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
            _demolishButton.onClick.RemoveListener(OnDemolish);
        }
    }
}