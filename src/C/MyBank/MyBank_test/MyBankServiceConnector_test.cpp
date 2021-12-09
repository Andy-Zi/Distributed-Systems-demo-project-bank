#include "pch.h"
#include "../MyBankAcc/MyBankServiceConnector.cpp"

struct MyBankServiceConnector_Test : testing::Test
{
	MyBankServiceConnector* mybankserviceconnector;

	MyBankServiceConnector_Test()
	{
		mybankserviceconnector = new MyBankServiceConnector();
	}

	~MyBankServiceConnector_Test() {
		delete mybankserviceconnector;
	}
};

TEST_F(MyBankServiceConnector_Test, login) {
	int token = mybankserviceconnector->login("admin", "admin");
	EXPECT_NO_THROW(mybankserviceconnector->login("user", "user"));
	EXPECT_ANY_THROW(mybankserviceconnector->login("admin", "admin"));
	EXPECT_ANY_THROW(mybankserviceconnector->login("wrong", "wrong"));
}

TEST_F(MyBankServiceConnector_Test, newaccount) {
	int token_a = mybankserviceconnector->login("admin", "admin");
	int token_u = mybankserviceconnector->login("user", "user");
	EXPECT_NO_THROW(mybankserviceconnector->newaccount(token_a,"user", "newacc"));
	EXPECT_ANY_THROW(mybankserviceconnector->newaccount(token_u, "user", "newacc"));
	mybankserviceconnector->logout(token_u);
}

TEST_F(MyBankServiceConnector_Test, newuser) {
	int token_a = mybankserviceconnector->login("admin", "admin");
	int token_u = mybankserviceconnector->login("user", "user");
	EXPECT_NO_THROW(mybankserviceconnector->newuser(token_a, "usr", "usr"));
	EXPECT_ANY_THROW(mybankserviceconnector->newuser(token_u, "usrr", "usrr"));
}

TEST_F(MyBankServiceConnector_Test, listaccounts) {
	int token_a = mybankserviceconnector->login("admin", "admin");
	int token_u = mybankserviceconnector->login("user", "user");
	int accid = mybankserviceconnector->newaccount(token_a, "user", "useracc");
	list<Accountdesc> accs = mybankserviceconnector->listaccounts(token_u);
	EXPECT_EQ((*accs.begin()).Account_Number,accid);
}

TEST_F(MyBankServiceConnector_Test, serialize_Accounts) {
	int token_a = mybankserviceconnector->login("admin", "admin");
	int token_u = mybankserviceconnector->login("user", "user");
	int accid = mybankserviceconnector->newaccount(token_a, "user", "useracc");
	list<Accountdesc> accs = mybankserviceconnector->listaccounts(token_u);
	auto test = mybankserviceconnector->SerializeAccountDescriptions(accs);
	EXPECT_EQ((*accs.begin()).Account_Number, accid);
}

/*
TEST_F(MyBankServiceConnector_Test, payinto) {
	int token_a = mybankserviceconnector->login("admin", "admin");
	int token_u = mybankserviceconnector->login("user", "user");
	int accid = mybankserviceconnector->newaccount(token_a, "user", "useracc");
	mybankserviceconnector->payinto(token_a,accid,20);
	EXPECT_EQ((*accs.begin()).Account_Number, accid);
}*/