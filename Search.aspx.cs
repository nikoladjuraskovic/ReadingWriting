using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReadingWritingFiles
{
    public partial class Search : System.Web.UI.Page
    {


        /*Ova stranica omogucava pretragu studenata procitanih iz .txt fajla prema njihovim podacima
         (ime, prezime i godina) putem veb forme i njihov ispis na veb strani.
        Ova pretraga sa filterom lici na filtere proizvoda na online shop-ovima.
        U Textbox-ove se unose ime i prezime, a u DropdownList-i se bira godina studenta.
       
        *Ideja je da studente koji zadovoljavaju SVE kriterijume pretrage, nakon klika na dugme Search, ispisemo u GridView.
        *Ako nema studenata koji zadovoljavaju kriterijume pretrage, onda tako i napisati u GridView.
        
        1)Student zadovoljava kriterijum pretrage za ime ako njegovo ime pocinje ili jednako je kao uneseno ime u TextBox,
         POD USLOVOM da je korisnik uneo nesto u TextBox za ime.
         Ako u TextBox za ime NIJE NISTA UNESENO, onda se smatra da student zadovoljava kriterijum pretrage za ime.

        2)Student zadovoljava kriterijum pretrage za prezime ako njegovo prezime pocinje ili jednako je kao uneseno prezime u TextBox,
         POD USLOVOM da je korisnik uneo nesto u TextBox za prezime.
         Ako u TextBox za prezime NIJE NISTA UNESENO, onda se smatra da student zadovoljava kriterijum pretrage za prezime.

        3)Student zadovoljava kriterijum pretrage za godinu ako je njegova godina jednaka izabranoj godini u DropDownList-i,
         POD USLOVOM da je korisnik izabrao element DropDownList-e koji jeste godina(nije prazan izbor).
         Ako u DropDownList-i za godinu NIJE IZABRANA GODINA(izabran je prazan izbor),
         onda se smatra da student zadovoljava kriterijum pretrage za godinu.

        *Student koji zadovoljava 1), 2) i 3) se smatra studentom koji zadovoljava SVE kriterijume pretrage.

        ***OBJASNJENJE za ne precizirane podatke***
        Ako se ne unese ime ili prezime ili ne naglasi koja je godina, onda taj kriterijum nije preciziran i
        mogu se dohvatiti studenti sa bilo kojim imenom, prezimenom ili godinom tim redom.
        ***        
         */


        /*listu studenata koja predstavlja sve studente iz .txt fajla stavljamo kao property klase da bi uvek imali istu listu*/

        List<Student> students = new List<Student>();


        protected void Page_Load(object sender, EventArgs e)
        {

            //System.Diagnostics.Debug.WriteLine("Broj studenata: " + students.Count);


                try
                {

                    string filePath = Server.MapPath("~/Files/Students2.txt");

                    CitajPodatke(filePath);


                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "SERVER ERROR";
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            /*mana ovog pristupa jeste sto svaki put citamo .txt fajlove sa diska*/
        }


        void CitajPodatke(string filePath)
        {
                  
            
            
            string[] studentsData;
            string student;
            string firstName;
            string lastName;
            string year;

            StreamReader reader = new StreamReader(filePath);
          
            while (!reader.EndOfStream)
            {
              
                student = reader.ReadLine();
                //System.Diagnostics.Debug.WriteLine("Red studenta: " + student);
                
                studentsData = student.Split('|');

                firstName = studentsData[0];
                lastName = studentsData[1];
                year = studentsData[2];

                
                Student s1 = new Student(firstName, lastName, int.Parse(year));

                students.Add(s1);
                             
            }

            /*U dropdown listu cemo ubaciti i prazan element " " koji predstavlja kada nije izabrana
             godina studenta tj. kada taj kriterijum nije preciziran.*/

            if(DropDownListYear.Items.Contains(new ListItem(" ")) == false)
                DropDownListYear.Items.Add(" ");

            /*prolazimo for petljom kroz sve procitane studente i u DropDown listu ispisujemo sve godine na kojim su studenti
             iz .txt fajla. Imajte u vidu da godine nisu sortirane, vec su ispisane redom kojim su citane iz .txt fajla.
            Mozete probati da sortirate godine ispisete u DropDownList-i za domaci*/
            foreach(Student s in students)
            {
                /*provera da li je takva godina vec u DropDown listi, ako jeste, onda je ne upisujemo dva puta.
                 * To se moze desiti ako su dva studenta na istoj godini.
                 * 
                 * */


                // Pri proveri se string mora pretvoriti u ListItem.*/
                if (DropDownListYear.Items.Contains(new ListItem(s.Year.ToString())) == false)
                    DropDownListYear.Items.Add(s.Year.ToString());
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {

                /*lokalna promenljiva gde stavljamo studente koji zadovoljavaju SVE kriterijume pretrage*/

                List<Student> searchedStudents = new List<Student>();

                /*Ideja pretrage(filtera):

                Prolazimo kroz sve studente procitane iz .txt fajla. 
                Ako je neki podatak UNET(ukucan u TextBox ili izabran iz DropDownListe(NIJE PRAZAN IZBOR)) i
                ako taj podatak NIJE POCETAK prezimena ili NIJE POCETAK imena studenta iz .txt fajla ili
                izabrana godina u dropdown listi nije godina tekuceg studenta iz .txt fajla, onda taj student nije trazen
                tj. kriterijum pretrage ne odgovara podatku tog studenta, sami tim, tog studenta NECEMO ispisivati u tabeli.
                U tom slucaju ide naredba continue koja prelazi na sledecu iteraciju petlje(prolaz kroz petlju).
                U suprotnom, ako neki kriterijum pretrage odgovara tekucem studentu, ide se dalje na sledeci kriterijum pretrage.
                I ako su svi kriterijumi pretrage prosli proveru,
                tj. tekuci student zadovoljava SVE kriterijume pretrage, onda se on ubacuje u listu trazenih studenata.

                 */

                foreach (Student s in students)
                {
                    if (TextBoxName.Text != "" && (!s.FirstName.StartsWith(TextBoxName.Text.Trim())))
                        continue;
                    if (TextBoxLastName.Text != "" && (!s.LastName.StartsWith(TextBoxLastName.Text.Trim())))
                        continue;
                    if (DropDownListYear.SelectedValue != " " && DropDownListYear.SelectedValue != s.Year.ToString())
                        continue;

                    searchedStudents.Add(s);


                }

                //Nakon formiranja trazene liste studenata, ispisujemo ih u GridView.

                GridView1.DataSource = searchedStudents;
                GridView1.DataBind();

            } catch(Exception ex)
            {
                ErrorLabel.Text = "SERVER ERROR";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

        }
    }
}