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
                string fullName = (string)_details[0] + (string)_details[1];
                _payslipData.Add("Name: " + fullName);

                //Pay period 
                string startDate = (string)_details[4];
                string endDate = (string)_details[5];
                string[] startDateDetails = startDate.Split('/');
                string[] endDateDetails = endDate.Split('/');
                
                
            } catch (IndexOutOfRangeException e){
                Console.WriteLine(e);
            }
            
        }
    }
}