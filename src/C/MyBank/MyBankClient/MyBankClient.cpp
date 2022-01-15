#include "MyBank_i.h"
#include "MyBankConfig.h"
#include "RpcException.h"
#include "MyBankConsoleClient.h"
#include <MyBankClient/MyBankClientFunctions.h>

static void UnBind(void);

int main(void)
{
    unsigned char* netwAddr = MYBANK_RPC_DEF_NETWADDR;
    unsigned char* endpoint = MYBANK_RPC_ENDPOINT;

    MyBankClientFunctions bank;

    MyBankConsoleClient console(bank);

    bool connected = false;

    while (!connected)
    {
        connected = console.start();
    }
    try
    {
        console.run();
        UnBind();
    }
    catch (...)
    {
        UnBind();
        cout << "Unexpected error occurred on the server. Please contact a system administrator.\n";
        cout << "Maybe the server can no longer be reached.\n";
    }
}

void UnBind(void)
{
    RPC_STATUS status;

    // Freigabe des Binding-Handles 
    status = RpcBindingFree(&hMyBank_i);
    if (status)
    {
        throw(RpcException(status, "RpcBindingFree failed", "RPC Error"));
    }
}