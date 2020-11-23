using System;
using System.Collections;

namespace Payslip{

    public class Calculator{

        private string[] Months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        
        private ArrayList _details = new ArrayList();
        private int _startMonthIndex;
        private int _endMonthIndex;
        private int _totalMonths;
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
            return "Pay Period: " + (string)_details[4] + " - " + (string)_details[5];
        }

        public void getMonthIndices(int[] indices){
            _startMonthIndex = indices[0];
            _endMonthIndex = indices[1];
        }

        public void calculateMonths(){
            try {
                string startYear = ((string)_details[4]).Split(' ')[1];
                string endYear = ((string)_details[5]).Split(' ')[1];

                if (String.Equals(startYear, endYear, StringComparison.OrdinalIgnoreCase)){
                    _totalMonths = _endMonthIndex - _startMonthIndex;
                } else {
                    _totalMonths = _endMonthIndex + 1 + (11 - _startMonthIndex);
                }
            } catch (IndexOutOfRangeException){}
           
            

        }

        public string getGrossIncome(){
            double grossIncome = Math.Round(Double.Parse((string)_details[2])/12, MidpointRounding.ToEven);
            return grossIncome.ToString();
        }
    }
}