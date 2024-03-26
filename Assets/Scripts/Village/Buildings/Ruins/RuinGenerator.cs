using System;
using UnityEngine;

namespace Village
{
    public class RuinGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Collider _areaBounds;

        [SerializeField] private GameObject _terrain;
        [SerializeField] private RuinObject[] _objects;

        private int _currentObjectIndex = 0;
        private float _currentObjectScale = 1f;

        private UniformObjectSpawner _uniformSpawner;

        private void Start()
        {
            _uniformSpawner = new UniformObjectSpawner(_areaBounds, _container);

            Generate();
        }

        private void Generate()
        {
            Instantiate(_terrain, _container);

            CreateObjects();
        }

        private void CreateObjects()
        {
            foreach (var @object in _objects)
            {
                for (int i = 0; i < @object.Amount; i++)
                {
                    GenerateNew(@object);
                    GameObject created = _uniformSpawner.SpawnObject(@object.Prefabs[_currentObjectIndex]);
                    created.transform.localScale = new Vector3(_currentObjectScale, _currentObjectScale, _currentObjectScale);
                }
            }
        }

        private void GenerateNew(RuinObject ruinObject)
        {
            _currentObjectIndex = UnityEngine.Random.Range(0, ruinObject.Prefabs.Length - 1);
            _currentObjectScale = UnityEngine.Random.Range(ruinObject.MinScale, ruinObject.MaxScale);
        }
    }

    [Serializable]
    public struct RuinObject
    {
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;
        [SerializeField] private int _amount;

        public GameObject[] Prefabs => _prefabs;
        public float MinScale => _minScale;
        public float MaxScale => _maxScale;
        public int Amount => _amount;
    }
}