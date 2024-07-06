
using LamondLu.EmailX.Domain.Enum;
using Newtonsoft.Json;

namespace LamondLu.EmailX.Domain.ViewModels
{
    public class EmailConnectorStatusViewModel
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        [JsonIgnore()]
        public EmailConnectorStatus Status { get; set; }

        [JsonProperty("status")]
        public string StatusText
        {
            get
            {
                return Status == EmailConnectorStatus.Running ? "Running" : "Stopped";
            }
        }
    }
}