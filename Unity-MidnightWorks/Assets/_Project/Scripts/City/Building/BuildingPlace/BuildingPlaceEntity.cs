using System;
using System.Linq;
using UnityEngine;

namespace CityBuilder.City
{
    public interface IReadonlyBuildingPlace
    {
        public event Action OnChangeBuilding;
        public BuildingEntity[] GetAvailableBuildings();
        public BuildingEntity GetCurrentBuilding();
    }
    
    [Serializable]
    public class BuildingPlaceEntity : IReadonlyBuildingPlace
    {
        public event Action OnChangeBuilding;
        
       [SerializeField] private BuildingEntity[] _availableBuildings;
       [SerializeField] private BuildingEntity _currentBuilding;

        public BuildingPlaceEntity(BuildingEntity[] availableBuildings, BuildingEntity currentBuilding)
        {
            _availableBuildings = availableBuildings ?? Array.Empty<BuildingEntity>();
            _currentBuilding = currentBuilding;
        }

        public BuildingEntity[] GetAvailableBuildings() 
            => _availableBuildings;
        public BuildingEntity GetCurrentBuilding() 
            => _currentBuilding;
        
        public bool TrySetCurrentBuilding(BuildingEntity newBuilding)
        {
            if (_availableBuildings.Contains(newBuilding))
            {
                _currentBuilding = newBuilding;
                OnChangeBuilding?.Invoke();

                return true;
            }

            return false;
        }

        public void ClearCurrentBuilding()
        {
            _currentBuilding = null;
            OnChangeBuilding?.Invoke();
        }
    }
}