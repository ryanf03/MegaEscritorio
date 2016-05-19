using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaEscritorioApplication
{
    class Program
    {
        double length;
        double width;
        string material;
        double drawers;
        double days;


        static void Main()
        {

            Console.WriteLine("Welcome to Mega Escritorio ordering portal."
                            + "\n Press any key to continue.");
            Console.ReadLine();

            Program program = new Program();
            program.getInfo();


        }



        private double getInfo()
        {

            double totalPrice;




            // Get desk length.
            Console.WriteLine("Please enter desired desk length in inches.");
            length = Convert.ToDouble(Console.ReadLine());

            // Get desk width.
            Console.WriteLine("Please enter width in inches.");
            width = Convert.ToDouble(Console.ReadLine());

            // Get material type.
            Console.WriteLine("Please enter desk material (Laminate, Oak, or Pine).");
            material = Console.ReadLine();

            //get number of drawers.
            Console.WriteLine("Please enter the number of drawers.");
            drawers = Convert.ToDouble(Console.ReadLine());

            // Get production length.
            Console.WriteLine("Please enter desired production time:"
                               + "\n 3, 5, 7 day dush or 14 day standard.");
            days = Convert.ToDouble(Console.ReadLine());

            totalPrice = calcPrice();

            StreamWriter writer;
            writer = new StreamWriter(@"C:\Users\Public\DeskOrder.txt");
            writer.WriteLine("\"Length\" : \"" + length + "\"");
            writer.WriteLine("\"Width\" : \"" + width + "\"");
            writer.WriteLine("\"Material\" : \"" + material + "\"");
            writer.WriteLine("\"Production Time\" : \"" + days + " days\"");
            writer.WriteLine("\"Total Price\" : \"" + totalPrice + "\"");
            writer.Close();

            Console.WriteLine("Final Price:  $" + totalPrice);
            Console.WriteLine("Press any key to Exit.");
            Console.ReadLine();

            return totalPrice;


        }


        private double calcPrice()
        {
            double area;
            double areaPrice;
            double materialPrice;
            double finalPrice;
            int rushDay;
            int rushSize;
            int rushlookup;
            double rushPrice;
            double prodPrice;
            double drawerPrice;

            // Calcualte desk surface area.
            area = length * width;

            // Calculate size costs 
            if (area > 1000)
            {
                areaPrice = 200 + ((area - 1000) * 5);
            }
            else
            {
                areaPrice = 200;
            }

            // Calculate material costs.
            if (material == "Laminate")
            {
                materialPrice = 100;
            }
            else if (material == "Oak")
            {
                materialPrice = 200;
            }
            else
            {
                materialPrice = 50;
            }

            //calculate drawer costs.
            drawerPrice = drawers * 50;

            // calculate production costs.
            string[] rush = System.IO.File.ReadAllLines(@"C:\Users\Public\rushOrderPrices.txt");

            // get rush table look up value
            if (days == 3)
            { rushDay = 0; }
            else if (days == 5)
            { rushDay = 3; }
            else
            { rushDay = 6; }

            if (area <= 1000)
            { rushSize = 1; }
            else if (area > 1000 && area < 2000)
            { rushSize = 2; }
            else { rushSize = 3; }

            rushlookup = (rushDay + rushSize) - 1;

            rushPrice = Convert.ToDouble(rush[rushlookup]);

            if (days != 14)
            { prodPrice = rushPrice; }
            else
            { prodPrice = 0; }





            // Calculate fianl price.
            finalPrice = areaPrice + materialPrice + prodPrice;

            return finalPrice;
        }

    }
}
