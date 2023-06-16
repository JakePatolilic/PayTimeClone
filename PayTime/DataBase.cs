using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PayTime
{
    public class DataBase
    {
        private static string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string payRollFileName = "PayTime.pt";
        public static List<PayRoll> PayRolls = new List<PayRoll>();

        public static void SavePayRoll()
        {
            string json = JsonConvert.SerializeObject(PayRolls, Formatting.Indented);
            File.WriteAllText(Path.Combine(rootDirectory, payRollFileName), json);
        }

        public static void LoadPayRoll()
        {
            string json = File.ReadAllText(Path.Combine(rootDirectory, payRollFileName));
            PayRolls = JsonConvert.DeserializeObject<List<PayRoll>>(json);
        }

        public static void AddPayRoll(PayRoll payroll)
        {
            PayRolls.Add(payroll);
        }

        public static void deletePayRoll(PayRoll payroll)
        {
            PayRolls.Remove(payroll);
            SavePayRoll();
        }

        public static void delEmployee(PayRoll payroll, Employee emp)
        {
            int num = 0;
            LoadPayRoll();
            foreach (PayRoll p in PayRolls)
            {
                if (p.PayRollName == payroll.PayRollName)
                {
                    PayRolls[num].RemoveEmployee(emp);
                }
                else
                    num++;
            }
            SavePayRoll();
        }

        public static void addEmployee(PayRoll payroll, Employee emp)
        {
            int num = 0;
            LoadPayRoll();
            foreach(PayRoll p in PayRolls)
            {
                if (p.PayRollName == payroll.PayRollName)
                {
                    PayRolls[num].AddEmployee(emp);
                }
                else
                    num++;
            }
            SavePayRoll();
        }

        public static void updatePayroll(PayRoll payroll)
        {
            LoadPayRoll();
            List<PayRoll> payRollsCopy = PayRolls.ToList();

            for (int i = 0; i < payRollsCopy.Count; i++)
            {
                if (payRollsCopy[i].PayRollName == payroll.PayRollName)
                {
                    PayRolls[i] = payroll;
                }
            }
            SavePayRoll();
        }
    }
}
