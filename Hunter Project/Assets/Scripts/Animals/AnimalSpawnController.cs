using DefaultNamespace;
using UnityEngine;

namespace HunterProject.Animals
{
    public class AnimalSpawnController : MonoBehaviour
    {
        [SerializeField]
        private HerdBrainUnity _herdBrain;
        [SerializeField]
        private int[] _deerPerHerdCount;

        [SerializeField]
        private RabbitUnity rabbitPrefab;
        [SerializeField]
        private int _rabbitCount;
        
        [SerializeField]
        private WolfUnity _wolf;
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
                var herd = Instantiate(_herdBrain, GetRandomSpawnPoint(), Quaternion.identity);
                herd.Init(herds[i]);
            }
        }
        
        private void SpawnRabbits(int count)
        {
            for (int i = 0; i != count; ++i)
            {
                Instantiate(rabbitPrefab, GetRandomSpawnPoint(), Quaternion.identity);
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