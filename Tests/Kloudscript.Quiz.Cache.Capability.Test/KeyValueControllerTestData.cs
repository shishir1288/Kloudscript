using Kloudscript.Quiz.Cache.Capability.Models;
using System.Collections.Generic;

namespace Kloudscript.Quiz.Cache.Capability.Test
{
    public class KeyValueControllerTestData
    {
        public List<KeyValue> GetCachedKeyValues()
        {
            return new List<KeyValue>()
            {
                new KeyValue()
                {
                    Key = "Key1",
                    Value = "Value1",
                    Expiration = 2 //has to be in interger but this is in hrs
                },

                new KeyValue()
                {
                    Key = "Key2",
                    Value = "Value2",
                    Expiration = 3 //has to be in interger but this is in hrs
                }
            };
        }
    }
}
