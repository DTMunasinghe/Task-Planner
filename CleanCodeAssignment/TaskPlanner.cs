using System;
using System.Collections.Generic;

namespace CleanCodeAssignment
{
    public class TaskPlanner
    {
        private TimeSpan workdayStartTime;
        private TimeSpan workdayStopTime;
        private TimeSpan workingTime;
        private List<DateTime> holidays;
        private List<DateTime> recurringHolidays;

        public TaskPlanner()
        {
            workdayStartTime = new TimeSpan(8, 00, 0);
            workdayStopTime = new TimeSpan(16, 00, 0);
            workingTime = workdayStopTime - workdayStartTime;
            holidays = new List<DateTime>();
            recurringHolidays = new List<DateTime>();
        }

        public void SetWorkdayStartAndStop(TimeSpan startTime, TimeSpan stopTime)
        {
            workdayStartTime = startTime;
            workdayStopTime = stopTime;
            workingTime = workdayStopTime - workdayStartTime;
        }

        public void SetHolidays(DateTime dateTime)
        {
            holidays.Add(dateTime);
        }

        public void SetRecuringHolidays(DateTime dateTime)
        {
            recurringHolidays.Add(dateTime);
        }
        
        public bool IsHoliday(DateTime fromDate)
        {
            if (holidays.Contains(fromDate.Date))
            {
                return true;
            }
            return false;
        }

        public bool IsRecurringHoliday(DateTime fromDate)
        {
            //Check the only month and day
            foreach (DateTime i in recurringHolidays)
            {
                if ((i.Month == fromDate.Month) && (i.Day == fromDate.Day)){
                    return true;
                }
            }
            return false;

        }

        //This method for switch methods according to the value of working days
        public DateTime GetTaskFinishDate(DateTime fromDate, double workingDays)
        {
            DateTime exactDateAndTime;
            if (workingDays >= 0)
            {
                exactDateAndTime = GetTaskFinishDateForPositiveDates(fromDate, workingDays);
            }
            else
            {
                exactDateAndTime = GetTaskFinishDateForNegativeDates(fromDate, workingDays);
            }
            return exactDateAndTime;
        }

        //get the task finish date and time for positive working days
        public DateTime GetTaskFinishDateForPositiveDates(DateTime fromDate, double workingDays)
        {

            TimeSpan reminingTime, addToNextDay;
            bool flag1, flag2;
            DateTime futureDate = fromDate;

            if (futureDate.TimeOfDay < workdayStartTime)
            {
                futureDate = futureDate.Add(workdayStartTime - futureDate.TimeOfDay);
            }
            if (futureDate.TimeOfDay > workdayStopTime)
            {
                futureDate = futureDate.AddDays(1);
                flag2 = true;

                while(flag2)
                {
                    if (futureDate.DayOfWeek != DayOfWeek.Saturday && futureDate.DayOfWeek != DayOfWeek.Sunday &&
                        !IsHoliday(futureDate) && !IsRecurringHoliday(futureDate))
                    {
                        futureDate = futureDate.Add(workdayStartTime - futureDate.TimeOfDay);
                        flag2 = false;
                    }
                }   
            }
            if (workingDays == 0)
            {
                futureDate = fromDate;
            }
            else if (workingDays <= 1 && workingDays > 0)
            {
                reminingTime = workingTime * workingDays;
                futureDate = futureDate.Add(reminingTime);
                futureDate = futureDate.AddSeconds(-futureDate.Second);            
            }
            else
            {
                while (workingDays > 1)
                {
                    futureDate = futureDate.AddDays(1);

                    if (futureDate.DayOfWeek != DayOfWeek.Saturday && futureDate.DayOfWeek != DayOfWeek.Sunday &&
                        !IsHoliday(futureDate) && !IsRecurringHoliday(futureDate))
                    {
                        workingDays--;
                    }
                    if (workingDays <= 1)
                    {
                        reminingTime = workingTime * workingDays;
                        futureDate = futureDate.Add(reminingTime);
                        futureDate = futureDate.AddSeconds(-futureDate.Second);
                    }
                }
            }
            if (futureDate.TimeOfDay > workdayStopTime)
            {
                addToNextDay = futureDate.TimeOfDay - workdayStopTime;
                flag1 = true;

                while (flag1)
                {
                    futureDate = futureDate.AddDays(1);
                    if (futureDate.DayOfWeek != DayOfWeek.Saturday && futureDate.DayOfWeek != DayOfWeek.Sunday &&
                            !IsHoliday(futureDate) && !IsRecurringHoliday(futureDate))
                    {
                        futureDate = futureDate.Add(-workingTime);
                        futureDate = futureDate.AddSeconds(-futureDate.Second);
                        flag1 = false;
                    }
                }
            }
            else
            {
                futureDate = futureDate.AddSeconds(-futureDate.Second);
                return futureDate;
            }
            return futureDate; // return the date and time
        }

        //get the task finish date and time for negative working days
        public DateTime GetTaskFinishDateForNegativeDates(DateTime fromDate, double workingDays)
        {
            TimeSpan reminingTime, addToPreviousDay;
            DateTime futureDate = fromDate;
            bool flag = true;
            
            if (futureDate.TimeOfDay > workdayStopTime)
            {
                futureDate = futureDate.Add(workdayStopTime - futureDate.TimeOfDay);
            }
            if (workingDays >= -1)
            {
                reminingTime = workingTime * workingDays;
                futureDate = futureDate.Add(reminingTime);
                futureDate = futureDate.AddSeconds(futureDate.Second);
            }
            else
            {
                while (workingDays < -1)
                {
                    futureDate = futureDate.AddDays(-1);

                    if (futureDate.DayOfWeek != DayOfWeek.Saturday && futureDate.DayOfWeek != DayOfWeek.Sunday &&
                        !IsHoliday(futureDate) && !IsRecurringHoliday(futureDate))
                    {
                        workingDays++;
                    }
                    if (workingDays >= -1)
                    {
                        reminingTime = workingTime * workingDays;
                        futureDate = futureDate.Add(reminingTime);
                        futureDate = futureDate.AddSeconds(futureDate.Second);
                    }
                }
            }
            if (futureDate.TimeOfDay < workdayStartTime)
            {
                addToPreviousDay = workdayStartTime - futureDate.TimeOfDay;
                while (flag)
                {
                    futureDate = futureDate.AddDays(-1);
                    if (futureDate.DayOfWeek != DayOfWeek.Saturday && futureDate.DayOfWeek != DayOfWeek.Sunday &&
                            !IsHoliday(futureDate) && !IsRecurringHoliday(futureDate))
                    {
                        futureDate = futureDate.Add(workdayStopTime - futureDate.TimeOfDay);
                        futureDate = futureDate.Subtract(addToPreviousDay);
                        futureDate = futureDate.AddSeconds(futureDate.Second);
                        flag = false;
                    }
                }
            }
            return futureDate; // return future date and time
        }
    }
}
