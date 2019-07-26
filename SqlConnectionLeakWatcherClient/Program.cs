using SqlConnectionLeakWatcher;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnectionLeakWatcherClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int max = 10;
            var randomList = new List<int> { random.Next(1, max), random.Next(1, max), random.Next(1, max) };
            for (int i = 1; i <= max; i++)
            {
                SqlConnection conn = null;
                WatchHandler handler = null;
                try
                {
                    conn = new SqlConnection("Data Source=DESKTOP-O80CRIA;Initial Catalog=Emp;Integrated Security=True");
                    handler = new WatchHandler(conn);
                    handler.StartWatch();
                    conn.Open();
                }
                finally
                {
                    //handler.StopWatch();
                    if (!randomList.Contains(i))
                        conn.Dispose();
                }
            }
            Console.ReadLine();
        }
    }
}
