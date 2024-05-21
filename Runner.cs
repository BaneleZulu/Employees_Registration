using System;
using System.Data.SqlTypes;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.PortableExecutable;

namespace DatabaseWork
{
    internal class Runner : Employee, Controls
    {
        private static SqlConnection connect;
        private static SqlCommand command;
        private static SqlDataReader reader;
        private const string url = "Data Source=DESKTOP-LUKU6V1; Initial Catalog=DS_Database; Integrated Security=true";
        private static String query;
        private Employee employee = new Employee();

        static public void Main(String[] main)
        {
            getConnection();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Hello and welcome to VISIONARY.co. A startup software development campony. \nTo Utilize our system you must first become a member");
            Console.WriteLine("Press: \n1 => Sign In.\n2 => Sign Up.");
            Console.Write("=> : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            int login  = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.DarkGray;

            try
            {
                if (login == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Enter username : ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    String username = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Enter passcode : ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    String passcode = Console.ReadLine();

                    query = "SELECT * FROM EMPLOYEE";
                    connect = new SqlConnection(url);
                    connect.Open();
                    command = new SqlCommand(query, connect);
                    reader = command.ExecuteReader();
                    bool isValid = false;

                    String realName = String.Empty;
                    String realastname = String.Empty;
                    String realPassword = String.Empty;
                    while (reader.Read())
                    {
                         realName =  reader.GetString("Name");
                         realastname = reader.GetString("Lastname");
                         realPassword = reader.GetString("Password");

                        if (username.Equals(realName, StringComparison.OrdinalIgnoreCase) && passcode.Equals(realPassword))
                        {
                            isValid = true;
                            break;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }

                    if(isValid == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Authentication Complete. Login Successful.");
                        Console.WriteLine("Welcome : " + realName + " " + realastname);
                        Console.WriteLine("==========================================================");
                        Console.ForegroundColor = ConsoleColor.White;
                        whatToDo();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Authentication Failed. Login details not found..");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    reader.Close();
                }
                else
                {
                    //CODE FOR SIGN UP.....
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }


        static public void getConnection()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                connect = new SqlConnection(url);
                connect.Open();
                Console.WriteLine("CONNECTION ESTABLISHED....");
            }
            catch(SqlException error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.Message);
            }
                Console.ForegroundColor = ConsoleColor.White;
        }

        static public void whatToDo()
        {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("What would you like to do?");
                String[] optins = { "Add new Employee", "Delete Employee", "Update Employee", "Search Employee", "View Employees" };
                Console.WriteLine("------------------------------------------------------");
                // Array.ForEach(optins, x => Console.WriteLine(x + " -> " + (track+1)));
                
                for (int run = 0; run < optins.Length; run++)
                {
                    Console.WriteLine((run + 1) + " -> " + optins[run]);
                }
                Console.WriteLine("------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("------------------------------------------------------");

            int id = 0; //Control variable to check employeeID
            switch (choice)
                {
                    case 1:
                        Controls addEmployee = new Runner();
                        addEmployee.add();
                        break;
                    case 2:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Enter employee number of employee to delete : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                        id = Convert.ToInt32(Console.ReadLine());
                        Controls deleteEmployee = new Runner();
                        deleteEmployee.delete(id);
                    break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Enter employee number of employee to update : ");
                        Console.ForegroundColor = ConsoleColor.Green;
                            id = Convert.ToInt32(Console.ReadLine());
                            Controls updateEmployee = new Runner();
                            updateEmployee.update(id);
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Enter employee number to search for : ");
                        Console.ForegroundColor = ConsoleColor.Green;
                            id = Convert.ToInt32(Console.ReadLine());
                            Controls searchEmployee = new Runner();
                            searchEmployee.search(id);
                            break;
                    case 5:
                        Controls displayEmployee = new Runner();
                        displayEmployee.display();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice selected");
                        Console.ForegroundColor = ConsoleColor.White;
                        whatToDo();    
                        break;
                }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static public String createPassword()
        {
            String password = String.Empty;
            const int LENGTH = 10;
            int randomIndex = 0;
            String random = "1234567890@!#$%&ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Char[] letters = new Char[LENGTH];
            Random anything = new Random();
            for (int run = 0; run < LENGTH; run++)
            {
                randomIndex = anything.Next(random.Length);
                letters[run] = random.ElementAt(randomIndex);

                password += letters[run];
            }
            return password;    
        }

        static public String generateEmail(String name, String lastname, String title, int id)
        {

            //********************EMAIL GENERATOR////////
            String? email;
            String? abbriv = null;
            char check = '\u0000';
            for (int run = 0; run < title.Length; run++)
            {
                check = (char)title.ElementAt(run);
                if (Char.IsUpper(check))
                {
                    abbriv += check.ToString();
                }
            }
            email = name + lastname + ((id / 50) - 6) + abbriv + "@Visionary.com";
            Console.Write("Email : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(email);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;

            String pick = String.Empty;
            Console.Write("This is your employee email. Would you like to change it? : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            pick = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("------------------------------------------------------");

            if (pick.Equals("Yes", StringComparison.OrdinalIgnoreCase) || pick.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter you new email : ");
                Console.ForegroundColor = ConsoleColor.Green;
                email = Console.ReadLine();

                while (!email.Contains("@gmail") || !email.Contains(".") || !email.Contains("com"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ensure that you've entered a valid email address: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    email = Console.ReadLine();
                }
            }
            else
            {
                email = email;
            }

            return email;
            //********************END HERE////////
        }

        static public String getTitle()
        {
            String title;
            String[] SDjobs = new string[] { "Software Developer", "Software Designer", "Front End Developer", "Full Stack Developer", "Database Administrator", "Web App Developer", "Database Analysis"};
            String[] NCjobs = new string[] { "Cyber Security Engineer", "Network Administrator", "Network engineer", "Data Analyst", "Desktop Supporter", "Network Specialist", "Security Engineer"};
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("We have 2 departments.\nPlease make a selection on which department you'ld like to join");
            Console.WriteLine("1 -> Software Development\n2 -> Network Communication.");
            Console.ForegroundColor =ConsoleColor.Yellow;
            int choice = Convert.ToInt32(Console.ReadLine());
            int team = 0, track = 0;
            Console.ForegroundColor =ConsoleColor.DarkGray;
            Console.WriteLine("------------------------------------------------------");

            
            if (choice == 1)
            {
                for (int run = 0; run < SDjobs.Length; run++)
                {
                    track++;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine((track) + "-> : " + SDjobs[run]);

                }
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write("Specify the team you would like to join [1-7]: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                team = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                title = SDjobs[team-1];
                Console.Write("You are now part of the team : ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(SDjobs[team-1]);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("WELCOME");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------");
            }
            else
            {
                for (int run = 0; run < NCjobs.Length; run++)
                {
                    track++;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine((track) + "-> : " + NCjobs[run]);

                }
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write("Specify the team you would like to join [1-7]: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                team = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                title = NCjobs[team-1];
                Console.Write("You are now part of the team : ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(NCjobs[team-1]);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("WELCOME");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------");
            }
            return title;
        }

        static public decimal generatePay()
        {
            Random anything = new Random();
            int pay = anything.Next(50_000);
            Console.WriteLine("You base salary. Good work will lead to increase$$$$");
            
            return pay;
        }

        public void add()
        {
            try
            {
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("As admin you can add new employees to VISINARY.co");
                Console.WriteLine("=======================================================\nPlease provide neccessary details for the new employee.");
                Console.WriteLine("------------------------------------------------------");

                //***************EMPLOYEE HIRE DATE////////
                DateTime today = DateTime.Now;
                employee.Hiredate = today;
                //***************END HERE////////

                //*******************MANUALLY ENTERED INFORMATION
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Name : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                String name = Console.ReadLine();
                employee.Name = name;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Lastname : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                String lastname = Console.ReadLine();
                employee.Lastname = lastname;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("ID Number : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int id = Convert.ToInt32(Console.ReadLine());
                employee.ID = id;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Date of Birth : ");
                Console.Write("Year : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int year = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Month (1 - 12) : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int month = Convert.ToInt32(Console.ReadLine());    
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Day : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int day = Convert.ToInt32(Console.ReadLine());


                    ////DERIVED ATTRIBUTE AGE MUST AUTOMATICALLY GET VALUE DEPENDING ON OTHER VARIABLES
                    DateOnly date = new DateOnly(year, month, day);
                    employee.DOB = (date.ToString());
                    DateTime currentDate = DateTime.Now;
                    int age = currentDate.Year - date.Year;
                    employee.Age = age;
                    //*********************END HERE//////

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Gender 'M', 'F' : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                String g = Console.ReadLine();
                employee.Gender = g;
                while(!(employee.Gender.Equals("F", StringComparison.OrdinalIgnoreCase) || employee.Gender.Equals("M", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid gender selected {M - F}");
                    
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Gender 'M', 'F' : ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    employee.Gender = Console.ReadLine().ToUpper();
                }
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Contact Details : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int number = Convert.ToInt32(Console.ReadLine());
                employee.Phone = number;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Country : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                String nation = Console.ReadLine();
                employee.Country = nation;
                Console.WriteLine("------------------------------------------------------");
                //*********************END HERE//////


                Console.ForegroundColor = ConsoleColor.Green;
                //MANUALLY ENTERED INFORMATION END HERE*********************/////
                //TO AVOID DUPLICATE AND VAOLATE SQL CONSTRANTS ON PRIMARY KEYS THE EemployeeID VALUE MUST AUTOMATICALLY INCREAMENT
                query = "SELECT * FROM EMPLOYEE";
                reader.Close();
                command = new SqlCommand(query, connect);
                reader = command.ExecuteReader();
                int previousID = 0;
                while(reader.Read())
                {
                    previousID = reader.GetInt32("EmployeeID");
                }
                employee.EmployeeID = (previousID+1);
                //*********************END HERE/////

                //GENERATE AUTOMATIC VALUES
                   ///GET TODAYS DATE
                    employee.Hiredate = DateTime.Now;
                ///GENERATE AUTOMATIC PASSWORD

                ///SELECTING EMPLOYEE TITLE
                employee.Title = getTitle();
                //*******************************END HERE//////////////////
                 
                //************GENERATING EMAIL////////
                employee.Email =  generateEmail(employee.Name, employee.Lastname, employee.Title, employee.Age);
                //************END HERE////////

                Console.WriteLine("------------------------------------------------------");

                // employee.Password = password;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Your password : ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(createPassword());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Your randomly generated password. Would you like to change it? : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                String responce = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("------------------------------------------------------");

                if (responce.Equals("Yes", StringComparison.OrdinalIgnoreCase) || responce.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter your new password : ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    employee.Password = Console.ReadLine();
                    while(employee.Password.Length > 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Password cannot be more then 10 characters");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Password : ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        employee.Password = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    employee.Password = createPassword();
                }
                Console.WriteLine("------------------------------------------------------");
                ///***************************END HERE//////

                ///RANDOMLY GENERATING EMPLOYEE PAY
                employee.Salary = generatePay();
                ///********************************END HERE//////

                ///****************************////////
                connect.Close();

                connect.Open();
                query = "INSERT INTO EMPLOYEE(EmployeeID, HireDate, Name, Lastname, DOB, Age, Gender, PhoneNumber, Email, Password, Title, Salary, Citizenship)" +
                        "VALUES(" + employee.EmployeeID + "," + "@Value1" + ",'" + employee.Name + "','" + employee.Lastname + "'," + "@Value2" + "," + employee.Age + ",'" + employee.Gender + "'," + employee.Phone + ",'" + employee.Email + "','" + employee.Password + "','" + employee.Title + "'," + employee.Salary + ",'" + employee.Country + "')";
                command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@value1", employee.Hiredate);
                command.Parameters.AddWithValue("@Value2", employee.DOB);
                command.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You've Successful registered a new employee");
                Console.ForegroundColor = ConsoleColor.White;
                connect.Close();
            }
            catch(SqlException error) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.Message);
            }
            Console.ForegroundColor = ConsoleColor.White;
                
            Console.WriteLine("------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(employee.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------------------------");
            whatToDo();
        }

        public void delete(int ID)
        {
            try
            {
                connect.Close ();
                reader.Close ();

                query = "SELECT * FROM EMPLOYEE WHERE EmployeeID = " + ID;
                connect.Open ();
                command = new SqlCommand(query, connect);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    employee.EmployeeID = reader.GetInt32("EmployeeID");
                    employee.Hiredate = reader.GetDateTime("Hiredate");
                    employee.Name = reader.GetString("Name");
                    employee.Lastname = reader.GetString("Lastname");
                    employee.DOB = reader.GetDateTime("DOB").ToString();
                    employee.Age = reader.GetInt32("Age");
                    employee.Gender = reader.GetString("Gender");
                    employee.Phone = reader.GetInt32("PhoneNumber");
                    employee.Email = reader.GetString("Email");
                    employee.Title = reader.GetString("Title");
                    employee.Salary = reader.GetDecimal("Salary");
                    employee.Country = reader.GetString("Citizenship");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(employee.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------");

                Console.Write("Delete this employee? : ");
                Console.ForegroundColor = ConsoleColor.Green;
                String response = Console.ReadLine();

                if(response.Equals("Yes", StringComparison.OrdinalIgnoreCase) || response.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    connect.Close();
                    query = "DELETE FROM EMPLOYEE WHERE EmployeeID = " + ID;
                    connect.Open();
                    command = new SqlCommand(query, connect);
                    command.ExecuteNonQuery();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("EMPLOYEE DELETED SUCESSFULY....");
                    Console.WriteLine("------------------------------------------------------");
                    whatToDo();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Action cancelled......");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("------------------------------------------------------");
                    whatToDo();
                }

            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public void update(int ID)
        {
            try
            {
                connect.Close();
                reader.Close();
                query = "SELECT * FROM EMPLOYEE WHERE EmployeeID = " + ID;
                connect.Open();
                command = new SqlCommand(query, connect);
                reader = command.ExecuteReader();

                Employee tempEmployee = new Employee();
                while (reader.Read())
                {
                    tempEmployee.EmployeeID = reader.GetInt32("employeeID");
                    tempEmployee.Hiredate = reader.GetDateTime("Hiredate");
                    tempEmployee.Name = reader.GetString("Name");
                    tempEmployee.Lastname = reader.GetString("Lastname");
                    tempEmployee.DOB = reader.GetDateTime("DOB").ToString();
                    tempEmployee.Age = reader.GetInt32("Age");
                    tempEmployee.Gender = reader.GetString("Gender");
                    tempEmployee.Phone = reader.GetInt32("PhoneNumber");
                    tempEmployee.Email = reader.GetString("Email");
                    tempEmployee.Title = reader.GetString("Title");
                    tempEmployee.Password = reader.GetString("Password");
                    tempEmployee.Salary = reader.GetDecimal("Salary");
                    tempEmployee.Country = reader.GetString("Citizenship");
                }

                String title = tempEmployee.Title;
                employee.Title = title;
                int id = tempEmployee.EmployeeID;
                employee.EmployeeID = ID;
                decimal salary = tempEmployee.Salary;
                employee.Salary = salary;
                DateTime hiredDate = tempEmployee.Hiredate;
                employee.Hiredate = hiredDate;

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The folling information cannot be modifeid.");
                Console.WriteLine("------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Title : " + tempEmployee.Title + "\nEmployee ID : " + tempEmployee.EmployeeID + "\nSalary :R " + tempEmployee.Salary + "\nHiredate : " + tempEmployee.Hiredate);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("------------------------------------------------------");


                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Name : " + tempEmployee.Name);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter new name : ");
                Console.ForegroundColor = ConsoleColor.Green;
                String name = Console.ReadLine();
                employee.Name = name;
                
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Lastname : " + tempEmployee.Lastname);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter new lastname : ");
                Console.ForegroundColor = ConsoleColor.Green;
                String lastname = Console.ReadLine();
                employee.Lastname = lastname;

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Gender : " + tempEmployee.Gender);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter new gender : ");
                Console.ForegroundColor = ConsoleColor.Green;
                String gender = Console.ReadLine();
                employee.Gender = gender;

                while (!(employee.Gender.Equals("F", StringComparison.OrdinalIgnoreCase) || employee.Gender.Equals("M", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid gender selected {M - F}");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Gender 'M', 'F' : ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    employee.Gender = Console.ReadLine().ToUpper();
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("DOB : " + tempEmployee.DOB);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Enter new DOB >: ");
                Console.Write("Year : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int year = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Month (1 - 12) : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int month = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Day : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int day = Convert.ToInt32(Console.ReadLine());

                ////DERIVED ATTRIBUTE AGE MUST AUTOMATICALLY GET VALUE DEPENDING ON OTHER VARIABLES
                DateOnly date = new DateOnly(year, month, day);
                employee.DOB = (date.ToString());
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - date.Year;
                employee.Age = age;
                //*********************END HERE//////
                

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Contact : " + tempEmployee.Phone);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter new phone number : ");
                Console.ForegroundColor = ConsoleColor.Green;
                int phone = Convert.ToInt32(Console.ReadLine());
                employee.Phone = phone;

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Email : " + tempEmployee.Email);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter new email : ");
                Console.ForegroundColor = ConsoleColor.Green;
                String email = Console.ReadLine();

                while (!email.Contains("@gmail") || !email.Contains('.') || !email.Contains("com"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ensure that you've entered a valid email address : ");
                    Console.WriteLine("Valid email must contail @gmail.com, or co.za");
                    Console.Write(">>> : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    email = Console.ReadLine();
                }
                employee.Email = email;

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Password : " + tempEmployee.Password);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter new password : ");
                Console.ForegroundColor = ConsoleColor.Green;
                String password = Console.ReadLine();
                employee.Password = password;

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Country : " + tempEmployee.Country);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter new country : ");
                Console.ForegroundColor = ConsoleColor.Green;
                String nation = Console.ReadLine();
                employee.Country = nation;

                connect.Close();
              //  command.Dispose();

                query = "UPDATE EMPLOYEE SET HireDate =@Value1, Name ='" + employee.Name + "', Lastname ='" + employee.Lastname + "', DOB =@Value2, " + "Age =" + employee.Age +  ", Gender ='" + employee.Gender + "', PhoneNumber =" + employee.Phone + ", Email ='" + employee.Email + "', Password ='" + employee.Password + "', Title ='" + employee.Title + "', Salary =" + Convert.ToDouble(employee.Salary) + ", Citizenship ='" + employee.Country + "'"  
                      + " WHERE EmployeeID =" + employee.EmployeeID;
                connect.Open();
                command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@value1", employee.Hiredate);
                command.Parameters.AddWithValue("@Value2", employee.DOB);
                command.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("Employee record updated successfuly");
                Console.WriteLine("------------------------------------------------------");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("Your updated Information");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(employee.ToString());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("------------------------------------------------------");

                whatToDo();
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        
        }

        public void search(int ID)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------------------------");
            try
            {
                reader.Close();
                query = "SELECT * FROM EMPLOYEE WHERE EmployeeID = " + ID;
                command = new SqlCommand(query, connect);
                reader = command.ExecuteReader();
                
                while (reader.Read()) 
                {
                    employee.EmployeeID = reader.GetInt32("EmployeeID");
                    employee.Hiredate = reader.GetDateTime("Hiredate");
                    employee.Name = reader.GetString("Name");
                    employee.Lastname = reader.GetString("Lastname");
                    employee.DOB = reader.GetDateTime("DOB").ToString();
                    employee.Age = reader.GetInt32("Age");
                    employee.Gender = reader.GetString("Gender");
                    employee.Phone = reader.GetInt32("PhoneNumber");
                    employee.Email = reader.GetString("Email");
                    employee.Title = reader.GetString("Title");
                    employee.Salary = reader.GetDecimal("Salary");
                    employee.Country = reader.GetString("Citizenship");
                }
                Console.WriteLine("------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(employee.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------");

                whatToDo();
            }
            catch
            (SqlException error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.Message);
            }
        }

        public void display() 
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                connect = new SqlConnection(url);
                connect.Open();
                query = "SELECT * FROM EMPLOYEE";
                command = new SqlCommand(query, connect);
                reader = command.ExecuteReader();

                Console.WriteLine("EMPLOYEE ID || HIREDATE || NAME || LASTNAME || DOB || AGE || GENDER || PHONE NO || EMAIL || TITLE || SALARY");
                Console.WriteLine("=====================================================================================================================");
                while (reader.Read()) 
                {
                    employee.EmployeeID = reader.GetInt32("EmployeeID");
                    DateTime hired = reader.GetDateTime("HireDate");
                    employee.Hiredate = hired;
                    employee.Name = reader.GetString("Name");
                    employee.Lastname = reader.GetString("Lastname");
                    string dateString = reader.GetDateTime("DOB").ToString();
                    employee.DOB = dateString;
                    employee.Age = reader.GetInt32("Age");
                    employee.Gender = reader.GetString("Gender");
                    employee.Phone = reader.GetInt32("PhoneNumber");
                    employee.Email = reader.GetString("Email");
                    employee.Title = reader.GetString("Title");
                    employee.Salary =  reader.GetDecimal("Salary");

                    Console.WriteLine(employee.EmployeeID +" || "+ employee.Hiredate + " || " + employee.Name +" || " + employee.Lastname +" || " + employee.DOB + " || " + employee.Age +" || " + employee.Gender +" || "+ employee.Phone +" || " + employee.Email +" || " + employee.Title +" || " + employee.Salary.ToString("0.00"));
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");

                }
                connect.Close();
                command.Dispose();
                reader.Close();
                whatToDo();
            }
            catch (SqlException error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.Message);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
