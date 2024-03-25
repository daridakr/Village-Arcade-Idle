using UnityEngine;

namespace Arena
{
    public class CharacterDeathHandler : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private GameObject[] _gameObjectsToDisable;
        [SerializeField] private Behaviour[] _componentsToDisable;
        [SerializeField] private Component[] _componentsDestroy;

        private void OnEnable() => _health.Emptied += OnCharacterDeath;

        private void OnCharacterDeath()
        {
            _health.Emptied -= OnCharacterDeath;

            foreach (var item in _gameObjectsToDisable)
            {
                item.gameObject.SetActive(false);
            }

            foreach (var item in _componentsToDisable)
            {
                item.enabled = false;
            }

            foreach (var item in _componentsDestroy)
            {
                Destroy(item);
            }
        }
    }
}