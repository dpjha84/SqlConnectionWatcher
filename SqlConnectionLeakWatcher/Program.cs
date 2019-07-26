using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnectionLeakWatcher
{
    internal class Watcher
    {
        static int counter = 0;
        readonly SqlConnection conn;

        internal Watcher(SqlConnection conn)
        {
            this.conn = conn;
        }

        internal void StartWatch()
        {
            conn.StateChange += Conn_StateChange;
            counter++;
        }

        internal void StopWatch()
        {
            conn.StateChange -= Conn_StateChange;
        }


        private void Conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            Console.WriteLine($"{e.CurrentState} {counter}");
        }
    }

    public class WatchHandler
    {
        private readonly Watcher watcher;

        public WatchHandler(SqlConnection conn)
        {
            watcher = new Watcher(conn);
        }

        public void StartWatch()
        {
            watcher.StartWatch();
        }
        public void StopWatch()
        {
            watcher.StopWatch();
        }
    }
}
