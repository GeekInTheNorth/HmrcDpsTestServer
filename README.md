# HMRC DPS Test Server

The HMRC has an api service which is used by Payroll software clients to download updates to Tax Codes, Student Loan payments and employer messages.  As part of assisting software developers, the HMRC has a "Third Party Test Server" TPVS which provides the software developer with a collection of random messages.

The problem with the TPVS is that the data is random and some provision has to be made in order to marry up an employee in a payroll application to the message being received from the test service.

Solution 1:
- Implemented a Web API that can be used in place of the HMRC TPVS Server to return P6, P9, SL1, SL2, AR & NOT messages.
- Implemented a single test file per message type that will return a set of constructed edge cases and will state that there are no more messages of that type to be downloaded.

Solution 2
- Implement a Web API that will wrap the HMRC TPVS server and change randomised NI Numbers and Employee Numbers inside the HMRC test data to a fixed set of acceptable integers and NI Numbers compatible with production systems.

# TODO

- Make Solution 1 handle multiple files per message type
- Make it easier for testers to change the files being served with their own data

# DONE

- Solution 1
- Solution 2

# GLOSSARY

P6: Coding Notice - Issued to change the employee's Tax Codes during the tax year

P9: Coding Notice - Issued to change the employee's Tax Codes at the start of the following Tax Year

SL1: Student Loan Notice - Issued to start Student Loan deductions from an employee

SL2: Student Loan Notice - Issued to end Student Loan deductions from an employee

AR: Annual Reminders for the employer

NOT: Notifications for the employer 
