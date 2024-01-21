using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class MaterialCustomizationConfig : CustomizationConfig
    {
        [SerializeField] private Material[] _materials;
        public override Object[] Customs => _materials;
    }
}