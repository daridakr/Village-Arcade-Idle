using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class CustomizableCharacter : MonoBehaviour
    {
        [SerializeField] private Renderer[] _bodyRenderers;
        [SerializeField] private Transform _headRig;
        public Renderer[] Body => _bodyRenderers;
    }
}