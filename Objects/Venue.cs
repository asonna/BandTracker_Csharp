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


    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      string foundVenueAddress = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
        foundVenueAddress = rdr.GetString(2);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueAddress, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundVenue;
    }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = newBand.Id;
      cmd.Parameters.Add(bandIdParameter);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.Id;
      cmd.Parameters.Add(venueIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE venues.id = @VenueId", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.Id;
      cmd.Parameters.Add(venueIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      List<Band> bands = new List<Band> {};

      while (rdr.Read())
      {
        int thisBandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        string bandNumber = rdr.GetString(2);
        Band foundBand = new Band (bandName, bandNumber, thisBandId);
        bands.Add(foundBand);

      }
      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
      return bands;
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
