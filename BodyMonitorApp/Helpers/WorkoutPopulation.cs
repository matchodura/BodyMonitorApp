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

                new WorkoutModel() { Category = "Neck", Name = "Dumbbell shrug", URL = "https://www.youtube.com/embed/cJRVVxmytaM"},
                new WorkoutModel() { Category = "Neck", Name = "Upright row", URL = "https://www.youtube.com/embed/Fq67opsS_hc"},
                new WorkoutModel() { Category = "Neck", Name = "Reverse fly", URL = "https://www.youtube.com/embed/3eOFjmSM9s8"},
               
                new WorkoutModel() { Category = "Chest", Name = "Barbell Bench Press", URL = "https://www.youtube.com/embed/rT7DgCr-3pg" },
                new WorkoutModel() { Category = "Chest", Name = "Chest Press", URL = "https://www.youtube.com/embed/xUm0BiZCWlQ" },
                new WorkoutModel() { Category = "Chest", Name = "Pushups", URL = "https://www.youtube.com/embed/IODxDxX7oi4" },
                new WorkoutModel() { Category = "Chest", Name = "Dips", URL = "https://www.youtube.com/embed/2z8JmcrW-As" },
                new WorkoutModel() { Category = "Chest", Name = "Chest flys", URL = "https://www.youtube.com/embed/6rr5p1jCZC4" },
                new WorkoutModel() { Category = "Chest", Name = "Pec Deck", URL = "https://www.youtube.com/embed/JJitfZKlKk4" },
          

                new WorkoutModel() { Category = "Stomach", Name = "Planks", URL = "https://www.youtube.com/embed/pSHjTRCQxIw" },
                new WorkoutModel() { Category = "Stomach", Name = "Side Plank", URL = "https://www.youtube.com/embed/NXr4Fw8q60o" },
                new WorkoutModel() { Category = "Stomach", Name = "Sit Ups", URL = "https://www.youtube.com/embed/1fbU_MkV7NE" },
                new WorkoutModel() { Category = "Stomach", Name = "Russian Twist", URL = "https://www.youtube.com/embed/wkD8rjkodUI" },
                new WorkoutModel() { Category = "Stomach", Name = "Crunches ", URL = "https://www.youtube.com/embed/Xyd_fa5zoEU" },
             


                new WorkoutModel() { Category = "Waist", Name = "Heel touchers", URL = "https://www.youtube.com/embed/fLajmFLpJ_w" },
                new WorkoutModel() { Category = "Waist", Name = "Triangle crunch", URL = "https://www.youtube.com/embed/9t4bKvpJ2ng" },
                new WorkoutModel() { Category = "Waist", Name = "Starfish crunch", URL = "https://www.youtube.com/embed/WdfyOO6cEVQ" },
                new WorkoutModel() { Category = "Waist", Name = "Standing cross-crunches", URL = "https://www.youtube.com/embed/IZ8_X5vK8Ug" },
             

                new WorkoutModel() { Category = "Hips", Name = "Side lunge with dumbbells", URL = "https://www.youtube.com/embed/qCA8E-dF8cI" },
                new WorkoutModel() { Category = "Hips", Name = "Side leg lifts", URL = "https://www.youtube.com/embed/izV5th7AQHM" },
                new WorkoutModel() { Category = "Hips", Name = "Squats", URL = "https://www.youtube.com/embed/mGvzVjuY8SY" },
                new WorkoutModel() { Category = "Hips", Name = "Dumbbell squats", URL = "https://www.youtube.com/embed/v_c67Omje48" },
                new WorkoutModel() { Category = "Hips", Name = "Hip raises", URL = "https://www.youtube.com/embed/UPcXgTL09lU" },
     

                new WorkoutModel() { Category = "Thigh", Name = "Lunges", URL = "https://www.youtube.com/embed/3XDriUn0udo" },
                new WorkoutModel() { Category = "Thigh", Name = "Step-ups", URL = "https://www.youtube.com/embed/l4AA5d5mInQ" },
                new WorkoutModel() { Category = "Thigh", Name = "Box jumps", URL = "https://www.youtube.com/embed/hxldG9FX4j4" },
                new WorkoutModel() { Category = "Thigh", Name = "Bridge", URL = "https://www.youtube.com/embed/wPM8icPu6H8" },
               

                new WorkoutModel() { Category = "Calf", Name = "Standing Barbell Calf Raise", URL = "https://www.youtube.com/embed/71lLP3aglGQ" },
                new WorkoutModel() { Category = "Calf", Name = "Seated Dumbbell Calf Raise", URL = "https://www.youtube.com/embed/W-NU8NUS8lI" },
                new WorkoutModel() { Category = "Calf", Name = "Farmer’s Walk", URL = "https://www.youtube.com/embed/rt17lmnaLSM" },
                new WorkoutModel() { Category = "Calf", Name = "Jumping Jacks", URL = "https://www.youtube.com/embed/Wj9SvxaXvis" },
             

                new WorkoutModel() { Category = "Biceps", Name = "Alternating Incline Dumbbell Curl", URL = "https://www.youtube.com/embed/AFWRIzDA5zI&" },
                new WorkoutModel() { Category = "Biceps", Name = "Seated Alternating Hammer Curl", URL = "https://www.youtube.com/embed/nRgxYX2Ve9w" },
                new WorkoutModel() { Category = "Biceps", Name = "Standing Reverse Barbell Curl", URL = "https://www.youtube.com/embed/g_FIfe2_GUo" },
                new WorkoutModel() { Category = "Biceps", Name = "Seated Alternating Dumbbell Curl", URL = "https://www.youtube.com/embed/85kXYq7Ssh4" },
                new WorkoutModel() { Category = "Biceps", Name = "Standing Cable Curl", URL = "https://www.youtube.com/embed/qf6KO7qKFRI" }
       
            };
        }
    }

}

