using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FacebookGroupMembers
{
    class Program
    {
        static void Main(string[] args)
        {
            Decoder decoder = new Decoder();
        }
    }

    public class Decoder
    {
        private string desktopPath;
        private string sourceFile;
        private string lastName;

        public Decoder()
        {
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            Console.WriteLine("Source file on desktop (with extension): ");
            sourceFile = Path.Combine(desktopPath, Console.ReadLine());

            Console.WriteLine("Last member full name: ");
            lastName = Console.ReadLine();

            string contents = File.ReadAllText(sourceFile);

            //Console.WriteLine(contents);

            string output = "";

            int div = contents.IndexOf("clearfix");
            int last_pos = 0;

            while (div > 0)
            {
                div = contents.IndexOf("clearfix", last_pos);

                int url_start = contents.IndexOf("<a href=", div) + 8;
                int url_end = contents.IndexOf("data-hovercard=", url_start);
                string url = contents.Substring(url_start + 1, url_end - url_start - 3);
                url = url.Replace("?fref=pb_other", "");
                Console.WriteLine(url);

                int name_start = contents.IndexOf("\">", url_end) + 2;
                int name_end = contents.IndexOf("</a>", name_start);
                string name = contents.Substring(name_start, name_end - name_start);
                Console.WriteLine(name);

                output = output + Environment.NewLine + url + "," + name;

                last_pos = name_end;

                if (name == lastName)
                    break;
            }

            File.WriteAllText(Path.Combine(desktopPath, "members.txt"), output);

            Console.ReadLine();

        }

    }
}