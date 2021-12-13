#pragma once
#include "Account.h"

struct Statement
{
public:
	Statement(Account acc, list<Transaction> trans) : acc{ acc }, trans{ trans }
	{

	}

	Statement() {

	}
	Account acc;
	list<Transaction> trans;

    void to_json(json& j) {

        json serializedAccount;
        this->acc.to_json(serializedAccount);

        json serializedTransactions = json::array();
        for (auto it = trans.begin(); it != trans.end(); it++)
        {
            json j;
            (*it).to_json(j);
            serializedTransactions.push_back(j);
        }

        j = json {
            {"Account", serializedAccount},
            {"Transactions", serializedTransactions},
        };
    }

    void from_json(const json& j) {
        Account deserilaizedAccount;
        deserilaizedAccount.from_json(j["Account"]);
        acc = deserilaizedAccount;
        list<Transaction> deserializedTransactions;
        for (auto it : j["Transactions"])
        {
            Transaction deserializedTransaction;
            deserializedTransaction.from_json(it);
            deserializedTransactions.push_back(deserializedTransaction);
        }
        trans = deserializedTransactions;
        
    }
};

