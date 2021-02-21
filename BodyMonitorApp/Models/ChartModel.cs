using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyMonitorApp
{
    public class ChartModel : ObservableObject
    {

        #region Fields

        private List<DateTime> _dateAdded;
        private List<decimal> _chest;
        private List<decimal> _biceps;
        private List<decimal> _weight;
        private List<decimal> _neck;
        private List<decimal> _stomach;
        private List<decimal> _waist;
        private List<decimal> _hips;
        private List<decimal> _thigh;
        private List<decimal> _calf;   
        
        #endregion

        #region properties

        public List<DateTime> DateAdded
        {
            get { return _dateAdded; }
            set
            {
                if (value != _dateAdded)
                {
                    _dateAdded = value;
                    OnPropertyChanged("DateAdded");
                }
            }
        }

        public List<decimal> Chest
        {
            get { return _chest; }
            set
            {
                if (value != _chest)
                {
                    _chest = value;
                    OnPropertyChanged("Chest");
                }
            }
        }

        public List<decimal> Biceps
        {
            get { return _biceps; }
            set
            {
                if (value != _biceps)

                {
                    _biceps = value;
                    OnPropertyChanged("Biceps");
                }
            }
        }

        public List<decimal> Weight
        {
            get { return _weight; }
            set
            {
                if (value != _weight)
                {
                    _weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }

        public List<decimal> Neck
        {
            get { return _neck; }

            set
            {
                if (value != _neck)
                {
                    _neck = value;
                    OnPropertyChanged("Neck");

                }
            }
        }

        public List<decimal> Stomach
        {
            get { return _stomach; }
            set
            {
                if (value != _stomach)
                {
                    _stomach = value;
                    OnPropertyChanged("Stomach");
                }
            }
        }

        public List<decimal> Waist
        {
            get { return _waist; }
            set
            {
                if (value != _waist)
                {
                    _waist = value;
                    OnPropertyChanged("Waist");
                }
            }
        }

        public List<decimal> Hips
        {
            get { return _hips; }
            set
            {
                if (value != _hips)
                {
                    _hips = value;
                    OnPropertyChanged("Hips");
                }
            }
        }

        public List<decimal> Thigh
        {
            get { return _thigh; }
            set
            {
                if (value != _thigh)
                {
                    _thigh = value;
                    OnPropertyChanged("Thigh");
                }
            }
        }

        public List<decimal> Calf
        {
            get { return _calf; }
            set
            {
                if (value != _calf)
                {
                    _calf = value;
                    OnPropertyChanged("Calf");
                }
            }
        }

        #endregion
    }

}
