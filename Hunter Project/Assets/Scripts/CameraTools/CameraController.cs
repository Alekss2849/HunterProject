using UnityEngine;

namespace HunterProject.CameraTools
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private void Update()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            transform.position = new Vector3(_player.position.x, 
                                             _player.position.y, 
                                             transform.position.z);
        }
    }
}