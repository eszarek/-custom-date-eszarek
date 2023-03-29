
using System.Collections.Generic;
using System;
using System.IO;

namespace DateProject {

    public class HolidayProvider : IHolidayProvider{
        
        public  List<Holiday> GetHolidays(int year) {
            List<Holiday> list1 = new List<Holiday>() ;

            try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader("holidays.txt"))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                   // Console.WriteLine(line);
                    string[] data = line.Split("-");
                    //parse into the Holiday object:
                    Holiday hol = new Holiday() {
                        TheDate=new Date(Int32.Parse(data[0]),Int32.Parse(data[1]),Int32.Parse(data[2])),
                        Name=data[3]
                    };
                    list1.Add(hol);

                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
            List<Holiday> list = new List<Holiday>() {
                //Would be getting this data from a database or web service or file
                new Holiday() {TheDate=new Date(2020, 12,25), Name="Christmas"},
                new Holiday() {TheDate=new Date(2020, 11,24), Name="Thanksgiving"},
                new Holiday() {TheDate=new Date(2020, 10,31), Name="Halloween"},
                new Holiday() {TheDate=new Date(2020, 9,22), Name="End of Summer"},
                new Holiday() {TheDate=new Date(2020, 8, 1), Name="Hot Day"},
                new Holiday() {TheDate=new Date(2020, 7,4), Name="Fourth of July"}

            };
            return list1;

        }
        
        
    }
    public class Holiday {
        public Date TheDate {get;set;}
        public string Name {get;set;}
    }

    
}