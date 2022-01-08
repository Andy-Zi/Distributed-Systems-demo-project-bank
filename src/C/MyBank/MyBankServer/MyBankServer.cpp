#include "MyBank_i.h"
#include "MyBankServer.h"
#include "RpcException.h"
#include "MyBankConfig.h"
#include <stdio.h>
#include "MyBankServiceConnector.h"


int main()
{
    //RPC_STATUS status;
    unsigned char* protocolSequence = MYBANK_RPC_PROT_SEQ;
    unsigned char* endpoint = MYBANK_RPC_ENDPOINT;
    unsigned char* security = NULL;             // Keine Sicherheit
    unsigned int   minCalls = 1;
    unsigned int   maxCalls = 10;

    MyBankServiceConnector bank;

    // Protokoll und Endpunkt registrieren
    RpcServerUseProtseqEp(protocolSequence,
        maxCalls,
        endpoint,
        security);
    //RPC_STATUS_ASSERT("RpcServerUseProtseqEp", status);
    printf("Protokoll und Endpunkt registrieren\n");
    // Schnittstelle registrieren
    RpcServerRegisterIf(MyBank_i_v1_0_s_ifspec, // Zu reg. Schnittstelle
        NULL,                 // MgrTypeUuid
        NULL);                // MgrEpv
    //RPC_STATUS_ASSERT("RpcServerRegisterIf", status);
    printf("Schnittstelle registrieren\n");
    // Auf Anforderungen warten...
    RpcServerListen(minCalls, maxCalls, FALSE);
    //RPC_STATUS_ASSERT("RpcServerListen", status);
    printf("Auf Anforderungen warten...\n");
}

void Shutdown(void)
{

    // Warten abbrechen
    RpcMgmtStopServerListening(NULL);
    //RPC_STATUS_ASSERT("RpcMgmtStopServerListening", status);

    // Schnittstelle deregistrieren
    RpcServerUnregisterIf(NULL, NULL, FALSE);
    //RPC_STATUS_ASSERT("RpcServerUnregisterIf", status);
}