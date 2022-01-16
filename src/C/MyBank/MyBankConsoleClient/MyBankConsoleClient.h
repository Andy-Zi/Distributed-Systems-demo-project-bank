#pragma once
#include <iostream>
#include <string>
#include "IMyBankClientFunctions.h"

using namespace std;

class MyBankConsoleClient
{
public:
    MyBankConsoleClient(IMyBankClientFunctions& MyBank,bool port_neeed) : bank(MyBank), port_need {port_neeed}
    {

    }
    void run(string username_from_cmd, string password_from_cmd);
    bool start(string netwAddr_from_cmd, string endpoint_from_cmd = "");
private:
    bool _loop(bool* logged_in);
    bool _login(string username_from_cmd, string password_from_cmd);
    void _parseSingleCommand(string func, bool* exit, bool* active, bool* logged_in);
    void _parseFullCommadn(string func, bool* exit, bool* active, bool* logged_in);
    IMyBankClientFunctions& bank;
    bool port_need;
};