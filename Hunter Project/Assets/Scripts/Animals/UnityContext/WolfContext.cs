using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class WolfContext : MonoBehaviour
    {
        [SerializeField]
        private ContextData _contextData;
        [SerializeField]
        private MovementProperties _movementProperties;

        [SerializeField]
        private float _searchDistance;

        private WolfController _wolfController;
        
        private const string _BORDER_TAG_ = "Bound";

        public void Start()
        {
            _wolfController = new WolfController(_contextData, _movementProperties, _searchDistance);
            _wolfController.UpdateState();
        }

        private void Update()
        {
            _wolfController.UpdateState();
            transform.position += _wolfController.GetNextMovePoint(transform.position);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag(_BORDER_TAG_))
            {
                return;
            }
            
            Destroy(collision.gameObject);
            _wolfController.UpdateState();
        }
    }
}