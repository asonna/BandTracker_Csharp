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

      Get["/venues/{id}/bands/add"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> currentBands = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();

        List<Band> availableBands = new List<Band> {};
        foreach(Band band in allBands)
        {
          // If the band is already enrolled in a venue it will return an index of 0 or above.
          if(currentBands.IndexOf(band) < 0)
          {
            availableBands.Add(band);
          }
        }
        model.Add("venue", selectedVenue);
        model.Add("available-bands", availableBands);
        return View["AddBandToVenue.cshtml", model];
      };

      Get["/venue/{venue_id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.venue_id);
        return View["venue.cshtml", selectedVenue];
      };

      Get["/band/{band_id}"] = parameters => {
        Band selectedBand = Band.Find(parameters.band_id);
        return View["band.cshtml", selectedBand];
      };

      Post["/band/{id}/venues/added"] = parameters => {
        int venueId = int.Parse(Request.Form["venue-id"]);
        Venue selectedVenue = Venue.Find(venueId);

        Band selectedBand = Band.Find(parameters.id);
        selectedBand.AddVenue(selectedVenue);

        return View["venue_added.cshtml", selectedBand];
      };

      Post["/venue/{id}/bands/added"] = parameters => {
        int bandId = int.Parse(Request.Form["band-id"]);
        Band selectedBand = Band.Find(bandId);

        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.AddBand(selectedBand);

        return View["band_added.cshtml", selectedVenue];
      };

      Get["/venues/{id}/update"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);

        return View["update.cshtml", selectedVenue];
      };

      Patch["/venues/{id}/update"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);

        string newName = Request.Form["new-name"];
        string newAddress = Request.Form["new-address"];

        selectedVenue.Update(newName, newAddress);

        return View["success.cshtml", selectedVenue];
      };

      Get["/venues/{id}/delete"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["delete.cshtml", selectedVenue];
      };

      Delete["/venues/{id}/delete"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);

        selectedVenue.Delete();

        return View["deleted.cshtml"];
      };


    }
  }
}
