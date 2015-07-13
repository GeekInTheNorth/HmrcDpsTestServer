# HmrcTpvsProxy

The HMRC has an api service which is used by Payroll software clients to download updates to Tax Codes, Student Loan payments and employer messages.  As part of assisting software developers, the HMRC has a "Third Party Test Server" TPVS which provides the software developer with a collection of random messages.

The problem with the TPVS is that the data is random and some provision has to be made in order to marry up an employee in a payroll application to the message being recieved from the test service.

The point of this project is to create a test system that will allow the end to end testing of a payroll client using one of the following methods:

*Modifying the file being sent by the TPVS to have NI Numbers and Employee Numbers so that they are no longer random and match our formatting and test data.
*Providing a constructed test files with rich edge case scenarios
