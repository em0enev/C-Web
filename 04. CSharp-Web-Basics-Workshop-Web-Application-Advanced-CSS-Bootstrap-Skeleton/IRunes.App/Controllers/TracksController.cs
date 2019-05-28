using System.Collections.Generic;
using System.Linq;
using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["albumId"].ToString();
            this.ViewData["AlbumId"] = albumId;

            return this.View();
        }

        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["albumId"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums
                    .Include(a => a.Tracks)
                    .FirstOrDefault(a => a.Id == albumId);

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                string name = ((ISet<string>)httpRequest.FormData["name"]).FirstOrDefault();
                string link = ((ISet<string>)httpRequest.FormData["link"]).FirstOrDefault();
                string price = ((ISet<string>)httpRequest.FormData["price"]).FirstOrDefault();

                Track track = new Track()
                {
                    Name = name,
                    Link = link,
                    Price = decimal.Parse(price),
                };

                decimal reducePriceBy13Percent = 0.87m;

                albumFromDb.Tracks.Add(track);

                albumFromDb.Price = albumFromDb.Tracks
                                        .Select(t => t.Price)
                                        .Sum() * reducePriceBy13Percent;

                context.Update(albumFromDb);
                context.SaveChanges();
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["albumId"].ToString();
            string trackId = httpRequest.QueryData["trackId"].ToString();

            using (var context = new RunesDbContext())
            {
                Track trackFromDb = context.Tracks.FirstOrDefault(a => a.Id == trackId);

                if (trackFromDb == null)
                {
                    return this.Redirect($"/Albums/Details?id={albumId}");
                }

                this.ViewData["Track"] = trackFromDb.ToHtmlDetails();
                this.ViewData["AlbumId"] = albumId;
            }

            return this.View();
        }
    }
}
