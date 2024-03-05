namespace Village
{
    public sealed class Wood : ForeverVillage.Item
    {
        private readonly WoodItemConfig _config;

        public Wood(WoodItemConfig config) : base(config)
        {
            _config = config;
        }
    }
}