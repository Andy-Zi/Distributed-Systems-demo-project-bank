#pragma once
#include <iostream>
#include <string>
#include "IMyBankFunctions.h"

using namespace std;

void run(IMyBankFunctions& MyBank);
bool start(IMyBankFunctions& MyBank);
bool _loop(IMyBankFunctions& MyBank);
bool _login(IMyBankFunctions& MyBank);