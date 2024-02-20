using System;
using Village.Character;

namespace Village
{
    public interface ISpecializationsController
    {
        public void SetupSpecializationsFor(object condition = null);
        public ICustomizableCharacter SelectSpecialization(ISpecialization specialization);
        public ISpecialization[] GetAllSpecializations();
        public ISpecialization GetSelectedSpecialization();
        public event Action Initialized;
    }
}