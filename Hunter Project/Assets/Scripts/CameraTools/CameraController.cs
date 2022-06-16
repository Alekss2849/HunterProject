using UnityEngine;

namespace HunterProject.CameraTools
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset;

        private float _zPos;

        private void Start()
        {
            _zPos = transform.position.z;
        }

        private void Update()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            transform.position = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, _zPos);
        }
    }
}