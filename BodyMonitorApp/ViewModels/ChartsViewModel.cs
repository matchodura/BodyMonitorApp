﻿using BodyMonitorApp.Models;
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
        #region fields

        private bool[] _modeArray = new bool[] { true, false, false };

        private SeriesCollection _seriesCollection;

        private Func<double, string> _formatter;

        private ChartModel _chartModel;

        private ComboBoxHistory _selectedItem;

        private bool _enoughValues = true;

        private Visibility _visibility = Visibility.Hidden;

        private Visibility _chartVisibility = Visibility.Hidden;
        #endregion

        #region properties

        public string Name
        {
            get { return "Charts"; }
            set {; }
        }

        public bool EnoughValues
        {
            get { return _enoughValues; }
            set
            {
                if (value != _enoughValues)

                {
                    _enoughValues = value;
                    OnPropertyChanged("EnoughValues");
                }
            }
        }

        public bool[] ModeArray
        {
            get { SetTimeRange(SelectedMode); return _modeArray; }
        }

        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); }
        }

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

        public ObservableCollection<ComboBoxHistory> ComboBoxChoices { get; set; }

        public ComboBoxHistory SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    OnPropertyChanged("SelectedItem");

                    CheckForEnoughValues();

                    if (EnoughValues)
                    {
                        SetChartValues(SelectedItem.Symbol);
                        SetTimeRange(SelectedMode);
                    }

                    else
                    {
                        MessageBox.Show("Add more values!");
                    }

                }
            }
        }

        public static List<int> TimeRangeValues { get; set; }

        public List<string> BodyPartsChoices { get; set; }

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

        #endregion

        /// <summary>
        /// ChartsView constructor, creates list of columns in database, time range for data to be displayed on the chart and
        /// sets values of properties of buttons on the view
        /// </summary>
        public ChartsViewModel()
        {
            ComboBoxChoices = new ObservableCollection<ComboBoxHistory>();

            // list of current actual choices of body parts values in db
            List<string> bodyPartsChoices = new List<string>() { "-----", "Weight", "Neck", "Chest", "Stomach", "Waist", "Hips", "Thigh", "Calf", "Biceps" };

            TimeRangeValues = new List<int>() { -365, -7, -30 };

            //adding body parts values to the combobox in view
            foreach (var bodyPart in bodyPartsChoices)
            {
                ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = bodyPart });
            }

            SelectedItem = ComboBoxChoices[0];
        }

        #region methods           

        /// <summary>
        /// Gets ChartModel data type from SQL query based on logged in User
        /// </summary>
        /// <param name="userId"></param>
        public void SetUserValues(int userId)
        {     
            ChartModel = Queries.GetUserValues(userId);
        }

        /// <summary>
        /// checks if chart can display enough datapoints
        /// </summary>
        public void CheckForEnoughValues()
        {
            if (ChartModel != null)
            {
                var noElements = ChartModel.DateAdded.Count;

                if (noElements > 1)
                {
                    EnoughValues = true;
                }

                else
                {
                    EnoughValues = false;
                }

            }
            
        }

        /// <summary>
        /// Sets Time Range of data e.g all, 7 days, 30 days, based on current selected mode in view
        /// </summary>
        /// <param name="mode"></param>
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

        /// <summary>
        /// Sets Chart values based on string value of column name in database
        /// </summary>
        /// <param name="bodyPartName"></param>
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

        /// <summary>
        /// Updates chart with filtered values by date 
        /// </summary>
        /// <param name="filteredValues"></param>
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


            var chartTitle = SelectedItem.Symbol;

            if(SelectedItem.Symbol == "Weight")
            {
                chartTitle = $"{SelectedItem.Symbol} [kg]";
            }

            else
            {
                chartTitle = $"{SelectedItem.Symbol} [cm]";
            }


            //set datapoints and title of the chart
            SeriesCollection = new SeriesCollection(dayConfig)
            {   
                new LineSeries
                {    
                    Title =  chartTitle,
                    Values = Values
                }
            };

            Formatter = value => new DateTime((long)value).ToString("yyyy-MM-dd");

        }
        #endregion
    }
}






