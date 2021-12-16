#include "MyBankServiceConnector.h"
#include <nlohmann/json.hpp>
using json = nlohmann::json;

int MyBankServiceConnector::login(string username, string password)
{
	return mybank.Login(username,password);
}

void MyBankServiceConnector::logout(int token)
{
	mybank.Logout(token);
}

int MyBankServiceConnector::newaccount(int token, string username, string description)
{
	if (mybank.getPriviligesbyToken(token) != Priviliges::admin) {
		throw std::invalid_argument("not admin");
	}
	return mybank.NewAccount((*mybank.getUserByName(username)).getId(), description);
	
}

void MyBankServiceConnector::newuser(int token, string username, string password)
{
	if (mybank.getPriviligesbyToken(token) != Priviliges::admin) {
		throw std::invalid_argument("not admin");
	}
	mybank.NewUser(username, password, Priviliges::user);
}

list<Accountdesc> MyBankServiceConnector::listaccounts(int token)
{
	list<Accountdesc> out = {};
	list<Account> myaccs = mybank.ListAccounts(token);
	for (auto it = myaccs.begin(); it != myaccs.end(); it++)
	{
		Accountdesc adesc((*it).getAccountnumber(), (*it).getDescription());
		out.push_back(adesc);
	}
	return out;
}

json MyBankServiceConnector::SerializeAccountDescriptions(list<Accountdesc> accountDescriptions) {
	json serialized_list = json::array();
	for (auto it = accountDescriptions.begin(); it != accountDescriptions.end(); it++)
	{
		json j;
		(*it).to_json(j);
		serialized_list.push_back(j);
	}
	
	return serialized_list;
}

json MyBankServiceConnector::SerializeStatements(list<Statement> statements) {
	json serialized_list = json::array();
	for (auto it = statements.begin(); it != statements.end(); it++)
	{
		json j;
		(*it).to_json(j);
		serialized_list.push_back(j);
	}

	return serialized_list;
}

void MyBankServiceConnector::payinto(int token, int account_number, float amount)
{
	if (mybank.getPriviligesbyToken(token) != Priviliges::admin) {
		throw std::invalid_argument("not admin");
	}
	mybank.PayInto(account_number, amount);
	
}

void MyBankServiceConnector::transfer(int token, int from_account_number, int to_account_number, float amount, string comment)
{
	list<Account> myaccs = mybank.ListAccounts(token);
	for (auto it = myaccs.begin(); it != myaccs.end(); it++)
	{
		if ((*it).getAccountnumber() == from_account_number)
		{
			mybank.Transfer(from_account_number, to_account_number, amount, comment);
			break;
		}
	}
}

list<Statement> MyBankServiceConnector::statement(int token, int account_number, bool detailed)
{
	list<Statement> out = {};
	Priviliges priv = mybank.getPriviligesbyToken(token);
	list<Account> accs = {};
	accs = mybank.ListAccounts(token);
	if(account_number != -1)
	{
		bool match = false;
		for (auto it = accs.begin(); it != accs.end(); it++)
		{
			if ((*it).getAccountnumber() == account_number)
			{
				auto account = *it;
				accs.clear();
				accs.push_back(account);
				match = true;
				break;
			}
		}
		if(!match)
			throw std::invalid_argument("wrong accountnumber");
	}
	for (auto it = accs.begin(); it != accs.end(); it++)
	{
		out.push_back(mybank.generate_Statement((*it).getAccountnumber()));	
	}
	return out;
}
