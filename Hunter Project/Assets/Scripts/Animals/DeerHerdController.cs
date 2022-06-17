using System;
using DefaultNamespace;
using HunterProject.Animals.Data;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace HunterProject.Animals
{
    public class DeerHerdController
    {
        private Transform _transform;
        private readonly float _searchDistance;
        private readonly float _spawnDistance;
        
        public event Action<Vector3> ChangeTargetEvent;

        public DeerHerdController(Transform transform, float searchDistance, float spawnDistance)
        {
            _transform = transform;
            _searchDistance = searchDistance;
            _spawnDistance = spawnDistance;
        }

        public void SpawnDeers(DeerUnity prefab, int count)
        {
            var position = _transform.position;
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnThreshold = new Vector3(Random.Range(-_spawnDistance, _spawnDistance), 
                    Random.Range(-_spawnDistance, _spawnDistance), 0);
                
                position += spawnThreshold;
                DeerUnity deer = Object.Instantiate(prefab, position, Quaternion.identity);
                deer.Init();
                deer.BindHerd(this);
            }
        }
        
        public void UpdateMovePoint()
        {
            ChangeTargetEvent?.Invoke(GetRandomPoint());
        }
        
        private Vector2 GetRandomPoint()
        {
            return new Vector2(Random.Range(-_searchDistance, _searchDistance),
                Random.Range(-_searchDistance, _searchDistance));
        }
    }
}