#include "User.h"

Priviliges User::getPriviliges(void)
{
	return this->Privilege;
}

string User::getName(void)
{
	return this->Username;
}

int User::getId()
{
	return id;
}

bool User::checkPassword(string pw)
{
	if (this->Password == pw)
	{
		return true;
	}
	return false;
}

void User::login(int Token)
{
	this->Token = Token;
}

void User::logout()
{
	this->Token = -1;
}

int User::getToken()
{
	return this->Token;
}
