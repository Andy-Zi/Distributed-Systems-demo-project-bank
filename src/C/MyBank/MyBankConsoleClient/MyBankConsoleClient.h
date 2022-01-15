#pragma once
#include <iostream>
#include <string>
#include "IMyBankClientFunctions.h"

using namespace std;

class MyBankConsoleClient
{
public:
    MyBankConsoleClient(IMyBankClientFunctions& MyBank) : bank(MyBank)
    {

    }
    void run();
    bool start();
private:
    bool _loop(bool* logged_in);
    bool _login();
    void _parseSingleCommand(string func, bool* exit, bool* active, bool* logged_in);
    void _parseFullCommadn(string func, bool* exit, bool* active, bool* logged_in);
    IMyBankClientFunctions& bank;
};