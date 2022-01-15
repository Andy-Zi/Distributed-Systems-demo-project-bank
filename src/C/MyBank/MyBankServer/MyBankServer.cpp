#include "MyBank_i.h"
#include "MyBankServer.h"
#include "RpcException.h"
#include "MyBankConfig.h"
#include <stdio.h>
#include <iostream>

using namespace std;

int main()
{
    RPC_STATUS status;
    const string errorText;
    const string errorType;
    unsigned char* protocolSequence = MYBANK_RPC_PROT_SEQ;
    unsigned char* endpoint = MYBANK_RPC_ENDPOINT;
    unsigned char* security = NULL;             // Keine Sicherheit
    unsigned int   minCalls = 1;
    unsigned int   maxCalls = 10;

    try
    {
        // Protokoll und Endpunkt registrieren
        status = RpcServerUseProtseqEp(protocolSequence,
            maxCalls,
            endpoint,
            security);
        if (status)
        {
            RpcException::Raise(status, "Error calling RpcServerRegisterIf","connectionerror");
        }
        printf("Protokoll und Endpunkt registrieren\n");
        // Schnittstelle registrieren
        status = RpcServerRegisterIf(MyBank_i_v1_0_s_ifspec, // Zu reg. Schnittstelle
            NULL,                 // MgrTypeUuid
            NULL);                // MgrEpv
        if (status)
        {
            RpcException::Raise(status, "Error calling RpcServerRegisterIf", "connectionerror");
        }
        printf("Schnittstelle registrieren\n");
        // Auf Anforderungen warten...
        status = RpcServerListen(minCalls, maxCalls, FALSE);
        if (status)
        {
            RpcException::Raise(status, "Error calling RpcServerListen", "connectionerror");
        }
        printf("Auf Anforderungen warten...\n");
    }
    catch (RpcException& e)
    {
        printf("stat=0x%x, text=%s, type=%s\n",
            (int)e.GetStatus(), e.GetErrorText(), e.GetErrorType());
    }
}

void Shutdown(void)
{
    RPC_STATUS status;
    // Warten abbrechen
    status = RpcMgmtStopServerListening(NULL);
    if (status)
    {
        RpcException::Raise(status, "Error calling RpcMgmtStopServerListening", "connectionerror");
    }

    // Schnittstelle deregistrieren
    status = RpcServerUnregisterIf(NULL, NULL, FALSE);
    if (status)
    {
        RpcException::Raise(status, "Error calling RpcServerUnregisterIf", "connectionerror");
    }
}