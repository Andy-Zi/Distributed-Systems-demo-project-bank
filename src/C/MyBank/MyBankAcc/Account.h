#pragma once
#include <string>
#include "Transaction.h"

using namespace std;

class Account
{
public:
	string Get_Owner(void);
	string Get_Description(void);
	string Get_Accountnumber(void);
	float Get_Value();
	void add_transaction();
	transaction* list_transactions();

private:
	char owner;
	char description;
	char AccountNumber;
	float value;
	transaction* transactionlist;
	int numTransactions = 0;
};

