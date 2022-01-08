#pragma once
#include <string>
#include "MyBank_i.h"
#include "RpcException.h"

using namespace std;

class IMyBankFunctions {
public:
	bool isConnected = false;
	bool isLoggedIn = false;

	void Bind(unsigned char* netwAddr, unsigned char* endpoint);

	string connect_c(string netadr, string port, bool& connected);

	string login_c(string username, string password, bool& connected);

	string newaccount_c(string username, string description);

	string newuser_c(string username, string password);

	string listaccounts_c();

	string transfer_c(long from_account_number,long to_account_number, float amount, string comment);

	string statement_c(long account_number, long detailed);

	string bye_c();
private:
	long token;
	bool HandleResult(HRESULT result);
};
