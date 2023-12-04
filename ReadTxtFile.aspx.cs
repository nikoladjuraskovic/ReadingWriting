using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReadingWritingFiles
{
    public partial class ReadTxtFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            /*
                       ***CITANJE TEKSTUALNIH DATOTEKA***
                       *
                       *Datoteka = fajl = file
                       *Direktorijum = folder
                       *
                       *
                       *Postoje situacije kada je neophodno da se iz tekstualnih fajlova citaju podaci i upisu u neku promenljivu u programu
                       *ili negde na veb stranici.
                       *Obrada i citanje tekstualnih podatka iz fajla se zove PARSIRANJE(PARSING) fajla.   
                       *
                       *Da bismo mogli da procitamo neki fajl, moramo znati tacno gde se on nalazi na nasem uredjaju(racunar) i
                       *kog tipa je taj fajl(tipovi: .txt, .xml, .json, ...)
                       *
                       *Putanja(Path) je niz karatera koji predstavlja lokaciju(mesto) nekog fajla na racunaru.
                       *Primer putanje manual.txt fajla: C/Program Files/Microsoft/manual.txt
                       *
                       *Apsolutna putanja(Absolute Path) je potpuna i precizna lokacija fajla od korenog direktorijuma(root folder) do samog
                       *fajla. Koreni direktorijum je, na Windows-u,  particija(C, D, ...).
                       *Primer apsolutne putanje fajla EuroTruckSimulator2logs.txt:  C/Program Files/Games/EuroTruckSimulator2logs.txt
                       *
                       *Relativna putanja je putanja nekog fajla u odnosu na nasu trenutnu lokaciju(tamo gde se sada nalazimo).
                       *Relativna putanja fajla pocinje od nase trenutne lokacije, koja se oznacava simbolom ~ (čita se tilda),
                       *i nastavlja se do samog fajla.
                       *Primer relativne putanje fajla Students.txt u ovom projektu: ~/Files/Students.txt.
                       *
                       *Najcesce cemo koristiti relativne putanje u okviru ovog projekta jer su one uvek iste bez obzira
                       *na to na kom racunaru se nalazio projekat. Ovo radimo da bi vam projekat odmah radio nakon prebacivanja na
                       *vas racunar. Upamtite, raspored i nazivi fajlova i foldera nisu isti na razlicitim racunarima.
                       *Zbog toga, apsolutne putanje se skoro uvek razlikuju, a relativne ne moraju, kao npr. u okviru ovog projekta,
                       *gde ce relativne putanje biti svima iste.
                       *Ako postoji potreba za koriscenjem apsolutne putanje nekog fajla, koristicemo funkcije koje pretvaraju
                       *relativne  putanje u apsolutne.
                       *
                       *Putanje u programu predstavljamo promenljivama tipa string.
                       *
                       *Jezik C# ima podrsku za rad sa tekstualnim fajlovima.
                       *Dokumentacija:
                       * https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file
                        
                       */
            try
            {

                /*prosledjujemo apsolutnu putanju. Nju mozete dobiti iz Solution Explorer-a.
                 * Desni klik na ime fajla i onda Copy Full Path. Zatim, paste-ujete u kod.
                 */
                //string filePath = "C:\Users\Nikola\source\repos\ReadingWritingFiles\Files\Students.txt";

                /*Bolji nacin je da koristite relativnu putanju, tj. putanju od trenutne lokacije gde se nalazite, jer je ona 
                 * uvek ista bez obzira na kom ste racunaru a onda da tu relativnu putanju pretvorimo u apsolutnu jer sve nase
                 * funkciije tj. metodi kao argument prihvataju apsolutnu putanju.
                 Koristimo metod MapPath objekta Server koji relativnu putanju fajla pretvara u apsolutnu.*/

                string filePath = Server.MapPath("~/Files/Students.txt");

                string filePath2 = Server.MapPath("~/Files/Students2.txt");

                CitajFajl(filePath, filePath2);

                CitajFajlStream(filePath, filePath2);

                

            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "SERVER ERROR";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }



        }

        void CitajFajl(string filePath, string filePath2)
        {
            

            /*metod ReadAllText cita CEO tekst iz fajla i vraca ga u obliku stringa.
             Prostor imena(namespace) System.IO ne morate pisati ako ste ga importov-ali(uvezli) gore na pocetku.
            Klasa File sadrzi staticke metode za rad sa fajlovima.
            Staticke metode(one koje ispred definicje metoda imaju rec static) klase su metode koje su iste
            u svakoj instanci(svakom objektu) te klase.
            Zato se pozivaju iz klase, a ne iz objekta te klase.

            */
            string text = System.IO.File.ReadAllText(filePath);

            /*Klasa File se nalazi u prostoru imena(namespace) System.IO.
             Mozete ili stalno pozivati taj namespace kao ovde, ili da ga importujete
            u vasu klasu i da gore pise using System.IO i onda je dovoljno samo navesti File klasu.*/


            // Ispis teksta u debug prozoru Visual Studija
            System.Diagnostics.Debug.WriteLine("ReadAllText:");
            System.Diagnostics.Debug.WriteLine(text);
            System.Diagnostics.Debug.WriteLine("---------------------------");

            //ispis na veb strani u label
            Label1.Text = text;

            // Example #2
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            /*metod ReadAllLines cita sve linije fajla i vraca niz stringova,
             * gde je svaki string(element niza) tekst jedne linije fajla*/
            string[] lines = System.IO.File.ReadAllLines(filePath);

            /* naredni kod bi proizveo gresku, jer lines je niz stringova, a ne string. Zato cemo ispisati drugacije.             
            Label2.Text = lines;
            */

            // Prikaz svih linija teksta for petljom, linija po linija.
                   
            foreach (string line in lines)
            {
                // nadovezivanje(dodavanje) svake linije teksta u label. Element niza lines je string i moze se upisati u label.
                Label2.Text += line;
                
                
            }


            /*ispis na veb strani u kontroli DropDownList. */

            DropDownList1.DataSource = lines;
            DropDownList1.DataBind();
           

            /*ponekad zelimo da detaljnije obradimo nase procitane podatke i da ih u nekom obliku sacuvamo u programu(npr. u objektu klase
             * koja predstavlja te podatke). Zatim da ispis na veb strani bude po zelji(customize-ovan).
             Ovde cemo podatke o studentima grupisati u objekte klase Student.*/

            /*
             Podaci u Students2.txt su podeljeni uspravnom crtom | i podatke je potrebno nekako grupisati u
            prezime, ime i godinu u odnosu na uspravnu crtu.
             */

            //lista studenata gde cuvamo nase podatke o studentima iz .txt fajla
            List<Student> studentsList = new List<Student>();

            //niz stringova gde se cuvaju podaci o jednom studentu: prezime, ime, godina
            string[] studentsData;

            string lastname;
            string firstname;
            int year;

            string[] students = System.IO.File.ReadAllLines(filePath2);
            foreach (string student in students)
            {

                /*metod Split vraca niz stringova. Taj niz stringova je dobijen razbijanjem stringa na podstringove u odnosu
                 na separator |. Dakle, sve reci su sada posebni stringovi osim |.
                 Ako bismo citali fajl Students.txt separator bi bila praznina ' '
                 U ovom slucaju studentsData je niz stringova sa elementima [prezime, ime, godina]*/
                studentsData = student.Split('|');

                /*dohvatamo ime, prezime i godinu iz niza studentsData redom*/
              
                firstname = studentsData[0];
                lastname = studentsData[1];
                //godina se cita kao string pa se mora pretvoriti u int
                year = int.Parse(studentsData[2]);

                //novi objekat tipa Student
                Student s = new Student(firstname, lastname, year);

                //ubacujemo novog studenta u listu studenata
                studentsList.Add(s);

            }


            //ispisujemo studente u GridView
            GridView1.DataSource = studentsList;
            GridView1.DataBind();

        }

        void CitajFajlStream(string filePath, string filePath2)
        {

            /*
             Stream je tok podataka(bajtova).
             */

            /*Klasa StreamReader sluzi za citanje fajlova.
             Docs:
            https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=netframework-4.8
            Citanje iz txt fajlova:
            https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-text-from-a-file
             
             */

            List<Student> students = new List<Student>();
            string[] studentsData;
            string student;

            /*Stream reader prihvata kao argument putanju fajla koji cita*/

            //StreamReader stavljamo u using blok da bismo sve njegove resurse nakon toga zatvorili

            using (StreamReader reader = new StreamReader(filePath2))
            {

                /*dokle god ne dodjemo do kraja steam-a odnosno do kraja toka podataka tj. do kraja fajla*/
                while (!reader.EndOfStream) // while(reader.EndOfStream == false)
                {
                    /*metod ReadLine vraca procitanu liniju teksta u string*/
                    student = reader.ReadLine();
                    System.Diagnostics.Debug.WriteLine("Red studenta: " + student);

                    /*objasnjenje u CitajFajl funkciji*/
                    studentsData = student.Split('|');

                    //ovog puta nismo pravili nove promenljive vec smo ih odmah dohvatili iz niza
                    Student s1 = new Student(studentsData[0], studentsData[1], int.Parse(studentsData[2]));

                    students.Add(s1);
                }

            }

            GridView2.DataSource = students;
            GridView2.DataBind();

        }


    }

    /*klasa student koja odgovara podacima iz fajla Students2.txt.
     Klasa je javna i zato je vidljiva u celom projektu tj. mozemo praviti
    objekte klase Student u bilo kom c# fajlu.*/
    public class Student
    {
        public Student(string firstName, string lastName, int year)
        {
            FirstName = firstName;
            LastName = lastName;
            Year = year;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
       

    }
}