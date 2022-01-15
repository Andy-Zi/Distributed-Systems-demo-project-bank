#include <iostream>
#include <atlcomcli.h>
#include "MyBankConsoleClient.h"
#include "MyBankClientFunctions.h"

int main(int argc, char* argv[])
{
    char empty = '\0';
    char* netwAddr = &empty;
    char* endpointCln = &empty;
    char* username = &empty;
    char* password = &empty;

    if (argc > 1) netwAddr = argv[1];
    if (argc > 2) endpointCln = argv[2];
    if (argc > 3) username = argv[3];
    if (argc > 4) password = argv[4];

    MyBankClientFunctions bank;

    MyBankConsoleClient console(bank);

    bool connected = false;
  
    while (!connected)
    {
        connected = console.start(netwAddr, endpointCln);
    }
    try
    {
        console.run(username, password);
    }
    catch (...)
    {
        cout << "Unexpected error occurred on the server. Please contact a system administrator.\n";
        cout << "Maybe the server can no longer be reached.\n";
    }
    printf("Press enter!\n");
    getchar();
}
