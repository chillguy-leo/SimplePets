using Exiled.API.Features;
using System;
using System.Net.Http;

namespace SimplePets
{
    public class ProfanityHandler
    {
        public bool CheckProfanity(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://www.purgomalum.com/service/containsprofanity?text={Uri.EscapeDataString(text)}";

                try
                {
                    string result = client.GetStringAsync(url).GetAwaiter().GetResult();
                    return result.Contains("true");
                }
                catch (Exception ex)
                {
                    Log.Error($"SimplePets ProfanityHandler Error: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
