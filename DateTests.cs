using System;
using Xunit;
using Moq;
using System.Collections.Generic;

namespace DateProject.Tests
{
    public class DateTests
    {
        [Fact]
        public void DateName_MonthIs1_ReturnsJanuary()
        {
            //Arrange
            Date d = new Date();

            //Act
            string monthName =d.MonthName;

            //Assert

            Assert.Equal("January", monthName);

        }
        [Fact]
        public void DateName_MonthIs2_ReturnsFeb(){
            //Arrange
            Date d = new Date(2020, 2, 1);

            //Act
            string MonthName = d.MonthName;

            //assert

            Assert.Equal ("February", MonthName);
        }
        [Theory]
        [InlineData(12,"December")]
        [InlineData(11,"November")]
        [InlineData(10,"October")]
        
        public void DateName_MonthIs1To12_ReturnsCorretctMonthNameString(int monthNum, string Name){
            //Arrange
            Date d = new Date(2020, monthNum, 1);

            //Act
            string MonthName = d.MonthName;

            //assert

            Assert.Equal (Name, MonthName);
        }
        [Fact]
        public void DateConstructor_YearIsTooBig_ThrowsArgumentOutOfRange()  {
            // Given
            int year = 10000;
            // When
            Date d; // = new Date (year, 1, 1);
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(()=>d=new Date(year, 1, 1));
        }

        [Fact]
        public void DateConstructor_YearIsTooSmall_ThrowArgumentOutOfRangeException()
        {
            // Given
            int year = -10000;

            // When
            Date d;            
        
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(()=>d=new Date (year, 1, 1));
        }
        [Fact]
        public void DateConstructor_MonthTooSmall_ThrowsArgumentsOutOfRangeException()
        {
            // Given
            int month = 0;
        
            // When
            Date d;
        
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(()=>d=new Date(2020, month, 1));
        }
        [Fact]
        public void DateConstructor_MonthToobig_ThrowsArgumentsOutOfRangeException()
        {
            // Given
            int month = 14;
        
            // When
            Date d;
        
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(()=>d=new Date(2020, month, 1));
        }

     
        public void DateConstructor_MonthIs30DayMax_DayIsBetween1And30_DateSetAccordingToParameters()
        {
            // Given
            int day = 30;
            int month = 4;
            // When
            Date d = new Date (2020, month, day);
            // Then
            Assert.Equal(2020, d.Year);
            Assert.Equal(d.Month, month);
            Assert.Equal(d.Day, day);
        }
        [Fact]
        public void DateConstructor_MonthIs30DayMax_DayIsNotBetween1And30_ThrowsArgumentInvalidException()
        {
            // Given
            int day = 31;
            int month = 4;
            // When
            Date d;
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(()=>d=new Date(2020, month, day));
        }
        //homework tests
        [Fact]
         public void DateConstructor_MonthIs31DayMax_DayIsBetween1And31_DateSetAccordingToParameters()
        {
            // Given
            int day = 31;
            int month = 10;
            // When
            Date d = new Date (2020, month, day);
            // Then
            Assert.Equal(2020, d.Year);
            Assert.Equal(d.Month, month);
            Assert.Equal(d.Day, day);
        }

        [Fact]
        public void DateConstructor_MonthIs31DayMax_DayIsNotBetween1And31_ThrowsArgumentInvalidException()
        {
            // Given
            int day = 32;
            int month = 10;
            // When
            Date d;
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(()=>d=new Date(2020, month, day));
        }


        [Fact]
        public void DateConstructor_MonthIs28orFebDayMax_DayIsBetween1And28_DateSetAccordingToParameters()
        {
            // Given
            int day = 28;
            int month = 2;
            // When
             Date d = new Date (2020, month, day);
            // Then
            Assert.Equal(2020, d.Year);
            Assert.Equal(d.Month, month);
            Assert.Equal(d.Day, day);
        }

        //Add tests for 
        [Fact]
        public void DateConstructor_MonthIs28orFebDayMax_DayIsBetween1And28_ThrowsArgumentInvalidException()
        {
            // Given
            int day = 32;
            int month = 2;
            // When
            Date d;
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(()=>d=new Date(2020, month, day));
        }

        [Fact]
        public void isTodayFunction_DateMatchesToday_ReturnsTrue()
        {
            // Given
            //mop framework creates fake isystemdateprovider
            var systemDateProvider = new Mock<ISystemDateProvider>();
            //defined fake object to return 01/01/2020
            systemDateProvider.Setup(m=>m.GetToday()).Returns(new Date (2020, 01, 01));
            //set up new date to use the dateprovider by passing in constructor.
            //
            Date date = new Date (2020, 01,01, systemDateProvider.Object);
        
            // When
            //call method with bool result
            //is today isolated from external dependency using above code
            bool isToday = date.IsToday();
        
            // Then
            Assert.True(isToday);
        }

