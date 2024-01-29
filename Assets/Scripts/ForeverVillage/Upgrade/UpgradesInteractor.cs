using System;
using Zenject;

namespace Village
{
    public sealed class UpgradesInteractor : 
        IInitializable,
        IDisposable
    {
        private IUpgradesRepository _repository;
        private IUpgradesController _controller;
        private UpgradesSaver _saver;

        public UpgradesInteractor(IUpgradesRepository repository, IUpgradesController controller)
        {
            _repository = repository;
            _controller = controller;
        }

        public void Initialize()
        {
            _saver = new UpgradesSaver(_repository, _controller);

            var upgradesInstaller = new UpgradesInstaller(_controller);
            if (_repository.Load(out var upgradesData))
            {
                upgradesInstaller.Install(upgradesData);
            }
        }

        public void Dispose()
        {
            _saver.Save();
        }
    }
}