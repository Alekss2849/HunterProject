using System;

namespace HunterProject.Animals.Data
{
    [Serializable]
    public class MovementProperties
    {
        public float Speed;
        public float SlowdownDistance = 1;
        public float LookRadius;
    }
}