using System.Collections;
using HunterProject.Animals;
using HunterProject.Animals.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeerHerdContext : MonoBehaviour
    {
        [SerializeField]
        private ContextData _contextData;
        [SerializeField]
        private DeerContext _prefab;
    
        [SerializeField]
        private int _maxDistance;

        [SerializeField]
        private int _maxSpawnDistance;

        private DeerHerdController _deerHerdController;
        
        public void StartSpawn( int count)
        {
            _deerHerdController = new DeerHerdController(_contextData, _maxDistance, _maxSpawnDistance);
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