using Newtonsoft.Json;

namespace Showcase_Profielpagina.Models;

public class RecaptchaResponse {
    [JsonProperty("success")] public bool Success { get; set; }

    [JsonProperty("challenge_ts")] public string ChallengeTs { get; set; }

    [JsonProperty("hostname")] public string Hostname { get; set; }
}