using Kloudscript.Quiz.ZipCode.Cache.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Kloudscript.Quiz.ZipCode.Cache.Models
{
    [ExcludeFromCodeCoverage]
    [Serializable()]
    public class Address
    {
        public Address()
        {
            this._ID = 1;
        }

        private int _ID;
        /// <summary>
        /// ID of this address
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _FirmName = "";
        /// <summary>
        /// Name of the Firm or Company
        /// </summary>
        public string FirmName
        {
            get { return _FirmName; }
            set { _FirmName = value; }
        }

        private string _Contact = "";
        /// <summary>
        /// The contact is used to send confirmation when a package is shipped
        /// </summary>
        public string Contact
        {
            get { return _Contact; }
            set { _Contact = value; }
        }

        private string _ContactEmail = "";
        /// <summary>
        /// The contacts email address is used to send delivery confirmation
        /// </summary>
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; }
        }


        private string _Address1 = "";
        /// <summary>
        /// Address Line 1 is used to provide an apartment or suite
        /// number, if applicable. Maximum characters allowed: 38
        /// </summary>
        public string Address1
        {
            get { return _Address1; }
            set 
            {
                if (value.Length > 38)
                    throw new USPSServiceException("Address1 is is limited to a maximum of 38 characters.");
                _Address1 = value; 
            }
        }

        private string _Address2 = "";
        /// <summary>
        /// Street address
        /// Maximum characters allowed: 38
        /// </summary>
        public string Address2
        {
            get { return _Address2; }
            set 
            {
                if (value.Length > 38)
                    throw new USPSServiceException("Address2 is is limited to a maximum of 38 characters.");
                _Address2 = value; 
            }
        }

        private string _City = "";
        /// <summary>
        /// City
        /// Either the City and State or Zip are required.
        /// Maximum characters allowed: 15
        /// </summary>
        public string City
        {
            get { return _City; }
            set 
            {
                if (value.Length > 15)
                    throw new USPSServiceException("City is is limited to a maximum of 15 characters.");
                _City = value; }
        }

        private string _State = "";
        /// <summary>
        /// State
        /// Either the City and State or Zip are required.
        /// Maximum characters allowed = 2
        /// </summary>
        public string State
        {
            get { return _State; }
            set 
            {
                if (value.Length > 2)
                    throw new USPSServiceException("State is is limited to a maximum of 2 characters.");
                _State = value; 
            }
        }

        private string _Zip = "";
        /// <summary>
        /// Zipcode
        /// Maximum characters allowed = 5
        /// </summary>
        public string Zip
        {
            get { return _Zip; }
            set 
            {
                if (value.Length > 5)
                    throw new USPSServiceException("Zip is is limited to a maximum of 5 characters.");
                _Zip = value; 
            }
        }

        private string _ZipPlus4 = "";
        /// <summary>
        /// Zip code extension
        /// Maximum characters allowed = 4
        /// </summary>
        public string ZipPlus4
        {
            get { return _ZipPlus4; }
            set 
            {
                if (value.Length > 5)
                    throw new USPSServiceException("Zip is is limited to a maximum of 5 characters.");
                _ZipPlus4 = value; 
            }
        }

        /// <summary>
        /// Get an Address object from an xml string.
        /// </summary>
        /// <param name="xml">XML representation of an Address Object</param>
        /// <returns>Address object</returns>
        public static Address FromXml(string xml)
        {
            Address a = new Address();

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xml);

            System.Xml.XmlNode element = doc.SelectSingleNode("/AddressValidateResponse/Address/FirmName");
            if (element != null)
                a._FirmName = element.InnerText;
            element = doc.SelectSingleNode("/AddressValidateResponse/Address/Address1");
            if (element != null)
                a._Address1 = element.InnerText;
            element = doc.SelectSingleNode("/AddressValidateResponse/Address/Address2");
            if (element != null)
                a._Address2 = element.InnerText;
            element = doc.SelectSingleNode("/AddressValidateResponse/Address/City");
            if (element != null)
                a._City = element.InnerText;
            element = doc.SelectSingleNode("/AddressValidateResponse/Address/State");
            if (element != null)
                a._State = element.InnerText;
            element = doc.SelectSingleNode("/AddressValidateResponse/Address/Zip5");
            if (element != null)
                a._Zip = element.InnerText;
            element = doc.SelectSingleNode("/AddressValidateResponse/Address/Zip4");
            if (element != null)
                a._ZipPlus4 = element.InnerText;

            return a;
        }
    }
}
