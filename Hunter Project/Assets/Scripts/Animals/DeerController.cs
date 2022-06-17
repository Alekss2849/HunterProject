using System;
using HunterProject.Animals.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class DeerController : AnimalController
    {
        public event Action<DeerController> Destroyed;

        private Vector2 _brainPosition;
        private AnimalState _brainState;

        public DeerController(Transform transform, MovementProperties movementProperties) : base(transform, movementProperties)
        {
        }
        
        public override void Update()
        {
            CurrentState = _brainState;
            Transform.position += (Vector3)GetSteeringVelocity(Transform.position) * Time.deltaTime;
        }
        
        private Vector2 GetSteeringVelocity(Vector2 currentPosition)
        {
            switch (CurrentState)
            { 
                case AnimalState.Run:
                    Vector2 targetPos = _brainPosition;
                    Debug.DrawLine(currentPosition, targetPos, Color.red);
                    return GetSteeringVelocity(MovementProperties.RunSpeed, MovementProperties.SlowdownDistance, currentPosition, targetPos);

                case AnimalState.Walk:
                    Vector2 movePos = _brainPosition;
                    Debug.DrawLine(currentPosition, movePos, Color.blue);
                    return GetSteeringVelocity(MovementProperties.WalkSpeed, MovementProperties.SlowdownDistance, currentPosition, movePos);
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
            Destroyed?.Invoke(this);
        }
    }
}