using System;

namespace Kloudscript.Quiz.Cache.Capability.Models
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Expiration { get; set; }
    }
}
