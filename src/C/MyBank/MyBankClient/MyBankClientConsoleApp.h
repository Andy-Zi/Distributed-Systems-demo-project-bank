#pragma once
#include <iostream>
#include <string>
#include "IMyBankFunctions.h"

using namespace std;

void run(IMyBankFunctions& MyBank);
void start(IMyBankFunctions& MyBank);
void _loop(IMyBankFunctions& MyBank);
void _login(IMyBankFunctions& MyBank);