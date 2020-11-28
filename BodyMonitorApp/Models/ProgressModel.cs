using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyMonitorApp
{
    public class ProgressModel : ObservableObject
    {

        #region Fields

        private int _userId;
        private int _postId;
        private DateTime _dateAdded;
        private decimal _weight;
        private decimal _neck;
        private decimal _chest;
        private decimal _stomach;
        private decimal _waist;
        private decimal _hips;
        private decimal _thigh;
        private decimal _calf;
        private decimal _biceps;
        private string _note;
        

        #endregion

        #region properties

           
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (value != _userId)
                {
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        public int PostId
        {
            get { return _postId; }
            set
            {
                if (value != _postId)
                {
                    _postId = value;
                    OnPropertyChanged("PostId");
                }
            }
        }

        public DateTime DateAdded
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

        public decimal Weight
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

        public decimal Neck
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

        public decimal Chest
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
               
        public decimal Stomach
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

        public decimal Waist
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

        public decimal Hips
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

        public decimal Thigh
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

        public decimal Calf
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

        public decimal Biceps
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

        public string Note
        {
            get { return _note; }
            set
            {
                if (value != _note)

                {
                    _note = value;
                    OnPropertyChanged("Note");
                }
            }
        }

        #endregion
    }
}
