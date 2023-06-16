using PayTime;
namespace PayTImeProjectUnitTest
{
    public class PayRollTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddCategory_RightCondition_CategoryIsInTheList()
        {
            //Arrange
            PayRoll payRoll = new PayRoll("ferry ship", 2500, DateTime.Now);
            Category category = new Category("SS", Factor.Deduction, 7.5);
            Category category1 = new Category("Tax", Factor.Deduction, 5);

            //Act
            payRoll.AddCategory(category);
            payRoll.AddCategory(category1);

            //Assert
            int ctr = 0;
            foreach(Category c in payRoll.Categories)
            {
                if (c == category || c == category1)
                {
                    ctr++;
                }
            }
            Assert.IsTrue(ctr == 2);

        }

        [Test]
        public void AddCategory_Exceed100Percent_ReturnFalse()
        {
            //Arrange
            PayRoll payRoll = new PayRoll("ferry ship", 2500, DateTime.Now);
            Category category = new Category("SS", Factor.Deduction, 90.5);
            Category category1 = new Category("Tax", Factor.Deduction, 40);
            Category category2 = new Category("Tax", Factor.Bonus, 20);

            //Act
            payRoll.AddCategory(category);
            payRoll.AddCategory(category2);
            var result = payRoll.AddCategory(category1);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AddEmployee_RightCondition_EmployeeIsInTheList()
        {
            //Arrange
            PayRoll payRoll = new PayRoll("Ferry boat", 3500, DateTime.Now);
            Employee emp = new Employee("Jake", "123", WorkerType.DockWorker);
            Employee emp1 = new Employee("Lorenz", "1234", WorkerType.DockWorker);

            //Act
            payRoll.AddEmployee(emp);
            payRoll.AddEmployee(emp1);

            //Assert
            int ctr = 0;
            foreach(Employee e in payRoll.Employees)
            {
                if(e == emp || e == emp1)
                {
                    ctr++;
                }
            }
            Assert.That(ctr.Equals(2));
        }

        [Test]
        public void RemoveCategory_RightCondition_CategoryListIsEmplty()
        {
            //Arrange
            PayRoll payRoll = new PayRoll("Ferry Boat", 4500, DateTime.Now);
            Category category = new Category("SS", Factor.Deduction, 5);
            Category category1 = new Category("Tax", Factor.Deduction, 3.5);

            payRoll.AddCategory(category);
            payRoll.AddCategory(category1);

            //Act
            payRoll.RemoveCategory(category);
            payRoll.RemoveCategory(category1);

            //Assert
            Assert.IsEmpty(payRoll.Categories);
        }

        [Test]
        public void RemoveEmployee_RightCondition_EmployeeListIsEmpty()
        {
            //Arrange
            PayRoll payRoll = new PayRoll("Ferry Boat", 4500, DateTime.Now);
            Employee emp = new Employee("Jake", "123", WorkerType.DockWorker);
            Employee emp1 = new Employee("Lorenz", "1235", WorkerType.DockWorker);

            payRoll.AddEmployee(emp);
            payRoll.AddEmployee(emp1);

            //Act
            payRoll.RemoveEmployee(emp);
            payRoll.RemoveEmployee(emp1);

            //Assert
            Assert.IsEmpty(payRoll.Employees);
        }

        [Test]
        public void CalculateSalary_RightCondition_CorrectComputation()
        {
            //Arrange
            PayRoll payRoll = new PayRoll("Ferry Boat", 5000, DateTime.Now);
            Employee emp = new Employee("Jake", "1", WorkerType.DockWorker);
            Employee emp1 = new Employee("Jake", "1", WorkerType.DockWorker);
            Employee emp2 = new Employee("Jake", "1", WorkerType.DockWorker);

            Category c = new Category("SS", Factor.Deduction, 3.5);
            Category c1 = new Category("pag-IBIG", Factor.Deduction, 2);
            Category c2 = new Category("Tax", Factor.Deduction, 5);

            payRoll.AddCategory(c);
            payRoll.AddCategory(c1);
            payRoll.AddCategory(c2);
            payRoll.AddEmployee(emp);
            payRoll.AddEmployee(emp1);
            payRoll.AddEmployee(emp2);

            emp.TimeIn = new TimeOnly(8, 0);
            emp1.TimeIn = new TimeOnly(7, 30);
            emp2.TimeIn = new TimeOnly(9, 0);

            emp.TimeOut = new TimeOnly(12, 0);
            emp1.TimeOut = new TimeOnly(12, 0);
            emp2.TimeOut = new TimeOnly(12, 0);

            //Act
            payRoll.CalculateSalary();

            int em = (int)emp.Salary;
            int em1 = (int)emp1.Salary;
            int em2 = (int)emp2.Salary;
            //Assert
            Assert.That(em.Equals(1562));
            Assert.That(em1.Equals(1757));
            Assert.That(em2.Equals(1171));
        }

        [Test]
        public void CalculateSalary_NumberOfCategoriesIsEqualToNumberOfCategoryMoney_ReturnTrue()
        {
            //Arrange
            int categoryCtr = 0;
            int empCategory = 0;
            PayRoll payRoll = new PayRoll("ferry ship", 2500, DateTime.Now);
            Category category = new Category("SS", Factor.Deduction, 10.5);
            Category category1 = new Category("Tax", Factor.Deduction, 40);
            Category category2 = new Category("Tax", Factor.Deduction, 20);
            Employee emp = new Employee("Jake", "1234", WorkerType.DockWorker);

            //Act
            payRoll.AddCategory(category);
            payRoll.AddCategory(category2);
            payRoll.AddCategory(category1);
            payRoll.AddEmployee(emp);

            payRoll.CalculateSalary();

            foreach(Category c in payRoll.Categories)
            {
                categoryCtr++;
            }
            foreach (double money in emp.CategoryMoney)
            {
                empCategory++;
            }

            //Assert
            Assert.That(categoryCtr.Equals(empCategory));
        }
    }
}