#include "pch.h"
#include "../MyBankAcc/Statement.h"


struct Statement_Test : testing::Test
{
	Statement* statement;

	int startaccID = 0;
	int endaccID = 1;
	float amount = 50;
	std::string comment = "comment";
	int id = 0;
	std::time_t time;

	Statement_Test()
	{
		auto account = new Account(69,"A Account Description",1234567890);

		list<Transaction> transactions;
		auto t1 = new Transaction(5, 6, 50.0, "foobar", 420);
		transactions.push_back(*t1);
		auto t2 = new Transaction(9999, 11111, 10000000.0, "foobar 2", 421);
		transactions.push_back(*t2);
		statement = new Statement(*account, transactions);
	}

	~Statement_Test() {
		delete statement;
	}
};

TEST_F(Statement_Test, SerializationPipeline) {
	json j;
	statement->to_json(j);

	Statement deserializedStatement;
	deserializedStatement.from_json(j);

	EXPECT_EQ(statement->acc.getAccountnumber(), deserializedStatement.acc.getAccountnumber());
	EXPECT_EQ(statement->acc.getDescription(), deserializedStatement.acc.getDescription());
	EXPECT_EQ(statement->acc.getOwnerID(), deserializedStatement.acc.getOwnerID());
	EXPECT_EQ(statement->acc.getValue(), deserializedStatement.acc.getValue());
	EXPECT_EQ(statement->trans.size(), deserializedStatement.trans.size());
}