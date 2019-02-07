using System.Windows.Forms;

namespace LabelMakerWPF_Plain.Models
{
    public class MessagesModel
    {
        public string Error = "Błąd!";

        public void QuantityError()
        {
            MessageBox.Show("Podświetlone pola wymagają uwagi!", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void BoxError()
        {
            MessageBox.Show("Numer aktualnej paczki nie może być wiekszy niż liczba wszystkich paczek!", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PrintError()
        {
            MessageBox.Show("Nie wydrukowałeś aktualnej etykiety", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }     
        
        public void TextBoxError()
        {
            MessageBox.Show("Pole tekstowe nie może być puste", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void FirstRunInfo()
        {
            string info = Properties.Resources.FirstRunInfo;            

            MessageBox.Show(info, "Pierwsze uruchomienie - instrukcja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void TrialTimeUp()
        {
            string info = "Czas użytkowania dobiegł końca.";
            MessageBox.Show(info,"Trial", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}
