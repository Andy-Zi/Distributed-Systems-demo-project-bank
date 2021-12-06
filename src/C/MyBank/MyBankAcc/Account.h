#pragma once
#include <string>
#include "Transaction.h"
#include <list>
#include "User.h"

class Account
{
public:
	~Account() {}
	Account(int ownerID, std::string description, int AccountNumber) : OwnerID{ ownerID }, Description{ description }, AccountNumber{ AccountNumber }
	{
	}
	int getOwnerID();
	std::string getDescription();
	int getAccountnumber();
	float getValue();
	void addValue(int change);
	void addTransaction(Transaction t);
	std::list<int> listTransactions();

private:
	int OwnerID;
	std::string Description;
	int AccountNumber;
	float Value = 0;
	std::list<int> Transactionlist;
};