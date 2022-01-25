namespace TestLibrary
{
    public class Employee : IComparable<Employee>
    {
        public int EmployeeID { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }

        /// <summary>
        /// Contstructs an Employee object with defined parameters or null
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        public Employee(int employeeId, string firstName = null, string lastName = null)
        {
            EmployeeID = employeeId;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Compares to another Employee object based on EmployeeID
        /// </summary>
        /// <param name="other">Employee object to compare to</param>
        /// <returns>Comparison of this object and other</returns>
        public int CompareTo(Employee? other)
        {
            return this.EmployeeID.CompareTo(other.EmployeeID);
        }

        /// <summary>
        /// Returns the Employee as a string containing their employee ID, first name, and last name.
        /// </summary>
        /// <returns>EmployeeID FirstName LastName</returns>
        public override string ToString()
        {
            return $"{EmployeeID} {FirstName ?? "null"} {LastName ?? "null"}";
        }
    }
}