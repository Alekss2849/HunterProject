using System;
using HunterProject.Animals.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class DeerController : AnimalController
    {
        public event Action Destroyed;

        private readonly Transform _transform;
        private readonly MovementProperties _movementProperties;

        private Vector2 _brainPosition;
        private AnimalState _brainState;

        public DeerController(Transform transform, MovementProperties movementProperties)
        {
            _movementProperties = movementProperties;
            _transform = transform;
        }
        
        public override void Update()
        {
            CurrentState = _brainState;
            _transform.position += (Vector3)GetSteeringVelocity(_transform.position) * Time.deltaTime;
        }
        
        private Vector2 GetSteeringVelocity(Vector2 currentPosition)
        {
            switch (CurrentState)
            { 
                case AnimalState.Run:
                    Vector2 targetPos = _brainPosition;
                    Debug.DrawLine(currentPosition, targetPos, Color.red);
                    return GetSteeringVelocity(_movementProperties.RunSpeed, _movementProperties.SlowdownDistance, currentPosition, targetPos);

                case AnimalState.Walk:
                    Vector2 movePos = _brainPosition;
                    Debug.DrawLine(currentPosition, movePos, Color.blue);
                    return GetSteeringVelocity(_movementProperties.WalkSpeed, _movementProperties.SlowdownDistance, currentPosition, movePos);
            }

            return Vector3.zero;
        }

        public void SetBrainData(Vector2 point, AnimalState state)
        {
            _brainPosition = point;
            _brainState = state;
        }

        public void OnDestroy()
        {
            Destroyed?.Invoke();
        }
    }
}