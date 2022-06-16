using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeerContext : MonoBehaviour
    {
        [SerializeField]
        private ContextData _contextData;
        [SerializeField]
        private MovementProperties _movementProperties;

        private DeerController _deerController;

        private void Start()
        {
            _deerController?.UpdateState();
        }

        private void Update()
        {
            _deerController.UpdateState();
            transform.position += _deerController.GetNextMovePoint(transform.position);
        }

        public void Init()
        {
            _deerController = new DeerController(_contextData, _movementProperties);
        }

        public void BindHerd(DeerHerdController deerHerd)
        {
            _deerController.Bind(deerHerd);
        }
    }
}