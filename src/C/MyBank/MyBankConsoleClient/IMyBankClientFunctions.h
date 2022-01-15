#pragma once
#include <string>
using namespace std;

class IMyBankClientFunctions
{
public:
	bool isConnected = false;
	bool isLoggedIn = false;

	virtual void Bind(unsigned char* netwAddr, unsigned char* endpoint) = 0;

	virtual string connect_c(string netadr, string port, long& connected) = 0;

	virtual string login_c(string username, string password, bool& connected) = 0;

	virtual string newaccount_c(string username, string description) = 0;

	virtual string newuser_c(string username, string password) = 0;

	virtual string listaccounts_c() = 0;

	virtual string transfer_c(long from_account_number, long to_account_number, float amount, string comment) = 0;

	virtual string payinto_c(long account_number, float amount) = 0;

	virtual string statement_c(long account_number, long detailed) = 0;

	virtual string bye_c() = 0;
protected:
	long token;
};