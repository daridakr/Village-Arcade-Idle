using System;
using Village.Character;

namespace Village
{
    public interface ISpecializationsController
    {
        public void SetupSpecializationsFor(object condition = null);
        public ICustomizableCharacter SelectSpecialization(ICharacterSpecialization specialization);
        public ICharacterSpecialization[] GetAllSpecializations();
        public ICharacterSpecialization Selected { get; }
        public event Action Initialized;
    }

    public interface ISpecializationGetter
    {
        public ICharacterSpecialization GetSpecialization(string guid);
    }
}