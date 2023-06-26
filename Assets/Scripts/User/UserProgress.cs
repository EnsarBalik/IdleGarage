using Newtonsoft.Json;

namespace Helpers.UserData
{
    public partial class UserProgress
    {
        [JsonProperty("user_id")] public string UserId;
        
        [JsonProperty("advertisement_id")] public string AdvertisementId;
        
        [JsonProperty("apps_flyer_id")] public string AppsFlyerId;
        
        [JsonProperty("last_asked_version")] public string LastAskedAppVersion;
    }
}