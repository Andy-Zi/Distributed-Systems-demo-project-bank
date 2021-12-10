#include "pch.h"
#include "../MyBankAcc/Account.cpp"

struct Account_Test : testing::Test
{
	Account* account;
	int ownerID = 0;
	std::string description = "Das ist ein Account";
	int AccountNumber = 0;

	Account_Test()
	{
		account = new Account(ownerID, description, AccountNumber);
	}

	~Account_Test() {
		delete account;
	}

};

TEST_F(Account_Test, Owner) {
	EXPECT_EQ(ownerID, account->getOwnerID());
}

TEST_F(Account_Test, Accountnumber) {
	EXPECT_EQ(AccountNumber, account->getAccountnumber());
}
TEST_F(Account_Test, Value_1) {
	EXPECT_EQ(0, account->getValue());
}

TEST_F(Account_Test, Value_2) {
	account->addValue(50);
	EXPECT_EQ(50, account->getValue());
}

TEST_F(Account_Test, positvTransaction) {
	float amount = 50;
	Transaction t_positiv(1, 0, amount, "t_positiv", 0);
	account->addTransaction(t_positiv);
	EXPECT_EQ((float)amount, account->getValue());
}

TEST_F(Account_Test, negativTransaction) {
	float amount = 50;
	Transaction t_positiv(0, 1, amount, "t_negativ", 0);
	account->addTransaction(t_positiv);
	EXPECT_EQ((float)amount*-1, account->getValue());
}

TEST_F(Account_Test, listTransaction) {
	int id = 0;
	Transaction t(1, 0, 50, "comment", id);
	account->addTransaction(t);
	std::list<int> transactions = account->listTransactions();
	int it = (*transactions.cbegin());
	EXPECT_EQ(it, id);
}

TEST_F(Account_Test, serialize) {
	string expectedText = "{\"AccountNumber\":0,\"Description\":\"Das ist ein Account\",\"OwnerID\":0,\"TransactionList\":[123,666],\"Value\":100.0}";
	Transaction t1(1, 0, 50, "comment", 123);
	account->addTransaction(t1);
	Transaction t2(1, 0, 50, "comment", 666);
	account->addTransaction(t2);
	json j;
	account->to_json(j);
	string text = j.dump();
	EXPECT_EQ(expectedText, text);
}

TEST_F(Account_Test, deserialize) {
	json jsonString = "{\"AccountNumber\":123,\"Description\":\"Das ist ein Account\",\"OwnerID\":69,\"TransactionList\":[123,666],\"Value\":100.0}"_json;
	Account a;
	a.from_json(jsonString);
	EXPECT_EQ(a.getAccountnumber(),123);
	EXPECT_EQ(a.getOwnerID(), 69);
	EXPECT_EQ(a.getValue(), 100);
	EXPECT_EQ(a.listTransactions().size(), 2);
}
