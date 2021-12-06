#pragma once
#include <string>
#include <stdexcept>
#include <ctime> 
#include <chrono>

class Transaction {
public:
	~Transaction(){}
	Transaction(int startaccountID, int endaccountID, float amount, std::string comment, int id) : startaccID{ startaccountID }, endaccID{ endaccountID }, amount{ amount }, comment{ comment },id{ id }
	{
		checkValidTransaction();
		auto now = std::chrono::system_clock::now();
		time = std::chrono::system_clock::to_time_t(now);
	}
	int getStartaccID();
	int getEndaccID();
	float getAmount();
	std::string getComment();
	int getId();
	void checkValidTransaction();
	std::time_t getTime();
private:
	int startaccID;
	int endaccID ;
	float amount;
	std::string comment;
	int id;
	std::time_t time;
};