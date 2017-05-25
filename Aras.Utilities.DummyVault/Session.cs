using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Aras.Utilities.DummyVault
{
    public class Session
    {
        public Aras.IO.Session IO { get; private set; }

        public DirectoryInfo Vault { get; private set; }

        public void Process()
        {
            Aras.IO.Item files = new IO.Item("File", "get");
            files.Select = "id,filename";
            Aras.IO.Request request = IO.Request(Aras.IO.Request.Operations.ApplyItem);
            request.AddItem(files);
            Aras.IO.Response response = request.Execute();

            foreach(Aras.IO.Item file in response.Items)
            {
                String id = file.ID;
                DirectoryInfo firstleveldir = new DirectoryInfo(this.Vault.FullName + "\\" + id.Substring(0, 1));

                if (!firstleveldir.Exists)
                {
                    firstleveldir.Create();
                }

                DirectoryInfo secondleveldir = new DirectoryInfo(firstleveldir.FullName + "\\" + id.Substring(1, 2));

                if (!secondleveldir.Exists)
                {
                    secondleveldir.Create();
                }

                DirectoryInfo thirdleveldir = new DirectoryInfo(secondleveldir.FullName + "\\" + id.Substring(3, id.Length - 3));

                if (!thirdleveldir.Exists)
                {
                    thirdleveldir.Create();
                }

                FileInfo filename = new FileInfo(thirdleveldir.FullName + "\\" + file.GetProperty("filename"));

                if (!filename.Exists)
                {
                    using(StreamWriter sw = new StreamWriter(filename.FullName))
                    {
                        sw.WriteLine("Dummy File");
                    }
                }
            }
        }

        public Session(Aras.IO.Session IO, DirectoryInfo Vault)
        {
            this.IO = IO;
            this.Vault = Vault;
        }
    }
}
