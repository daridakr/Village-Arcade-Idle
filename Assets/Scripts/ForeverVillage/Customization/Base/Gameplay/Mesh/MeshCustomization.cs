using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class MeshCustomization : Customization
    {
        private readonly MeshCustomizationConfig _config;

        protected MeshFilter _meshFilter;

        public override Object[] Customs => _config.Customs;

        public MeshCustomization(MeshCustomizationConfig config) : base(config)
        {
            _config = config;
        }

        public override void ApplyCustom()
        {
            if (_meshFilter == null)
                return;

            MeshFilter meshFilter = (MeshFilter)Customs[Index];
            _meshFilter.sharedMesh = meshFilter.sharedMesh;
        }
    }
}