using UnityEngine;

namespace Village.Character
{
    public interface ICustomizableHairCharacter
    {
        public MeshFilter HairMesh { get; }
        public Renderer[] HairRenderers { get; }
    }
}