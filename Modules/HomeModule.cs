using Nancy;
using System.Collections.Generic;
using System;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };

      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };

      Get["/venues/new"] = _ => {
        return View["venue_form.cshtml"];
      };

      Get["/bands/new"] = _ => {
        return View["band_form.cshtml"];
      };

      Post["/venues/new"] = _ => {
        string venueName = Request.Form["venue-name"];
        string venueAddress = Request.Form["venue-address"];
        Venue newVenue = new Venue(venueName, venueAddress);
        newVenue.Save();
        return View["venue.cshtml", newVenue];
      };

      Post["/bands/new"] = _ => {
        string bandName = Request.Form["band-name"];
        string bandType = Request.Form["band-type"];
        Band newBand = new Band(bandName, bandType);
        newBand.Save();
        return View["band.cshtml", newBand];
      };

    }
  }
}
