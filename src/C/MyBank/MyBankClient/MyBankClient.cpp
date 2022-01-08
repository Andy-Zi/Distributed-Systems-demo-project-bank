#include "MyBank_i.h"
#include "MyBankConfig.h"
#include "RpcException.h"
#include "MyBankClientConsoleApp.h"

static void UnBind(void);

int main(void)
{
    unsigned char* netwAddr = MYBANK_RPC_DEF_NETWADDR;
    unsigned char* endpoint = MYBANK_RPC_ENDPOINT;

    IMyBankFunctions bank;

    bool connected = false;

    try
    {
        while (!connected)
        {
            connected = start(bank);
        }
        try
        {
            run(bank);
        }
        catch (...)
        {
            UnBind();
            throw;
        }
        /* RPC-Bindung freigeben */
        UnBind();
    }
    catch (RpcException& e)
    {
        cout << "Verbindung unterbrochen";
        printf("stat=0x%x, text=%s, type=%s\n",
            (int)e.GetStatus(), e.GetErrorText(), e.GetErrorType());
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