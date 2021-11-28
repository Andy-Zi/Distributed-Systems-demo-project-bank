#include "MyBank_i.h"
#include <stdio.h>

static void Bind(void);
static void UnBind(void);

void main(void)
{
	// RPC binden
	Bind();

	UnBind();
}

void Bind(void)
{
	unsigned char* stringBinding = NULL;
	RpcStringBindingCompose(NULL, (RPC_WSTR)"ncacn_ip_tcp", (RPC_WSTR)"127.0.0.1", (RPC_WSTR)"1200", NULL, (RPC_WSTR)&stringBinding);
	RpcBindingFromStringBinding((RPC_WSTR)stringBinding, &hMyBank_i);
	RpcStringFree((RPC_WSTR*)&stringBinding);
}

void UnBind(void)
{
	RpcBindingFree(&hMyBank_i);
}