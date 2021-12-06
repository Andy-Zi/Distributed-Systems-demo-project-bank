#pragma once
#include <string>
#include <list>
#include "User.h"
#include "Account.h"
#include "Statement.h"


class MyBank
{
public:
	~MyBank(){}
	MyBank()
	{
		NewUser("admin", "admin", Priviliges::admin);
		NewUser("user", "user", Priviliges::user);
	}
	int Login(string username, string password);
	void Logout(int token);
	void NewUser(string username, string password, Priviliges privilige);
	int NewAccount(int ownerID, string description);
	list<Account> ListAccounts(int Token);
	int PayInto(int Accountnumber, float amount);
	int Transfer(int from_accountnumber, int to_accountnumber, float amount, string comment = "");
	Priviliges getPriviligesbyToken(int Token);
	Priviliges getPriviligesbyID(int ID);
	Priviliges getPriviligesbyUsername(string username);
	User* getUserByToken(int Token);
	User* getUserByID(int ID);
	User* getUserByName(string username);
	Transaction getTransactionbyID(int transac_id);
	list<Transaction> getTransactionsForAccount(int acc_id);
	Statement generate_Statement(int accountID);
	Account getAccountByID(int accountID);
	//TODO
	void shutdown();
	//TODO
	void loadData();
	//TODO
	void storeData();
	list<User> getKnownUsers();
	list<User*> getLoggedinUsers();
private:
	list<User*> LoggedinUsers = {};
	list<User> KnownUsers = {};
	list<Account> Accounts = {};
	list<Transaction> Transactions = {};
};

