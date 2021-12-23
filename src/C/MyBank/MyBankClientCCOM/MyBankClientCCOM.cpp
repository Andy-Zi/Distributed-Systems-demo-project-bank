#include <iostream>
#include <atlcomcli.h>
#include "IMyBankFunctions.h"
#include "MyBankClientConsoleApp.h"

int main()
{
    IMyBankFunctions bank;

  
        start(bank);
        if (bank.isConnected) {
            try
            {
                run(bank);
            }
            catch (...)
            {
                throw;
            }
        }
}
