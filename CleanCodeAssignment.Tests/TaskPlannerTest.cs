using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCodeAssignment.Tests
{
    [TestClass]
    public class TaskPlannerTest
    {   
        [TestMethod]
        public void GetTaskFinishDate_IncreaseDaysAndTime_IfStartTimeIsEarlyAndWorkingDaysIsPositive()
        {

            //Arrange
            var taskPlanner = new TaskPlanner();

            //Act
            taskPlanner.SetWorkdayStartAndStop(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
            taskPlanner.SetRecuringHolidays(new DateTime(2004, 5, 17, 0, 0, 0));
            taskPlanner.SetHolidays(new DateTime(2004, 5, 27, 0, 0, 0));

            var start = new DateTime(2004, 5, 24, 07, 03, 0);
            double numberOfDays = 8.276628;

            var actual = taskPlanner.GetTaskFinishDate(start, numberOfDays);
            var expected = new DateTime(2004, 6, 4, 10, 12, 0);

            //Assert
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void GetTaskFinishDate_IncreaseDaysAndTime_IfStartTimeIsAfterTheWorkDayStopTimeAndWorkingDaysIsPositive()
        {
            //Arrange
            var taskPlanner = new TaskPlanner();

            //Act
            taskPlanner.SetWorkdayStartAndStop(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
            taskPlanner.SetRecuringHolidays(new DateTime(2004, 5, 17, 0, 0, 0));
            taskPlanner.SetHolidays(new DateTime(2004, 5, 27, 0, 0, 0));

            var start = new DateTime(2004, 5, 24, 19, 03, 0);
            double numberOfDays = 44.723656;

            var actual = taskPlanner.GetTaskFinishDate(start, numberOfDays);
            var expected = new DateTime(2004, 7, 27, 13, 47, 0);

            //Assert
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void GetTaskFinishDate_IncreaseDaysAndTime_IfStartTimeIsAfterTheWorDayStartTimeAndWorkingDaysIsPositive()
        {
            //Arrange
            var taskPlanner = new TaskPlanner();

            //Act
            taskPlanner.SetWorkdayStartAndStop(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
            taskPlanner.SetRecuringHolidays(new DateTime(2004, 5, 17, 0, 0, 0));
            taskPlanner.SetHolidays(new DateTime(2004, 5, 27, 0, 0, 0));

            var start = new DateTime(2004, 5, 24, 08, 03, 0);
            double numberOfDays = 12.782709;

            var actual = taskPlanner.GetTaskFinishDate(start, numberOfDays);
            var expected = new DateTime(2004, 6, 10, 14, 18, 0);

            //Assert
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void GetTaskFinishDate_DecreaseDaysAndTime_IfStartTimeIsAfterTheWorDayStopTimeAndWorkingDaysIsNegative()
        {
            //Arrange
            var taskPlanner = new TaskPlanner();

            //Act
            taskPlanner.SetWorkdayStartAndStop(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
            taskPlanner.SetRecuringHolidays(new DateTime(2004, 5, 17, 0, 0, 0));
            taskPlanner.SetHolidays(new DateTime(2004, 5, 27, 0, 0, 0));

            var start = new DateTime(2004, 5, 24, 18, 05, 0);
            double numberOfDays = -5.5;

            var actual = taskPlanner.GetTaskFinishDate(start, numberOfDays);
            var expected = new DateTime(2004, 5, 14, 12, 00, 0);

            //Assert
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void GetTaskFinishDate_DecreaseDaysAndTime_IfStartTimeIsAfterTheWorDayStopTimeAndWorkingDaysIsNegative2()
        {
            //Arrange
            var taskPlanner = new TaskPlanner();

            //Act
            taskPlanner.SetWorkdayStartAndStop(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
            taskPlanner.SetRecuringHolidays(new DateTime(2004, 5, 17, 0, 0, 0));
            taskPlanner.SetHolidays(new DateTime(2004, 5, 27, 0, 0, 0));

            var start = new DateTime(2004, 5, 24, 18, 03, 0);
            double numberOfDays = -6.7470217;

            var actual = taskPlanner.GetTaskFinishDate(start, numberOfDays);
            var expected = new DateTime(2004, 5, 13, 10, 02, 0);

            //Assert
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
