using System.Collections.Generic;

namespace Kloudscript.Quiz.ZipCode.Cache.Test
{
    public class StateLookUpControllerTestData
    {
        public List<StateResponse> GetCachedStatesByZipCode()
        {
            return new List<StateResponse>() {
                     new StateResponse(){
                         State ="CA",
                         ZipCode = "90210"
                     },
                     new StateResponse(){
                         State ="UT",
                         ZipCode = "84604"
                     }
            };
        }
    }

    public class StateResponse
    {
        public string ZipCode { get; set; }
        public string State { get; set; }
    }
}
