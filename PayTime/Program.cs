using PayTime;

public class Program
{
    private static void Main(string[] args)
    {
        int categoryCtr = 0;
        int empCategory = 0;
        double test = 5.5;
        PayRoll payRoll = new PayRoll("ferry ship", 2500, DateTime.Now);
        Category category = new Category("SS", Factor.Deduction, 10.5);
        Category category1 = new Category("Tax", Factor.Deduction, 40);
        Category category2 = new Category("Tax", Factor.Deduction, 20);
        Employee emp = new Employee("Jake", "1234", WorkerType.DockWorker);

        //Act
        payRoll.AddCategory(category);
        payRoll.AddCategory(category1);
        payRoll.AddCategory(category2);
        payRoll.AddEmployee(emp);
        emp.TimeIn = new TimeOnly(8, 0);
        emp.TimeOut = new TimeOnly(10, 0);

        payRoll.CalculateSalary();

        emp.CategoryMoney.Add(test);
        Console.WriteLine(emp.CategoryMoney[3]);

        foreach (Category c in payRoll.Categories)
        {
            categoryCtr++;
        }

        foreach (double value in emp.CategoryMoney)
        {
            Console.WriteLine(value);
            empCategory++;
        }

        Console.WriteLine(emp);

        //Assert
        DataBase.AddPayRoll(payRoll);
        DataBase.SavePayRoll();
        Console.WriteLine(categoryCtr);
        Console.WriteLine(empCategory);
    }

    /*double totalPercent = 0;
    PayRoll payRoll = new PayRoll("ferry ship", 2500);
    Category category = new Category("SS", Factor.Deduction, 90.5);
    Category category1 = new Category("Tax", Factor.Deduction, 40);
    Category category2 = new Category("Tax", Factor.Bonus, 20);

    //Act
    payRoll.AddCategory(category);
    payRoll.AddCategory(category2);
    payRoll.AddCategory(category1);

    //Assert
    foreach(Category c in payRoll.Categories)
    {
        if (c.Factor == Factor.Deduction)
        {
            totalPercent += c.Percent;
        }
        else if (c.Factor == Factor.Bonus)
        {
            totalPercent -= c.Percent;
        }
    }
    Console.WriteLine(totalPercent);*/


    /*PayRoll payRoll = new PayRoll("Ferry Boat", 5000);
    Employee emp = new Employee("Jake", "1", WorkerType.DockWorker);
    Employee emp1 = new Employee("Lorenz", "1", WorkerType.DockWorker);
    Employee emp2 = new Employee("Pato", "1", WorkerType.DockWorker);

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
    foreach (Employee e in payRoll.Employees)
    {
        Console.WriteLine(e.Name);

        Console.WriteLine(e.Salary);
    }*/

    /*Employee a = new Employee("jake", "123", WorkerType.DockWorker);
    Employee b = new Employee("lorenz", "12345", WorkerType.DockWorker);
    Employee e = new Employee("pato", "1234", WorkerType.DockWorker);
    Category c = new Category("name", Factor.Deduction, 10);
    PayRoll payRoll = new PayRoll("ferry", 5000);
    payRoll.AddCategory(c);
    payRoll.AddEmployee(e);
    payRoll.AddEmployee(b);
    payRoll.AddEmployee(a);
    e.TimeIn = new TimeOnly(8, 0);
    e.TimeOut = new TimeOnly(9, 30);
    a.TimeIn = new TimeOnly(8, 0);
    a.TimeOut = new TimeOnly(9, 30);
    b.TimeIn = new TimeOnly(8, 0);
    b.TimeOut = new TimeOnly(9, 30);

    payRoll.CalculateSalary();*/

    /*DataBase.AddPayRoll(payRoll);
    DataBase.SavePayRoll();*/
    /*DataBase.LoadPayRoll();
    foreach(PayRoll p in DataBase.PayRolls)
    {
        Console.WriteLine(p);
    }*/
    //Console.WriteLine(e.Salary);
    //Console.WriteLine(a.Salary);
    //Console.WriteLine(b.Salary);

    //Console.WriteLine(payRoll);


    //Console.WriteLine(e.TimeIn);
}
