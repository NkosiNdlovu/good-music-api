using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GoodMusic.Models
{
    public class MusicbrainzWebServiceClient
    {
        public void GetReleaseInfo(string artist_Id)
        {
            try
            {
                var request = WebRequest.Create(string.Format("http://musicbrainz.org/ws/2/release/?query=arid:{0}", artist_Id)) as HttpWebRequest;
                if (request == null)
                    //return new XElement("");

                request.Accept = "*/*";
                request.UserAgent = "MusicBrainze.API/2.0";
                request.Proxy = WebRequest.DefaultWebProxy;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.UseDefaultCredentials = true;

                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    var doc = XDocument.Load(stream);
                    var xsn = new XmlSerializerNamespaces();
                    xsn.Add("ext", "http://musicbrainz.org/ns/ext#-2.0");
                    //return xml.Root != null ? xml.Root.Elements().FirstOrDefault() : new XElement("");
                    var serializer = new XmlSerializer(typeof(Release));

                    foreach (XElement element in doc.Root.Elements().Elements())
                    {
                        serializer.Deserialize(stream);
                    }

                    var test = from c in doc.Root.Descendants("release")
                    select new
                    {
                        firstname = c.Element("status"),
                        surnmane = c.Element("title")
                    };
                }
            }
            catch (Exception ex)
            {
                //return new XElement("");
            }
        }


    }
}