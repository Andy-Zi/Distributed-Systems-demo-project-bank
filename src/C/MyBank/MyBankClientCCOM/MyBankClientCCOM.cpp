#include <iostream>
#include <atlcomcli.h>
#include "MyBankConsoleClient.h"
#include "MyBankClientFunctions.h"

int main(int argc, char* argv[])
{
    char empty = '\0';
    char* netwAddr = &empty;
    char* username = &empty;
    char* password = &empty;

    if (argc > 1) netwAddr = argv[1];
    if (argc > 2) username = argv[2];
    if (argc > 3) password = argv[3];

    MyBankClientFunctions bank;

    MyBankConsoleClient console(bank,0);

    bool connected = false;
  
    while (!connected)
    {
        connected = console.start((string)netwAddr);
    }
    try
    {
        console.run((string)username, (string)password);
        bank.UnBind();
    }
    catch (...)
    {
        bank.UnBind();
        cout << "\nUnexpected error occurred on the server. Please contact a system administrator.\n";
        cout << "Maybe the server can no longer be reached.\n";
    }
    printf("Press enter!\n");
    getchar();
}
