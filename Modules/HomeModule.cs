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

      Get["/band/{id}/venues/add"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> currentVenues = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();

        List<Venue> availableVenues = new List<Venue> {};
        foreach(Venue venue in allVenues)
        {
          // If a venue is already in the band it will return an index of 0 or above.
          if(currentVenues.IndexOf(venue) < 0)
          {
            availableVenues.Add(venue);
          }
        }
        model.Add("band", selectedBand);
        model.Add("available-venues", availableVenues);
        return View["AddVenueToBand.cshtml", model];
      };

      Get["/venue/{id}/bands/add"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> currentBands = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();

        List<Band> availableBands = new List<Band> {};
        foreach(Band band in allBands)
        {
          // If the venue is already enrolled in a band it will return an index of 0 or above.
          if(currentBands.IndexOf(band) < 0)
          {
            availableBands.Add(band);
          }
        }
        model.Add("venue", selectedVenue);
        model.Add("available-bands", availableBands);
        return View["AddBandToVenue.cshtml", model];
      };


    }
  }
}
