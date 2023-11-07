using EFLab10.Dta;

using EFLab10.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;
using System.Linq;


namespace EFLab10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (NorthwindContext Context = new NorthwindContext())
            {
                Console.WriteLine("Do you want to sort in ascending or descending order? (A/D)");
                string UserInput = Console.ReadLine();

                var customers = Context.Customers

                    .Select(c => new
                    {
                        c.CompanyName,
                        c.Country,
                        c.Phone,
                        c.Region,

                        OrderCount = c.Orders.Count

                    })
                   
                    .ToList();


                if (UserInput == "A")
                {
                    customers = customers.OrderBy(c => c.CompanyName).ToList();
                }
                else if (UserInput == "D")
                {
                    customers = customers.OrderByDescending(c => c.CompanyName).ToList();
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    return;  
                }

                foreach (var c in customers)
                {
                    Console.WriteLine($"Company name is:{c.CompanyName}\n Country is : {c.Country}\n phone is :{c.Phone}\n Region is :{c.Region}\n  Total Order is:{c.OrderCount}");

                }
                while (true)
                {
                    Console.WriteLine("Choose one Customer on the list bu entering their CompanyName: ");
                    string ChoiceCustomer = Console.ReadLine();

                    var SelectedCustomer = Context.Customers

                      .Include(c => c.Orders)
                      .SingleOrDefault(c => c.CompanyName == ChoiceCustomer);


                    if (SelectedCustomer != null)
                    {

                        Console.WriteLine(
                            $"Companyname:{SelectedCustomer.CompanyName}\n " +
                            $"Contactname:{SelectedCustomer.ContactName}\n " +
                            $"Contacttitle:{SelectedCustomer.ContactTitle}\n " +
                            $"Address:{SelectedCustomer.Address}\n " +
                            $"City:{SelectedCustomer.Region}\n " +
                            $"Region:{SelectedCustomer.City}\n " +
                            $"PostalCode:{SelectedCustomer.PostalCode}\n " +
                            $"Country:{SelectedCustomer.Country}\n " +
                            $"phone:{SelectedCustomer.Phone}\n " +
                            $"fax:{SelectedCustomer.Fax}\n "
                         );

                        Console.WriteLine("All orders the customer has made are :\n");
                        foreach (var order in SelectedCustomer.Orders)

                        {
                            Console.WriteLine($"OrderId: {order.OrderId} ");
                            Console.WriteLine($"Orderdate:{order.OrderDate}\n");
                            //Console.WriteLine($"RequiredDate:{order.RequiredDate}");
                            //Console.WriteLine($"ShipName:{order.ShipName}");
                            //Console.WriteLine($"ShipAddress:{order.ShipAddress}");
                            //Console.WriteLine($"ShipCity:{order.ShipCity}");
                            //Console.WriteLine($"ShipRegion:{order.ShipRegion}");
                            //Console.WriteLine($"ShipPostalCode:{order.ShipPostalCode}");
                            //Console.WriteLine($"ShipCountry:{order.ShipCountry}");
                        }
                        break;


                    }
                    else
                    {
                        Console.WriteLine("This customer was not found");
                       

                    }
                    
                }


                Console.WriteLine("You need add a new Customner\n ");
                Customer newCustomer = new Customer
                {
                    CustomerId= randStr()     
                };


                Console.Write("Enter Companyname: ");
                string CompanyNameInput = Console.ReadLine() ;
                newCustomer.CompanyName = string.IsNullOrEmpty(CompanyNameInput) ? null : CompanyNameInput;

                Console.Write("Enter ContactName: ");
                string ContactNameInput = Console.ReadLine();
                newCustomer.ContactName = string.IsNullOrEmpty( ContactNameInput) ? null : ContactNameInput;

                Console.Write("Enter ContactTitle: ");
                string ContactTitleInput = Console.ReadLine();
                newCustomer.ContactTitle = string.IsNullOrEmpty(ContactTitleInput) ? null : ContactTitleInput;

                Console.Write("Enter Address: ");
                string AddressInput = Console.ReadLine();
                newCustomer.Address = string.IsNullOrEmpty( AddressInput) ? null : AddressInput;


                Console.Write("Enter Region: ");
                string RegionInput = Console.ReadLine();
                newCustomer.Region = string.IsNullOrEmpty( RegionInput) ? null : RegionInput;

                Console.Write("Enter City: ");
                string CityInput = Console.ReadLine();
                newCustomer.City = string.IsNullOrEmpty( CityInput) ? null : CityInput;

                Console.Write("Enter Country: ");
                 string CountryInput = Console.ReadLine();
                newCustomer.Country = string.IsNullOrEmpty(CountryInput) ? null : CountryInput; 

                Console.Write("Enter Phone: ");
                 string PhoneInput= Console.ReadLine();
                newCustomer.Phone= string.IsNullOrEmpty( PhoneInput) ? null :   PhoneInput;

                Console.Write("Enter Fax: ");
                 string FaxInput = Console.ReadLine();
                newCustomer.Fax= string.IsNullOrEmpty( FaxInput) ? null : FaxInput;

                Console.WriteLine("Done");



                Context.Customers.Add(newCustomer);
                Context.SaveChanges();

            }
           
        }
        public static string randStr() 
        {
            const string chars = "ABCDEFGHIJKLMNOQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars,5)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());        
        }
    }

}   



