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
                    Console.WriteLine("\nPlease enter your " + _userQuestions[i] + ": ");
                    string input = Console.ReadLine();
                    valid = validator(input, _userQuestions[i]);
                    if (valid == false){
                        Console.WriteLine("Your " + _userQuestions[i] + " is invalid.");
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
                case "payment start date": //Fallthrough
                case "payment end date":
                    string[] dates = input.Split('/');
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
                        } 

                        bool rtrn = checkMonthDate(month, date);
                        bool leap = checkLeap(year, month, date);
                        if ((rtrn == false)||(leap == false)){
                            break;
                        } else {
                            return true;
                        }

                    } catch (FormatException){
                        break;
                    }
                default: 
                    return false;
            }
            return false;
        }

        public bool checkMonthDate(int month, int date){
            if ((month < 8)&&(month % 2 == 1)){
                if (date <= 31){
                    return true;
                }
            } else if (month % 2 == 0){
                if (date <= 31){
                    return true;
                }
            } else {
                if (date <= 30){
                    return true;
                }
            }
            return false;
        }

        public bool checkLeap(int year, int month, int date){
            if ((month == 2)&&(date == 29)){
                if (year % 4 == 0){
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        }
    }
}