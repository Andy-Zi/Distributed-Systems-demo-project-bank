#pragma once
#include <string>
#include "MyBank_i.h"
#include "RpcException.h"

using namespace std;

class IMyBankFunctions {
public:
	void connect_c(string netadr);

	string login_c(string username, string password);

	string newaccount_c(string username, string description);

	string newuser_c(string username, string password);

	string listaccounts_c();

	string transfer_c(long to_account_number, float amount, string comment);

	string statement_c(long account_number, long detailed);

	string bye_c();
private:
	long token;
};
