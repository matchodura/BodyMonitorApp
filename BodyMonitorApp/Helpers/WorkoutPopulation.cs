using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class WorkoutPopulation
    {

        public List<WorkoutModel> Workouts { get; set; }


        public WorkoutPopulation()
        {
            Workouts = new List<WorkoutModel>()
            {

                new WorkoutModel() { Category = "Neck", Name = "cwiczenie nr 1", URL = "https://www.youtube.com/embed/cxtnot8lY4U"},
                new WorkoutModel() { Category = "Neck", Name = "cwiczenie nr 2", URL = "https://www.youtube.com/embed/MWilsN_5Y-s"},
                new WorkoutModel() { Category = "Neck", Name = "cwiczenie nr 3", URL = "https://www.youtube.com/embed/jIHkc6ICOcc"},
               
                new WorkoutModel() { Category = "Chest", Name = "Chest1", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Chest", Name = "Chest2", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Chest", Name = "Chest3", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Chest", Name = "Chest3", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Chest", Name = "Chest3", URL = "wykop.pl" },

                new WorkoutModel() { Category = "Stomach", Name = "Testowanie1", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Stomach", Name = "Testowanie2", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Stomach", Name = "Testowanie3", URL = "wykop.pl" },


                new WorkoutModel() { Category = "Waist", Name = "Chest3", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Waist", Name = "Chest3", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Waist", Name = "Chest3", URL = "wykop.pl" },

                new WorkoutModel() { Category = "Hips", Name = "Neck1", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Hips", Name = "Neck2", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Hips", Name = "Neck2", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Hips", Name = "Neck2", URL = "wykop.pl" },

                new WorkoutModel() { Category = "Thigh", Name = "Neck2", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Thigh", Name = "Neck2", URL = "wykop.pl" },

                new WorkoutModel() { Category = "Calf", Name = "Neck2", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Calf", Name = "Neck2", URL = "wykop.pl" },

                new WorkoutModel() { Category = "Biceps", Name = "Testowanie1", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Biceps", Name = "Testowanie2", URL = "wykop.pl" },
                new WorkoutModel() { Category = "Biceps", Name = "Testowanie3", URL = "wykop.pl" }



            };
        }
    }

}

