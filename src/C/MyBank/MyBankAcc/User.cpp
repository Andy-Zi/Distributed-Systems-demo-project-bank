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

void User::to_json(json& j) {
	//Serialize the User
	j = json{
		{"Privilege",Privilege},
		{"Username", Username},
		{"Password",Password},
		{"id",id}
	};
}

void User::from_json(const json& j) {
	//Deserilaize the User
	j.at("Privilege").get_to(Privilege);
	j.at("Username").get_to(Username);
	j.at("Password").get_to(Password);
	j.at("id").get_to(id);
}

