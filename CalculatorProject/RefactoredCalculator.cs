using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProject
{
    class RefactoredCalculator
    {
        //private members of the RefactoredCalculator class
        private String result;  //the result of an equation as a string, can also be an error message
        private Queue<String> previousResults;  //queue containing the 10 most recent results along with the current result 

        //constructor
        public RefactoredCalculator()
        {
            result = "";
            previousResults = new Queue<String>(11);
        }

        //public method to perform a calculation and return the results
        public String calculate(List<String> input)
        {
            //if the input contains only one string
            if (input.Count == 1) return input[0];

            //check every string in the input list for syntax errors
            for(int i=0; i<input.Count; i++)
            {
                //check the first and final string
                if(i == 0 || i == input.Count - 1)
                {
                    //if the string is an operator
                    if(input[i] == "+" || input[i] == "-" || input[i] == "*" || input[i] == "/")
                    {
                        result = "Input Error: Must begin and end with a number";
                        enqueueResult(result);

                        return result; //return a syntax error
                    }
                }

                //check each string for proper syntax
                foreach(char c in input[i])
                {
                    //if the character is not a digit, opertator, decimal, or comma
                    if(!char.IsDigit(c) && c != '+' && c != '-' && c != '*' && c != '/' && c != '.' && c != ',')
                    {
                        result = String.Format("Error: \"{0}\" is not a number", input[i]);
                        enqueueResult(result);

                        return result; //return a syntax error with the offending string
                    }
                }

                //check every odd string
                if(i%2 != 0)
                {
                    //if every odd string is not an operator
                    if(input[i] != "+" && input[i] != "-" && input[i] != "*" && input[i] != "/")
                    {
                        result = "Syntax Error: Missing operator";
                        enqueueResult(result);

                        return result; //return a syntax error
                    }
                }
            }

            //perform and multiplation and division operations first in left to right order
            for(int i=0; i<input.Count; i++)
            {
                Double left = 0.0;
                Double right = 0.0;

                if (input[i] == "*")
                {
                    bool success = Double.TryParse(input[i - 1], out left);

                    //if the conversion was successful, and the converted number is not Double.NaN
                    if (success && !Double.IsNaN(left))
                    {
                        success = Double.TryParse(input[i + 1], out right);

                        if(success && !Double.IsNaN(right))
                        {
                            //try-catch in case of overflow
                            try
                            {
                                //if the result of the multiplication is positive or negative infinity
                                if (Double.IsInfinity(left * right))
                                {
                                    throw new OverflowException();  //throw an overflow exception
                                }
                                input[i] = (left * right).ToString();   //set input[i] as the value of left * right as a string

                                input.RemoveAt(i+1);    //remove the item in front of where we stored our answer, becuase we have consumed it
                                input.RemoveAt(i - 1);  //remove the item behind of where we stored our answer, becuase we have consumed it

                                i--;    //decrement i becuase the list has been resized
                            }
                            catch (OverflowException)
                            {
                                result = "Error: Overflow";  //set result as an overflow error
                                enqueueResult(result);

                                return result; //return an overflow error
                            }
                        }
                        else
                        {
                            result = String.Format("Error: \"{0}\" is not a number", input[i + 1]);
                            enqueueResult(result);

                            return result; //return a syntax error with the offending string
                        }
                    }
                    else
                    {
                        result = String.Format("Error: \"{0}\" is not a number", input[i - 1]);
                        enqueueResult(result);

                        return result; //return a syntax error with the offending string
                    }
                }
                else if (input[i] == "/")
                {
                    bool success = Double.TryParse(input[i - 1], out left);

                    //if the conversion was successful, and the converted number is not Double.NaN
                    if (success && !Double.IsNaN(left))
                    {
                        success = Double.TryParse(input[i + 1], out right);

                        if (success && !Double.IsNaN(right))
                        {
                            //try-catch in case of overflow
                            try
                            {
                                //if the dividend is zero
                                if (right == 0)
                                {
                                    throw new DivideByZeroException();  //throw a divide by zero exception
                                }
                                //if the result of the division is either positive or negative infinity
                                else if (Double.IsInfinity(left / right))
                                {
                                    throw new OverflowException();  //throw an overflow exception
                                }
                                input[i] = (left / right).ToString();   //set input[i] as the value of left / right as a string

                                input.RemoveAt(i + 1);    //remove the item in front of where we stored our answer, becuase we have consumed it
                                input.RemoveAt(i - 1);  //remove the item behind of where we stored our answer, becuase we have consumed it

                                i--;    //decrement i becuase the list has been resized
                            }
                            catch (DivideByZeroException)
                            {
                                result = "Error: Division by 0";    //set result as a division by zero error
                                enqueueResult(result);

                                return result; //return the error
                            }
                            catch (OverflowException)
                            {
                                result = "Error: Overflow";  //set result as an overflow error
                                enqueueResult(result);

                                return result; //return an overflow error
                            }
                        }
                        else
                        {
                            result = String.Format("Error: \"{0}\" is not a number", input[i + 1]);
                            enqueueResult(result);

                            return result; //return a syntax error with the offending string
                        }
                    }
                    else
                    {
                        result = String.Format("Error: \"{0}\" is not a number", input[i - 1]);
                        enqueueResult(result);

                        return result; //return a syntax error with the offending string
                    }
                }
            }

            //now perform addition and subtraction operations in left to right order
            for(int i=0; i<input.Count; i++)
            {
                Double left = 0.0;
                Double right = 0.0;

                //if the operator is addition
                if (input[i] == "+")
                {
                    bool success = Double.TryParse(input[i - 1], out left);

                    //if the conversion was successful, and the converted number is not Double.NaN
                    if (success && !Double.IsNaN(left))
                    {
                        success = Double.TryParse(input[i + 1], out right);

                        if (success && !Double.IsNaN(right))
                        {
                            //try-catch in case of overflow
                            try
                            {
                                //if the result is positive or negative infinity
                                if (Double.IsInfinity(left + right))
                                {
                                    throw new OverflowException();  //throw an overflow exception
                                }
                                input[i] = (left + right).ToString();   //set input[i] as the value of left + right as a string

                                input.RemoveAt(i + 1);    //remove the item in front of where we stored our answer, becuase we have consumed it
                                input.RemoveAt(i - 1);  //remove the item behind of where we stored our answer, becuase we have consumed it

                                i--;    //decrement i becuase the list has been resized
                            }
                            catch (OverflowException)
                            {
                                result = "Error: Overflow";  //set result as an overflow error
                                enqueueResult(result);

                                return result; //return an overflow error
                            }
                        }
                        else
                        {
                            result = String.Format("Error: \"{0}\" is not a number", input[i + 1]);
                            enqueueResult(result);

                            return result; //return a syntax error with the offending string
                        }
                    }
                    else
                    {
                        result = String.Format("Error: \"{0}\" is not a number", input[i - 1]);
                        enqueueResult(result);

                        return result; //return a syntax error with the offending string
                    }
                }
                //if the operator is subtraction
                else if (input[i] == "-")
                {
                    bool success = Double.TryParse(input[i - 1], out left);

                    //if the conversion was successful, and the converted number is not Double.NaN
                    if (success && !Double.IsNaN(left))
                    {
                        success = Double.TryParse(input[i + 1], out right);

                        if (success && !Double.IsNaN(right))
                        {
                            //try-catch in case of overflow
                            try
                            {
                                //if the result is positive or negative infinity
                                if (Double.IsInfinity(left - right))
                                {
                                    throw new OverflowException();  //throw an overflow exception
                                }
                                input[i] = (left - right).ToString();   //set input[i] as the value of left - right as a string

                                input.RemoveAt(i + 1);    //remove the item in front of where we stored our answer, becuase we have consumed it
                                input.RemoveAt(i - 1);  //remove the item behind of where we stored our answer, becuase we have consumed it

                                i--;    //decrement i becuase the list has been resized
                            }
                            catch (OverflowException)
                            {
                                result = "Error: Overflow";  //set result as an overflow error
                                enqueueResult(result);

                                return result; //return an overflow error
                            }
                        }
                        else
                        {
                            result = String.Format("Error: \"{0}\" is not a number", input[i + 1]);
                            enqueueResult(result);

                            return result; //return a syntax error with the offending string
                        }
                    }
                    else
                    {
                        result = String.Format("Error: \"{0}\" is not a number", input[i - 1]);
                        enqueueResult(result);

                        return result; //return a syntax error with the offending string
                    }
                }
            }

            //there should now be only one string left in the input list
            if(input.Count == 1)
            {
                result = input[0];
                enqueueResult(result);
            }

            return result;
        }

        //private method to add the result of a calculation to the previous results queue
        private void enqueueResult(String str)
        {
            //if the queue is at max capacity
            if (previousResults.Count == 11)
            {
                previousResults.Dequeue();  //dequeue the result from the front of the queue
            }
            previousResults.Enqueue(result);    //enqueue the new result at the back of the queue
        }

        //public method to return the most recent result
        public String getResult()
        {
            return result;  //return the result
        }

        //public method to return a previous result at a given index
        public String getPreviousResult(int index)
        {
            //if the index is outside of the bounds provided in the project documentation: 1 - 10
            if (index < 1 || index > 10)
            {
                return "Index Error";   //return an error
            }

            String[] _previousResults = previousResults.ToArray();  //copy the elements of the queue to an array
            Array.Reverse(_previousResults);    //reverse the array

            //if the index we are looking at is within the bounds of the number of elements in the array
            if (index < _previousResults.Length)
            {
                return _previousResults[index]; //return the result at the index
            }

            return "";   //return an empty string
        }

        //public method to get the maximum index that can be used for the getPreviousResults method
        public int getMaximumIndex()
        {
            return previousResults.Count;
        }
    }
}
