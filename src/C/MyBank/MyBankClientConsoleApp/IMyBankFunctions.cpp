#include "IMyBankFunctions.h"

void IMyBankFunctions::connect_c(string netadr)
{
}

string IMyBankFunctions::login_c(string username, string password)
{
	return "Hallo " + username + "\n";
}

string IMyBankFunctions::newaccount_c(string username, string description)
{
	return "Der Account für " + username + " wurde angelegt";
}

string IMyBankFunctions::newuser_c(string username, string password)
{
	return "Der Nutzer " + username + " wurde angelegt";
}

string IMyBankFunctions::listaccounts_c()
{
	return string();
}

string IMyBankFunctions::transfer_c(long to_account_number, float amount, string comment)
{
	return "Das Geld wurde überwiesen";
}

string IMyBankFunctions::statement_c(long account_number, long detailed)
{
	return string();
}

string IMyBankFunctions::bye_c()
{
	return "Bis zum nächsten mal.\n";
}
