


namespace Auth.Core.Requests
{

    public class AddSystemSettingRequest
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
    }
}