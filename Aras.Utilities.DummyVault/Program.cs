using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Aras.Utilities.DummyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            Aras.IO.Server server = new IO.Server("http://localhost/11SP9");
            Aras.IO.Database database = server.Database("Sprue");
            Aras.IO.Session session = database.Login("admin", Aras.IO.Server.PasswordHash("innovator"));

            Session vaultsession = new Session(session, new DirectoryInfo("E:\\Aras\\11SP9\\Vault\\Sprue"));
            vaultsession.Process();
        }
    }
}
