using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CharacterCreator
    {
        private string _folder;

        public CharacterCreator(string folderPath) => _folder = folderPath;

        public void Create(CustomizableCharacter character)
        {
            //Resources.
            //Resources.Load<CustomizableCharacter>(path);
        }
    }
}