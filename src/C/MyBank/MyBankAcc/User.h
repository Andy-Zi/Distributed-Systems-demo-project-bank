#pragma once
#include <string>
#include "Priviliges.h"

using namespace std;

class User {
	Priviliges Privilege;
	string Username;
	string Password;
	int id;
	int Token = -1;
public:
	~User() {}
	User() {}
	User(Priviliges priv, string u, string pw, int id) : Username{ u }, Password{ pw }, Privilege{ priv }, id {id}
	{
	}
	Priviliges getPriviliges();
	string getName();
	int getId();
	bool checkPassword(string pw);
	void login(int Token);
	void logout();
	int getToken();
};