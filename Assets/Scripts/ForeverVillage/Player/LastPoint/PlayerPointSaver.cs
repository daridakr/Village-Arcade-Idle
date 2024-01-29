using System;
using Zenject;

namespace Village
{
    public class PlayerPointSaver :
        IGameSaveDataListener,
        IDisposable
    {
        private PlayerPointRepository _repository;
        private global::PlayerReference _player;

        [Inject]
        public void Construct(PlayerPointRepository repository, global::PlayerReference player)
        {
            _repository = repository;
            _player = player;
        }

        public void Dispose()
        {
            var playerPointData = new PlayerPointData
            {
                X = _player.transform.position.x,
                Z = _player.transform.position.z
            };

            _repository.Save(playerPointData);
        }

        public void OnSaveData(GameSaveReason reason)
        {
            
        }
    }
}