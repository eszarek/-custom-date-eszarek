using System;
using System.Collections.Generic;


namespace DateProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Date class");
            Date d1 = new Date(2020,10,1);
            Console.WriteLine("Calling isToday: "+d1.IsToday());
            Console.WriteLine(d1);
            Console.WriteLine(d1.MonthName);
            Console.WriteLine(d1.MonthNameAbbrev);
            HolidayProvider hp = new HolidayProvider();
            List<Holiday> list = hp.GetHolidays(2020);

            foreach(Holiday h in list) {
                Console.WriteLine($"{h.TheDate} - {h.Name}");
            }


            
        }
    }
}
