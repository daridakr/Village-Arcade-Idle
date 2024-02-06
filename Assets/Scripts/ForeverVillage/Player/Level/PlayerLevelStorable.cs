namespace Village
{
    public sealed class PlayerLevelStorable : PlayerLevel,
        IGameSaveDataListener
    {
        protected override void Initialize()
        {
            _level = new ExperienceLevel(SaveKeyParams.Player.ExperienceLevel);
            _level.Load();
        }

        private void Save() => _level.Save();
        public void OnSaveData(GameSaveReason reason) => Save();
        protected override void OnValueChanged() => Save();
        protected override void OnDisabled() => Save();
    }
}