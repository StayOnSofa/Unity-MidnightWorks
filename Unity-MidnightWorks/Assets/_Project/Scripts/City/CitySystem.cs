using System;
using System.Collections.Generic;
using CityBuilder.Inventory;
using CityBuilder.SaveStorage;
using UnityEngine;

namespace CityBuilder.City
{
    public class CitySystem
    {
        private const string PathSettings = "ScriptableObjects/Settings/CitySettings";
        private const string PathDefaultBuildings = "ScriptableObjects/Settings/DefaultBuildingPlaces";
        private const string PathBuildings = "ScriptableObjects/Buildings";

        public event Action OnDayIncome;
        
        public float SecondsInDayCycle => _citySettings.SecondsInDayCycle;
        public float TimeBeforeReward { private set; get; }
        
        private readonly UserInventory _userInventory;

        private readonly CitySettings _citySettings;
        private readonly DefaultBuildingPlaces _defaultBuildingPlaces; 
        private readonly CitySaveLoad _citySaveLoad;
        private readonly ILocalStorage _localStorage;
        
        private BuildingPlaceEntity[] _buildingPlaces;
        
        private bool _flagIsGameStarted;
        private float _timer;
        
        public CitySystem(UserInventory userInventory, ILocalStorage localStorage)
        {
            _localStorage = localStorage;
            _userInventory = userInventory;
            
            _citySettings = Resources.Load<CitySettings>(PathSettings);
            _defaultBuildingPlaces = Resources.Load<DefaultBuildingPlaces>(PathDefaultBuildings);

            var allBuildings = Resources.LoadAll<BuildingEntity>(PathBuildings);
            _citySaveLoad = new CitySaveLoad(allBuildings, localStorage);
        }
        
        private void GiveStartingItems()
        {
            foreach (var itemWithAmount in _citySettings.StartingItems)
                _userInventory.AddItem(itemWithAmount.Item, itemWithAmount.Amount);
        }
        
        public bool HasSavedCity()
            => _localStorage.HasSaves();

        private BuildingPlaceEntity[] GetOrCreateBuildingPlaces()
        {
            if (_citySaveLoad.TryLoadBuildingPlaces(out BuildingPlaceEntity[] buildingPlaces))
                return buildingPlaces;

            return _defaultBuildingPlaces.Places;
        }

        private void SavePlaces(BuildingPlaceEntity[] buildingPlaces)
            => _citySaveLoad.SaveBuildingPlaces(buildingPlaces);

        public void StartNewGame()
        {
            if (_localStorage.HasSaves())
                _localStorage.ClearSaves();
            
            _userInventory.Clear();
            
            GiveStartingItems();
            LoadGame();

            SavePlaces(_buildingPlaces);
        }
        
        public void LoadGame()
        {
            _buildingPlaces = GetOrCreateBuildingPlaces();
            _flagIsGameStarted = true;
        }

        public IReadOnlyCollection<IReadonlyBuildingPlace> GetPlaces()
            => _buildingPlaces;
        
        public void Tick(float dt)
        {
            if (!_flagIsGameStarted)
                return;

            _timer += dt;
            if (_timer >= SecondsInDayCycle)
            {
                CalculateDayCycleIncome();
                _timer = 0;
            }
            
            TimeBeforeReward = SecondsInDayCycle - _timer;
        }

        private void CalculateDayCycleIncome()
        {
            foreach (var place in _buildingPlaces)
            {
                var building = place.GetCurrentBuilding();
                
                if (building == null)
                    continue;

                var pay = building.Brings;
                _userInventory.AddItem(pay.Item, pay.Amount);
            }

            OnDayIncome?.Invoke();
        }

        private bool TryFindPlaceEntity(IReadonlyBuildingPlace place, out BuildingPlaceEntity entity)
        {
            entity = null;
            
            foreach (var buildingPlace in _buildingPlaces)
            {
                if (buildingPlace == place)
                {
                    entity = buildingPlace;
                    return true;
                }
            }

            return false;
        }

        public bool TryDemolishBuilding(IReadonlyBuildingPlace buildingPlace)
        {
            if (buildingPlace.GetCurrentBuilding() == null)
                return false;

            if (TryFindPlaceEntity(buildingPlace, out BuildingPlaceEntity buildingPlaceEntity))
            {
                buildingPlaceEntity.ClearCurrentBuilding();
                return true;
            }

            return false;
        }

        public bool TryBuyBuildingFor(IReadonlyBuildingPlace buildingPlace, BuildingEntity building)
        {
            if (buildingPlace.GetCurrentBuilding() != null)
                return false;
            
            if (TryFindPlaceEntity(buildingPlace, out BuildingPlaceEntity buildingPlaceEntity))
            {
                Item priceItem = building.Price.Item;
                int priceAmount = building.Price.Amount;
                
                bool hasEnough = _userInventory.GetCurrentAmount(priceItem) >= priceAmount;
                
                if (hasEnough && buildingPlaceEntity.TrySetCurrentBuilding(building))
                {
                    if (_userInventory.TryRemoveItem(priceItem, priceAmount))
                    {
                        SavePlaces(_buildingPlaces);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}