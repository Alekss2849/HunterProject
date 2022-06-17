using HunterProject.Animals;
using HunterProject.Animals.Data;
using HunterProject.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class WolfUnity : MonoBehaviour
    {
        [SerializeField]
        private MovementProperties _movementProperties;

        [SerializeField]
        private float _walkDistance;

        private WolfController _wolfController;
        
        public void Start()
        {
            _wolfController = new WolfController(transform, _movementProperties, _walkDistance);
        }

        private void Update()
        {
           _wolfController.Update();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag(Idents._WOLF_TAG) || other.gameObject.CompareTag(Idents._BORDER_TAG))
            {
                return;
            }
            
            Destroy(other.gameObject);
            _wolfController?.UpdateState(transform);
        }
    }
}