#pragma once
#include <string>
#include <nlohmann/json.hpp>

using namespace std;
using json = nlohmann::json;

struct Accountdesc
{
public:
	Accountdesc(int Account_Number, string Description): Account_Number{ Account_Number }, Description{ Description }
	{}

	Accountdesc(){}
	int Account_Number;
	string Description;

	void to_json(json& j) {
		j = json{ {"AccountNumber", Account_Number}, {"Description", Description} };
	}

	void from_json(const json& j) {
		j.at("AccountNumber").get_to(Account_Number);
		j.at("Description").get_to(Description);
	}
};






