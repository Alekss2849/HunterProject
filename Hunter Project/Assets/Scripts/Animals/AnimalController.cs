using HunterProject.Animals.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public abstract class AnimalController : IAnimalController
    {
        protected AnimalState CurrentState;
        protected Vector3 Velocity;
        protected readonly Transform Transform;
        protected readonly MovementProperties MovementProperties;
        
        private const float _MOVE_POINT_REACH_SQR_TOLERANCE_ = .3f;

        protected AnimalController(Transform transform, MovementProperties movementProperties)
        {
            Transform = transform;
            MovementProperties = movementProperties;
        }
        
        protected Vector3 GetSteeringVelocity(float speed, float slowdownDistance, Vector3 currentPosition, Vector3 targetPosition)
        {
            Vector3 distanceToTarget = targetPosition - currentPosition;
            Vector3 targetDirection = distanceToTarget.normalized;
            Vector3 desiredVelocity = targetDirection * speed;
            Vector3 steering = desiredVelocity - Velocity;

            Velocity += steering * Time.deltaTime;

            float slowDownFactor = Mathf.Clamp01(distanceToTarget.magnitude / slowdownDistance);
            Velocity *= slowDownFactor;
            Velocity.z = 0;

            return Velocity;
        }
        
        protected Vector2 GetEscapePoint(Vector2 currentPos, Vector2[] enemyPositions, float speed)
        {
            Vector2 sum = Vector2.zero;
            
            for (int i = 0; i != enemyPositions.Length; ++i)
            {
                var direction = enemyPositions[i] - currentPos;

                sum += direction.normalized;
            }

            return currentPos - sum * speed;
        }

        protected Vector2 GetWalkPoint(Vector2 currentPos, Vector2 currentMovePoint, float walkRadius)
        {
            if (currentMovePoint == Vector2.zero
                || CurrentState == AnimalState.Run
                || (currentPos - currentMovePoint).sqrMagnitude < _MOVE_POINT_REACH_SQR_TOLERANCE_)
            {
                return GetRandomPoint(currentPos, walkRadius);
            }

            return currentMovePoint;
        }
        
        protected Vector2 GetRandomPoint(Vector2 currentPos, float radius)
        {
            return currentPos + new Vector2(
                Random.Range(-radius, radius),
                Random.Range(-radius, radius));
        }

        public abstract void Update();
    }
}