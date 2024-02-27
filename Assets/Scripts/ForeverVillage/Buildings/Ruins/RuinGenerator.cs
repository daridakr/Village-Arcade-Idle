using UnityEngine;

namespace Village
{
    public class RuinGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        [SerializeField] private GameObject _terrain;
        [SerializeField] private Transform[] _rockPoints;
        [SerializeField] private GameObject[] _rocks;
        [SerializeField] private float _minRocksScale = 0.5f;
        [SerializeField] private float _maxRocksScale = 1f;

        [SerializeField] private Transform[] _debrisPoints;
        [SerializeField] private GameObject[] _debris;

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            Instantiate(_terrain, _container);

            int randomIndex = 0;
            float randomScale = 0;

            foreach (var point in _rockPoints)
            {
                randomIndex = Random.Range(0, _rocks.Length - 1);
                randomScale = Random.Range(_minRocksScale, _maxRocksScale);

                GameObject rock = Instantiate(_rocks[randomIndex], point);
                rock.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            }

            foreach (var point in _debrisPoints)
            {
                randomIndex = Random.Range(0, _debris.Length - 1);

                GameObject debris = Instantiate(_debris[randomIndex], point);
                debris.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            }
        }
    }
}