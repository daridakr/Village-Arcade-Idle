using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class MaterialCustomization : Customization
    {
        protected List<Renderer> _renderers;
        private readonly MaterialCustomizationConfig _config;
        public override UnityEngine.Object[] Customs => _config.Customs;

        public override event Action<UnityEngine.Object> Customized;

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
            //Customized?.Invoke(Customs[index]);
        }
    }
}