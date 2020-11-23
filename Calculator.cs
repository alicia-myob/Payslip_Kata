using System;
using System.Collections;

namespace Payslip{

    public class Calculator{

        private string[] Months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        
        private ArrayList _details = new ArrayList();
        private ArrayList _payslipData = new ArrayList();
        public Calculator(){
        }

        public void getDetails(string input) => _details.Add(input);

        public void generateVals(){
            
            try {
                //Add fullname 
                _payslipData.Add("Name: " + getFullName());

                //Pay period 
                _payslipData.Add(getPayPeriod());
                
                //Gross income
                
            } catch (IndexOutOfRangeException e){
                Console.WriteLine(e);
            }
            
        }

        public string getFullName(){
            return (string)_details[0] + (string)_details[1];
        }

        public string getPayPeriod(){
            
            string startDate = (string)_details[4];
            string endDate = (string)_details[5];

            string[] startDateDetails = startDate.Split('/');
            string[] endDateDetails = endDate.Split('/');

            char startMonth = startDateDetails[1][1];
            char endMonth = endDateDetails[1][1];

            string payPeriod = startDateDetails[0] + " " + Months[int.Parse(startMonth.ToString())] + " - " + endDateDetails[0] + " " + Months[int.Parse(endMonth.ToString())];
            return payPeriod;
        }

        public string getGrossIncome(){
            string gIncome = "rrr";
            int incomeForPeriod = int.Parse((string)_details[2]);
            
            return gIncome;
        }
    }
}