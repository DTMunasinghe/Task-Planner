using System;
using System.Collections.Generic;

namespace CleanCodeAssignment
{
    class Program
    {
        public static void Main(string[] args)
        {
            //TimeSpan startTime, stopTime;

            TaskPlanner taskPlanner = new TaskPlanner();

            /*
            Console.WriteLine("Enter Starting Time :");
            string str1 = Console.ReadLine();
            TimeSpan.TryParse(str1, out startTime);

            Console.WriteLine("Enter Stopping Time :");
            string str2 = Console.ReadLine();
            TimeSpan.TryParse(str2, out stopTime);

            taskPlanner.SetWorkdayStartAndStop(startTime, stopTime);

            */
            taskPlanner.SetRecuringHolidays(new DateTime(2004, 5, 17, 0, 0, 0));
            taskPlanner.SetHolidays(new DateTime(2004, 5, 27, 0, 0, 0));

            DateTime stdate = new DateTime(2004, 5, 14, 8, 0, 0);

            DateTime taskFinishDateAndTime = taskPlanner.GetTaskFinishDate(stdate, 1.5);
            //DateTime taskFinishDateAndTime = taskPlanner.GetTaskFinishDate(stdate, -1.75);

            string output = taskFinishDateAndTime.ToString("dd-MM-yyyy H:mm");

            Console.WriteLine(output);

        }
    }
}
