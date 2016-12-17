using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Venue
  {

    public int Id {get; set;}
    public string Name {get; set;}
    public string Address {get; set;}

    public Venue(string name, string address, int id = 0)
    {
      this.Id = id;
      this.Name = name;
      this.Address = address;
    }

    
  }
}
