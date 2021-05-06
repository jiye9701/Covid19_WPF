using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;


namespace Midterm

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /// 

    public partial class MainWindow : Window
    {
        //private ObservableCollection<Covid19> covid19 = new ObservableCollection<Covid19>();
        public MainWindow()
        {
            InitializeComponent();
            dataGridCovid.ItemsSource = LoadCovidData.GetDatas();
            LoadCovidData loadCollectionData = new LoadCovidData();
            //List<string> lists = GetCountryList();
            cmbCountry.ItemsSource = GetCountryList();




        }
        //public static List<string> GetAllCountrysNames()
        //{
        //    CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        //    var rez = cultures.Select(cult => (new RegionInfo(cult.LCID)).DisplayName).Distinct().OrderBy(q => q).ToList();

        //    return rez;
        //}


        //all countries

        public static List<string> GetCountryList()
        {
            List<string> cultureList = new List<string>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);


            foreach (CultureInfo item in cultures)
            {
                RegionInfo region = new RegionInfo(item.LCID);

                if (!(cultureList.Contains(region.EnglishName)))
                {
                    cultureList.Add(region.EnglishName);
                }
            }
            var sorted = cultureList;
            return sorted = cultureList.OrderBy(name => name).ToList();
            //return cultureList.OrderBy(a => a.Country).ToList();
            //cultureList.Sort();


        }

        public class LoadCovidData
        {
            public static string dateCheck;
            public static List<Covid19> GetDatas()
            {
                var filename = @"C:\Users\User\Desktop\jiyeyu_21W\COMP212\Midterm\Midterm\covid19_confirmed_global.csv";
                var lines = File.ReadAllLines(filename);


                List<Covid19> covid19List = new List<Covid19>();


                int count = 2;

                for (int j = 0; j < 386; j++)
                {

                    for (int i = 0; i < 1; i++)
                    {
                        string[] line2 = lines[i].Split(',');

                        dateCheck = line2[count];
                    }


                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] line = lines[i].Split(',');


                        var covid = new Covid19()
                        {

                            Country = line[1],
                            State = line[0],
                            NumberofCase = Int32.Parse(line[count]),
                            ConfirmedDate = dateCheck



                        };

                        if (!covid.NumberofCase.Equals(0))
                        {
                            covid19List.Add(covid);
                        }


                    }
                    count++;
                }


                return covid19List.OrderBy(a => a.Country).ToList();

            }


        }


        public class getCountryData
        {
            public static string dateCheck;
            public static List<Covid19> GetDatas()
            {
                var filename = @"C:\Users\User\Desktop\jiyeyu_21W\COMP212\Midterm\Midterm\covid19_confirmed_global.csv";
                var lines = File.ReadAllLines(filename);

                List<Covid19> covidList2 = new List<Covid19>();



                for (int i = 1; i < lines.Length; i++)
                {
                    string[] line = lines[i].Split(',');


                    var covid = new Covid19()
                    {
                        State = line[0],
                        Country = line[1]

                    };

                    if (line[i] != line[i - 1])
                    {

                        covidList2.Add(covid);
                    }



                }


                return covidList2.OrderBy(a => a.Country).ToList();

            }
        }

        //async
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string countryName = cmbCountry.Text;
            Console.WriteLine("Check:" + countryName);
            Task<Covid19> covidTask1 = Task.Run(() => StartCovidSearch(countryName));
            dataGridCovid.ItemsSource = covidTask1.Result.covid19List;

        }




        Covid19 StartCovidSearch(string name)
        {
            var result = new Covid19();

            result.covid19List = CovSearch(name);

            return result;

        }

        public static string dateCheck;
        public List<Covid19> CovSearch(string name)
        {
            var filename = @"C:\Users\User\Desktop\jiyeyu_21W\COMP212\Midterm\Midterm\covid19_confirmed_global.csv";
            var lines = File.ReadAllLines(filename);

            List<Covid19> covid19List = new List<Covid19>();

            int count = 2;

            for (int j = 0; j < 386; j++)
            {

                for (int i = 0; i < 1; i++)
                {
                    string[] line2 = lines[i].Split(',');

                    dateCheck = line2[count];
                }


                for (int i = 1; i < lines.Length; i++)
                {
                    string[] line = lines[i].Split(',');
                    var covid = new Covid19()
                    {
                        Country = line[1],
                        State = line[0],
                        NumberofCase = Int32.Parse(line[count]),
                        ConfirmedDate = dateCheck

                    };

                    if (covid.Country.Contains(name))
                    {

                        if (!covid.NumberofCase.Equals(0))
                        {
                            covid19List.Add(covid);
                        }
                    }

                }
                count++;
            }



            return covid19List.OrderBy(a => a.Country).ToList();
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            //init
            dataGridCovid.ItemsSource = LoadCovidData.GetDatas();
            LoadCovidData loadCollectionData = new LoadCovidData();
            cmbCountry.Text = string.Empty;

            //cmbCountry.SelectedIndex = countries.GetCountryList();
            //cmd.Parameters.AddWithValue("@country", this.cmbCountry.SelectedItem.ToString());
        }

        private void btnIcon_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\Program Files\Internet Explorer\IEXPLORE.EXE", "http://www.centennialcollege.ca");
        }

        private void cmbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }








        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\User\Desktop\jiyeyu_21W\COMP212\Midterm\Midterm\covid19_confirmed_global.csv");
        //    Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
        //    Excel.Range range = xlWorksheet.get_Range("B2:B274");


        //    foreach (Excel.Range item in range.Cells)
        //    {

        //        string lst = (string)item.Text;
        //        cmbCountry.Items.Add(lst);
        //    }
        //}
    }
}

