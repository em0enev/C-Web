using System.Linq;
using System.Net;
using System.Web;
using IRunes.Models;
using SIS.WebServer;

namespace IRunes.App.Extensions
{
    public static class EntityExtensions
    {
        public static string ToHtmlAll(this Album album)
        {
            return $"<div><a href=\"/Albums/Details?id={album.Id}\">{HttpUtility.UrlDecode(album.Name)}</a></div>";
        }
        public static string ToHtmlDetails(this Album album)
        {
            return $"<div class=\"album-details\">" +
                   $"   <div class=\"album-data\">" +
                   $"      <a><image src=\"{HttpUtility.UrlDecode(album.Cover)}\"</br>" +
                   $"      <h1>Album name: {HttpUtility.UrlDecode(album.Name)}</h1>" +
                   $"      <h1>Album price: ${album.Price:f2}</h1>" +
                   $"      <br />" +
                   $"   </div>" +
                   $"   <div class=\"album-tracks\">" +
                   $"       <h1>Tracks</h1>" +
                   $"       <hr style=\"height: 2px\" />" +
                   $"       <a href=\"/Tracks/Create?albumId={album.Id}\">Create Track</a>" +
                   $"       <hr style=\"height: 2px\" />" +
                   $"       <ul class=\"track-list\">" +
                   $"           {GetTracksFromAlbum(album)}" +
                   $"       </ul>" +
                   $"   </div>" +
                   $"</div>";
        }

        public static string ToHtmlAll(this Track track,string albumId, int index)
        {
            return $"<li><a href=\"/Tracks/Details?albumId={albumId}&trackId={track.Id}\">{index}. {HttpUtility.UrlDecode(track.Name)}\"</li>";
        }

        public static string ToHtmlDetails(this Track track)
        {
            return "<div class=\"track-details\">" +
                   $"    <iframe src=\"{WebUtility.UrlDecode(track.Link)}\"></iframe>" +
                   $"    <h1>Track name: {HttpUtility.UrlDecode(track.Name)}</h1>" +
                   $"    <h1>Track price: ${track.Price:f2}</h1>" +
                   "</div>";
        }

        private static string GetTracksFromAlbum(Album album)
        {
            if (album.Tracks.Count != 0)
            {
                return string.Join(string.Empty, (album.Tracks.Select((track, index) => track.ToHtmlAll(album.Id,index + 1))));
            }
            else
            {
                return "<a>No tracks in album</a>";
            }
        }
    }
}
