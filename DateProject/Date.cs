using System.Collections.Generic;
using System;
namespace DateProject{

    public class Date {

        private ISystemDateProvider _provider;
        private IHolidayProvider _holidayProvider;

        

        private int _year;
        private int _month;
        private int _day;

        public int Year {
            get {
                return _year;
            }
        }
        public int Month { 
            get { return _month;}
        }
        public string MonthName {
            get {
                switch(_month) {
                    case 1: return "January"; 
                    case 2: return "February"; 
                    case 3: return "March"; 
                    case 4: return "April"; 
                    case 5: return "May";
                    case 6: return "August";
                    case 7: return "September";
                    case 10: return "October";
                    case 11: return "November";
                    case 12: return "December";
                    default: return "Unknown"; 
                }
            }
        }
        public string MonthNameAbbrev {
            get {
                switch(_month) {
                    case 1: return "Jan"; 
                    case 2: return "Feb"; 
                    case 3: return "Mar"; 
                    case 4: return "Apr";
                    case 5: return "May";
                    case 6: return "Jun";
                    case 7: return "Jul";
                    case 8: return "Aug";
                    case 9: return "Sept";
                    case 10: return "Oct";
                    case 11: return "Nov";
                    case 12: return "Dec";
                    default: return "Unknown"; 
                }
            }
        }

        public int Day {
            get {
                return _day;
            }
        }
        public Date() {
            _year=1900;
            _month=1;
            _day=1;
        }

        //checks validdity of passed date in constructor of  Date, if no date provided passes the system date
        public Date(int year, int month, int day) {
            //you can only create a date this way
            if(year >= -9998 && year <= 9999) {
                _year=year;
            }
            else {
                throw new System.ArgumentOutOfRangeException("Year must be between -9998 and 9999.");
            }
            if (month >=1 && month <=12) {
                _month=month;
            }
            else {
                throw new System.ArgumentOutOfRangeException ("Month must be between 1 and 12.");
            }
            if(month==9 || month == 4 || month ==6 || month==11) {
                if(day >=1 && day <= 30) {
                    _day=day;
                }
                else {
                    throw new System.ArgumentOutOfRangeException("Day must be between 1 and 30.");
                }
            }
            else if(month==2) {
                if(day >=1 && day <= 28) {
                    _day=day;
                }
                else {
                    throw new System.ArgumentOutOfRangeException("Day must be between 1 and 28.");
                }
            }
            else {
                if(day >=1 && day <= 31) {
                    _day=day;
                }
                else {
                    throw new System.ArgumentOutOfRangeException("Day must be between 1 and 31.");
                }
            }

        _provider = new MySystemDateProvider();
        _holidayProvider =  new HolidayProvider();


            
        }
        //overloads constructor if date is provided
        //The *this* keyword refers to the current instance of the class and is also used as a modifier of the first parameter of an extension method.
        public Date(int year, int month,int day, ISystemDateProvider provider ):this(year,month,day) {
            _provider=provider;
        }

        public Date(int year, int month,int day, IHolidayProvider holidayProvider ):this(year,month,day) {
            _holidayProvider=holidayProvider;
        }

        /*public Date (int year, int month,int day, IFindHolidays holidayProvider ):this(year,month,day) {
            _FindHolidays=FindHolidays;
        }*/

        public override string ToString()
        {
            return $"{{{Year}-{Month}-{Day}}}";
        }
       
        //Seam for unit testing
        public bool IsToday() {
            //System.DateTime today = System.DateTime.UtcNow;
            Date today = _provider.GetToday();
            //When you do this, create an provider object and call a get today on it. When create a Date object, create a Isystemdateprovider interface. this interface has
            //one method that returns the date, and an implementation Myssytemdateprovider, which returns the system date and a custom my custom datetime object.
            //When creating object, pass system date provider to object. If you don't provide one, it defaults system date. 
    
            
            if(this.Month == today.Month && this.Day == today.Day)
                return true;
            else
                return false; 
        }

       
        public string WhatHolidayIsOnThisDay() {
            //assume this is an external dependency - code that you don't control or test
            //can't predict what these dates will be when writing tests
            //HolidayProvider _holidayProvider = new HolidayProvider();

            List<Holiday> list = _holidayProvider.GetHolidays(Year);

            foreach(Holiday h in list) {
                Console.WriteLine($"{h.TheDate} - {h.Name}");
            }

            foreach(Holiday holiday in list) {
                //add code later to handle if two holidays are in the list for this day
                if(holiday.TheDate.Month == Month && holiday.TheDate.Day==Day)
                    return holiday.Name;
            }
            return null;

        }

        public  List<String> WhatHolidaysAreOnThisDay() {
            //assume this is an external dependency - code that you don't control or test
            //can't predict what these dates will be when writing tests
            //HolidayProvider _holidayProvider = new HolidayProvider();

            List<Holiday> holidayList = _holidayProvider.GetHolidays(Year);
            
            List<String> foundHoliday = new List<String>() ;
            bool anythingFound = false;
            foreach (Holiday holiday in holidayList)
            {
                    if(holiday.TheDate.Month != Month && holiday.TheDate.Day!=Day)
                    {
                    foundHoliday.Add (holiday.Name);
                    anythingFound = true;
                    
                    }    
  
            }
            if (!anythingFound) {
                return foundHoliday;}

            else {return null;}

        }
    
     
        
        public Date AddOneMonth() {
             if(this.Month==12) {
                    return new Date(this.Year+1, 1, InvalidDayForMonth(1,this.Day)? MaxDayOfMonth(1):this.Day);
                }
             else return new Date(this.Year, this.Month+1,InvalidDayForMonth(this.Month+1,this.Day)? MaxDayOfMonth(this.Month+1):this.Day );

        }
        //add these private methods for help with validation - don't need to test, only need to test public facing methods

        
        private int MaxDayOfMonth(int month) {
            switch(month) {
                case 9: 
                case 4:
                case 6:
                case 11: return 30;
                case 2: return 28;
                default: return 31;
            }
        }
        private bool InvalidDayForMonth(int day, int month) {
            switch(month) {
                case 9: 
                case 4:
                case 6:
                case 11: if(day>30) return true;break;
                case 2: if(day>28) return true; break;
                default: if(day>31) return true;break;
            }
            return false;
        }

    }

    //date interface
    public interface ISystemDateProvider {
        public Date GetToday();
       
    }

    //date interface implementation
    public class MySystemDateProvider:ISystemDateProvider {
        public Date GetToday() {
            System.DateTime now = System.DateTime.UtcNow;
            return new Date(now.Year,now.Month,now.Day);
        }
    }

    //holiday interface
    public interface IHolidayProvider {
        public List<Holiday> GetHolidays (int year);
    } 

}
