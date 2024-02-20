using UnityEngine;

namespace Village.Character
{
    public interface ICustomizableSkinCharacter
    {
        public Renderer[] SkinRenderers { get; }
    }
}