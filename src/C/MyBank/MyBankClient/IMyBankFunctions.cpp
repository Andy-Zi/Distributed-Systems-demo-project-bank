#include "IMyBankFunctions.h"

string IMyBankFunctions::connect_c(string netwAddr, string endpoint)
{
	/* RPC binden */
	Bind((unsigned char*)netwAddr.c_str(), (unsigned char*)endpoint.c_str());
    return "Bank verbunden\n";
}

string IMyBankFunctions::login_c(string username, string password)
{
    login((unsigned char*)username.c_str(), (unsigned char*)password.c_str(),&token);
	return "Hallo " + username + "\n";
}

string IMyBankFunctions::newaccount_c(string username, string description)
{
    long accountnumber;
    newaccount(token, (unsigned char*)username.c_str(), (unsigned char*)username.c_str(), &accountnumber);
	return "Der Account für " + username + " wurde angelegt";
}

string IMyBankFunctions::newuser_c(string username, string password)
{
    newuser(token, (unsigned char*)username.c_str(), (unsigned char*)password.c_str());
	return "Der Nutzer " + username + " wurde angelegt";
}

string IMyBankFunctions::listaccounts_c()
{
    unsigned char* listacc;
    listaccounts(token,&listacc);
	return (reinterpret_cast<char*>(listacc));
}

string IMyBankFunctions::transfer_c(long from_account_number,long to_account_number, float amount, string comment)
{
    transfer(token, from_account_number, to_account_number, amount, (unsigned char*)comment.c_str());
	return "Das Geld wurde überwiesen";
}

string IMyBankFunctions::statement_c(long account_number, long detailed)
{
    unsigned char* statement_out;
    statement(token, account_number, detailed, &statement_out);
	return (reinterpret_cast<char*>(statement_out));
}

string IMyBankFunctions::bye_c()
{
    bye(token);
	return "Bis zum nächsten mal.\n";
}

void IMyBankFunctions::Bind(unsigned char* netwAddr, unsigned char* endpoint)
{
    RPC_STATUS status;
    unsigned char* protocolSequence = (UCHAR*)"ncacn_ip_tcp";

    unsigned char* stringBinding = NULL;


    // Erzeugung der Stringdarstellung des Binding-Handles 
    status = RpcStringBindingCompose(NULL,             // Objekt UUID
        protocolSequence, // Server-/Verbindungsdaten
        netwAddr,
        endpoint,
        NULL,             // Keine Optionen fuer TCP/IP
        &stringBinding);
    if (status)
    {
        throw(RpcException(status, (string)"RpcStringBindingCompose failed: Network Address = " +
            (char*)netwAddr,
            "RPC Runtime Error"));
    }

    // Erzeugung des Binding-Handles
    status = RpcBindingFromStringBinding(stringBinding,
        &hMyBank_i);
    if (status)
    {
        throw(RpcException(status, (string)"RpcBindingFromStringBinding failed: String = " +
            (char*)stringBinding,
            "RPC Runtime Error"));
    }

    // Freigabe der Stringdarstellung des Binding-Handles 
    status = RpcStringFree(&stringBinding);
    if (status)
    {
        throw(RpcException(status, "RpcStringFree failed", "RPC Error"));
    }
}