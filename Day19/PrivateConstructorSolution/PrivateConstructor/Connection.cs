using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateConstructor
{
    public class Connection
    {
        public string ConnectionName { get; set; }
        private static Connection connection = null;
        private Connection() { }

        public static Connection GetConnection()
        {
            if(connection == null)
            {
                connection = new Connection();
            }
            return connection;
        }
    }

}
