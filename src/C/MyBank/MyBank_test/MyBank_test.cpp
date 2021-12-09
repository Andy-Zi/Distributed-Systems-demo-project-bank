#include "pch.h"
#include "../MyBankAcc/MyBank.cpp"

struct MyBank_Test : testing::Test
{
	MyBank* mybank;
	User usr_a;
	User usr_u;

	MyBank_Test()
	{
		mybank = new MyBank();
		usr_a = mybank->getKnownUsers().front();
		usr_u = mybank->getKnownUsers().back();
	}

	~MyBank_Test() {
		delete mybank;
	}
};

TEST_F(MyBank_Test, addUser) {
	EXPECT_EQ("admin", usr_a.getName());
}

TEST_F(MyBank_Test, addAccount) {
	mybank->NewAccount(usr_a.getId(), "description");
	EXPECT_EQ("admin", usr_a.getName());
}

TEST_F(MyBank_Test, login_wrongPassword) {
	EXPECT_ANY_THROW(mybank->Login("admin","wronpassword"));
}

TEST_F(MyBank_Test, login_wrongUsername) {
	EXPECT_ANY_THROW(mybank->Login("wronguser", "admin"));
}

TEST_F(MyBank_Test, login) {
	EXPECT_EQ(11,mybank->Login("admin", "admin"));
}

TEST_F(MyBank_Test, loginUserList) {
	mybank->Login("admin", "admin");
	User usr = **mybank->getLoggedinUsers().begin();
	EXPECT_EQ("admin", usr.getName());
}

TEST_F(MyBank_Test, getUsrByToken) {
	int token = mybank->Login("admin", "admin");
	User usr = *(mybank->getUserByToken(token));
	EXPECT_EQ("admin", usr.getName());
}

TEST_F(MyBank_Test, getUsrBywrongToken) {
	EXPECT_ANY_THROW(mybank->getUserByToken(25));
}

TEST_F(MyBank_Test, getPriviligesbyToken) {
	int token = mybank->Login("admin", "admin");
	EXPECT_EQ(Priviliges::admin, mybank->getPriviligesbyToken(token));
}

TEST_F(MyBank_Test, getUsrByID) {
	int token = mybank->Login("admin", "admin");
	User usr = *(mybank->getUserByID((*(mybank->getUserByToken(token))).getId()));
	EXPECT_EQ("admin", usr.getName());
}

TEST_F(MyBank_Test, getUsrBywrongID) {
	EXPECT_ANY_THROW(mybank->getUserByID(25));
}

TEST_F(MyBank_Test, getPriviligesbyID) {
	int token = mybank->Login("admin", "admin");
	EXPECT_EQ(Priviliges::admin, mybank->getPriviligesbyID((*(mybank->getUserByToken(token))).getId()));
}

TEST_F(MyBank_Test, getUsrByName) {
	int token = mybank->Login("admin", "admin");
	User usr = *(mybank->getUserByName("admin"));
	EXPECT_EQ("admin", usr.getName());
}

TEST_F(MyBank_Test, getUsrByWrongName) {
	EXPECT_ANY_THROW(mybank->getUserByName("wrongName"));
}

TEST_F(MyBank_Test, getPriviligesbyUsername) {
	int token = mybank->Login("admin", "admin");
	EXPECT_EQ(Priviliges::admin, mybank->getPriviligesbyUsername("admin"));
}

TEST_F(MyBank_Test, logout) {
	User* usr = mybank->getUserByName("admin");
	EXPECT_EQ(-1, (*usr).getToken());
	int token = mybank->Login("admin", "admin");
	EXPECT_NO_THROW(mybank->getUserByToken(token));
	EXPECT_EQ(token, (*usr).getToken());
	mybank->Logout(token);
	EXPECT_ANY_THROW(mybank->getUserByToken(token));
	EXPECT_EQ(-1, (*usr).getToken());
}

TEST_F(MyBank_Test, ListAccounts) {
	int token = mybank->Login("admin", "admin");
	mybank->NewAccount(usr_u.getId(), "testacc");
	list<Account> accs = mybank->ListAccounts(token);
	EXPECT_EQ("testacc", (*accs.begin()).getDescription());
}

TEST_F(MyBank_Test, getTransactionbyID) {
	int uaccid = mybank->NewAccount(usr_u.getId(), "usracc");
	mybank->PayInto(uaccid, 50);
	int aaccid = mybank->NewAccount(usr_a.getId(), "adminacc");
	int transid = mybank->Transfer(uaccid, aaccid, 20, "testtrans");
	EXPECT_EQ("testtrans", mybank->getTransactionbyID(transid).getComment());
}



TEST_F(MyBank_Test, getTransactionsForAccount) {
	int uaccid = mybank->NewAccount(usr_u.getId(), "usracc");
	mybank->PayInto(uaccid, 50);
	int aaccid = mybank->NewAccount(usr_a.getId(), "adminacc");
	int transid = mybank->Transfer(uaccid, aaccid, 20, "testtrans");
	EXPECT_EQ("testtrans", (*mybank->getTransactionsForAccount(aaccid).begin()).getComment());
}