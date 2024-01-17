using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class Character : MonoBehaviour
    {
        private Gender _gender;

        public Gender Gender => _gender;

        public void SetGender(Gender gender)
        {
            _gender = gender;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}