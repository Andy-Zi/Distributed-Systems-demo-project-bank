#pragma once
#include "User.h"
#include "Priviliges.h"
#include "Accountdesc.h"
#include "MyBank.h"
#include <string>


using namespace std;

class MyBankServiceConnector
{
public:
    MyBankServiceConnector() {
        char* buf = nullptr;
        size_t sz = 0;
        _dupenv_s(&buf, &sz, "APPDATA");
        string appdata = buf;
        free(buf);
        appdata += "\\..\\Local\\MyBank";
        mybank = new MyBank(appdata);
    }

    ~MyBankServiceConnector(){}
    int login(string username, string password);
    void logout(int token);
/*
## a) login(in username, in password, out token)
Username: The Username of the User [string]
Password: The Password of the User [string]
Token: The Token of the User [Hex-String]
(Last Digit represents the User-Type)
0: User
1: Admin
*/
    int newaccount(int token, string username, string description); //(Admin only)
/*
## b 1) (Admin only) newaccount(in token, in username, in description, out account_number) 
Token: The Token of the User making the request[Hex-String]
Username: The Name of the User to Create a new Account for [string]
Description: The Description of the Account [string]
Account_Number: The Account Number of the new Account [string (GUID)]
*/
    void newuser(int token, string username, string password); //(Admin only)
/*
## b 2) (Admin only) newuser(in token, in username, in password) 
Token: The Token of the User making the request[Hex-String]
Username: The Name of the User to Create [string]
Password: The Password of the User to Create [string]
*/
    list<Accountdesc> listaccounts(int token);
/*
## c) listaccounts(in token, out (Account_Number,Description))
Token: The Token of the User making the request[Hex-String]
Account_Number,Description: A List of all Account_Numbers and Desciptions accessible by this User [(string,string)[]]
*/
    void payinto(int token, int account_number, float amount);   //(Admin only)
/*
## d) (Admin only) payinto(in token, in account_number, in amount)
Token: The Token of the User making the request[Hex-String]
Account_Number: The Account Number of the Account to pay into [string (GUID)]
Amount: The Amount to pay into the Account [float]
*/
    void transfer(int token, int from_account_number, int to_account_number, float amount, string comment);
/*
## e) transfer(in token, in from_account_number, in to_account_number, in amount, in comment)
Token: The Token of the User making the request[Hex-String]
From_Account_Number: The Account Number of the Account to pay from [string (GUID)]
To_Account_Number: The Account Number of the Account to pay into [string (GUID)]
Amount: The Amount to pay into the Account [float]
Comment: The Comment to add to the Transaction [string]
*/
    list<Statement> statement(int token, int account_number = -1, bool detailed = 0);
/*
## f) statement (in token, in [optional] account_number,in [optional] detailed , out accounts)
Token: The Token of the User making the request[Hex-String]
Account_Number: The Account Number of the Account to get the Statement for [string (GUID)]
Detailed: If set to true, the Statement will include all Transactions, otherwise none [boolean]
Accounts: A List of all Accounts or the given Account accessible by this User [Account-Object[]]
*/
    void bye(int token);
/*
## g) bye(in token)
Token: The Token of the User making the request[Hex-String]
*/

    json SerializeAccountDescriptions(list<Accountdesc> accountDescriptions);

    json SerializeStatements(list<Statement> statements);

private:
    MyBank* mybank;
};

