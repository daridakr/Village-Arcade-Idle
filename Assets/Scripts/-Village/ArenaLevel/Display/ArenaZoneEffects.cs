using UnityEngine;

namespace Village
{
    public class ArenaZoneEffects : MonoBehaviour
    {
        [SerializeField] private ArenaZone _zone;
        [SerializeField] private SpriteRenderer _zoneSquare;
        [SerializeField] private ParticleSystem _effect;
    }
}