using UnityEngine;

namespace Village.Character
{
    public interface ICustomizableFaceCharacter :
        ICustomizableBrowsCharacter, ICustomizableEyesCharacter,
        ICustomizableHairCharacter, ICustomizableMouthCharacter
    {
        public MeshFilter HeadMesh { get; }
    }
}