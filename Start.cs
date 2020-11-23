using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Payslip_Kata{
    
    /**
     * <summary> This payslip program calculates an employee's pay for a specified range of calendar months (within one annum only).
     * This program also assumes that the payslip cannot be for a range less than a month. </summary>
    */
    /**
     * <summary>Class <c>Start</c> starts the program, fetches question data, asks and reads user input then prints the payslip</summary>
     */
    internal class Start{
        
        private readonly string[] _months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        private readonly List<int> _monthIndex = new List<int>();
        private readonly Calculator _calculator = new Calculator();
        private string[] _userQuestions;
        private ArrayList _payslip = new ArrayList();
        
        /**
         * <summary> Entry point of the program</summary>
         */
        public static void Main(String[] args){
            var payslipProgram = new Start();

            //Welcome messages
            Console.WriteLine("\nWelcome to the payslip generator!");
            Console.WriteLine("*Note for pay period: enter start month as the month pay begins and end at the month inclusive");
            Console.WriteLine(" e.g. March 2020 - April 2020 means 01 March 2020 to 31 April 2020\n");
            
            //Fetch questions to ask user
            payslipProgram.ReadQuestions(); 

            //Ask user and check input
            payslipProgram.PrintAndValidate();

            //Calculate details for payslip and retrieve + print
            payslipProgram._calculator.GeneratePayslip();
            payslipProgram._payslip = payslipProgram._calculator.GetPayslip();
            payslipProgram.PrintPayslip();

            //Ending messages
            Console.WriteLine("\nThank you for using MYOB!\n");
        }

        /**
         * <summary>Method <c>ReadQuestions</c> reads the questions to ask the user from the text file</summary>
        */
        private void ReadQuestions(){
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Variables.txt");
            try {
                _userQuestions = System.IO.File.ReadAllLines(path);
            } catch (FileNotFoundException ex){
                Console.WriteLine(ex);
            }
        }
    
        /**
         * <summary>Method <c>PrintAndValidate</c> asks the user to input their details and checks the input with validator</summary>
        */
        private void PrintAndValidate()
        {    
            //Loop through each question
            foreach (var t in _userQuestions)
            {
                var valid = false;
                //If the answer is valid, then move to the next question
                while (!valid){
                    Console.Write("Please enter your " + t + ": ");
                    var input = Console.ReadLine();
                    valid = Validator(input, t);
                    if (valid == false){
                        Console.WriteLine("Your " + t + " is invalid.");
                    } else {
                        _calculator.GetDetails(input);
                    }
                }
            }
            //If input is successful, put month indices into an array and pass onto calculator
            try {
                var indices = _monthIndex.ToArray();
                _calculator.GetMonthIndices(indices);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /**
         * <summary>Method <c>Validator</c> checks the user input given the question asked</summary>
         * <param name="input">The user's input</param>
         * <param name="type">What the question asked</param>
         * <returns>A bool that is true if the user's input is valid, otherwise false</returns>
         */
        private bool Validator(string input, string type)
        {

            //Not accepting empty inputs
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input cannot be blank. \n");
                return false;
            }

            //Each type of question needs to be checked differently
            switch (type)
            {
                //Accept any string for name and surname
                case "name": //Fallthrough
                case "surname":
                    return true;

                //Accept only positive integers for salary
                case "annual salary":
                    try
                    {
                        var annualSalary = (int) long.Parse(input);
                        if (annualSalary < 0)
                        {
                            break;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (FormatException)
                    {
                        break;
                    }
                //Accept only rates between 0 and 50 inclusive 
                case "super rate":
                    try
                    {
                        var superRate = decimal.Parse(input);
                        if ((superRate < 0) || (superRate > 50))
                        {
                            break;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (FormatException)
                    {
                        break;
                    }
                //Accept only properly formatted date
                case "payment start month and year (e.g. November 2020)": //Fallthrough
                case "payment end month and year (e.g. November 2020)":
                    var dates = input.Split(' ');
                    try
                    {
                        var month = dates[0];
                        var year = int.Parse(dates[1]);
                        if (year < 0)
                        {
                            break;
                        }
                        else
                        {
                            for (var i = 0; i < 12; i++)
                            {
                                if (!string.Equals(month, _months[i], StringComparison.OrdinalIgnoreCase)) continue;
                                _monthIndex.Add(i);
                                return true;
                            }

                            break;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                default:
                    return false;
            }

            return false;
        }

        /**
         * <summary>Method <c>PrintPayslip</c> prints the payslip data fetched from the calculator</summary>
         */
        private void PrintPayslip(){
            Console.WriteLine("\nYour payslip has been generated:\n");
            foreach (var i in _payslip){
                Console.WriteLine(i);
            }
        }
        
    }
}