using Zenject;

namespace Village
{
    public class VillagersStore : Store<Villager>
    {
        private PlayerVillagersList _villagersList;

        protected override DataList<Villager> DataList => _villagersList;

        [Inject]
        private void Construct(PlayerVillagersList villagersList)
        {
            _villagersList = villagersList;
        }
    }
}