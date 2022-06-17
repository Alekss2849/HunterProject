using System.Linq;
using HunterProject.Animals.Data;
using HunterProject.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class WolfController : AnimalController
    {
        private readonly float _walkRadius;

        private Vector3 _targetPosition;
        private Vector3 _movePoint;

        private const float _BORDER_RESTRICT_DISTANCE_ = 5;

        public WolfController(Transform transform, MovementProperties movementProperties, float walkRadius): base(transform, movementProperties)
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
                    // Debug.DrawLine(currentPosition, _targetPosition, Color.red);
                    return GetSteeringVelocity(MovementProperties.RunSpeed, MovementProperties.SlowdownDistance, currentPosition, _targetPosition);
                case AnimalState.Walk:
                    // Debug.DrawLine(currentPosition, _movePoint, Color.blue);
                    return GetSteeringVelocity(MovementProperties.WalkSpeed, MovementProperties.SlowdownDistance, currentPosition, _movePoint);
            }

            return Vector3.zero;
        }

        public void UpdateState(Transform transform)
        {
            var currentPosition = (Vector2) transform.position;

            var allHits = Physics2D.CircleCastAll(currentPosition, MovementProperties.LookRadius, Vector2.zero)
                .Where(hit => hit.collider.transform != transform).ToArray();
            
            var targetHits = allHits
                .Where(hit => !hit.collider.CompareTag(Idents.WOLF_TAG) && !hit.collider.CompareTag(Idents.BORDER_TAG))
                .Select(hit => hit.point).ToArray();
            
            if (targetHits.Length == 0)
            {
                _movePoint = GetWalkPoint(transform.position, _movePoint, _walkRadius);
                CurrentState = AnimalState.Walk;
                return;
            }
            
            var escapeHits = Physics2D.CircleCastAll(currentPosition, _BORDER_RESTRICT_DISTANCE_, Vector2.zero)
                .Where(hit => hit.collider.CompareTag(Idents.BORDER_TAG)).Select(hit => hit.point).ToArray();
            
            var escapeDirection = (GetEscapePoint(currentPosition, escapeHits, MovementProperties.RunSpeed) - currentPosition).normalized;
            var target = (GetClosestTarget(currentPosition, targetHits) - currentPosition).normalized;

            _targetPosition = currentPosition + (escapeDirection + target).normalized * MovementProperties.RunSpeed;
            
            CurrentState = AnimalState.Run;
        }

        private Vector2 GetClosestTarget(Vector2 currentPosition, Vector2[] enemyPoints)
        {
            return enemyPoints.OrderBy(x => (x - currentPosition).sqrMagnitude)
                                       .First();
        }
    }
}