using System;
using System.Collections.Generic;
using System.IO;

namespace Payslip{
    
    class Start{
        
        private Dictionary<string, string> _info = new Dictionary<string, string>();
        private Calculator _calculator = new Calculator();
        private string[] _userQuestions;

        public static void Main(String[] args){
            Start payslipProgram = new Start();
            Console.WriteLine("Welcome to the payslip generator!");
            Console.WriteLine("This program calculates the payslip for any start date, in full calendar months. \nIf your end date does not reach the next full month, it will calculate it to the previous full month.");
            Console.WriteLine("*With the exception that if the period is shorter than a month, the program will calculate to one full month\n");
            payslipProgram.readQuestions();
            payslipProgram.printAndValidate();
        }

        /*
            This method reads the questions to ask the user from the text file
        */
        public void readQuestions(){
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Variables.txt");
            try {
                _userQuestions = System.IO.File.ReadAllLines(path);
            } catch (FileNotFoundException ex){
                Console.WriteLine(ex);
            }
        }

        public void printAndValidate(){
            
            for (int i = 0; i < _userQuestions.Length; i++){
                bool valid = false;
                while (!valid){
                    Console.WriteLine("Please enter your " + _userQuestions[i] + ": ");
                    string input = Console.ReadLine();
                    valid = validator(input, _userQuestions[i]);
                    if (valid == false){
                        Console.WriteLine("Your " + _userQuestions[i] + " is invalid.");
                    } else {
                        _calculator.getDetails(input);
                    }
                }
            }
        }

        public bool validator(string input, string type){

            if (String.IsNullOrEmpty(input)){
                Console.WriteLine("Input cannot be blank. \n");
                return false;
            } 

            switch(type){
                case "name": //Fallthrough
                case "surname":
                    return true;

                case "annual salary":
                    try {
                        int annualSalary = (int)Int64.Parse(input);
                        if (annualSalary < 0){
                            break;
                        } else {
                            return true;
                        }
                    } catch (FormatException){
                        break;
                    }
                case "super rate":
                    try {
                        decimal superRate = decimal.Parse(input);
                        if ((superRate< 0)||(superRate > 50)){
                            break;
                        } else {
                            return true;
                        }
                    } catch (FormatException){
                        break;
                    }
                case "payment start month and year (e.g. November 2020)": //Fallthrough
                case "payment end month and year (e.g. November 2020)":
                    string[] dates = input.Split(' ');
                    try {
                        int month = int.Parse(dates[1]);
                        int year = int.Parse(dates[2]);
                        if (year < 0){
                            break;
                        } else if ((month < 1)||(month > 12)){
                            break;
                        }
                        
                        break;
                    } catch (FormatException){
                        break;
                    }
                default: 
                    return false;
            }
            return false;
        }

        
    }
}