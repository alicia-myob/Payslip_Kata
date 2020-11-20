using System;
using System.Collections.Generic;
using System.IO;

namespace Payslip{
    
    
    class Start{
        
        private Dictionary<string, string> _info = new Dictionary<string, string>();
        private string[] _userQuestions;

        public static void Main(String[] args){
            Start payslipProgram = new Start();
            Console.WriteLine("Welcome to the payslip generator!");
            payslipProgram.readQuestions();
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
            bool valid = true;
            for (int i = 0; i < _userQuestions.Length; i++){
                while (valid){
                    Console.WriteLine("\nPlease enter your " + _userQuestions[i] + ": ");
                    string input = Console.ReadLine();
                    valid = validator(input, _userQuestions[i]);
                }
            }
        }

        public bool validator(string input, string type){

            if (String.IsNullOrEmpty(input)){
                Console.WriteLine("Input cannot be blank. \n");
                return false;
            } 

            switch(type){
                case "name":
                case "surname":
                    break;

                case "annual salary":
                    try {
                        long annualSalary = long.Parse(input);
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
                case "payment start date": 
                    string[] dates = input.Split("//");
                    try {
                        int date = int.Parse(dates[0]);
                        int month = int.Parse(dates[1]);
                        int year = int.Parse(dates[2]);
                        if (year < 0){
                            break;
                        } else if ((month < 1)||(month > 12)){
                            break;
                        } else if ((date < 1)||(date > 31)){
                            break;
                        } else {
                            
                        }
                    } catch (FormatException){
                        break;
                    }
                    break;

            }

            return false;
        }

        public void checkMonthDate(int month, int date){
            
        }

    }
}