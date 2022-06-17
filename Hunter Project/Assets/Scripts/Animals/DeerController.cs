using System.Linq;
using HunterProject.Animals.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class DeerController : Animal
    {
        private readonly MovementProperties _movementProperties;
        private readonly ContextData _context;
        
        private AnimalState _currentState;

        private Vector3 _movePoint;
        private Vector3 _targetPosition;

        private const string _WOLF_TAG_ = "Wolf";
        private const string _PLAYER_TAG_ = "Player";
        
        public DeerController(ContextData contextData, MovementProperties movementProperties)
        {
            _context = contextData;
            _movementProperties = movementProperties;
        }
        
        public void Bind(DeerHerdController deerHerd)
        {
            deerHerd.ChangeTargetEvent += UpdateMovePoint;
        }
        
        public Vector3 GetRunSteeringVelocity(Vector3 currentPosition)
        {
            return GetSteeringVelocity(-_movementProperties.Speed * 2, _movementProperties.SlowdownDistance, currentPosition, _targetPosition);
        }
        
        public Vector3 GetWalkSteeringVelocity(Vector3 currentPosition)
        {
            return GetSteeringVelocity(_movementProperties.Speed, _movementProperties.SlowdownDistance, currentPosition, _movePoint);
        }
        
        public AnimalState GetState()
        {
            return _currentState;
        }
       
        public void UpdateState()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_context.Transform.position, _movementProperties.LookRadius)
                                              .Where(x => x.gameObject.CompareTag(_PLAYER_TAG_) || x.gameObject.CompareTag(_WOLF_TAG_))
                                              .ToArray();

            if (colliders.Length == 0)
            {
                _currentState = AnimalState.Walk;
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
            _targetPosition = colliders
                              .OrderBy(x => Vector2.Distance(x.transform.position, _context.Transform.position))
                              .First().transform.position;
            _currentState = AnimalState.Run;
        }
        
        private void UpdateMovePoint(Vector3 newTarget)
        {
            _movePoint = newTarget;        
        }
    }
}