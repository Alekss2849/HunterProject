using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeerUnity : MonoBehaviour
    {
        [SerializeField]
        private MovementProperties _movementProperties;

        private DeerController _deerController;

        public DeerController Init()
        {
            _deerController = new DeerController(transform, _movementProperties);

            return _deerController;
        }

        private void OnDestroy()
        {
            _deerController.OnDestroy();
        }
    }
}