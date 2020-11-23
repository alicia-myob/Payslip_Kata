using System;
using System.Collections;
using System.Globalization;

namespace Payslip_Kata{

    public class Calculator{

        private string[] _months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        
        private readonly ArrayList _details = new ArrayList();
        private int _startMonthIndex;
        private int _endMonthIndex;
        private int _totalMonths;
        private int _grossIncome;
        private int _incomeTax;
        private readonly ArrayList _payslipData = new ArrayList();
        public Calculator(){}

        public void GetDetails(string input) => _details.Add(input);

        public void GeneratePayslip(){
            
            try {
                //Add fullname 
                _payslipData.Add("Name: " + GetFullName());

                //Pay period 
                _payslipData.Add(GetPayPeriod());

                GetPayPeriod();
                CalculateMonths();
                
                //Gross income
                _payslipData.Add("Gross Income: " + GetGrossIncome());

                //Income tax
                _payslipData.Add("Income Tax: " + GetIncomeTax());

                //Net income
                _payslipData.Add("Net Income: " + GetNetIncome());

                //Super
                _payslipData.Add("Super: " + GetSuper());

            } catch (IndexOutOfRangeException e){
                Console.WriteLine(e);
            }
            
        }

        public ArrayList GetPayslip(){
            return _payslipData;
        }

        private string GetFullName(){
            return (string)_details[0] + " " + (string)_details[1];
        }

        private string GetPayPeriod(){
            return "Pay Period: " + (string)_details[4] + " - " + (string)_details[5];
        }

        public void GetMonthIndices(int[] indices){
            _startMonthIndex = indices[0];
            _endMonthIndex = indices[1];
        }

        private void CalculateMonths(){
            try {
                var startYear = ((string)_details[4])?.Split(' ')[1];
                var endYear = ((string)_details[5])?.Split(' ')[1];

                if (string.Equals(startYear, endYear, StringComparison.OrdinalIgnoreCase)){
                    _totalMonths = _endMonthIndex + 1 - _startMonthIndex;
                } else {
                    _totalMonths = _endMonthIndex + 1 + (11 - _startMonthIndex);
                }
            } catch (IndexOutOfRangeException){}
        }

        private string GetGrossIncome(){
            try
            {
                _grossIncome = (int) Math.Round(Double.Parse(((string) _details[2])!) * _totalMonths / 12,
                    MidpointRounding.ToEven);
            }
            catch (ArgumentNullException)
            {
                //Ignored
            }
            
            return _grossIncome.ToString();
        }

        private string GetIncomeTax(){
            var taxer = new Taxer();
            var oneMonthTax = Taxer.GetTax(int.Parse((string)_details[2] ?? throw new InvalidOperationException()));
            _incomeTax = oneMonthTax*_totalMonths;
            return _incomeTax.ToString();
        }

        private string GetNetIncome(){
            return (_grossIncome - _incomeTax).ToString();
        }

        private string GetSuper(){
            var superRate = decimal.Parse((string)_details[3] ?? throw new InvalidOperationException())/100;
            var superTotal = _grossIncome * superRate;
            return Math.Round(superTotal, MidpointRounding.ToEven).ToString(CultureInfo.InvariantCulture);
        }
    }
}