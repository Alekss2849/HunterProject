using System.Linq;
using HunterProject.Animals.Data;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace HunterProject.Animals
{
    public class RabbitController : AnimalController
    {
        private readonly float _walkRadius;

        private Vector3 _targetPosition;
        private Vector3 _movePoint;

        public RabbitController(Transform transform, MovementProperties movementProperties, float walkRadius) : base(transform, movementProperties)
        {
            _walkRadius = walkRadius;
        }

        public override void Update()
        {
            UpdateState(Transform);
            Transform.position += GetSteeringVelocity(Transform.position) * Time.deltaTime;
        }

        private Vector3 GetSteeringVelocity(Vector3 currentPosition)
        {
            switch (CurrentState)
            {
                case AnimalState.Run:
                    return GetSteeringVelocity(MovementProperties.RunSpeed, MovementProperties.SlowdownDistance, currentPosition, _targetPosition);

                case AnimalState.Walk:
                    return GetSteeringVelocity(MovementProperties.WalkSpeed, MovementProperties.SlowdownDistance, currentPosition, _movePoint);
            }

            return Vector3.zero;
        }

        private void UpdateState(Transform transform)
        {
            var hits = Physics2D.CircleCastAll(transform.position, MovementProperties.LookRadius, Vector2.zero)
                .Where(hit => hit.transform != transform).Select(hit => hit.point).ToArray();

            if (hits.Length == 0)
            {
                _movePoint = GetWalkPoint(transform.position, _movePoint, _walkRadius);
                CurrentState = AnimalState.Walk;
                return;
            }
            
            _targetPosition = GetEscapePoint(transform.position, hits, MovementProperties.RunSpeed);
            CurrentState = AnimalState.Run;
        }
    }
}