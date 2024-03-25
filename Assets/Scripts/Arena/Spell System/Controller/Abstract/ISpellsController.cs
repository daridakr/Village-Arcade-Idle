namespace Arena
{
    public interface ISpellsController :
        IInitilizable
    {
        public void ActivateSpellAtIndex(int index);
        public Spell[] GetAllActiveSpells();
    }
}