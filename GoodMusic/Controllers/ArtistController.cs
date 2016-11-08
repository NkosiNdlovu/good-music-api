using Domain.Repositories;
using GoodMusic.Models;
using Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GoodMusic.Controllers
{
    public class ArtistController : ApiController
    {
        // GET api/artist
        [HttpGet]
        [Route("api/artist/Search/{searchCriteria}/{pageNumber}/{pageSize}")]
        [Route("api/artist/Search/{searchCriteria}")]
        public ArtistSearchApiModel Search(string searchCriteria, int pageNumber = 1, int pageSize = 10)
        {
            // TBD: inject the context
            using (UnitOfWork unitOfWork = new UnitOfWork(new ArtistDbContext()))
            {
                int TotalRecords = unitOfWork.Artists.FindArtistsThatStartWith(searchCriteria).Count();
                var artists = unitOfWork.Artists.GetPagedArtistsThatStartWith(searchCriteria,pageNumber,pageSize).ToList();
                ModelMapper mapper = new ModelMapper();
                var model = mapper.GetSearchViewModel(artists, pageNumber, pageSize, TotalRecords);
                return model;
            }
        }
        
        [HttpGet]
        [Route("api/artist/{artistId}/Releases")]
        public async Task<IHttpActionResult> Releases(string artistId)
        {
            List<string> releases = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "d-fens HttpClient");

                var response = await client.GetAsync("http://musicbrainz.org/ws/2/release/?query=arid:" + artistId.Trim());
                if (response.IsSuccessStatusCode)
                {
                    var xml = response.Content.ReadAsStringAsync().Result;

                    XNamespace def = "http://musicbrainz.org/ns/mmd-2.0#";
                    XElement root = XElement.Parse(xml);

                    var elements = root.Descendants(def + "title");

                    foreach (var element in elements)
                    {
                        releases.Add(element.Value);
                    }
                    return Ok(releases);
                }
                return NotFound();
            }
        }

        [HttpGet]
        [Route("artist/{artistId}/albums")]
        public async Task<IHttpActionResult> Albums(string artistId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "d-fens HttpClient");

                var response = await client.GetAsync("http://musicbrainz.org/ws/2/release/?query=arid:" + artistId.Trim());
                if (response.IsSuccessStatusCode)
                {
                    var xml = response.Content.ReadAsStringAsync().Result;

                    XNamespace def = "http://musicbrainz.org/ns/mmd-2.0#";
                    XDocument doc = XDocument.Parse(xml);
                    List<Album> albumsList = new List<Album>();

                    try
                    {
                        var albums = doc.Descendants(def + "release");

                        foreach (var item in albums)
                        {
                            Album album = new Album();
                            //read only albums
                            var type = (item.Descendants(def + "primary-type").ToList().Count == 0) ? " " : item.Descendants(def + "primary-type").ElementAt(0).Value.ToString();
                            if (type == "Album")
                            {
                                //add release id
                                album.ReleaseId = item.Attribute("id").Value.ToString();
                                //add album title
                                album.Title = (item.Descendants(def + "title").ToList().Count == 0) ? " " : item.Descendants(def + "title").ElementAt(0).Value.ToString();
                                //add album status
                                album.Status = (item.Descendants(def + "status").ToList().Count == 0) ? " " : item.Descendants(def + "status").ElementAt(0).Value.ToString();
                                //add no of tracks
                                album.NoOfTracks = (item.Descendants(def + "track-count").ToList().Count == 0) ? " " : item.Descendants(def + "track-count").ElementAt(0).Value.ToString();
                                //add labels
                                var label = item.Descendants(def + "label");
                                var labelList = "";
                                foreach (var label_item in label)
                                {
                                    labelList += label_item.Element(def + "name").Value.ToString() + ", ";
                                }
                                album.Label = labelList;
                                //add collaborating artists
                                var collaborators = item.Descendants(def + "artist");
                                List<OtherArtist> colArtist = new List<OtherArtist>();
                                foreach (var collaborator in collaborators)
                                {
                                    OtherArtist otherArtist = new OtherArtist();
                                    otherArtist.Id = collaborator.Attribute("id").Value.ToString();
                                    otherArtist.Name = collaborator.Element(def + "name").Value.ToString();
                                    colArtist.Add(otherArtist);
                                }
                                album.otherArtists = colArtist;
                                //add album to list
                                albumsList.Add(album);
                                album = null;
                                if (albumsList.Count == 10)
                                {
                                    break;
                                }
                            }
                        }
                        return Ok(albumsList);
                    }
                    catch (Exception)
                    {

                    }
                }
                return NotFound();
            }
        }
    }
}
