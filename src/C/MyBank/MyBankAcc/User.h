#pragma once
#include <string>
#include "Priviliges.h"
#include <nlohmann/json.hpp>

using json = nlohmann::json;
using namespace std;

class User {
public:
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
	void to_json(json& j);
	void from_json(const json& j);

private:
	Priviliges Privilege = Priviliges::user;
	string Username = "";
	string Password = "";
	int id = -1;
	int Token = -1;
};