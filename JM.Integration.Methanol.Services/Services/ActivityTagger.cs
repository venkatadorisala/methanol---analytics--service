using JM.Integration.Methanol.Services.Interface;
using System.Diagnostics;

namespace JM.Integration.Methanol.Services.Services
{
    public class ActivityTagger : IActivityTagger
    {
        public void AddTag(string key, string value)
        {
            Activity.Current?.AddTag(key, value);
        }
    }
}