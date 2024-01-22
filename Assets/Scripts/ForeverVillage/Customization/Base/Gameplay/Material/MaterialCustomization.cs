using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class MaterialCustomization : Customization
    {
        private readonly MaterialCustomizationConfig _config;

        protected List<Renderer> _renderers;

        public override Object[] Customs => _config.Customs;

        public MaterialCustomization(MaterialCustomizationConfig config) : base(config)
        {
            _config = config;
            _renderers = new List<Renderer>();
        }

        public override void ApplyCustom(int index)
        {
            if (_renderers == null)
                return;

            foreach (Renderer renderer in _renderers)
            {
                renderer.material = (Material)Customs[index];
            }
        }
    }
}