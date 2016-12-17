using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class DB // This is the DB class which will let us manage our database in our application
  {
    public static SqlConnection Connection()
    {
      SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
