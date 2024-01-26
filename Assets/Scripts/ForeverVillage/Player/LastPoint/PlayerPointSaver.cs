using Zenject;

namespace ForeverVillage.Scripts
{
    public class PlayerPointSaver :
        IGameSaveDataListener
    {
        private PlayerPointRepository _repository;
        private PlayerInstanceInfo _player;

        [Inject]
        public void Construct(PlayerPointRepository repository, PlayerInstanceInfo player)
        {
            _repository = repository;
            _player = player;
        }

        public void OnSaveData(GameSaveReason reason)
        {
            var playerPointData = new PlayerPointData
            {
                X = _player.transform.position.x,
                Z = _player.transform.position.z
            };

            _repository.Save(playerPointData);
        }
    }
}