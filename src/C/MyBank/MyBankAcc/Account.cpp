#include "Account.h"

int Account::getOwnerID() {
	return this->OwnerID;
}
string Account::getDescription() {
	return this->Description;
}
int Account::getAccountnumber() {
	return this->AccountNumber;
}
float Account::getValue() {
	return this->Value;
}
void Account::addValue(int change)
{
	this->Value += change;
}
void Account::addTransaction(Transaction t) {
	if (t.getStartaccID() == getOwnerID())
	{
		if ((Value - t.getAmount()) > 0)
		{
			throw std::invalid_argument("not enough funds");
		}
		Value -= t.getAmount();
	}
	if (t.getEndaccID() == getOwnerID())
		Value += t.getAmount();
	this->TransactionList.push_back(t.getId());
}

std::list<int> Account::listTransactions() {
	return this->TransactionList;
}

void Account::to_json(json& j) {
	//Serialize the Account
	j = json{ 
		{"OwnerID", OwnerID},
		{"Description", Description},
		{"AccNumber",AccountNumber},
		{"Value", Value},
		{"TransactionList",TransactionList}
	};
}

void Account::from_json(const json& j) {
	//Deserilaize the Account
	j.at("OwnerID").get_to(OwnerID);
	j.at("Description").get_to(Description);
	j.at("AccNumber").get_to(AccountNumber);
	j.at("Value").get_to(Value);
	j.at("TransactionList").get_to(TransactionList);
}
