using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//Need to update code so the program disables timestamp in mYsql before running code, and also actives it after its done.
namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            double value = 0;
            int count = 0;
            int days = -720;

            try
            {
                DbConnect con1 = new DbConnect();
                //con1.DisableTimeStamp();
                Console.WriteLine("Writing Data");
                
                while (count <= 720)
                {
                    DbConnect con2 = new DbConnect();
                    value = con2.Temperature(count);
                    con2.PopulateHIstorian(value, days, 2, 1);
                    Console.WriteLine(value + " added");

                    
                    count++;
                    days++;
                    
                }

                Console.WriteLine("Data added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
            }
           
            Console.ReadLine();
            
        }

    }
}
