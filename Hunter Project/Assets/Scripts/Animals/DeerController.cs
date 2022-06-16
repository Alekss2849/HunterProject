using System.Linq;
using HunterProject.Animals.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class DeerController
    {
        private readonly MovementProperties _movementProperties;
        private readonly ContextData _context;
        
        private AnimalState _currentState;

        private Vector3 _movePoint;
        private Vector3 target;
        
        private const string _WOLF_TAG_ = "Wolf";
        private const string _PLAYER_TAG_ = "Player";

        private const float _TARGET_REACH_TOLERANCE_ = 0.2f;
        
        public DeerController(ContextData contextData, MovementProperties movementProperties)
        {
            _context = contextData;
            _movementProperties = movementProperties;
        }
        
        public void Bind(DeerHerdController deerHerd)
        {
            deerHerd.ChangeTargetEvent += UpdateMovePoint;
        }
        
        public Vector3 GetRunPosition(Vector3 currentPosition)
        {
            return -1 * _movementProperties.Speed * 2 * Time.deltaTime * (target - currentPosition).normalized; 
        }
        
        public Vector3 GetWalkPosition(Vector3 currentPosition)
        {
            return Vector2.MoveTowards(currentPosition, _movePoint, _movementProperties.Speed * Time.deltaTime);
        }

        public AnimalState GetState()
        {
            return _currentState;
        }
       
        public void UpdateState()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_context.Transform.position, _movementProperties.LookRadius)
                .Where(x => x.gameObject.tag == _PLAYER_TAG_ || x.gameObject.tag == _WOLF_TAG_).ToArray();

            if (colliders.Length == 0)
            {
                _currentState = AnimalState.Walk;
            }
            else
            {
                UpdateTargetPosition(colliders);
            }
        }

        private void UpdateTargetPosition(Collider2D[] colliders)
        {
            if (_currentState == AnimalState.Walk || target == Vector3.zero || Vector2.Distance(_context.Transform.position, target) < _TARGET_REACH_TOLERANCE_)
            {
                target = colliders
                    .OrderBy(x => Vector2.Distance(x.transform.position, _context.Transform.position)).First().transform.position;
                _currentState = AnimalState.Run;
            }
        }
        
        private void UpdateMovePoint(Vector3 newTarget)
        {
            _movePoint = newTarget;        
        }
    }
}