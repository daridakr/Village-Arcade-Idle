using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class ExperiencePoint : Item
    {
        [SerializeField] private int _experience = 1;

        public int Experience => _experience;
    }
}
