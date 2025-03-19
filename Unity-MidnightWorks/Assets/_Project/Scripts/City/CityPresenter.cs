using System;
using System.Collections.Generic;
using CityBuilder.City.View;
using CityBuilder.Views;

namespace CityBuilder.City
{
    public class CityPresenter
    {
        public event Action<IReadOnlyCollection<IReadonlyBuildingPlace>> OnBuildingPlaced;
        
        private readonly UIContainer _uiContainer;
        private readonly CitySystem _citySystem;

        private IReadonlyBuildingPlace _selectedBuildingPlace;
        
        public float SecondsInDayCycle => _citySystem.SecondsInDayCycle;
        public float TimeBeforeReward => _citySystem.TimeBeforeReward;
        
        public CityPresenter(CitySystem citySystem, UIContainer container)
        {
            _citySystem = citySystem;
            _uiContainer = container;
        }
        
        public IReadonlyBuildingPlace GetSelectedPlace()
            => _selectedBuildingPlace;
        
        public bool HasSavedCity()
            => _citySystem.HasSavedCity();

        public void StartNewGame()
        {
            _citySystem.StartNewGame();
            
            OnBuildingPlaced?.Invoke(_citySystem.GetPlaces());
            ShowMenu();
        }

        public void LoadGame()
        {
            _citySystem.LoadGame();
            OnBuildingPlaced?.Invoke(_citySystem.GetPlaces());
            
            ShowMenu();
        }

        public bool TryBuyBuildingFor(IReadonlyBuildingPlace buildingPlace, BuildingEntity entity)
            => _citySystem.TryBuyBuildingFor(buildingPlace, entity);

        public bool TryDemolishBuilding(IReadonlyBuildingPlace buildingPlace)
            => _citySystem.TryDemolishBuilding(buildingPlace);
        
        private void ShowMenu()
            => _uiContainer.ShowMenu<MapScreenView>();

        public void ShowBuilding(IReadonlyBuildingPlace place)
        {
            _selectedBuildingPlace = place;
            _uiContainer.ShowPopUp<BuildingPlaceView>();
        }
    }
}