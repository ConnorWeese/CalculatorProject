# CalculatorProject

# Developed by Connor Weese
# Using .NET Framework 4.7.2 and Visual Studio 2019

Assumptions:
- User would want to be able to paste equations into the calculator
- User would want to be able to type equations into the calculator using their keyboard
- User would want to be able to toggle between the simple calculator and the refactored calculator
- User would want to be able to use Scientific Notation for ease of reading
- The simple and refactored calculators would have separate histories
- Errors would be stored in each calculator's history
- A "number" is any collection of digits and symbols that: starts with a digit, contains any number of {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, ','}, and contains either zero or one '.'
- An "operator" is any one of {+, -, \*, /}
- An "integer" is any collection of digits
- Scientific Notation is accepted as a "number" if it is of the form "number"E+"integer" or "number"E-"integer"

Simple Calculator Instructions:
- The simple calculator will be used if the "Refactored Calculator" checkbox is unchecked
- Accepted input will be "number""operator""number", anything else will give the user an error message
- Pressing the Undo button will give the user a previous result up to 10 results prior
- Pressing the Redo button will give the user the next result up to the most recent result
- Pressing the Clear button will only clear the input text from the textbox, it does not clear history
- Pressing the E button will add an E for Scientific Notation
- Pressing the (-) button will add a '-', this can be used for either subtraction or to denote a negative number in the same way as the - button

Refactored Calculator Instructions:
- The refactored calculator will be used if the "Refactored Calculator" checkbox is checked
- Accepted input will be "number""operator""number" ... "operator""number", anything else will give the user an error message
- Results will be calculated using the order of operations (e.g. 1+2\*10 = 21, not 30)
- Pressing the Undo button will give the user a previous result up to 10 results prior
- Pressing the Redo button will give the user the next result up to the most recent result
- Pressing the Clear button will only clear the input text from the textbox, it does not clear history
- Pressing the E button will add an E for Scientific Notation
- Pressing the (-) button will add a '-', this can be used for either subtraction or to denote a negative number in the same way as the - button
