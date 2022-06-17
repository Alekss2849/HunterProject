using System;
using HunterProject.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HunterProject.Gun
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        
        private Vector3 _direction;

        private const float _BULLET_LIFETIME_ = 5f;

        private void Start()
        {
            Destroy(gameObject, _BULLET_LIFETIME_);
        }

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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!gameObject)
            {
                return;
            }
            
            if (other.gameObject.CompareTag(Idents.BORDER_TAG) ||
                other.gameObject.CompareTag(Idents.PLAYER_TAG))
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