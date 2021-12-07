#include "MyBank_i.h"
#include "MyBankConfig.h"
#include "RpcException.h"

static void Bind(unsigned char* netwAddr, unsigned char* endpoint);
static void UnBind(void);

int main(int argc, char* argv[])
{
    unsigned char* netwAddr = TLK_RPC_DEF_NETWADDR;
    unsigned char* endpoint = MYBANK_RPC_ENDPOINT;

    if (argc > 1) netwAddr = (unsigned char*)argv[1];
    if (argc > 2) endpoint = (unsigned char*)argv[2];
    try
    {
        /* RPC binden */
        Bind(netwAddr, endpoint);
        try
        {
            printf("Hallo");
            long token;
            login((UCHAR*)"hallo", (UCHAR*)"hallo", &token);
            printf("%d", token);
            char c = getchar();

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
        printf("stat=0x%x, text=%s, type=%s\n",
            (int)e.GetStatus(), e.GetErrorText(), e.GetErrorType());
    }
}

void Bind(unsigned char* netwAddr, unsigned char* endpoint)
{
    RPC_STATUS status;
    unsigned char* protocolSequence = (UCHAR*)MYBANK_RPC_PROT_SEQ;

    unsigned char* stringBinding = NULL;


    // Erzeugung der Stringdarstellung des Binding-Handles 
    status = RpcStringBindingCompose(NULL,             // Objekt UUID
        (RPC_WSTR)protocolSequence, // Server-/Verbindungsdaten
        (RPC_WSTR)netwAddr,
        (RPC_WSTR)endpoint,
        NULL,             // Keine Optionen fuer TCP/IP
        (RPC_WSTR*)(&stringBinding));
    if (status)
    {
        throw(RpcException(status, (string)"RpcStringBindingCompose failed: Network Address = " +
            (char*)netwAddr,
            "RPC Runtime Error"));
    }

    // Erzeugung des Binding-Handles
    status = RpcBindingFromStringBinding((RPC_WSTR)stringBinding,
        &hMyBank_i);
    if (status)
    {
        throw(RpcException(status, (string)"RpcBindingFromStringBinding failed: String = " +
            (char*)stringBinding,
            "RPC Runtime Error"));
    }

    // Freigabe der Stringdarstellung des Binding-Handles 
    status = RpcStringFree((RPC_WSTR*)(&stringBinding));
    if (status)
    {
        throw(RpcException(status, "RpcStringFree failed", "RPC Error"));
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