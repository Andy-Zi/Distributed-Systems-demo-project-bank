#include "MyBank_i.h" 

void login(unsigned char* username, unsigned char* password, long* token)
{
	*token = 3;
}

void newaccount(long token,unsigned char* username,unsigned char* description, long* account_number)
{

}

void newuser(long token, unsigned char* username, unsigned char* password)
{

}

void listaccounts(long token, long accountnumbers, unsigned char* descriptions)
{

}

void transfer(long token, long from_account_number, long to_account_number, float amount, unsigned char* comment)
{

}

void statement(long token, long account_number, long detailed)
{

}

void bye(long token)
{

}