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

        private readonly Transform _transform;
        private readonly MovementProperties _movementProperties;
        private Vector2 _movePoint;
        private Vector2 _targetPosition;
        private readonly float _walkRadius;
        private readonly float _spawnDistance;

        public HerdBrainController(Transform transform, MovementProperties movementProperties, float walkRadius, float spawnDistance)
        {
            _transform = transform;
            _spawnDistance = spawnDistance;
            _movementProperties = movementProperties;
            _walkRadius = walkRadius;

            _deers = new List<DeerController>();
            _offsets = new List<Vector2>();
            _regroupTimers = new List<float>();
        }

        public void SpawnMembers(DeerUnity prefab, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 spawnPos = (Vector2) _transform.position + Random.insideUnitCircle * _spawnDistance;
                DeerController deer = Object.Instantiate(prefab, spawnPos, Quaternion.identity).Init();
                deer.Destroyed += () => _deers.Remove(deer);
                _deers.Add(deer);
                _offsets.Add(Random.insideUnitCircle * 5);
                _regroupTimers.Add(Random.Range(3f, 8f));
            }
        }

        public override void Update()
        {
            UpdateState(_transform);
            _transform.position += (Vector3) GetTargetVelocity(_transform.position) * Time.deltaTime;

            Vector2 position = _transform.position;

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
                    return direction * _movementProperties.RunSpeed;

                case AnimalState.Walk:
                    return direction * _movementProperties.WalkSpeed;
            }

            return Vector2.zero;
        }

        private void UpdateState(Transform transform)
        {
            Vector2[] hits = Physics2D.CircleCastAll(transform.position, _movementProperties.LookRadius, Vector2.zero)
                                      .Where(hit => hit.transform != transform && !hit.collider.CompareTag(Idents._RABBIT_TAG) && !hit.collider.CompareTag(Idents._DEER_TAG))
                                      .Select(hit => hit.point)
                                      .ToArray();

            if (hits.Length == 0)
            {
                _movePoint = GetWalkPoint(transform.position, _movePoint, _walkRadius);
                Debug.DrawLine(transform.position, _movePoint, Color.cyan);
                CurrentState = AnimalState.Walk;

                return;
            }

            _targetPosition = GetEscapePoint(transform.position, hits, _movementProperties.RunSpeed);
            Debug.DrawLine(transform.position, _targetPosition, Color.magenta);
            CurrentState = AnimalState.Run;
        }
    }
}