using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.City.View
{
    public class CityMapView : MonoBehaviour
    {
        [SerializeField] private AppCoreDependencies _dependencies;
        [SerializeField] private BuildingPlaceGraphics[] _buildingPlaceGraphics;
        
        private CityPresenter _cityPresenter;
        
        private void Start()
        {
            _cityPresenter = _dependencies.CityPresenter;
            _cityPresenter.OnBuildingPlaced += OnBuildingPlaces;
        }

        private void OnEnable()
        {
            foreach (var graphics in _buildingPlaceGraphics)
                graphics.OnBuildingClick += OnBuildingClick;
        }

        private void OnDisable()
        {
            foreach (var graphics in _buildingPlaceGraphics)
                graphics.OnBuildingClick -= OnBuildingClick;
        }

        private void OnBuildingPlaces(IReadOnlyCollection<IReadonlyBuildingPlace> entities)
        {
            int index = 0;
            
            foreach (var entity in entities)
            {
                var graphics = _buildingPlaceGraphics[index];
                index += 1;
                
                graphics.Constructor(entity);
            }
        }

        private void OnBuildingClick(IReadonlyBuildingPlace place)
        {
            _cityPresenter.ShowBuilding(place);
        }

        private void OnDestroy()
        {
            _cityPresenter.OnBuildingPlaced -= OnBuildingPlaces;
        }
    }
}