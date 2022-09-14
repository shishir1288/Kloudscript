using Kloudscript.Quiz.ZipCode.Cache.Exceptions;
using Kloudscript.Quiz.ZipCode.Cache.Models;
using Kloudscript.Quiz.ZipCode.Cache.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kloudscript.Quiz.ZipCode.Cache.Services
{
    public class USPSService : IUSPSService
    {
        #region Private Members 
        private readonly IOptions<Applicationsettings> _applicationSettings;
        private WebClient web;
        #endregion

        #region Constructors
        public USPSService(IOptions<Applicationsettings> applicationSettings)
        {
            web = new WebClient();
            _applicationSettings = applicationSettings;
        }

        #endregion

        #region Properties
        private bool _TestMode;
        /// <summary>
        /// Determines if the Calls to the USPS server is made to the Test or Production server.
        /// </summary>
        public bool TestMode
        {
            get { return _TestMode; }
            set { _TestMode = value; }
        }

        #endregion

        #region Address Methods
        /// <summary>
        /// Get the city and state by proving the zip code.
        /// </summary>
        /// <param name="zipcode">Zipcode</param>
        public Task<string> GetCityState(string zipcode)
        {
            Address a = new Address();
            a.Zip = zipcode;
            return Task.FromResult(GetCityState(a));
        }

        /// <summary>
        /// Get the city and state by proving the zip code.
        /// </summary>
        /// <param name="address">Address object</param>
        /// <returns>Address Object</returns>
        private string GetCityState(Address address)
        {
            try
            {
                //The address must contain a city and state
                if (address.Zip == null || address.Zip.Length < 1)
                    throw new USPSServiceException("You must supply a zipcode for a city/state lookup request.");

                string citystateurl = "?API=CityStateLookup&XML=<CityStateLookupRequest USERID=\"{0}\"><ZipCode ID= \"{1}\"><Zip5>{2}</Zip5></ZipCode></CityStateLookupRequest>";
                string url = _applicationSettings?.Value.Url + citystateurl;
                url = String.Format(url, _applicationSettings?.Value.UserId, address.ID.ToString(), address.Zip);
                string addressXml = web.DownloadString(url);
                if (addressXml.Contains("<Error>"))
                {
                    int idx1 = addressXml.IndexOf("<Description>") + 13;
                    int idx2 = addressXml.IndexOf("</Description>");
                    int l = addressXml.Length;
                    string errDesc = addressXml.Substring(idx1, idx2 - idx1);
                    throw new USPSServiceException(errDesc);
                }

                if (!string.IsNullOrEmpty(addressXml))
                {
                    var xdoc = XDocument.Parse(addressXml);
                    //extracting state from the addressXML
                    string state = xdoc.Descendants("ZipCode").Select(e => e.Element("State").Value).FirstOrDefault();

                    if (state != null)
                        return state;
                }

                return ""; //if nothing has been found
            }
            catch (WebException ex)
            {
                throw new USPSServiceException(ex);
            }
        }
        #endregion
    }
}
