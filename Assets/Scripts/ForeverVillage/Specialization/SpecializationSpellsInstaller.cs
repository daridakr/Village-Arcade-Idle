using Village;
using Zenject;

namespace Arena
{
    public sealed class SpecializationSpellsInstaller :
        IInitializable
    {
        private readonly ISpellsSetupper _spellsSetupper;
        private readonly SpecializationInstaller _specInstaller;

        public SpecializationSpellsInstaller(
            ISpellsSetupper spellsSetupper,
            SpecializationInstaller specializationInstaller)
        {
            _spellsSetupper = spellsSetupper;
            _specInstaller = specializationInstaller;
        }

        public void Initialize() => _spellsSetupper.Setup(_specInstaller.Specialization.Data.Spells);
    }
}