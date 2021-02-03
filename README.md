# SedolValidation
###Introduction
The objective of this exercise is to implement a SEDOL checker in C# based on requirements provided below. The following provides some background information around SEDOL validation:

>SEDOL stands for Stock Exchange Daily Official List, a list of security identifiers used in the United Kingdom and Ireland for clearing purposes.
>SEDOLs are seven characters in length, consisting of two parts: a six-place alphanumeric code and a trailing check digit. 

>The check digit for a SEDOL is chosen to make the total weighted sum of all seven characters a multiple of 10. 

>The check digit is computed using a weighted sum of the first six characters. 

>Letters have the value of 9 plus their alphabet position, such that B = 11 and Z = 35. 
 
>The resulting string of numbers is then multiplied by the weighting factor as follows:

>First:  1; Second: 3; Third: 1; Fourth: 7; Fifth: 3; Sixth: 9; Seventh: 1 (the check digit)

>The character values are multiplied by the weights. 
The check digit is chosen to make the total sum, including the check digit, a multiple of 10, which can be calculated from the weighted sum of the first six characters as 

>(10 − (weighted sum modulo 10)) modulo 10.

>The first character of a user defined SEDOL is a 9.

###Our Team Values
We value simple, functional and pragmatic solutions. 


###Example
Given a SEDOL - 0709954

The checksum can be calculated by multiplying the first six digits by their weightings:
(0×1, 7×3, 0×1, 9×7, 9×3, 5×9) = (0, 21, 0, 63, 27, 45)


Then summing up the results:
0 + 21 + 0 + 63 + 27 + 45 = 156

The check digit is then calculated by:
[10 - (156 modulo 10)] modulo 10 = (10 - 6) modulo 10 = 4 modulo 10 = 4

###Your Task
Implement SEDOL validation logic by using the following interfaces:

```
public interface ISedolValidator
{
    ISedolValidationResult ValidateSedol(string input);
}
public interface ISedolValidationResult
{
    string InputString { get; }
    bool IsValidSedol { get; }
    bool IsUserDefined { get; }
    string ValidationDetails { get; }
}
```
For each call to your implementation of the ISedolValidator interface the expected return is an  implementation the ISedolValidationResult interface.

###Expected effort for completion
We expect the solution to take no more than three hours to complete.


###Specification
The validation logic shold be implemented as per the specification defined in the scenarios below any deviation from the specification should be acompanied with a rationale.


**Scenario:**  Null, empty string or string other than 7 characters long
InputString Test Value|IsValidSedol|IsUserDefined|ValidationDetails
---|--|--|--|
Null|False|False|Input string was not 7-characters long
""|False|False|Input string was not 7-characters long
12|False|False|Input string was not 7-characters long
123456789|False|False|Input string was not 7-characters long


**Scenario:**  Invalid Checksum non user defined SEDOL
InputString Test Value|IsValidSedol|IsUserDefined|ValidationDetails
---|--|--|--|
1234567|False|False|Checksum digit does not agree with the rest of the input


**Scenario:**  Valid non user define SEDOL
InputString Test Value|IsValidSedol|IsUserDefined|ValidationDetails
---|--|--|--|
0709954|True|False|Null
B0YBKJ7|True|False|Null

**Scenario** Invalid Checksum user defined SEDOL
InputString Test Value|IsValidSedol|IsUserDefined|ValidationDetails
---|--|--|--|
9123451|False|True|Checksum digit does not agree with the rest of the input
9ABCDE8|False|True|Checksum digit does not agree with the rest of the input

**Scenario** Invaid characters found
InputString Test Value|IsValidSedol|IsUserDefined|ValidationDetails
---|--|--|--|
9123_51|False|False|SEDOL contains invalid characters
VA.CDE8|False|False|SEDOL contains invalid characters

**Scenario:** Valid user defined SEDOL
InputString Test Value|IsValidSedol|IsUserDefined|ValidationDetails
---|--|--|--|
9123458|True|True|Null
9ABCDE1|True|True|Null


##Expected solution format and restrictions
- You may not collaborate with others or post your solution on a discussion board
- Implement in .NET C#
- Supply a zipped Visual Studio solution.
- In order to avoid problems with firewalls/antivirus checker, please do not include any binary executable files.
- The solution will be executed in a test engine against the test scenarios described above – in order to ensure smooth execution please implement the validator with parameterless constructor so that it can be launched as follows:
var results = tester.TestSedolValidator(new YourImplementedSedolValidator()); 



