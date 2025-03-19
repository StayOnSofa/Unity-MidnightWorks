using System;
using UnityEngine;

namespace CityBuilder.City
{
    public class BuildingPlaceGraphics : MonoBehaviour
    {
        public event Action<IReadonlyBuildingPlace> OnBuildingClick;
        
        [SerializeField] private Transform _place;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Space] 
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _defaultColor; 
        
        private IReadonlyBuildingPlace _buildingPlaceEntity;
        private GameObject _placedBuildingInstance;
        
        public void Constructor(IReadonlyBuildingPlace entity)
        {
            Clear();
            _buildingPlaceEntity = entity;
            OnChangeBuilding();

            _buildingPlaceEntity.OnChangeBuilding += OnChangeBuilding;
        }

        private void CreateBuilding(BuildingEntity entity)
        {
            ClearBuilding();
            _placedBuildingInstance = Instantiate(entity.Prefab, _place);
        }

        private void OnChangeBuilding()
        {
            var current = _buildingPlaceEntity.GetCurrentBuilding();
            ClearBuilding();
            
            if (current != null)
                CreateBuilding(current);
        }

        private void ClearBuilding()
        {
            if (_placedBuildingInstance != null)
            {
                Destroy(_placedBuildingInstance);
                _placedBuildingInstance = null;
            }
        }

        private void Clear()
        {
            ClearBuilding();
            
            if (_buildingPlaceEntity != null)
            {
                _buildingPlaceEntity.OnChangeBuilding -= OnChangeBuilding;
                _buildingPlaceEntity = null;
            }
        }
        private void OnDestroy()
            => Clear();

        public void Click()
            => OnBuildingClick?.Invoke(_buildingPlaceEntity);
        
        public void SetHighlights(bool state)
        {
            var color = state ? _selectedColor : _defaultColor;
            _spriteRenderer.color = color;
        }
    }
}