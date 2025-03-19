using System.Collections.Generic;
using System.Linq;
using CityBuilder.SaveStorage;
using UnityEngine;

namespace CityBuilder.City
{
    public class CitySaveLoad
    {
        private const string SaveBuildingKey = "CityBuildings";

        private readonly ILocalStorage _storage;
        private readonly Dictionary<string, BuildingEntity> _guidToBuilding;

        public CitySaveLoad(BuildingEntity[] allBuildings, ILocalStorage storage)
        {
            _guidToBuilding = allBuildings.ToDictionary(b => b.GUID);
            _storage = storage;
        }

        public void SaveBuildingPlaces(BuildingPlaceEntity[] buildingPlaces)
        {
            var serializedValues = buildingPlaces
                .Select(place => new JsonBuildingPlace(place))
                .ToArray();

            _storage.SaveValue(SaveBuildingKey, new JsonBuildingPlaceArray {Array = serializedValues});
        }

        public bool TryLoadBuildingPlaces(out BuildingPlaceEntity[] buildingPlaces)
        {
            if (_storage.TryGetValue(SaveBuildingKey, out JsonBuildingPlaceArray serializedValues))
            {
                buildingPlaces = serializedValues.Array
                    .Select(jsonPlace => jsonPlace.ToBuildingPlace(_guidToBuilding))
                    .ToArray();

                return true;
            }

            buildingPlaces = null;
            return false;
        }
    }
}