using UnityEngine;

namespace Village
{
    public class BuildingZoneEffects : MonoBehaviour
    {
        [SerializeField] private BuildingZone _zone;
        [SerializeField] private SpriteRenderer _dottedSquare;
        [SerializeField] private ParticleSystem _freeEffect;
        [SerializeField] private ParticleSystem _buildEffect;

        private void OnEnable()
        {
            _zone.Cleared += OnZoneCleared;
            _zone.Building += OnZoneBuildingEffect;
            _zone.Builded += OnZoneBuilded;
        }

        private void OnZoneCleared()
        {
            _zone.Cleared -= OnZoneCleared;
            _freeEffect.Stop();
        }

        private void OnZoneBuildingEffect()
        {
            _zone.Building -= OnZoneBuildingEffect;
            _buildEffect.Play();
        }

        private void OnZoneBuilded()
        {
            _zone.Builded -= OnZoneBuilded;
            _dottedSquare.enabled = false;
            _buildEffect.Stop();
        }
    }
}