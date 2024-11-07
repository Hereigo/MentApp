namespace WebApi.ApiModels
{
    public class ConfigItems
    {
        private static int _defaultMaxValue;

        public ConfigItems() { }

        public ConfigItems(int defaultMaxValue)
        {
            _defaultMaxValue = defaultMaxValue;
        }

        public const string SectionItems = "items";

        public int Max { get; set; } = _defaultMaxValue;
    }
}
