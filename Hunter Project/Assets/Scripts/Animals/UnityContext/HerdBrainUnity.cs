using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class HerdBrainUnity : MonoBehaviour
    {
        [SerializeField]
        private DeerUnity _prefab;

        [SerializeField]
        private int _walkRadius;

        [SerializeField]
        private int _maxSpawnDistance;

        [SerializeField]
        private MovementProperties _movementProperties;

        private HerdBrainController _herdBrainController;

        public void Init(int count)
        {
            _herdBrainController = new HerdBrainController(transform, _movementProperties, _walkRadius, _maxSpawnDistance);
            _herdBrainController.SpawnMembers(_prefab, count);
        }

        private void Update()
        {
            _herdBrainController.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 4);
        }
    }
}