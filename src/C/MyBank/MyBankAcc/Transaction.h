#pragma once
#include <string>

struct transaction {
	std::string startacc;
	std::string endacc;
	float amount;
	float data;
	std::string comment;
};