using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CharacterLoader
    {
        public Character Load(string path)
        {
            return Resources.Load<Character>(path);
        }

        public Character LoadMaleCharacter()
        {
            return Resources.Load<Character>(ResourcesParams.Character.Gender.Male);
        }

        public Character LoadFemaleCharacter()
        {
            return Resources.Load<Character>(ResourcesParams.Character.Gender.Female);
        }
    }
}