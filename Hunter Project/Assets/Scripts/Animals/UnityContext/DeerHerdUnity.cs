using System.Collections;
using HunterProject.Animals;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeerHerdUnity : MonoBehaviour
    {
        [SerializeField]
        private DeerUnity _prefab;
    
        [SerializeField]
        private int _maxDistance;

        [SerializeField]
        private int _maxSpawnDistance;

        private DeerHerdController _deerHerdController;
        
        public void StartSpawn( int count)
        {
            _deerHerdController = new DeerHerdController(transform, _maxDistance, _maxSpawnDistance);
            _deerHerdController.SpawnDeers(_prefab, count);
            _deerHerdController.UpdateMovePoint();
            StartCoroutine(MovePointUpdater());
        }

        private IEnumerator MovePointUpdater()
        {
            while (true)
            {
                _deerHerdController.UpdateMovePoint();
                yield return new WaitForSeconds(.3f);
            }
        }
    }
}