using System.Collections.Generic;
using System.Linq;
using HunterProject.Animals.Data;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace HunterProject.Animals
{
    public class RabbitController
    {
        private readonly MovementProperties _movementProperties;
        private readonly ContextData _context;
        private readonly float _searchDistance;

        private AnimalState _currentState;

        private Vector3 _targetPosition;
        private Vector3 _movePoint;
        private Vector3 _velocity;

        private const float _MOVE_POINT_REACH_TOLERANCE_ = .3f;
        private const string _BORDER_TAG_ = "Border";

        public RabbitController(ContextData contextData, MovementProperties movementProperties, float searchDistance)
        {
            _context = contextData;
            _movementProperties = movementProperties;
            _searchDistance = searchDistance;
        }

        public Vector3 GetSteeringVelocity(Vector3 currentPosition)
        {
            switch (_currentState)
            {
                case AnimalState.Run:
                    Debug.DrawLine(currentPosition, _targetPosition, Color.red);
                    return GetSteeringVelocity(-_movementProperties.Speed, _movementProperties.SlowdownDistance, currentPosition, _targetPosition);

                case AnimalState.Walk:
                    Debug.DrawLine(currentPosition, _movePoint, Color.blue);
                    return GetSteeringVelocity(_movementProperties.Speed / 2, _movementProperties.SlowdownDistance, currentPosition, _movePoint);
            }

            return Vector3.zero;
        }

        private Vector3 GetSteeringVelocity(float speed, float slowdownDistance, Vector3 currentPosition, Vector3 targetPosition)
        {
            Vector3 distanceToTarget = targetPosition - currentPosition;
            Vector3 targetDirection = distanceToTarget.normalized;
            Vector3 desiredVelocity = targetDirection * speed;
            Vector3 steering = desiredVelocity - _velocity;

            _velocity += steering * Time.deltaTime;

            float slowDownFactor = Mathf.Clamp01(distanceToTarget.magnitude / slowdownDistance);
            _velocity *= slowDownFactor;
            _velocity.z = 0;

            return _velocity;
        }

        public void UpdateState()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_context.Transform.position, _movementProperties.LookRadius)
                                              .Where(x => x.transform != _context.Transform)
                                              .ToArray();

            if (colliders.Length == 0)
            {
                UpdateMovePoint();
            }
            else
            {
                UpdateTargetPosition(colliders);
                AvoidBorders();
            }
        }
        
        private void AvoidBorders()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(_context.Transform.position, (_context.Transform.position - _targetPosition).normalized, _movementProperties.LookRadius);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Border"))
                {
                    _targetPosition = hit.point;
                    _currentState = AnimalState.Run;
                }
            }
        }

        private void UpdateTargetPosition(Collider2D[] colliders)
        {
            var arr = colliders
                .Where(x => !x.CompareTag(_BORDER_TAG_)).ToArray();

            if (arr.Length == 0)
            {
                return;
            }
                
            _targetPosition = arr
                .OrderBy(x => Vector2.Distance(x.transform.position, _context.Transform.position))
                .First().transform.position;
            _currentState = AnimalState.Run;
        }

        private void UpdateMovePoint()
        {
            if (_currentState == AnimalState.Run || Vector2.Distance(_movePoint, _context.Transform.position) < _MOVE_POINT_REACH_TOLERANCE_ ||
                _movePoint == Vector3.zero)
            {
                _movePoint = GetRandomPoint();
            }
            
            _currentState = AnimalState.Walk;
        }

        private Vector2 GetRandomPoint()
        {
            return new Vector2(
                Random.Range(-_searchDistance, _searchDistance),
                Random.Range(-_searchDistance, _searchDistance));
        }
    }
}