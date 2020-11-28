using BodyMonitorApp.Models;
using Helpers;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace BodyMonitorApp
{
    public class ChartsViewModel : ObservableObject, IPageViewModel
    {
       
        public string Name
        {
            get
            {
                return "Charts";
            }
            set {; }
        }

        private bool[] _modeArray = new bool[] { true, false, false };
        public bool[] ModeArray
        {
            get { SetTimeRange(SelectedMode); return _modeArray; }
        }
        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); }
        }

        private SeriesCollection _seriesCollection;

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                if (_seriesCollection != value)
                {
                    _seriesCollection = value;

                    OnPropertyChanged("SeriesCollection");
                }
            }
        }

        private Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get
            {
                return _formatter;
            }
            set
            {
                if (_formatter != value)
                {
                    _formatter = value;

                    OnPropertyChanged("Formatter");
                }
            }
        }
       

        private ChartModel _chartModel;

        public ChartModel ChartModel
        {
            get { return _chartModel; }
            set
            {
                if (value != _chartModel)

                {
                    _chartModel = value;
                    OnPropertyChanged("ChartModel");
                }
            }
        }

        public List<ChartValueDate> ChartValueDate { get; set; }


        public ChartValues<DateModel> Values { get; set; }

        private Visibility _visibility = Visibility.Hidden;
        public Visibility Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;

                OnPropertyChanged("Visibility");
            }
        }

        private Visibility _chartVisibility = Visibility.Hidden;
        public Visibility ChartVisibility
        {
            get
            {
                return _chartVisibility;
            }
            set
            {
                _chartVisibility = value;

                OnPropertyChanged("ChartVisibility");
            }
        }


        public ObservableCollection<ComboBoxHistory> ComboBoxChoices { get; set; }

        private ComboBoxHistory _selectedItem;

        public ComboBoxHistory SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    OnPropertyChanged("SelectedItem");

                    SetChartValues(SelectedItem.Symbol);
                    SetTimeRange(SelectedMode);
                   
                }


            }
        }

        public static List<int> TimeRangeValues { get; set; }
        public List<string> BodyPartsChoices { get; set; }


        public ChartsViewModel()
        {

            ComboBoxChoices = new ObservableCollection<ComboBoxHistory>();
            
            // list of current actual choices of body parts values in db
            List<string> bodyPartsChoices = new List<string>() {"-----", "Weight", "Neck", "Chest", "Stomach", "Waist", "Hips", "Thigh", "Calf", "Biceps" };

           
            TimeRangeValues = new List<int>() { -365, -7, -30 };

            //adding body parts values to the combobox in view
            foreach(var bodyPart in bodyPartsChoices)
            {
                ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = bodyPart });
            }

            
            SelectedItem = ComboBoxChoices[0];

        }

        #region methods
        public void SetUserValues(int userId)
        {
            //get data from sql query   
            Queries query = new Queries(userId);
            ChartModel = query.GetUserValues();

        }

               
        // TODO: change the way in which selected time range is chosen and set(radiobuttons and array)
        public void SetTimeRange(int mode)
        {

          
            int timeSubstract = 0;
            switch (mode)
            {
                case 0:
                    timeSubstract = -365;
                    break;
                case 1:
                    timeSubstract = -7;
                    break;
                case 2:
                    timeSubstract = -30;
                    break;
            }
                

                            
            DateTime timeRange = DateTime.Now.AddDays(timeSubstract);
            DateTime now = DateTime.Now;

            try
            {
                var filtered = ChartValueDate
                   .Where(t => t.Date >= timeRange);
               
                UpdateChart(filtered.ToList());
            }

            catch
            {

                //ChartValueDate narazie jest puste, pojawia się to później - specjalnie puste na już - do poprawienia
            };


        }


        public void SetChartValues(string bodyPartName)
        {
            List<ChartValueDate> chartValueDate = new List<ChartValueDate>();
            List<decimal> valuesList = new List<decimal>();

            try
            {

                if (bodyPartName == "-----")
                {
                    ChartVisibility = Visibility.Hidden;

                }

                else
                {
                    ChartVisibility = Visibility.Visible;
                    //moving values from db query property (ChartModel) to the values list property(ChartValueDate) for chart updating, based on selected name of body part

                    valuesList = (List<decimal>)ChartModel.GetType().GetProperty(bodyPartName).GetValue(ChartModel, null);


                    //adding existing values of body parts to the list ChartValueDate(value, dateAdded)
                    int i = 0;
                    foreach (var item in ChartModel.DateAdded)
                    {
                        chartValueDate.Add(new ChartValueDate() { Date = ChartModel.DateAdded[i], Value = valuesList[i] });
                        i++;
                    }

                    ChartValueDate = chartValueDate.ToList();

                }
              
               
            }

            catch
            {
                //to do
            }
                        
        }


        public void UpdateChart(List<ChartValueDate> filteredValues)
        {

           //map chart ticks for x axis
            var dayConfig = Mappers.Xy<DateModel>()
                          .X(dayModel => dayModel.DateTime.Ticks)
                          .Y(dayModel => dayModel.Value);




            Values = new ChartValues<DateModel>();

            // create new datapoints on chart, based on filtered values by date(all,7d,30d)
            int i = 0;
            foreach (var value in filteredValues)
            {

                Values.Add(new DateModel
                {
                    DateTime = filteredValues[i].Date,
                    Value = (double)filteredValues[i].Value
                });

                i++;
            }

            //set datapoints and title of the chart
            SeriesCollection = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Title = SelectedItem.Symbol,

                    Values = Values

                }
              };

            Formatter = value => new DateTime((long)value).ToString("yyyy-MM-dd");


        }
        #endregion
    }


}






