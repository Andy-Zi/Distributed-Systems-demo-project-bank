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