using Newtonsoft.Json;

namespace Helpers.UserData
{
    public partial class UserProgress
    {
        [JsonProperty("level")] public int Level = 1;
        [JsonProperty("coin")] public int Coin;
    }
}