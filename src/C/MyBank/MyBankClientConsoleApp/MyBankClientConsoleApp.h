#pragma once
#include <iostream>
#include <string>
#include "IMyBankFunctions.h"

using namespace std;

namespace client {
	void run();
	void _start(IMyBankFunctions& MyBank);
	void _loop(IMyBankFunctions& MyBank);
	void _login(IMyBankFunctions& MyBank);
}
