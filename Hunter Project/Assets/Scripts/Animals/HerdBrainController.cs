using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using HunterProject.Animals.Data;
using HunterProject.Data;
using UnityEngine;

namespace HunterProject.Animals
{
    public class HerdBrainController : AnimalController
    {
        private readonly List<DeerController> _deers;
        private readonly List<Vector2> _offsets;
        private readonly List<float> _regroupTimers;

        private Vector2 _movePoint;
        private Vector2 _targetPosition;
        private readonly float _walkRadius;
        private readonly float _spawnDistance;

        public HerdBrainController(Transform transform, MovementProperties movementProperties, float walkRadius, float spawnDistance) : base(transform, movementProperties)
        {
            _spawnDistance = spawnDistance;
            _walkRadius = walkRadius;

            _deers = new List<DeerController>();
            _offsets = new List<Vector2>();
            _regroupTimers = new List<float>();
        }

        public void SpawnMembers(DeerUnity prefab, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 spawnPos = (Vector2) Transform.position + Random.insideUnitCircle * _spawnDistance;
                DeerController deer = Object.Instantiate(prefab, spawnPos, Quaternion.identity).Init();
                deer.Destroyed += OnDeerDestroyed;
                _deers.Add(deer);
                _offsets.Add(Random.insideUnitCircle * 5);
                _regroupTimers.Add(Random.Range(3f, 8f));
            }
        }

        public override void Update()
        {
            UpdateState(Transform);
            Transform.position += (Vector3) GetTargetVelocity(Transform.position) * Time.deltaTime;

            Vector2 position = Transform.position;

            switch (CurrentState)
            {
                case AnimalState.Run:
                    position = _targetPosition;

                    break;

                case AnimalState.Walk:
                    position = _movePoint;

                    break;
            }

            for (int i = 0; i != _deers.Count; ++i)
            {
                _deers[i].SetBrainData(position + _offsets[i], CurrentState);
                _deers[i].Update();
            }

            TickRegroupTimers();
        }

        private void TickRegroupTimers()
        {
            for (int i = 0; i != _regroupTimers.Count; ++i)
            {
                _regroupTimers[i] -= Time.deltaTime;

                if (_regroupTimers[i] <= 0)
                {
                    _regroupTimers[i] = Random.Range(3f, 8f);
                    GenerateOffsets(i);
                }
            }
        }

        private void GenerateOffsets(int index)
        {
            _offsets[index] = Random.insideUnitCircle * 5;
        }

        private Vector2 GetTargetPos()
        {
            switch (CurrentState)
            {
                case AnimalState.Run:
                    return _targetPosition;

                case AnimalState.Walk:
                    return _movePoint;
            }

            return Vector2.zero;
        }

        private Vector2 GetTargetVelocity(Vector2 currentPos)
        {
            Vector2 targetPos = GetTargetPos();
            Vector2 direction = (targetPos - currentPos).normalized;

            switch (CurrentState)
            {
                case AnimalState.Run:
                    return direction * MovementProperties.RunSpeed;

                case AnimalState.Walk:
                    return direction * MovementProperties.WalkSpeed;
            }

            return Vector2.zero;
        }

        private void UpdateState(Transform transform)
        {
            Vector2[] hits = Physics2D.CircleCastAll(transform.position, MovementProperties.LookRadius, Vector2.zero)
                                      .Where(hit => hit.transform != transform && !hit.collider.CompareTag(Idents.RABBIT_TAG) && !hit.collider.CompareTag(Idents.DEER_TAG))
                                      .Select(hit => hit.point)
                                      .ToArray();

            if (hits.Length == 0)
            {
                _movePoint = GetWalkPoint(transform.position, _movePoint, _walkRadius);
                Debug.DrawLine(transform.position, _movePoint, Color.cyan);
                CurrentState = AnimalState.Walk;

                return;
            }

            _targetPosition = GetEscapePoint(transform.position, hits, MovementProperties.RunSpeed);
            Debug.DrawLine(transform.position, _targetPosition, Color.magenta);
            CurrentState = AnimalState.Run;
        }

        private void OnDeerDestroyed(DeerController deer)
        {
            _deers.Remove(deer);

            if (_deers.Count == 0 && Transform)
            {
                Object.Destroy(Transform.gameObject);
            }
        }
    }
}