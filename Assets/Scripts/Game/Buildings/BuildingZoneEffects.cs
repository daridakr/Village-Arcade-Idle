using UnityEngine;

public class BuildingZoneEffects : MonoBehaviour
{
    [SerializeField] private BuildingZone _zone;
    [SerializeField] private SpriteRenderer _dottedSquare;
    [SerializeField] private ParticleSystem _freeEffect;
    [SerializeField] private ParticleSystem _buildEffect;

    private void OnEnable()
    {
        _zone.Cleared += OnZoneCleared;
    }

    private void OnZoneCleared()
    {
        _freeEffect.Stop();
    }

    private void OnZoneBuildingEffect()
    {
        _dottedSquare.enabled = false;
        _buildEffect.Play();
    }

    private void OnZoneBuilded()
    {
        _buildEffect.Stop();
    }
}
