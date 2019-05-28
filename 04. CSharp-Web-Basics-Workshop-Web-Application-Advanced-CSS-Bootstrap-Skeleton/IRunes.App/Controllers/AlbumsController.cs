﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
    public class AlbumsController : BaseController
    {
        public IHttpResponse All(IHttpRequest request)
        {

            if (!this.IsLoggedIn(request))
            {
                return this.Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                ICollection<Album> allAlbums = context.Albums.ToList();

                if (allAlbums.Count == 0)
                {
                    this.ViewData["Albums"] = "There are currently no albums.";
                }
                else
                {
                    this.ViewData["Albums"] = string.Join("<br />",
                        context.Albums.Select(album => album.ToHtmlAll()).ToList());
                }

                return this.View();
            }
        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsLoggedIn(request))
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public IHttpResponse CreateConfirm(IHttpRequest request)
        {
            if (!this.IsLoggedIn(request))
            {
                return this.Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                string name = ((ISet<string>)request.FormData["name"]).FirstOrDefault();
                string cover = ((ISet<string>)request.FormData["cover"]).FirstOrDefault();

                Album album = new Album()
                {
                    Name = name,
                    Cover = cover,
                    Price = 0M
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }

            return this.Redirect("/Albums/All");
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            if (!this.IsLoggedIn(request))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = request.QueryData["id"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums
                    .Include(a => a.Tracks)
                    .FirstOrDefault(a => a.Id == albumId);

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                this.ViewData["Album"] = albumFromDb.ToHtmlDetails();
            }
            

            return this.View();

        }
    }
}
