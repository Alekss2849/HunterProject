using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HunterProject.Gun
{
    public class Bullet : MonoBehaviour
    {
        private const string _BORDER_TAG_ = "Border";
        private const string _PLAYER_TAG_ = "Player";

        [SerializeField] private float _speed;
        private Vector3 _direction;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += _speed * Time.deltaTime * _direction;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_BORDER_TAG_) ||
                other.gameObject.CompareTag(_PLAYER_TAG_))
            {
                return;
            }

            DestroyEnemy(other.gameObject);
            DestroyBullet(gameObject);
        }

        private void DestroyEnemy(Object enemy)
        {
            Destroy(enemy);
        }

        private void DestroyBullet(Object bullet)
        {
            Destroy(bullet);
        }
    }
}