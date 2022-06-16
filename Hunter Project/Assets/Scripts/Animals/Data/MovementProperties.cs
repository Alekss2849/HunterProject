using System;
using UnityEngine;

namespace HunterProject.Animals.Data
{
    [Serializable]
    public struct MovementProperties
    {
        public float Speed;
        public float LookRadius;
        public Transform Transform;
    }
}