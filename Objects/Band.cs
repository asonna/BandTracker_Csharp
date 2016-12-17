using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Band
  {

    public int Id {get; set;}
    public string Name {get; set;}
    public string Type {get; set;}

    public Band(string name, string type, int id = 0)
    {
      this.Id = id;
      this.Name = name;
      this.Type = type;
    }

    
  }
}
