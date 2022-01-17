#include "MyBankServiceConnector.h"
#include "MyBankServiceConnector.h"
#include <nlohmann/json.hpp>
#include "JsonUtilityFunctions.cpp"
using json = nlohmann::json;

int MyBankServiceConnector::login(string username, string password)
{
	return mybank->Login(username,password);
}

void MyBankServiceConnector::logout(int token)
{
	mybank->Logout(token);
}

int MyBankServiceConnector::newaccount(int token, string username, string description)
{
	if (mybank->getPriviligesbyToken(token) != Priviliges::admin) {
		throw std::invalid_argument("You need admin Priviliges to do that. Please check that you are logged in with the correct user.\n");
	}
	return mybank->NewAccount((*mybank->getUserByName(username)).getId(), description);
	
}

void MyBankServiceConnector::newuser(int token, string username, string password)
{
	if (mybank->getPriviligesbyToken(token) != Priviliges::admin) {
		throw std::invalid_argument("You need admin Priviliges to do that. Please check that you are logged in with the correct user.\n");
	}
	mybank->NewUser(username, password, Priviliges::user);
}

list<Accountdesc> MyBankServiceConnector::listaccounts(int token)
{
	list<Accountdesc> out = {};
	list<Account> myaccs = mybank->ListAccounts(token);
	for (auto it = myaccs.begin(); it != myaccs.end(); it++)
	{
		Accountdesc adesc((*it).getAccountnumber(), mybank->getUserByID((*it).getOwnerID())->getName(), (*it).getDescription());
		out.push_back(adesc);
	}
	return out;
	
}

void MyBankServiceConnector::bye(int token)
{
	mybank->Logout(token);
}

json MyBankServiceConnector::SerializeAccountDescriptions(list<Accountdesc> accountDescriptions) {
	return SerilaizeList<Accountdesc>(accountDescriptions);
}

json MyBankServiceConnector::SerializeStatements(list<Statement> statements) {
	return SerilaizeList<Statement>(statements);
}

void MyBankServiceConnector::payinto(int token, int account_number, float amount)
{
	if (mybank->getPriviligesbyToken(token) != Priviliges::admin) {
		throw std::invalid_argument("You need admin Priviliges to do that. Please check that you are logged in with the correct user.\n");
	}
	mybank->PayInto(account_number, amount);
	
}

void MyBankServiceConnector::transfer(int token, int from_account_number, int to_account_number, string receiver_name, float amount, string comment)
{
	bool sender_exists = false;
	bool receiver_exists = false;
	list<Account> myaccs = mybank->ListAccounts(token);
	for (auto it = myaccs.begin(); it != myaccs.end(); it++)
	{
		if ((*it).getAccountnumber() == from_account_number)
		{
			sender_exists = true;
		}
		if ((*it).getAccountnumber() == to_account_number)
		{
			receiver_exists = true;
		}
		if(sender_exists && receiver_exists)
		{
			mybank->Transfer(from_account_number, to_account_number, receiver_name, amount, comment);
			return;
		}
	}
	throw std::invalid_argument("Please check the accountnumber. Sender or Receiver doesn't exist.\n");
}

list<Statement> MyBankServiceConnector::statement(int token, int account_number, bool detailed)
{
	list<Statement> out = {};
	Priviliges priv = mybank->getPriviligesbyToken(token);
	list<Account> accs = {};
	accs = mybank->ListAccounts(token);
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
			throw std::invalid_argument("Please check the accountnumber. You do not have acces to this account.\n");
	}
	for (auto it = accs.begin(); it != accs.end(); it++)
	{
		out.push_back(mybank->generate_Statement((*it).getAccountnumber()));
	}
	return out;
}
