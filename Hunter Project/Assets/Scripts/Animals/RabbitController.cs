using System.Linq;
using HunterProject.Animals.Data;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace HunterProject.Animals
{
    public class RabbitController : AnimalController
    {
        private readonly Transform _transform;
        private readonly MovementProperties _movementProperties;
        private readonly float _walkRadius;

        private AnimalState _currentState;

        private Vector3 _targetPosition;
        private Vector3 _movePoint;

        public RabbitController(Transform transform, MovementProperties movementProperties, float walkRadius)
        {
            _transform = transform;
            _movementProperties = movementProperties;
            _walkRadius = walkRadius;
        }

        public override void Update()
        {
            UpdateState(_transform);
            _transform.position += GetSteeringVelocity(_transform.position) * Time.deltaTime;
        }

        private Vector3 GetSteeringVelocity(Vector3 currentPosition)
        {
            switch (_currentState)
            {
                case AnimalState.Run:
                    return GetSteeringVelocity(_movementProperties.RunSpeed, _movementProperties.SlowdownDistance, currentPosition, _targetPosition);

                case AnimalState.Walk:
                    return GetSteeringVelocity(_movementProperties.WalkSpeed, _movementProperties.SlowdownDistance, currentPosition, _movePoint);
            }

            return Vector3.zero;
        }

        private void UpdateState(Transform transform)
        {
            var hits = Physics2D.CircleCastAll(transform.position, _movementProperties.LookRadius, Vector2.zero)
                .Where(hit => hit.transform != transform).Select(hit => hit.point).ToArray();

            if (hits.Length == 0)
            {
                _movePoint = GetWalkPoint(transform.position, _movePoint, _walkRadius);
                _currentState = AnimalState.Walk;
                return;
            }
            
            _targetPosition = GetEscapePoint(transform.position, hits, _movementProperties.RunSpeed);
            _currentState = AnimalState.Run;
        }
    }
}