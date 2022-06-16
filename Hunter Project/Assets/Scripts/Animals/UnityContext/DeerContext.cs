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
            if (_deerController == null)
            {
                return;
            }
            
            _deerController.UpdateState();
            
            switch (_deerController.GetState())
            {
                case AnimalState.Run:
                    transform.position += _deerController.GetRunPosition(transform.position);
                    break;
                case AnimalState.Walk:
                    transform.position = _deerController.GetWalkPosition(transform.position);
                    break;
            }
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