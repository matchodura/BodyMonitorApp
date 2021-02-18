using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BodyMonitorApp
{
    public class WorkoutViewModel : ObservableObject, IPageViewModel
    {

        #region fields

        private Visibility _visibility = Visibility.Hidden;
        private Visibility _exercisesVisibility = Visibility.Hidden;

        private ICommand _changeBodyPartCommand;
        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private int _currentPageIndex = 0;
        private int _indexPageCount;

       
        private string _bodyPart;
     

        private List<WorkoutModel> _workouts;
        private List<WorkoutModel> _chosenWorkouts;

        private WorkoutModel _workout;
        #endregion

        public WorkoutModel Workout
        {
            get
            {
                return _workout;
            }
            set
            {
                _workout = value;

                OnPropertyChanged("Workout");
            }
        }


        public WorkoutViewModel()
        {

            var workouts = new WorkoutPopulation();
            Workouts = workouts.Workouts;

            ChosenWorkouts = workouts.Workouts;

            IndexPagecount = Workouts.Count -1;
            Workout = Workouts[0];
        }
    



        #region properties/commands

       
        public List<WorkoutModel> Workouts
        {
            get { return _workouts; }

            set
            {
                if (value != _workouts)
                {
                    _workouts = value;
                    OnPropertyChanged("Workouts");
                }
            }

        }


        public List<WorkoutModel> ChosenWorkouts
        {
            get { return _chosenWorkouts; }

            set
            {
                if (value != _chosenWorkouts)
                {
                    _chosenWorkouts = value;
                    OnPropertyChanged("ChosenWorkouts");
                }
            }

        }

        public string Name
        {
            get
            {
                return "Workouts";
            }
            set {;}
        }
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

        public Visibility ExercisesVisibility
        {
            get
            {
                return _exercisesVisibility;
            }
            set
            {
                _exercisesVisibility = value;

                OnPropertyChanged("ExercisesVisibility");
            }
        }
        public string BodyPart
        {
            get
            {
                return _bodyPart;
            }
            set
            {
                _bodyPart = value;

                OnPropertyChanged("BodyPart");
            }
        }

        public ICommand ChangeBodyPartCommand
        {
            get
            {
                if (_changeBodyPartCommand == null)
                {
                    _changeBodyPartCommand = new RelayCommand(
                        param => ChangeBodyPart(param));

                }
                return _changeBodyPartCommand;
            }
        }

        public ICommand NextPageCommand
        {          
            get
            {
                if (_nextPageCommand == null)
                {
                    _nextPageCommand = new RelayCommand(
                        param => IndexUp());

                }
                return _nextPageCommand;
            }

        }


        public ICommand PreviousPageCommand
        {
           
            get
            {
                if (_previousPageCommand == null)
                {
                    _previousPageCommand = new RelayCommand(
                        param => IndexDown());

                }
                return _previousPageCommand;
            }
        }


        public int CurrentPageIndex
        {
            get
            {
                return _currentPageIndex;
            }
            set
            {
                _currentPageIndex = value;
              
             
                OnPropertyChanged("CurrentPageIndex");
            }
        }


        public int IndexPagecount
        {
            get
            {
                return _indexPageCount;
            }
            set
            {
                _indexPageCount = value;
                OnPropertyChanged("CurrentPageIndex");
            }
        }

       
        public void ChangeBodyPart(object param)
        {
            BodyPart = (string)param;
            ExercisesVisibility = Visibility.Visible;
            ChosenWorkouts = Workouts.Where(p => p.Category == BodyPart).ToList();
            IndexPagecount = ChosenWorkouts.Count - 1;
            CurrentPageIndex = 0;
            Workout = ChosenWorkouts[CurrentPageIndex];
        }

        public void IndexUp()
        {

            if (CurrentPageIndex < IndexPagecount)
            {
                CurrentPageIndex += 1;      
                
            }

            else
            {
                CurrentPageIndex = 0;
            }

            Workout = ChosenWorkouts[CurrentPageIndex];

        }


        public void IndexDown()
        {

            if (CurrentPageIndex > 0)
            {
                CurrentPageIndex -= 1;
            }
            else
            {
                CurrentPageIndex = IndexPagecount;
            }

            Workout = ChosenWorkouts[CurrentPageIndex];
        }
        #endregion

    }

}
