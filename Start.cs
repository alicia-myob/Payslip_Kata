using System;
using System.Collections.Generic;

namespace Payslip{
    
    class Start{
        
        private Dictionary<string, string> _info = new Dictionary<string, string>();
        private string[] _userQuestions;

        public static void Main(String[] args){
            Start payslipProgram = new Start();
            Console.WriteLine("Welcome to the payslip generator! \n");
            payslipProgram.readQuestions();
        }
        /*
            This method reads the questions to ask the user from the text file
        */
        public void readQuestions(){
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Variables.txt");
            _userQuestions = System.IO.File.ReadAllLines(path);
            
        }

        public bool validator(string input){
            return false;
        }

    }
}