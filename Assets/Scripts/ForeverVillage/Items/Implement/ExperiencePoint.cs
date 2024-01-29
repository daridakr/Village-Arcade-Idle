using UnityEngine;

namespace Village
{
    public class ExperiencePoint : Item
    {
        [SerializeField] private int _experience = 1;

        public int Experience => _experience;
    }
}
