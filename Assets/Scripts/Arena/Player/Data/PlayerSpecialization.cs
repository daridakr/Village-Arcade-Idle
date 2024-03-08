using UnityEngine;

namespace Village
{
    public class PlayerSpecialization : MonoBehaviour
    {
        private Specialization _specialization;

        public void Setup(Specialization specialization)
        {
            if (specialization == null)
                return;
            
            _specialization = specialization;
        }
    }
}