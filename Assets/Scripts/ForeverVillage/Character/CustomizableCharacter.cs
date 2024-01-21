using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class CustomizableCharacter : MonoBehaviour
    {
        [SerializeField] private Renderer[] _bodyRenderers;
        [SerializeField] private Transform _headRig;
        public Renderer[] Body => _bodyRenderers;

        //[Inject]
        //public void Construct(SkinColorCustomization skinColor, HeadCustomization headCustomization)
        //{
        //    _skinColor = skinColor;
        //    _skinColor.Customized += SetSkinColor;
        //}

        //private void SetSkinColor(Object material)
        //{
        //    if (_bodyRenderers == null)
        //        return;

        //    foreach (Renderer renderer in _bodyRenderers)
        //    {
        //        renderer.material = (Material)material;
        //    }
        //}

        //private void OnDestroy()
        //{
        //    _skinColor.Customized -= SetSkinColor;
        //}
    }
}