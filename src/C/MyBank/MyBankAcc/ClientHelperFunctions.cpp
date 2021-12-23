#include <nlohmann/json.hpp>
#include "Statement.h"
#include "Accountdesc.h"
#include <string>
#include <iostream>
#include <ctime>
#include "JsonUtilityFunctions.cpp"

using json = nlohmann::json;
using namespace std;

list<Accountdesc> DeserializeAccountDescriptions(string jsonString) {

    json jsonArray = json::parse(jsonString);
    return DeserilaizeList<Accountdesc>(jsonArray);
}

list<Statement> DeserializeStatements(string jsonString) {
    json jsonArray = json::parse(jsonString);
    return DeserilaizeList<Statement>(jsonArray);
}


void PrettyPrintAccountDescriptions(list<Accountdesc> accounts) {

    for (auto it = accounts.begin(); it != accounts.end(); it++)
    {
        Accountdesc currentAccount = (*it);
        cout << "------------------------------------------------\n";
        cout << ("Account Number: " + currentAccount.Account_Number) << "\n";
        cout << ("Description: " + currentAccount.Description) << "\n";
        cout << "------------------------------------------------\n\n";
    }
}

void PrettyPrintStatements(list<Statement> statements) {

    for (auto it = statements.begin(); it != statements.end(); it++)
    {
        Statement currentStatement = (*it);

        Account account = currentStatement.acc;
        cout << "################################################\n\n";
        cout << "Account Information:\n";
        cout << "------------------------------------------------\n";
        cout << "AccountNumber: " << account.getAccountnumber() << "\n";
        cout << "Description: " << account.getDescription() << "\n";
        cout << "Owner: " << account.getOwnerID() << "\n";
        cout << "Value: " << to_string(account.getValue())<< "\n";
        cout << "------------------------------------------------\n\n";
        list<Transaction> transactions = currentStatement.trans;

        cout << "Transactions:\n";
        if (transactions.size() == 0) {
            cout << "No Transactions found!\n\n";
        }
        else {
            for (auto it_t = transactions.begin(); it_t != transactions.end(); it_t++)
            {

                Transaction currentTransaction = (*it_t);
                std::time_t time = currentTransaction.getTime();
                char buffer[32];
                std::tm* ptm = std::localtime(&time);
                std::strftime(buffer, 32, "%a, %d.%m.%Y %H:%M:%S", ptm);
                cout << "------------------------------------------------\n";
                cout << "From: " << currentTransaction.getStartaccID() << "\n";
                cout << "To: " << currentTransaction.getEndaccID() << "\n";
                cout << "Value: " << currentTransaction.getAmount() << "\n";
                cout << "Time: " << buffer << "\n";
                cout << "Comment: " << currentTransaction.getComment()<<"\n";
                cout << "------------------------------------------------\n\n";
            }
        }
        cout << "################################################\n\n\n";
    }
}



