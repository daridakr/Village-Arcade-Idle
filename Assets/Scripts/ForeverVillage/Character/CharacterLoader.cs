using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CharacterLoader
    {
        public Character Load(string path)
        {
            return Resources.Load<Character>(path);
        }

        public CustomizableCharacter LoadCustomizable(string path)
        {
            return Resources.Load<CustomizableCharacter>(path);
        }
    }
}