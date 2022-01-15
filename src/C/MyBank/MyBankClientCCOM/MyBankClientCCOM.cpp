#include <iostream>
#include <atlcomcli.h>
#include "MyBankConsoleClient.h"
#include "MyBankClientFunctions.cpp"

int main()
{
    MyBankClientFunctions bank;

    MyBankConsoleClient console(bank);

    bool connected = false;
  
    while (!connected)
    {
        connected = console.start();
    }
        if (bank.isConnected) {
            try
            {
                console.run();
            }
            catch (...)
            {
                throw;
            }
        }
}
