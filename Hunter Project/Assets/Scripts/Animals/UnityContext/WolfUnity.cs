using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class WolfUnity : MonoBehaviour
    {
        [SerializeField]
        private MovementProperties _movementProperties;

        [SerializeField]
        private float _searchDistance;

        private WolfController _wolfController;
        
        private const string _BORDER_TAG_ = "Border";
        private const string _WOLF_TAG_ = "Wolf";

        public void Start()
        {
            _wolfController = new WolfController(transform, _movementProperties, _searchDistance);
            _wolfController.UpdateState();
        }

        private void Update()
        {
            _wolfController.UpdateState();
            transform.position += _wolfController.GetSteeringVelocity(transform.position) * Time.deltaTime;
        }
        
        //
        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if(collision.gameObject.CompareTag(_BORDER_TAG_) || collision.gameObject.CompareTag(_WOLF_TAG_))
        //     {
        //         return;
        //     }
        //     
        //     Destroy(collision.gameObject);
        //     _wolfController.UpdateState();
        // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag(_BORDER_TAG_) || other.gameObject.CompareTag(_WOLF_TAG_))
            {
                return;
            }
            
            Destroy(other.gameObject);
            _wolfController.UpdateState();
        }
    }
}