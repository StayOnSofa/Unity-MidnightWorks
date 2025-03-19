using UnityEngine;

namespace CityBuilder.City
{
    [CreateAssetMenu(fileName = "DefaultBuildingSettings", menuName = "City/DefaultBuildingSettings")]
    public class DefaultBuildingPlaces : ScriptableObject
    {
        [SerializeField] private BuildingPlaceEntity[] _places;
        
        public BuildingPlaceEntity[] Places => _places;
    }
}