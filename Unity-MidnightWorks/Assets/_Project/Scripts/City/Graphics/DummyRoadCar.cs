using UnityEngine;

namespace CityBuilder.City
{
    public class DummyRoadCar : MonoBehaviour
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _distance = 100;
        
        private Vector3 _startCoords;
        
        private void Start()
        {
            _startCoords = transform.position;
        }

        private void Update()
        {
            var forward = transform.forward;
            transform.position += forward * (Time.deltaTime * _speed);
            
            if (Vector3.Distance(transform.position, _startCoords) > _distance)
                transform.position = _startCoords;
        }
    }
}