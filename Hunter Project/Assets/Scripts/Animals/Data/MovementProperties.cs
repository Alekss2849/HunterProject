using System;

namespace HunterProject.Animals.Data
{
    [Serializable]
    public class MovementProperties
    {
        public float WalkSpeed;
        public float RunSpeed;
        public float SlowdownDistance = 1;
        public float LookRadius;
    }
}