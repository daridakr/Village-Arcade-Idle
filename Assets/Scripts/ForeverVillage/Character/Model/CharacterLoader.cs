using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CharacterLoader
    {
        public CustomizableCharacter LoadCustomizable(string path)
        {
            return Resources.Load<CustomizableCharacter>(path);
        }
    }
}