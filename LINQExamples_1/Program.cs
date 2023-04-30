namespace LINQExamples_1
{
   public class Program
    {
        static void Main(string[] args)
        {
            //GetAllMethod();
            //GetAllQuery();

            //GetHighSalaryLazy();

            //GetHighSalaryImmediate();
           // GetAllByJoinMethod();
            //GetAllByJoinQuery();
            GetAllByGroupJoinQuery();

        }

        public static void GetAllByGroupJoinQuery()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = from dept in departmentList
                          join emp in employeeList
                          on dept.Id equals emp.DepartmentId
                          into employeeGroup
                          select new
                          {
                              Employees = employeeGroup,
                              DepartmentName = dept.LongName
                          };

            foreach (var item in results)
            {
                Console.WriteLine($"Department Name: {item.DepartmentName}");
                foreach (var emp in item.Employees)
                    Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");

            }

            Console.ReadKey();

        }

        public static void GetAllByGroupJoinMethod()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = departmentList.GroupJoin(employeeList,
                dept => dept.Id,
                emp => emp.DepartmentId, 
                (dept, employeesGroup) => new
                    {
                        Employees = employeesGroup,
                        DepartmentName = dept.LongName
                    }
                );
            foreach (var item in results)
            {
                Console.WriteLine($"Department Name: {item.DepartmentName}");
                foreach (var emp in item.Employees)
                    Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");

            }

            Console.ReadKey();
        }

        public static void GetAllByJoinQuery()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = from dept in departmentList
                          join emp in employeeList
                          on dept.Id equals emp.DepartmentId
                          select new
                          {
                              fullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary,
                              DepartmentName = dept.LongName
                          };
            foreach (var item in results)
            {
                Console.WriteLine($"{item.fullName,-20} {item.AnnualSalary,10}\t{item.DepartmentName}");
            }

            Console.ReadKey();


        }
        public static void GetAllByJoinMethod()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = departmentList.Join(employeeList,
                department => department.Id,
                employee => employee.DepartmentId, 
                (department, employee) => new
                {
                    fullName = employee.FirstName + " " + employee.LastName,
                    AnnualSalary = employee.AnnualSalary,
                    departmentName = department.LongName
                }
              );
            foreach (var item in results)
            {
                Console.WriteLine($"{item.fullName,-20} {item.AnnualSalary,10}\t{item.departmentName}");
            }

            Console.ReadKey();

        }

        static void GetHighSalaryImmediate()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = (from emp in employeeList.GetHighSalaryEmployees()
                          select new
                          {
                              fullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary
                          }).ToList();
            employeeList.Add(new Employee
            {
                Id = 5,
                FirstName = "Sam",
                LastName = "Davies",
                AnnualSalary = 10000.20m,
                IsManager = true,
                DepartmentId = 2,
            });

            foreach (var item in results)
            {
                Console.WriteLine($"{item.fullName,-20} {item.AnnualSalary,10}");
            }

            Console.ReadKey();
        }
        static void GetHighSalaryLazy()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = from emp in employeeList.GetHighSalaryEmployees()
                          select new
                          {
                              fullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary
                          };
            foreach (var item in results)
            {
                Console.WriteLine($"{item.fullName,-20} {item.AnnualSalary,10}");
            }
            employeeList.Add(new Employee
            {
                Id = 5,
                FirstName = "Sam",
                LastName = "Davies",
                AnnualSalary = 10000.20m,
                IsManager = true,
                DepartmentId = 2,
            });

        }
        public static void GetAllQuery()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();
            var results = from emp in employeeList
                          //where emp.AnnualSalary >= 50000
                          select new
                          {
                              fullName = emp.LastName + " " + emp.FirstName,
                              AnnualSalary = emp.AnnualSalary
                          };
            employeeList.Add(new Employee
            {
                Id = 5,
                FirstName = "Sam",
                LastName = "Davies",
                AnnualSalary = 10000.20m,
                IsManager = true,
                DepartmentId = 2,
            });

            foreach (var item in results)
            {
                Console.WriteLine($"{item.fullName,-20} {item.AnnualSalary,10}");
            }
        }

        public static void GetAllMethod()
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = employeeList.Select(e => new
            {
                fullName = e.FirstName + " " + e.LastName,
                AnnualSalary = e.AnnualSalary

            }).Where(e => e.AnnualSalary >= 50000);

            foreach (var item in results)
            {
                Console.WriteLine($"{item.fullName,-20} {item.AnnualSalary,10}");
            }

            Console.ReadKey();
        }

    }

   public static class EnumerableCollectionExtentionMethiods
    {
        public static IEnumerable<Employee> GetHighSalaryEmployees(this IEnumerable<Employee> employees)
        {
            foreach(Employee emp in employees)
            {
                Console.WriteLine($"Accessing employee: {emp.FirstName + "" + emp.LastName}");
                if (emp.AnnualSalary >= 50000)
                {
                    yield return emp;
                }
            }
        }
    }

    public class Employee
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }

    }

    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }

    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnnualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Jameson",
                AnnualSalary = 80000.1m,
                IsManager = true,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 3,
                FirstName = "Douglas",
                LastName = "Roberts",
                AnnualSalary = 40000.2m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Stevens",
                AnnualSalary = 30000.2m,
                IsManager = false,
                DepartmentId = 3
            };
            employee = new Employee
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Stevens",
                AnnualSalary = 30000.2m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 5,
                FirstName = "David",
                LastName = "Ukpoju",
                AnnualSalary = 0,
                IsManager = true,
                DepartmentId = 4,

            };
            employees.Add(employee);

            return employees;

        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            Department department = new Department
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resources"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);

            return departments;
        }

    }
}