using System;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;


namespace DatabaseWork
{
    public class Employee
    {
        public String Name { get { return this.name; } set { name = value; } }
        public String Lastname { get { return this.lastname; } set { lastname = value; } }
        public int Age { get { return this.age; } set { age = value; } }
        public int ID { get { return this.id; } set { id = value; } }
        public String Gender { get { return this.gender; } set { gender = value; } }
        public String DOB { get { return this.birthDay; } set { birthDay = value; } }
        public int Phone { get { return this.phoneNO; } set { phoneNO = value; } }
        public String Email { get { return this.email; } set { email = value; } }
        public string Title { get { return this.title; } set { title = value; } }
        public String Password { get { return this.password; } set { password = value; } }
        public int EmployeeID { get { return this.employeeID; } set { employeeID = value; } }
        public decimal Salary { get { return this.salary; } set { salary = value; } }
        public DateTime Hiredate { get { return this.hiredate; } set { hiredate = value; } }
        public String Country { get { return this.country; } set { country = value; } }

        private String name;       ///
        private String lastname;  ///
        private int age;          //*****
        private int id;          ///
        private String gender;    //
        private String birthDay;  //
        private int phoneNO;  //
        private String email;  //
        private string title;  //******
        private String password; // 
        private int employeeID;  //****
        private decimal salary;  //*****
        private DateTime hiredate;  //*****
        private String country;  //


        public Employee() { }

        public Employee(string name, string lastname, int id, string gender, String birthDay, int age, int phone, string email, string title, String password, int employeeID, decimal salary, DateTime hiredate, String country)
        {
            this.name = name;
            this.lastname = lastname;
            this.id = id;
            this.gender = gender;
            this.birthDay = birthDay;
            this.age = age;
            this.phoneNO = phone;
            this.email = email;
            this.title = title;
            this.password = password;
            this.employeeID = employeeID;
            this.salary = salary;
            this.hiredate = hiredate;
            this.country = country;
        }

        
        public sealed override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            return ("Name ---------- : " + this.Name + "\n" +
                    "Lastname ------ : " + this.Lastname + "\n" +
                    "Identity No --- : " + this.ID + "\n" +
                    "Date Of Birth - : " + this.DOB + "\n" +
                    "Age ----------- : " + this.Age + "\n" +
                    "Phone Number -- : 0" + this.Phone + "\n" +
                    "Email --------- : " + this.Email + "\n" +
                    "Title --------- : " + this.Title + "\n" +
                    "Employee ID --- : " + this.EmployeeID + "\n" +
                    "Salary -------- : R " + this.Salary + "\n" +
                    "Hired Date ---- : " + this.Hiredate + "\n" +
                    "Conutry ------- : " + this.Country
                    );
        }

        /*
        private static SqlConnection connect;
        private static SqlCommand command;
        private static SqlDataReader reader;
        private const string url = "Data Source=DESKTOP-LUKU6V1; Initial Catalog=DS_Database; Integrated Security=true";
        private static String query;

        
        static void Main(String[] main)
        {
            DateOnly date = new DateOnly(2001, 01, 04);
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - date.Year;

            Console.WriteLine(age);

            String password = String.Empty;
            const int LENGTH = 10;
            int randomIndex = 0;
            String random = "1234567890@!#$%&ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Char[] letters = new Char[LENGTH];
            Random anything = new Random();
            for (int run = 0; run < LENGTH; run++)
            {
                randomIndex = anything.Next(random.Length);
                password = Convert.ToString(random.ElementAt(randomIndex));

                letters[run] = Convert.ToChar(password);

                Console.Write(letters[run]);
            }
            Console.WriteLine();


            try
            {
                Console.ForegroundColor = ConsoleColor.Green;

                connect = new SqlConnection(url);
                connect.Open();
                Console.WriteLine("CONNECTION ESTABLISHED....");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter username : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                String name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter passcode : ");
                String lastname = Console.ReadLine(); 

                DateTime today = DateTime.Now;
                Console.WriteLine(today);

                 query = "INSERT INTO STUDENTS( Name, Lastname, Date_Info)" +
                               "VALUES('" + name + "','" + lastname + "', @value)";
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@value", today);
                command.ExecuteNonQuery();
            }
            catch (SqlException error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.Message);
            }
            Console.ForegroundColor = ConsoleColor.White;

        }*/


        /*
         *            try
            {
                // Create an SQL statement that inserts data into the database
                string sql = "INSERT INTO STUDENTS(Name, Lastname) VALUES (@Value2, @Value3)";
                Console.Write("Name : ");
                String name = Console.ReadLine();
                Console.Write("Lastname : ");
                String lastname = Console.ReadLine();

                //query = "INSERT INTO STUDENTS(Id, Name, Lastname) VALUES (3, '" + name + "','" + lastname + "')";
                //command = new SqlCommand(query, connect);

                // Create a command object that executes the SQL statement
                command = new SqlCommand(sql, connect);
                //command.Parameters.AddWithValue("@Value1", 2);
                command.Parameters.AddWithValue("@Value2", name);
                command.Parameters.AddWithValue("@Value3", lastname);
                Console.WriteLine("DONE");

                // Execute the command object to insert data into the database
                command.ExecuteNonQuery();
            }
            catch(SqlException error)
            {
                Console.WriteLine(error.Message);
            }
         */

    }
}

