#pragma once
#include "Priviliges.h"
#include "Accountdesc.h"
#include "User.h"
#include <string>
#include <list>
#include "Account.h"
#include "Statement.h"
#include <mutex>

class MyBank
{
public:
	std::mutex mybank_mutex;

	~MyBank(){
		if (directory != "NONE") {
			Save(directory);
		}
	}
	MyBank()
	{
		NewUser("admin", "admin", Priviliges::admin);
		NewUser("user", "user", Priviliges::user);
		NewAccount(0, "Thats an account of admin.");
		NewAccount(1, "Thats an account of user");
		PayInto(0, 100);
		Transfer(0, 1, 10, "Thats an transacton from admin to user.");
	}

	MyBank(string saveDirectory) {
		directory = saveDirectory;
		Load(directory);

		if (KnownUsers.size() < 1) {
			NewUser("admin", "admin", Priviliges::admin);
			NewUser("user", "user", Priviliges::user);
			NewAccount(0, "Thats an account of admin.");
			NewAccount(1, "Thats an account of user");
			PayInto(0, 100);
			Transfer(0, 1, 10, "Thats an transacton from admin to user.");
		}
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
	list<User> getKnownUsers();
	list<User*> getLoggedinUsers();
	void Save(string directory);
	void Load(string directory);

private:
	string directory= "NONE";
	void SaveFile(string content, string path);
	string LoadFile(string path);
	list<User*> LoggedinUsers = {};
	list<User> KnownUsers = {};
	list<Account> Accounts = {};
	list<Transaction> Transactions = {};
};

