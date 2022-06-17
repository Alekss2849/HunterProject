using UnityEngine;

namespace HunterProject.Animals
{
    public abstract class Animal
    {
        protected Vector3 Velocity;
        
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
    }
}