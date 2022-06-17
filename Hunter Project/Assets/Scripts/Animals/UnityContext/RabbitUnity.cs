using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class RabbitUnity : MonoBehaviour
    {
        [SerializeField]
        private MovementProperties _movementProperties;

        [SerializeField]
        private float _walkDistance;

        private RabbitController _rabbitController;

        private void Start()
        {
            _rabbitController = new RabbitController(transform, _movementProperties, _walkDistance);
        }

        private void Update()
        {
            _rabbitController.Update();
        }
    }
}