#pragma once
#include <string>
#include "Transaction.h"
#include <list>
#include "User.h"
#include <nlohmann/json.hpp>

using json = nlohmann::json;

class Account
{
public:
	~Account() {}
	Account(int ownerID, std::string description, int AccountNumber) : OwnerID{ ownerID }, Description{ description }, AccountNumber{ AccountNumber }
	{
	}

	Account() {

	}
	Account(const Account& old) : OwnerID {old.OwnerID}, Description {old.Description}, AccountNumber {old.AccountNumber}, Value {old.Value }, TransactionList {old.TransactionList }
	{

	}
	int getOwnerID();
	std::string getDescription();
	int getAccountnumber();
	float getValue();
	void addValue(int change);
	void addTransaction(Transaction t);
	std::list<int> listTransactions();
	void to_json(json& j);
	void from_json(const json& j);

private:
	int OwnerID;
	std::string Description;
	int AccountNumber;
	float Value = 0;
	std::list<int> TransactionList;
};