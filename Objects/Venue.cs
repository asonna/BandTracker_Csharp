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

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.Id == newVenue.Id);
        bool nameEquality = (this.Name == newVenue.Name);
        bool addressEquality = (this.Address == newVenue.Address);
        return (idEquality && nameEquality && addressEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> AllVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string venueAddress = rdr.GetString(2);
        Venue newVenue = new Venue(venueName, venueAddress, venueId);
        AllVenues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return AllVenues;
    }


    public void Save()

    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, address) OUTPUT INSERTED.id VALUES (@VenueName, @VenueAddress);", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@VenueName";
      nameParam.Value = this.Name;

      SqlParameter addressParam = new SqlParameter();
      addressParam.ParameterName = "@VenueAddress";
      addressParam.Value = this.Address;

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(addressParam);

      SqlDataReader rdr = cmd.ExecuteReader();


      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
