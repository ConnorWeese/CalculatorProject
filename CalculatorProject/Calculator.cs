using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProject
{
    class Calculator
    {
        //private members of the Calculator class
        private double left;    //the left operand in an equation
        private double right;   //the right operand in an equation
        private String op;  //the operator in an equation
        private String result;  //the result of an equation as a string, can also be an error message
        private Queue<String> previousResults;  //queue containing the 10 most recent results along with the current result 

        Calculator(String _left, String _right, String _op)
        {
            previousResults = new Queue<String>(11);    //initialize the queue with a capcity of 11

            //set members left, right, op and result
            if (setLeft(_left))
            {
                if (setRight(_right))
                {
                    if (setOperator(_op))
                    {
                        result = "";
                    }
                    else
                    {
                        result = "Operator Error";
                    }
                }
                else
                {
                    result = "Right Operand Error";
                }
            }
            else
            {
                result = "Left Operand Error";
            }
            
        }

        public bool setLeft(String value)
        {
            //try to parse value into a Double and store the result in left
            bool success = Double.TryParse(value, out left);
            
            //if the conversion was successful
            if (success)
            {
                return true;    //return true;
            }

            return false;   //else, return false
        }

        public bool setRight(String value)
        {
            //try to parse value into a Double and store the result in right
            bool success = Double.TryParse(value, out right);

            //if the conversion was successful
            if (success)
            {
                return true;    //return true;
            }

            return false;   //else, return false
        }

        public bool setOperator(String value)
        {
            //if value is any one of +,-,*,/
            if (value == "+" || value == "-" || value == "*" || value == "/")
            {
                op = value; //set op to value
                return true;    //return true
            }

            return false;   //return false
        }

        public void calculate()
        {
            //if the opperation is addition
            if (op == "+")
            {
                //try-catch in case of overflow
                try
                {
                    result = (left + right).ToString();   //set result as the value of left + right as a string
                }
                catch (OverflowException)
                {
                    result = "Overflow";  //set result as an overflow error
                }
            }
            //if the opperation is subtraction
            else if (op == "-")
            {
                //try-catch in case of overflow
                try
                {
                    result = (left - right).ToString();   //set result as the value of left - right as a string
                }
                catch (OverflowException)
                {
                    result = "Overflow";  //set result as an overflow error
                }
            }
            //if the operation is multiplication
            else if (op == "*")
            {
                //try-catch in case of overflow
                try
                {
                    result = (left * right).ToString();   //set result as the value of left * right as a string
                }
                catch (OverflowException)
                {
                    result = "Overflow";  //set result as an overflow error
                }
            }
            //if the operation is division
            else if (op == "/")
            {
                //try-catch in case of division by zero
                try{
                    result = (left / right).ToString();   //set result as the value of left / right as a string
                }
                catch (DivideByZeroException)
                {
                    result = "Division by Zero Error";    //set result as a division by zero error
                }
            }
            //if the operation is none of the above
            else
            {
                result = null;    //set result as null
            }
        }

        public String getResult()
        {
            //if the queue is at max capacity
            if(previousResults.Count == 11)
            {
                previousResults.Dequeue();  //dequeue the result from the front of the queue
            }
            previousResults.Enqueue(result);    //enqueue the new result at the back of the queue

            return result;  //return the result
        }

        public String getPreviousResult(int index)
        {
            //if the index is outside of the bounds provided in the project documentation: 1 - 10
            if(index <= 0 || index > 10)
            {
                return "Index Error";   //return an error
            }

            String[] _previousResults = previousResults.ToArray();  //copy the elements of the queue to an array
            Array.Reverse(_previousResults);    //reverse the array

            //if the index we are looking at is within the bounds of the number of elements in the array
            if(index < _previousResults.Length)
            {
                return _previousResults[index]; //return the result at the index
            }

            return "Index Error";   //return an error
        }
    }
}
