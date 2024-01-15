using Zenject;

namespace ForeverVillage.Scripts
{
    public class VillagersStore : Store<Villager>
    {
        private PlayerVillagersList _villagersList;

        protected override DataList<Villager> DataList => _villagersList;

        [Inject]
        public void Construct(PlayerVillagersList villagersList)
        {
            _villagersList = villagersList;
        }
    }
}