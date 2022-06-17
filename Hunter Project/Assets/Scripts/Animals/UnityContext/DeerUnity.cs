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
                    transform.position += _deerController.GetRunSteeringVelocity(transform.position) * Time.deltaTime;
                    break;
                case AnimalState.Walk:
                    transform.position += _deerController.GetWalkSteeringVelocity(transform.position) * Time.deltaTime;
                    break;
            }
        }

        public void Init()
        {
            _deerController = new DeerController(transform, _movementProperties);
        }

        public void BindHerd(DeerHerdController deerHerd)
        {
            _deerController.Bind(deerHerd);
        }
    }
}