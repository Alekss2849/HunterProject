using DefaultNamespace;
using UnityEngine;

namespace HunterProject.Animals
{
    public class AnimalSpawnController : MonoBehaviour
    {
        [SerializeField]
        private int _wolfCount;
        [SerializeField]
        private WolfContext _wolf;

        [SerializeField]
        private int[] _deersCount;
        // [SerializeField]
        // private GroupOfDeers _deersGroupe;

        [SerializeField]
        private int _rabbitCount;
        [SerializeField]
        private RabbitContext _rabbit;

        [SerializeField]
        private float _maxDistance;

        public void Start()
        {
            SpawnDeers(_deersCount);
            SpawnRabbits(_rabbitCount);
            SpawnWolfs(_wolfCount);
        }

        private void SpawnWolfs(int count)
        {       
            for (int i = 0; i != count; ++i)
            {
                Instantiate(_wolf, new Vector3(UnityEngine.Random.Range(-_maxDistance, _maxDistance), UnityEngine.Random.Range(-_maxDistance, _maxDistance), 1), Quaternion.identity);
            }
        }
        private void SpawnRabbits(int count)
        {
            for (int i = 0; i != count; ++i)
            {
                Instantiate(_rabbit, new Vector3(UnityEngine.Random.Range(-_maxDistance, _maxDistance), UnityEngine.Random.Range(-_maxDistance, _maxDistance), 1), Quaternion.identity);
            }
        }

        private void SpawnDeers(int[] groups)
        {
            // for (int i = 0; i < groups.Length; i++)
            // {
            //     var groupe = Instantiate(_deersGroupe, new Vector3(UnityEngine.Random.Range(-_maxDistance, _maxDistance), UnityEngine.Random.Range(-_maxDistance, _maxDistance), 1), Quaternion.identity);
            //     groupe.StartSpawn(groups[i]);       
            // }
        }    
    }
}