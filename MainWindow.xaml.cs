using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MuveltsegiVerseny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kerdes> kerdesek = new List<Kerdes>();
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader(path: @"..\..\..\src\feladatok.txt", encoding: Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                    kerdesek.Add(new Kerdes(sr.ReadLine()));
            }

            foreach (var kerdes in kerdesek)
            {
                kerdesekLB.Items.Add(kerdes.KerdesString);
            }
        }

        private void feladat2gomb_Click(object sender, RoutedEventArgs e)
        {
            feladat2.Content = "";
            feladat2.Content += $"A kérdőjeles feladatok száma: {kerdesek.Count(k => k.KerdesString.Contains('?'))}";
        }

        private void feladat3gomb_Click(object sender, RoutedEventArgs e)
        {
            feladat3.Content = "";
            feladat3.Content = $"Az adatfájlban {kerdesek.Count(k => k.Tema == "kemia" && k.Pont == 3)} db 3 pontos kémiai feladat van.";
        }

        private void feladat4gomb_Click(object sender, RoutedEventArgs e)
        {
            feladat4.Content = "";
            List<Kerdes> sorban = kerdesek.OrderBy(k => k.Valasz).ToList();
            feladat4.Content = $"A válszok számértéke {sorban.First().Valasz} éa {sorban.Last().Valasz} között terjed.";
        }

        private void feladat5gomb_Click(object sender, RoutedEventArgs e)
        {
            feladat5.Items.Clear();
            var temak = kerdesek.Select(k => k.Tema).Distinct().Order();
            foreach (var tema in temak)
                feladat5.Items.Add(tema);
        }

        private void feladat6gomb_Click(object sender, RoutedEventArgs e)
        {
            feladat6.Items.Clear();
            feladat6valasz.Content = "";
            feladat6TextBoxValasz.Text = "";

            string reszlet = feladat6TextBox_Kereses.Text;
            List<Kerdes> talalatok = kerdesek.Where(k => k.KerdesString.ToLower().Contains(reszlet.ToLower())).ToList();

            if (talalatok.Count() == 0)
            {
                feladat6.Items.Add("Nincs találat");

                feladat6kerdes.Visibility = Visibility.Hidden;
                feladat6valasz.Visibility = Visibility.Hidden;
                feladat6TextBoxValasz.Visibility = Visibility.Hidden;
                feladat6gomb_Valasz.Visibility = Visibility.Hidden;
            }
            else
            {
                foreach (var talalat in talalatok)
                    feladat6.Items.Add(talalat.KerdesString);

                feladat6kerdes.Visibility = Visibility.Visible;
                feladat6valasz.Visibility = Visibility.Visible;
                feladat6TextBoxValasz.Visibility = Visibility.Visible;
                feladat6gomb_Valasz.Visibility = Visibility.Visible;

                feladat6kerdes.Content = talalatok[rnd.Next(talalatok.Count())].KerdesString;
            }
        }

        private void feladat6ValaszGomb_Click(object sender, RoutedEventArgs e)
        {
            Kerdes jelenlegiKerdes = kerdesek.Find(k => k.KerdesString == feladat6kerdes.Content.ToString());
            int valasz = int.Parse(feladat6TextBoxValasz.Text);

            feladat6valasz.Content = $"A válasz {(jelenlegiKerdes.Valasz == valasz ? jelenlegiKerdes.Pont : 0)} pontot ér.";

        }

        private void feladat7gomb_Click(object sender, RoutedEventArgs e)
        {
            List<Kerdes> kerdes15 = new();

            for (int i = 0; i < 15; i++)
            {
                Kerdes kerdes = kerdesek[rnd.Next(kerdesek.Count)];
                while (kerdes15.Contains(kerdes))
                    kerdes = kerdesek[rnd.Next(kerdesek.Count)];

                kerdes15.Add(kerdes);
            }

            string kiiras = $"{kerdes15.Sum(k => k.Pont)}@";
            foreach (var kerdes in kerdes15)
                kiiras += $"{kerdes.KerdesString}@";
            // utolsó @ törlése:
            kiiras = kiiras.Substring(0, kiiras.Length - 1);

            using (StreamWriter sw = new StreamWriter(path: @"..\..\..\src\15_feladat.txt", append: false, encoding: Encoding.UTF8))
            {
                sw.WriteLine(kiiras);
            }
        }
    }
}