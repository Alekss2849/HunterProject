using System.Linq;
using HunterProject.Animals.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class WolfController
    {
        private readonly MovementProperties _movementProperties;
        private readonly ContextData _context;
        private readonly float _searchDistance;

        private AnimalState _currentState;

        private Vector3 _targetPosition;
        private Vector3 _movePoint;

        private const float _TARGET_REACH_TOLERANCE_ = 0.2f;
        private const float _MOVE_POINT_REACH_TOLERANCE_ = 3f;

        private const string _WOLF_TAG_ = "Wolf";
        private const string _BORDER_TAG_ = "Border";
        
        public WolfController(ContextData contextData, MovementProperties movementProperties, float searchDistance)
        {
            _context = contextData;
            _movementProperties = movementProperties;
            _searchDistance = searchDistance;
        }

        public Vector3 GetNextMovePoint(Vector3 currentPosition)
        {
            switch (_currentState)
            {
                case AnimalState.Run:
                    return _movementProperties.Speed * Time.deltaTime * (_targetPosition - currentPosition).normalized;
                case AnimalState.Walk:
                    return _movementProperties.Speed / 2 * Time.deltaTime * (_movePoint - currentPosition).normalized;
            }

            return Vector3.zero;
        }

        public void UpdateState()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_context.Transform.position, _movementProperties.LookRadius)
                .Where(x => x.CompareTag(_WOLF_TAG_) == false && x.CompareTag(_BORDER_TAG_) == false).ToArray();
            
            if (colliders.Length == 0)
            {
                UpdateMovePoint();
            }
            else
            {
                UpdateTargetPosition(colliders);
            }
        }

        private void UpdateTargetPosition(Collider2D[] colliders)
        {
            if (_currentState == AnimalState.Walk)
            {
                _targetPosition = colliders.OrderBy(x => Vector2.Distance(x.transform.position, _context.Transform.position)).First().transform.position;
                _currentState = AnimalState.Run;
            }
            
            if (_targetPosition == Vector3.zero || Vector2.Distance(_context.Transform.position, _targetPosition) < _TARGET_REACH_TOLERANCE_)
            {
                _targetPosition = colliders.OrderBy(x => Vector2.Distance(x.transform.position, _context.Transform.position)).First().transform.position;
                _currentState = AnimalState.Run;
            }
        }

        private void UpdateMovePoint()
        {
            if (_currentState == AnimalState.Run || _movePoint == Vector3.zero || Vector2.Distance(_movePoint, _context.Transform.position) < _MOVE_POINT_REACH_TOLERANCE_)
            {
                _movePoint = GetRandomPoint();
                _currentState = AnimalState.Walk;
            }
        }

        private Vector2 GetRandomPoint()
        {
            return new Vector2(Random.Range(-_searchDistance, _searchDistance),
                Random.Range(-_searchDistance, _searchDistance));
        }
    }
}