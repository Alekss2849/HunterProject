using UnityEngine;

namespace HunterProject.Hunter
{
    public class HunterCharacter : MonoBehaviour
    {
        private const float _VELOCITY_MULTIPLIER_ = 100;
        
        [SerializeField] private Gun.Gun _gun;
        [SerializeField] private float _movementSpeed;

        private readonly HunterInput _hunterInput = new HunterInput();
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
            Shoot();
        }

        private void Move()
        {
            MovementState movementState = _hunterInput.TrackMovement();
            Vector2 movementDirection = GetMovementDirection(movementState);
            _rigidbody.velocity = movementDirection * (_movementSpeed * _VELOCITY_MULTIPLIER_ * Time.fixedDeltaTime);
        }

        private void Shoot()
        {
            if (_hunterInput.TrackShoot())
            {
                _gun.Shoot();
            }
        }

        private Vector2 GetMovementDirection(MovementState movementState)
        {
            Vector2 vector = Vector2.zero;

            switch (movementState)
            {
                case MovementState.MoveUp:
                    vector += Vector2.up;

                    break;

                case MovementState.MoveDown:
                    vector += Vector2.down;

                    break;

                case MovementState.MoveRight:
                    vector += Vector2.right;

                    break;

                case MovementState.MoveLeft:
                    vector += Vector2.left;

                    break;
            }

            return vector;
        }
    }
}