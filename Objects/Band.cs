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

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.Id == newBand.Id);
        bool nameEquality = (this.Name == newBand.Name);
        bool typeEquality = (this.Type == newBand.Type);
        return (idEquality && nameEquality && typeEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static List<Band> GetAll()
    {
      List<Band> AllBands = new List<Band>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        string bandType = rdr.GetString(2);
        Band newBand = new Band(bandName, bandType, bandId);
        AllBands.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return AllBands;
    }


    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, type) OUTPUT INSERTED.id VALUES (@BandName, @BandType);", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@BandName";
      nameParam.Value = this.Name;

      SqlParameter typeParam = new SqlParameter();
      typeParam.ParameterName = "@BandType";
      typeParam.Value = this.Type;

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(typeParam);

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

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = id.ToString();
      cmd.Parameters.Add(bandIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;
      string foundBandType = null;
      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
        foundBandType = rdr.GetString(2);
      }
      Band foundBand = new Band(foundBandName, foundBandType, foundBandId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundBand;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
