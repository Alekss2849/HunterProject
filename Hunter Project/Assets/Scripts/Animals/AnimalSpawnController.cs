using DefaultNamespace;
using UnityEngine;

namespace HunterProject.Animals
{
    public class AnimalSpawnController : MonoBehaviour
    {
        [SerializeField]
        private DeerHerdContext _deerHerd;
        [SerializeField]
        private int[] _deerPerHerdCount;

        [SerializeField]
        private RabbitContext _rabbit;
        [SerializeField]
        private int _rabbitCount;
        
        [SerializeField]
        private WolfContext _wolf;
        [SerializeField]
        private int _wolfCount;

        [SerializeField]
        private float _spawnDistance;

        public void Start()
        {
            SpawnDeerHerd(_deerPerHerdCount);
            SpawnRabbits(_rabbitCount);
            SpawnWolfs(_wolfCount);
        }
        
        private void SpawnDeerHerd(int[] herds)
        {
            for (int i = 0; i != herds.Length; ++i)
            {
                var herd = Instantiate(_deerHerd, GetRandomSpawnPoint(), Quaternion.identity);
                herd.StartSpawn(herds[i]);
            }
        }
        
        private void SpawnRabbits(int count)
        {
            for (int i = 0; i != count; ++i)
            {
                Instantiate(_rabbit, GetRandomSpawnPoint(), Quaternion.identity);
            }
        }

        private void SpawnWolfs(int count)
        {       
            for (int i = 0; i != count; ++i)
            {
                Instantiate(_wolf, GetRandomSpawnPoint(), Quaternion.identity);
            }
        }
        
        private Vector3 GetRandomSpawnPoint()
        {
            return new Vector3(Random.Range(-_spawnDistance, _spawnDistance),
                Random.Range(-_spawnDistance, _spawnDistance), 1);
        }
    }
}