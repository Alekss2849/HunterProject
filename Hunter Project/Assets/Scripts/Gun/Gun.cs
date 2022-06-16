using UnityEngine;

namespace HunterProject.Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private BulletCountLabel _bulletCountLabel;
        [SerializeField] private int _bulletCount;
        [SerializeField] private Bullet _bulletPrefab;

        private Vector2 _aimDirection;

        private void Start()
        {
            _bulletCountLabel.SetBulletCount(_bulletCount);
        }

        private void Update()
        {
            Aim();
        }

        private void Aim()
        {
            transform.localEulerAngles = new Vector3(0, 0, GetAimAngle());
        }

        private float GetAimAngle()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _aimDirection = (mousePos - (Vector2) transform.position).normalized;
            float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;

            return angle;
        }

        public void Shoot()
        {
            if (_bulletCount == 0)
            {
                return;
            }

            Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            bullet.SetDirection(_aimDirection);

            _bulletCount--;
            _bulletCountLabel.SetBulletCount(_bulletCount);
        }
    }
}