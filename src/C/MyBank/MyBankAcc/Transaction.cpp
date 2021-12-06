#include "Transaction.h"

int Transaction::getStartaccID()
{
	return this->startaccID;
}

int Transaction::getEndaccID()
{
	return this->endaccID;
}

float Transaction::getAmount()
{
	return this->amount;
}

std::string Transaction::getComment()
{
	return this->comment;
}

int Transaction::getId()
{
	return this->id;
}

void Transaction::checkValidTransaction()
{
	if (this->startaccID == this->endaccID)
	{
		throw std::invalid_argument("startaccount equals endaccount");
	}
	if(this->amount == 0)
	{
		throw std::invalid_argument("zeroAmount Transaction");
	}
	if (this->amount < 0)
	{
		throw std::invalid_argument("negativAmount Transaction");
	}
}

std::time_t Transaction::getTime()
{
	return this->time;
}
