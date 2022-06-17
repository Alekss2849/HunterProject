using System.Linq;
using HunterProject.Animals.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class WolfController : Animal
    {
        private readonly MovementProperties _movementProperties;
        private readonly ContextData _context;
        private readonly float _searchDistance;

        private AnimalState _currentState;

        private Vector3 _targetPosition;
        private Vector3 _movePoint;

        private const float _MOVE_POINT_REACH_TOLERANCE_ = .3f;

        private const string _WOLF_TAG_ = "Wolf";
        private const string _BORDER_TAG_ = "Border";


        public WolfController(ContextData contextData, MovementProperties movementProperties, float searchDistance)
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
                    // Debug.DrawLine(currentPosition, _targetPosition, Color.red);
                    return GetSteeringVelocity(_movementProperties.Speed, _movementProperties.SlowdownDistance, currentPosition, _targetPosition);
                case AnimalState.Walk:
                    // Debug.DrawLine(currentPosition, _movePoint, Color.blue);
                    return GetSteeringVelocity(_movementProperties.Speed / 2, _movementProperties.SlowdownDistance, currentPosition, _movePoint);
            }

            return Vector3.zero;
        }

        public void UpdateState()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_context.Transform.position, _movementProperties.LookRadius)
                                              .Where(x => x.CompareTag(_WOLF_TAG_) == false && x.CompareTag(_BORDER_TAG_) == false)
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
            RaycastHit2D[] hits = Physics2D.RaycastAll(_context.Transform.position, (_targetPosition - _context.Transform.position).normalized, _movementProperties.LookRadius);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Border"))
                {
                    _targetPosition = hit.point;
                    _currentState = AnimalState.Run;
                }
            }
        }

        private void UpdateMovePoint()
        {
            if (_movePoint == Vector3.zero || Vector2.Distance(_movePoint, _context.Transform.position) < _MOVE_POINT_REACH_TOLERANCE_)
            {
                _movePoint = GetRandomPoint();
            }
            
            _currentState = AnimalState.Walk;
        }

        private void UpdateTargetPosition(Collider2D[] colliders)
        {
            _targetPosition = colliders.OrderBy(x => Vector2.Distance(x.transform.position, _context.Transform.position))
                                       .First().transform.position;
            _currentState = AnimalState.Run;
        }

        private Vector2 GetRandomPoint()
        {
            return new Vector2(
                Random.Range(-_searchDistance, _searchDistance),
                Random.Range(-_searchDistance, _searchDistance));
        }
    }
}