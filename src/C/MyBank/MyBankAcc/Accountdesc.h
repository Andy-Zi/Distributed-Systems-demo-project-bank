#pragma once
#include <string>
#include <nlohmann/json.hpp>

using namespace std;
using json = nlohmann::json;

struct Accountdesc
{
public:
	Accountdesc(int Account_Number, string Owner, string Description): Account_Number{ Account_Number }, Owner { Owner }, Description{Description}
	{}

	Accountdesc(){}
	int Account_Number;
	string Owner;
	string Description;

	void to_json(json& j) {
		j = json{ {"AccountNumber", Account_Number},{"Owner",Owner}, {"Description", Description}};
	}

	void from_json(const json& j) {
		j.at("AccountNumber").get_to(Account_Number);
		j.at("Owner").get_to(Owner);
		j.at("Description").get_to(Description);
	}
};






