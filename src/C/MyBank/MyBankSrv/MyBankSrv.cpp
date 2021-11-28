#include "MyBank_i.h"
#include <stdio.h>

void main(void)
{
	unsigned int minCalls = 1;
	unsigned int maxCalls = 10;

	//Protokoll und Endpunkt registrieren
	RpcServerUseProtseqEp((RPC_WSTR)"ncacn_ip_tcp", maxCalls, (RPC_WSTR)"1200", NULL);

	//Schnittseteele registrieren
	RpcServerRegisterIf(MyBank_i_v0_0_s_ifspec, NULL, NULL);

	//Auf Anforderungen warten...
	RpcServerListen(minCalls, maxCalls, FALSE);
}

void Shutdown(void)
{
	//Warten abbrechen
	RpcMgmtStopServerListening(NULL);

	//Schnitstelle deregistrieren
	RpcServerUnregisterIf(NULL, NULL, FALSE);
}