using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace  BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equal_AreObjectsEquivalent_true()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("westin Ball Room", "Portland, OR");
      Venue secondVenue = new Venue("westin Ball Room", "Portland, OR");

      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void GetAllAndSave_SaveNewVenueToDatabase_ListOfEnteredVenues()
    {
      //Arrange
      Venue testVenue = new Venue("westin Ball Room", "Portland, OR");
      testVenue.Save();

      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_SaveAssignsIdToObject_Id()
    {
      //Arrange
      Venue testVenue = new Venue("westin Ball Room", "Portland, OR");
      testVenue.Save();

      //Act
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.Id;
      int testId = testVenue.Id;

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Find_FindsVenueInDatabase_true()
    {
      //Arrange
      Venue testVenue = new Venue("westin Ball Room", "Portland, OR");
      testVenue.Save();

      //Act
      Venue result = Venue.Find(testVenue.Id);

      //Assert
      Assert.Equal(testVenue, result);
    }

    [Fact]
    public void AddBand_AddsBandToVenue_bandList()
    {
      //Arrange
      Venue testVenue = new Venue("Hilton Lounge", "Beaverton, OR");
      testVenue.Save();

      Band testBand = new Band("Jazzo Band", "Jazz");
      testBand.Save();

      //Act
      testVenue.AddBand(testBand);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetBands_ReturnsAllVenueBands_bandList()
    {
      //Arrange
      Venue testVenue = new Venue("Hilton Lounge", "Beaverton, OR");
      testVenue.Save();

      Band testBand1 = new Band("Jazzo Band", "Jazz");
      testBand1.Save();

      Band testBand2 = new Band("Rocky Band", "Rock");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand1);
      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Delete_DeletesVenueAssociationsFromDatabase()
    {
      //Arrange
      Band testBand = new Band("Jazzo Band", "Jazz");
      testBand.Save();

      string testName = "Hilton Lounge";
      string testAddress = "Beaverton, OR";
      Venue testVenue = new Venue(testName, testAddress);
      testVenue.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenues = testBand.GetVenues();
      List<Venue> testBandVenues = new List<Venue> {};

      //Assert
      Assert.Equal(testBandVenues, resultBandVenues);
    }

    [Fact]
    public void Update_UpdateVenueNameAndAddress_NameAndAddressUpdatedInDatabase()
    {
      Venue testVenue = new Venue("Hilton Lounge", "Beaverton, OR");
      testVenue.Save();

      testVenue.Update("Magestic Club", "Tigard, OR");
      Venue result = Venue.GetAll()[0];
      Venue UpdatedVenue = new Venue("Magestic Club", "Tigard, OR", testVenue.Id);

      Assert.Equal(result, UpdatedVenue);
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
