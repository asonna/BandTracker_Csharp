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

    [Fact]
    public void AddVenue_AddsVenueToBand_studentList()
    {
      //Arrange
      Band testBand = new Band("Jazzo Band", "Jazz");
      testBand.Save();

      Venue testVenue = new Venue("westin Ball Room", "Portland, OR");
      testVenue.Save();

      Venue testVenue2 = new Venue("Hilton Lounge", "Beaverton, OR");
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue);
      testBand.AddVenue(testVenue2);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue, testVenue2};

      //Assert
      Assert.Equal(testList, result);
    }

    // [Fact]
    // public void Update_UpdateBandNameAndNumber_NameAndNumberUpdatedInDatabase()
    // {
    //   Band testBand = new Band("Rooky Band", "Rock");
    //   testBand.Save();
    //
    //   testBand.Update("Jazzo Band", "Jazz");
    //   Band result = Band.GetAll()[0];
    //
    //   Assert.Equal(result, testBand);
    // }
    //
    // [Fact]
    // public void Delete_DeletesBandFromDatabase_testBand2()
    // {
    //   //Arrange
    //   string name1 = "Rooky Band";
    //   string type1 = "Rock";
    //   Band testBand1 = new Band(name1, type1);
    //   testBand1.Save();
    //
    //   string name2 = "Jazzo Band";
    //   string type2 = "Jazz";
    //   Band testBand2 = new Band(name2, type2);
    //   testBand2.Save();
    //
    //   //Act
    //   testBand1.Delete();
    //   List<Band> resultBands = Band.GetAll();
    //   List<Band> testBandList = new List<Band> {testBand2};
    //
    //   //Assert
    //   Assert.Equal(testBandList, resultBands);
    // }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
