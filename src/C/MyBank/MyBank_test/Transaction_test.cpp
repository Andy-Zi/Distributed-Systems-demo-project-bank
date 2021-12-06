#include "pch.h"
#include "../MyBankAcc/Transaction.cpp"

struct Transaction_Test : testing::Test
{
	Transaction* transaction;

	int startaccID = 0;
	int endaccID = 1;
	float amount = 50;
	std::string comment = "comment";
	int id = 0;
	std::time_t time;

	Transaction_Test()
	{
		transaction = new Transaction(startaccID, endaccID, amount, comment, id);
		auto now = std::chrono::system_clock::now();
		time = std::chrono::system_clock::to_time_t(now);
	}

	~Transaction_Test() {	
		delete transaction;
	}
};

TEST_F(Transaction_Test, startAccount) {
	EXPECT_EQ(startaccID, transaction->getStartaccID());
}

TEST_F(Transaction_Test, endAccount) {
	EXPECT_EQ(endaccID, transaction->getEndaccID());
}

TEST_F(Transaction_Test, Amount) {
	EXPECT_EQ(amount, transaction->getAmount());
}

TEST_F(Transaction_Test, Comment) {
	EXPECT_EQ(comment, transaction->getComment());
}

TEST_F(Transaction_Test, ID) {
	EXPECT_EQ(id, transaction->getId());
}

TEST_F(Transaction_Test, sameStartEnd) {
	EXPECT_ANY_THROW(Transaction transaction(startaccID, startaccID, amount, comment, id));
}

TEST_F(Transaction_Test, zeroAmount) {
	EXPECT_ANY_THROW(Transaction transaction(startaccID, endaccID, 0, comment, id));
}

TEST_F(Transaction_Test, negativAmount) {
	EXPECT_ANY_THROW(Transaction transaction(startaccID, endaccID, -5, comment, id));
}

TEST_F(Transaction_Test, Time) {
	EXPECT_EQ(time,transaction->getTime());
}