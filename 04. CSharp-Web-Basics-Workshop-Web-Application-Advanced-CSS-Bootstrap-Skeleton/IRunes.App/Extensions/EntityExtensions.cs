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
            return $"            <div class=\"album - details d - flex justify - content - between row\">" +
                $"<div class=\"album-data col-md-5\">" +
                $"   <img src = \"{HttpUtility.UrlDecode(album.Cover)}\" class=\"img-thumbnail\"/>" +
                $"   <h1 class=\"text-center\">Album Name: {HttpUtility.UrlDecode(album.Name)}</h1>" +
                $"   <h1 class=\"text-center\">Album Price: ${album.Price}</h1>" +
                $"   <div class=\"d-flex justify-content-between\">" +
                $"       <a class=\"btn btn-success text-white\" href=\"/Tracks/Create?albumId={album.Id}\">Create Track</a>" +
                $"       <a class=\"btn btn-success text-white\" href=\"/Albums/All\">Back to all</a>" +
                $"   </div>" +
                $"</div>" +
                $"<div class=\"album-track col-md-6\">" +
                $"       <h1>Tracks</h1>" +
                $"       {GetTracksFromAlbum(album)} " +
                $"</div>";
        }

        public static string ToHtmlAll(this Track track, string albumId, int index)
        {
            return $"<li><a href=\"/Tracks/Details?albumId={albumId}&trackId={track.Id}\"><strong class=\"text-dark\">{index}. </strong><i>{HttpUtility.UrlDecode(track.Name)}</i>\"</li>";
        }

        public static string ToHtmlDetails(this Track track)
        {
            return
                $"       <div class=\"text-center\">" +
                $"           <p><strong>Track Name: {WebUtility.UrlDecode(track.Name)}</strong></p>" +
                $"           <p><strong>Track Price: ${track.Price}</strong></p>" +
                $"       </div>" +
                $"   <hr class=\"bg-success\" style=\"height: 2px\" />" +
                $"    <div class=\"embed-responsive embed-responsive-16by9\">" +
                $"   <iframe class=\"embed - responsive - item\" src =\"{WebUtility.UrlDecode(track.Link)}\"  ></iframe>" +
                $"   </div>";
        }

        private static string GetTracksFromAlbum(Album album)
        {
            if (album.Tracks.Count != 0)
            {
                return string.Join(string.Empty, (album.Tracks.Select((track, index) => track.ToHtmlAll(album.Id, index + 1))));
            }
            else
            {
                return "<a>No tracks in album</a>";
            }
        }
    }
}
