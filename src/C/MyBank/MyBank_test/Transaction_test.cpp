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

TEST_F(Transaction_Test, Serialize) {
	std::string expectedText = "{\"amount\":50.0,\"comment\":\"comment\",\"endaccID\":1,\"id\":0,\"startaccID\":0,\"time\":"+std::to_string(time)+"}";
	json j;
	transaction->to_json(j);
	std::string text = j.dump();
	EXPECT_EQ(expectedText,text);
}

TEST_F(Transaction_Test, Deserialize) {
	std::string jsonString = ("{\"amount\":50.0,\"comment\":\"comment\",\"endaccID\":1,\"id\":0,\"startaccID\":0,\"time\":" + std::to_string(time) + "}");
	auto j = json::parse(jsonString);
	Transaction t;
	t.from_json(j);
	EXPECT_EQ(t.getAmount(), 50.0);
	EXPECT_EQ(t.getComment(), "comment");
	EXPECT_EQ(t.getEndaccID(), 1);
	EXPECT_EQ(t.getId(), 0);
	EXPECT_EQ(t.getStartaccID(), 0);
	EXPECT_EQ(t.getTime(), time);
}