using System;
using System.Collections;

namespace Payslip{

    public class Calculator{

        private string[] Months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        
        private ArrayList _details = new ArrayList();
        private int _startMonthIndex;
        private int _endMonthIndex;
        private int _totalMonths;
        private int _grossIncome;
        private int _incomeTax;
        private ArrayList _payslipData = new ArrayList();
        public Calculator(){
        }

        public void getDetails(string input) => _details.Add(input);

        public void generatePayslip(){
            
            try {
                //Add fullname 
                _payslipData.Add("Name: " + getFullName());

                //Pay period 
                _payslipData.Add(getPayPeriod());

                getPayPeriod();
                calculateMonths();
                
                //Gross income
                _payslipData.Add("Gross Income: " + getGrossIncome());

                //Income tax
                _payslipData.Add("Income Tax: " + getIncomeTax());

                //Net income
                _payslipData.Add("Net Income: " + getNetIncome());

                //Super
                _payslipData.Add("Super: " + getSuper());

            } catch (IndexOutOfRangeException e){
                Console.WriteLine(e);
            }
            
        }

        public ArrayList getPayslip(){
            return _payslipData;
        }

        public string getFullName(){
            return (string)_details[0] + " " + (string)_details[1];
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
                    _totalMonths = _endMonthIndex + 1 - _startMonthIndex;
                } else {
                    _totalMonths = _endMonthIndex + 1 + (11 - _startMonthIndex);
                }
            } catch (IndexOutOfRangeException){}
        }

        public string getGrossIncome(){
            _grossIncome = (int)Math.Round(Double.Parse((string)_details[2])*_totalMonths/12, MidpointRounding.ToEven);
            return _grossIncome.ToString();
        }

        public string getIncomeTax(){
            Taxer taxer = new Taxer();
            int oneMonthTax = taxer.getTax(Int32.Parse((string)_details[2]));
            _incomeTax = oneMonthTax*_totalMonths;
            return _incomeTax.ToString();
        }

        public string getNetIncome(){
            return (_grossIncome - _incomeTax).ToString();
        }

        public string getSuper(){
            decimal superRate = decimal.Parse((string)_details[3])/100;
            decimal superTotal = _grossIncome * superRate;
            return Math.Round(superTotal, MidpointRounding.ToEven).ToString();

        }
    }
}