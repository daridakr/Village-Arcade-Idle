using UnityEngine;

namespace Village
{
    public abstract class MeshCustomizationConfig : CustomizationConfig
    {
        [SerializeField] private MeshFilter[] _meshes;
        public override Object[] Customs => _meshes;
    }
}