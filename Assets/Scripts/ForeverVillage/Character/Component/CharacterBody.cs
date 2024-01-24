using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [RequireComponent(typeof(Renderer))]
    public sealed class CharacterBody : MonoBehaviour
    {
        [SerializeField] private bool _isSkin = true;

        private Renderer _renderer;

        public Renderer Renderer => _renderer;
        public bool IsSkin => _isSkin;
        public void Initialize() => _renderer = GetComponent<Renderer>();
    }
}