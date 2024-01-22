using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class MeshCustomizationConfig : CustomizationConfig
    {
        [SerializeField] private MeshFilter[] _meshes;
        public override Object[] Customs => _meshes;
    }
}