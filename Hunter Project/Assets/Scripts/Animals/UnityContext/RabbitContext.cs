using System;
using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace DefaultNamespace
{
    public class RabbitContext : MonoBehaviour
    {
        [SerializeField]
        private ContextData _contextData;
        [SerializeField]
        private MovementProperties _movementProperties;

        [SerializeField]
        private float _searchDistance;

        private RabbitController _rabbitController;

        private void Start()
        {
            _rabbitController = new RabbitController(_contextData, _movementProperties, _searchDistance);
            _rabbitController.UpdateState();
        }

        private void Update()
        {
            _rabbitController.UpdateState();
            transform.position += _rabbitController.GetSteeringVelocity(transform.position) * Time.deltaTime;
        }
    }
}