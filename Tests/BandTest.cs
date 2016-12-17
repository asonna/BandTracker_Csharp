using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equal_AreObjectsEquivalent_true()
    {
      //Arrange, Act
      Band firstBand = new Band("Rooky Band", "Rock");
      Band secondBand = new Band("Rooky Band", "Rock");

      //Assert
      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void GetAll_SaveNewBandToDatabase_ListOfEnteredBands()
    {
      //Arrange
      Band testBand = new Band("Rooky Band", "Rock");
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_SaveAssignsIdToObject_Id()
    {
      //Arrange
      Band testBand = new Band("Rooky Band", "Rock");
      testBand.Save();

      //Act
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.Id;
      int testId = testBand.Id;

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Find_FindsBandInDatabase_true()
    {
      //Arrange
      Band testBand = new Band("Rooky Band", "Rock");
      testBand.Save();

      //Act
      Band result = Band.Find(testBand.Id);

      //Assert
      Assert.Equal(testBand, result);
    }


    [Fact]
    public void GetVenues_ReturnsAllBandVenue_studentList()
    {
      //Arrange
      Band testBand = new Band("Rooky Band", "Rock");
      testBand.Save();

      Venue testVenue1 = new Venue("westin Ball Room", "Portland, OR");
      testVenue1.Save();

      Venue testVenue2 = new Venue("Hilton Lounge", "Beaverton, OR");
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue1);
      List<Venue> savedVenue = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1};

      //Assert
      Assert.Equal(testList, savedVenue);
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
