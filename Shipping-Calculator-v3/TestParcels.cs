/*
 * C9519
 * Program 1b
 * 17 October 2016
 * CIS 200-01
 * This test program uses LINQ on dummy data to sort and select parcels
*/

// Program 1A
// CIS 200-01/76
// Fall 2016
// Due: 10/10/2016
// By: Andrew L. Wright (students use Grading ID)

// File: TestParcels.cs
// This is a simple, console application designed to exercise the Parcel hierarchy.
// It creates several different Parcels and prints them.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1b
{
    class Program
    {
        // Precondition:  None
        // Postcondition: Pauses screen output
        public static void Pause()
        {
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
            Console.Clear();
        }

        // Precondition:  None
        // Postcondition: List of Parcels is created and displayed
        static void Main(string[] args)
        {
            Address a1 = new Address("John Smith", "123 Any St.", "Apt. 45",
                "Louisville", "KY", 40202); // Test Address 1
            Address a2 = new Address("Jane Doe", "987 Main St.", "",
                "Beverly Hills", "CA", 90210); // Test Address 2
            Address a3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321",
                "El Paso", "TX", 79901); // Test Address 3
            Address a4 = new Address("John Crichton", "678 Pau Place", "Apt. 7",
                "Portland", "ME", 04101); // Test Address 4
            Address a5 = new Address("Frank Miller", "5503 Hames Trace", "Apt 466",
                "Louisville", "KY", 40215); // Test address 5
            Address a6 = new Address("Desirae Griggs", "2009 Joe Lane", "",
                "Kearney", "MO", 64048); // Test address 6
            Address a7 = new Address("William Faust", "346 S Howard St", "PO Box 12",
                "Atlanta", "GA", 30317); // Test address 7
            Address a8 = new Address("Judy Thompson", "3983 Explorer Rd", "",
                "Wheatfield", "IN", 46392); // Test address 8

            Letter l1 = new Letter(a1, a3, 1.50M); // Test Letter 1
            Letter l2 = new Letter(a2, a5, 1.25M); // Test Letter 2
            Letter l3 = new Letter(a4, a6, 1.75M); // Test Letter 3
            Letter l4 = new Letter(a8, a7, 2.00M); // Test Letter 4

            GroundPackage gp1 = new GroundPackage(a1, a2, 12, 4, 2, 2); // Test ground package 1
            GroundPackage gp2 = new GroundPackage(a5, a4, 12, 4, 2, 2); // Test ground package 2
            GroundPackage gp3 = new GroundPackage(a3, a7, 64, 25, 9, 10); // Test ground package 3
            GroundPackage gp4 = new GroundPackage(a8, a6, 80, 12, 8, 4); // Test ground package 4

            NextDayAirPackage ndap1 = new NextDayAirPackage(a2, a7, 12, 4, 2, 2, 25.00m); // Test next-day air package 1
            NextDayAirPackage ndap2 = new NextDayAirPackage(a4, a3, 75, 25, 10, 10, 30.00m); // Test next-day air package 2
            NextDayAirPackage ndap3 = new NextDayAirPackage(a1, a5, 12, 4, 2, 80, 15.00m); // Test next-day air package 3
            NextDayAirPackage ndap4 = new NextDayAirPackage(a8, a6, 75, 25, 20, 95, 20.00m); // Test next-day air package 4

            TwoDayAirPackage tdap1 = new TwoDayAirPackage(a7, a1, 12, 4, 2, 2, TwoDayAirPackage.Delivery.Saver); // Test two-day air package 1
            TwoDayAirPackage tdap2 = new TwoDayAirPackage(a4, a2, 12, 4, 2, 2, TwoDayAirPackage.Delivery.Early); // Test two-day air package 2
            TwoDayAirPackage tdap3 = new TwoDayAirPackage(a5, a8, 64, 25, 9, 10, TwoDayAirPackage.Delivery.Early); // Test two-day air package 3
            TwoDayAirPackage tdap4 = new TwoDayAirPackage(a6, a3, 12, 4, 2, 2, TwoDayAirPackage.Delivery.Saver); // Test two-day air package 4

            List<Parcel> parcels = new List<Parcel>(); // Test list of parcels

            // Add test data to list
            parcels.Add(l1);
            parcels.Add(l2);
            parcels.Add(l3);
            parcels.Add(l4);
            parcels.Add(gp1);
            parcels.Add(gp2);
            parcels.Add(gp3);
            parcels.Add(gp4);
            parcels.Add(ndap1);
            parcels.Add(ndap2);
            parcels.Add(ndap3);
            parcels.Add(ndap4);
            parcels.Add(tdap1);
            parcels.Add(tdap2);
            parcels.Add(tdap3);
            parcels.Add(tdap4);

            // Display LINQ reports
            Console.WriteLine($"Program 1b - LINQ Reports{Environment.NewLine}");

            //Holds results of LINQ to sort descending by destination zip code
            var zipDescending =
                from parcel in parcels
                orderby parcel.DestinationAddress.Zip descending
                select parcel;

            Console.WriteLine("Sorted in descending order by destination zip code");
            Console.WriteLine("==================================================");
            foreach (var i in zipDescending)
                Console.WriteLine($"{i.DestinationAddress.Zip:d5}");
            Console.WriteLine();
            Pause();

            // Holds results of LINQ to sort by cost
            var costDescending =
                  from parcel in parcels
                  orderby parcel.CalcCost()
                  select parcel;

            Console.WriteLine("Sorted by cost");
            Console.WriteLine("==============");
            foreach (var i in costDescending)
                Console.WriteLine($"{i.CalcCost().ToString("C"),14}");
            Console.WriteLine();
            Pause();

            // Holds results of LINQ to sort descending by parcel type and ascending for cost
            var parcelTypeAndCost =
                  from parcel in parcels
                  let type = parcel.GetType()   // Holds parcel type
                  let cost = parcel.CalcCost()  // Holds parcel cost
                  orderby type.ToString() descending, cost
                  select new { type, cost };

            Console.WriteLine("Sorted by parcel type and in descending order by cost");
            Console.WriteLine("=====================================================");
            foreach (var i in parcelTypeAndCost)
                Console.WriteLine($"{i.type,-20} {i.cost.ToString("C"),15}");
            Console.WriteLine();
            Pause();

            // Holds results of LINQ to display only air packages sorted in descending order by weight
            var airPackageByWeight =
                from parcel in parcels
                let airPackage = parcel as AirPackage   // Holds parcel so it can be treated like an air package
                where airPackage != null && airPackage.IsHeavy()
                orderby airPackage.Weight descending
                select airPackage;

            Console.WriteLine("Select only heavy air packages and sort descending by weight");
            Console.WriteLine("============================================================");
            foreach (var i in airPackageByWeight)
                Console.WriteLine(i.Weight);
            Console.WriteLine();
        }
    }
}