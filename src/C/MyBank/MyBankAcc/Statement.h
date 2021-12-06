#pragma once
#include "Account.h"
struct Statement
{
public:
	Statement(Account acc, list<Transaction> trans) : acc{ acc }, trans{ trans }
	{}
	Account acc;
	list<Transaction> trans;
};