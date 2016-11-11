using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodMusic.Models
{
    public class ModelMapper
    {
        public ArtistSearchApiModel GetSearchViewModel(List<Artist> artists,int page,int pageize, int totalRecords)
        {
            ArtistSearchApiModel artistSearchViewModel = new ArtistSearchApiModel();
            artistSearchViewModel.results = new List<ArtistApiModel>();
            if (totalRecords % pageize == 0)
            {
                artistSearchViewModel.numberOfPages = Convert.ToInt32(totalRecords / pageize).ToString();
            }else
            {
                artistSearchViewModel.numberOfPages = (Convert.ToInt32(totalRecords / pageize) + 1).ToString();
            }
            artistSearchViewModel.pageSize = pageize.ToString();
            artistSearchViewModel.page = page.ToString();
            artistSearchViewModel.numberOfSearchResults = totalRecords;

            foreach (var artist in artists)
            {
                ArtistApiModel ArtistVM = new ArtistApiModel();
                ArtistVM.Artist_Id = artist.Artist_Id.ToString();
                if (artist.Aliases != null)
                    ArtistVM.Aliases = artist.Aliases.Select(a => a.Name).ToList();
                ArtistVM.Country = artist.Country;
                ArtistVM.Name = artist.Name;
                artistSearchViewModel.results.Add(ArtistVM);
            }
            return artistSearchViewModel;
        }
    }
}