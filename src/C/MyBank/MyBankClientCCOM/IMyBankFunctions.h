#pragma once
#include <string>
#include <ComUtil.h>

using namespace std;

class IMyBankFunctions {
public:
	bool isConnected = false;
	bool isLoggedIn = false;

	void Bind(string netwAddr);

	string connect_c(string netadr);

	string login_c(string username, string password);

	string newaccount_c(string username, string description);

	string newuser_c(string username, string password);

	string listaccounts_c();

	string transfer_c(int from_account_number, int to_account_number, float amount, string comment);

	string statement_c(int account_number, bool detailed);

	string bye_c();
private:
	int token;
	bool HandleResult(HRESULT result);
};
