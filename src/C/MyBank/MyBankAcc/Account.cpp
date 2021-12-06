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
	this->Transactionlist.push_back(t.getId());
}
std::list<int> Account::listTransactions() {
	return this->Transactionlist;
}
