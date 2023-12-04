using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReadingWritingFiles
{
    public partial class WriteTxtFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             
                    ***PISANJE U TEKSTUALNE FAJLOVE***
                    

                    Postoji potreba u programu da ponekad sadrzaj nekih promenljivih, objekata pisemo u .txt fajl.
                    To mogu biti i promenljive koje cuvaj sadrzaj necega unetog na veb stranici.

                    https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file

                    
             
             */

            try
            {


                string path = "~/Files/write.txt";
                string path2 = "~/Files/write2.txt";

                path = Server.MapPath(path);
                path2 = Server.MapPath(path2);

                PisiUFajl(path, path2);

            } catch (Exception ex)
            {
                ErrorLabel.Text = "SERVER ERROR";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);

            }
        }


        void PisiUFajl(string filePath, string filePath2)
        {
            string text = "Neki tekst koji se upisuje u fajl.";

            /*metod WriteAllText ispisuje string u jednoj liniji fajla. Pri svakom ispisu, brisu se stari sadrzaj fajla*/

            System.IO.File.WriteAllText(filePath, text);

            string[] lines = { "prva", "druga", "treca" };

            /*metod WriteAllLines ispisuje niz stringova u fajl, svaki string u posebnu liniju*/
            System.IO.File.WriteAllLines(filePath2, lines);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            /*klikom na dugme se sadrzaj textboxa ispisuje u .txt fajl */

            TextBox1.Text = "";

            try
            {


                /*

                 StreamWriter klasa za pisanje.
                https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=netframework-4.8

                 */

                //klikom na dugme se sadrzaj textbox-a ispisuje u .txt fajl

                string path = "~/Files/write3.txt";

                path = Server.MapPath(path);

                //StreamWriter stavljamo u using blok da bismo sve njegove resurse nakon toga zatvorili

                using (StreamWriter writer = new StreamWriter(path))
                {

                    writer.WriteLine(TextBox1.Text.Trim());

                }

            } catch (Exception ex)
            {
                ErrorLabel.Text = "SERVER ERROR";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}