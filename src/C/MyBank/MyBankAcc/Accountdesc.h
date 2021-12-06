#pragma once
#include <string>

using namespace std;

struct Accountdesc
{
public:
	Accountdesc(int Account_Number, string Description): Account_Number{ Account_Number }, Description{ Description }
	{}
	int Account_Number;
	string Description;
};