        [Fact]
        public void isTodayFunction_DateDoesNotMatchToday_ReturnsFalse()
        {
            // Given
            //mop framework creates fake isystemdateprovider
            var systemDateProvider = new Mock<ISystemDateProvider>();
            //defined fake object to return 01/01/2020
            systemDateProvider.Setup(m=>m.GetToday()).Returns(new Date (2020, 01, 01));
            //set up new date to use the dateprovider by passing in constructor
            Date date = new Date (2020, 01,02, systemDateProvider.Object);
        
            // When
            //call method with bool result
            bool isToday = date.IsToday();
        
            // Then
            Assert.False(isToday);
        }

        //holiday test with interface to fake a holiday
        [Fact]
        public void WhatHolidayIsOnThisDay_FindAHolidayBasedOnDate_ReturnHolidayStringName() {
            // Given mock holiday provider
            var holidayProvider = new Mock<IHolidayProvider>();
            List<Holiday> holidays = new List<Holiday>(){
                new Holiday () {TheDate=new Date (2020, 12, 25), Name="Christmas"},
                new Holiday () {TheDate= new Date(2020, 10, 31), Name="Halloween"}
            };
            holidayProvider.Setup(m=>m.GetHolidays(2020)).Returns(holidays);

            Date date = new Date(2020,12, 25,holidayProvider.Object);
        
            // When
            string foundHoliday = date.WhatHolidayIsOnThisDay();
            
            // Then
            Assert.Equal("Christmas", foundHoliday);
        }

        [Fact]
        public void WhatHolidayIsOnThisDay_FindNoHoliday_ReturnsNull() {
            // Given mock holiday provider
            var holidayProvider = new Mock<IHolidayProvider>();
            List<Holiday> holidays = new List<Holiday>(){
                new Holiday () {TheDate=new Date (2020, 12, 25), Name="Christmas"},
                new Holiday () {TheDate= new Date(2020, 10, 31), Name="Halloween"}
            };
            holidayProvider.Setup(m=>m.GetHolidays(2020)).Returns(holidays);

            Date date = new Date(2020,12, 29,holidayProvider.Object);
        
            // When
            string foundHoliday = date.WhatHolidayIsOnThisDay();
            
            // Then
            Assert.Null(foundHoliday);
        }

        [Theory]
        [InlineData(12,"Dec")]
        [InlineData(11,"Nov")]
        [InlineData(10,"Oct")]
        
        public void DateName_MonthIs1To12_ReturnsCorretctAbbreviatedMonthNameString(int monthNum, string Name){
            //Arrange
            Date d = new Date(2020, monthNum, 1);

            //Act
            string MonthNameAbbrev = d.MonthNameAbbrev;

            //assert

            Assert.Equal (Name, MonthNameAbbrev);
        }        
        
        [Fact]
        public void AddOneMonth_Monthis1to11_ReturnIntMonthParameterPlus1()
        {
            // arrange
            int month = 1;
            Date d = new Date (2020, month, 22);
            
            
            // When
           d = d.AddOneMonth();
            // Then
            Assert.Equal(2, d.Month);
                       
        }
        [Fact]
        public void WhatHolidaysAreOnThisDay_TwoHolidaysOnOneDay_ReturnsBothStringHolidayNames()
        {
           var holidayProvider = new Mock<IHolidayProvider>();
            List<Holiday> holidays = new List<Holiday>(){
                new Holiday () {TheDate=new Date (2020, 12, 25), Name="Christmas"},
                new Holiday () {TheDate= new Date(2020, 10, 31), Name="Halloween"},
                new Holiday () {TheDate= new Date(2020, 10, 31), Name="Candy Day"}
            };
            holidayProvider.Setup(m=>m.GetHolidays(2020)).Returns(holidays);
           

            Date date2 = new Date(2020,10, 31,holidayProvider.Object);
            
            // When
            List<String> found = date2.WhatHolidaysAreOnThisDay();


            // Then
            

            Assert.Contains(found, item => item is "Halloween");
            Assert.Contains(found, item => item is "Candy Day");
        }
        [Fact]
        public void WhatHolidaysAreOnThisDay_NoHolidaysReturn_ThrowsException()
        {
           var holidayProvider = new Mock<IHolidayProvider>();
            List<Holiday> holidays = new List<Holiday>(){
                new Holiday () {TheDate=new Date (2020, 12, 25), Name="Christmas"},
                new Holiday () {TheDate= new Date(2020, 10, 31), Name="Halloween"},
                new Holiday () {TheDate= new Date(2020, 10, 31), Name="Candy Day"}
            };
            holidayProvider.Setup(m=>m.GetHolidays(2020)).Returns(holidays);
           Date date2 = new Date(2020,09, 12,holidayProvider.Object);
            // When
            List<String> found = date2.WhatHolidaysAreOnThisDay();

            // Then
            Assert.Null(found);
            
        }
    }   
}
