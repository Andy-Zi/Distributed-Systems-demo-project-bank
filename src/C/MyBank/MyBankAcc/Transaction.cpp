#include "Transaction.h"
#include <nlohmann/json.hpp>

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

void Transaction::to_json(json& j) {
	//Serialize the Account
	j = json{
		{"startaccID",startaccID},
		{"endaccID", endaccID},
		{"amount",amount},
		{"comment", comment},
		{"id",id},
		{"time",time}
	};
}

void Transaction::from_json(const json& j) {
	//Deserilaize the Account
	j.at("startaccID").get_to(startaccID);
	j.at("endaccID").get_to(endaccID);
	j.at("amount").get_to(amount);
	j.at("comment").get_to(comment);
	j.at("id").get_to(id);
	j.at("time").get_to(time);
}
