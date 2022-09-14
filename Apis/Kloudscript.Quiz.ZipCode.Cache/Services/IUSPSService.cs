using Kloudscript.Quiz.ZipCode.Cache.Models;
using System.Threading.Tasks;

namespace Kloudscript.Quiz.ZipCode.Cache.Services
{
    public interface IUSPSService
    {
        bool TestMode { get; set; } 
        Task<string> GetCityState(string zipcode);
    }
}