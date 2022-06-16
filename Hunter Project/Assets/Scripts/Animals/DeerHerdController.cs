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
        private readonly ContextData _contextData;
        private readonly float _searchDistance;
        private readonly float _spawnDistance;
        
        public event Action<Vector3> ChangeTargetEvent;

        public DeerHerdController(ContextData contextData, float searchDistance, float spawnDistance)
        {
            _contextData = contextData;
            _searchDistance = searchDistance;
            _spawnDistance = spawnDistance;
        }

        public void SpawnDeers(DeerContext prefab, int count)
        {
            var position = _contextData.Transform.position;
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnThreshold = new Vector3(Random.Range(-_spawnDistance, _spawnDistance), 
                    Random.Range(-_spawnDistance, _spawnDistance), 0);
                
                position += spawnThreshold;
                DeerContext deer = Object.Instantiate(prefab, position, Quaternion.identity);
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