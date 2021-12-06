#include "pch.h"
#include "../MyBankAcc/User.cpp"

struct User_Test : testing::Test
{
	User* user;
	std::string name = "Bob";
	std::string password = "1234";
	int id = 0;
	Priviliges priv = Priviliges::admin;

	User_Test()
	{
		user = new User(priv, name, password, id);
	}

	~User_Test() {
		delete user;
	}
	
};

TEST_F(User_Test, Name) {
	EXPECT_EQ(name, user->getName());
}

TEST_F(User_Test, Privilege) {
	EXPECT_EQ(priv, user->getPriviliges());
}

TEST_F(User_Test, rightPassword) {
	EXPECT_TRUE(user->checkPassword(password));
}

TEST_F(User_Test, wrongPassword) {

	EXPECT_FALSE(user->checkPassword("abc"));
}

TEST_F(User_Test, ID) {

	EXPECT_EQ(id, user->getId());
}

TEST_F(User_Test, getToken) {

	EXPECT_EQ(-1, user->getToken());
}

TEST_F(User_Test, login) {

	user->login(1);
	EXPECT_EQ(1, user->getToken());
}

TEST_F(User_Test, logout) {

	user->logout();
	EXPECT_EQ(-1, user->getToken());
}