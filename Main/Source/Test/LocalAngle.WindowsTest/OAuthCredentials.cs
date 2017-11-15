using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LocalAngle.WindowsTest
{

    internal class OAuthCredentials : IOAuthCredentials
    {
        private string _consumerKey;
        public string ConsumerKey
        {
            get { return _consumerKey; }
        }

        private string _consumerSecret;
        public string ConsumerSecret
        {
            get { return _consumerSecret; }
        }

        private string _token;
        public string Token
        {
            get { return _token; }
        }

        private string _tokenSecret;
        public string TokenSecret
        {
            get { return _tokenSecret; }
        }

        private static OAuthCredentials _default;
        public static IOAuthCredentials Default 
        { 
            get
            {
                if ( _default == null)
                {
                    _default = new OAuthCredentials() { _consumerKey = ConfigurationManager.AppSettings["ConsumerKey"], _consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"], _token = ConfigurationManager.AppSettings["Token"], _tokenSecret = ConfigurationManager.AppSettings["TokenSecret"] };
                }

                return _default;
            }
        }
    }
}
