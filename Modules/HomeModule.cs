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

  
    }
  }
}
