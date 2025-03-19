using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilder.City
{
    [Serializable]
    public class JsonBuildingPlaceArray
    {
        public JsonBuildingPlace[] Array;
    }

    [Serializable]
    public class JsonBuildingPlace
    {
        [SerializeField] private string[] availableBuildingsGUID = new string[0];
        [SerializeField] private string currentBuildingGUID = string.Empty;
        
        public JsonBuildingPlace(BuildingPlaceEntity place)
        {
            availableBuildingsGUID = place.GetAvailableBuildings().Select(b => b.GUID).ToArray();
            currentBuildingGUID = place.GetCurrentBuilding()?.GUID;
        }

        public BuildingPlaceEntity ToBuildingPlace(Dictionary<string, BuildingEntity> buildingMap)
        {
            var availableBuildings = availableBuildingsGUID
                .Select(buildingMap.GetValueOrDefault)
                .Where(building => building != null)
                .ToArray();

            buildingMap.TryGetValue(currentBuildingGUID, out var currentBuilding);
            return new BuildingPlaceEntity(availableBuildings, currentBuilding);
        }
    }
}