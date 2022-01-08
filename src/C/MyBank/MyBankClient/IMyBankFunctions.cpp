#include "IMyBankFunctions.h"

string IMyBankFunctions::connect_c(string netwAddr, string endpoint,bool& connected)
{
    try
    {
        /* RPC binden */
        Bind((unsigned char*)netwAddr.c_str(), (unsigned char*)endpoint.c_str());
        connected = is_connected();
    }
    catch (std::exception& e)
    {
        auto err = e.what();
        return "Fehler bei der Bankverbindung.\n";
    }
    return "Bank verbunden\n";
}

string IMyBankFunctions::login_c(string username, string password,bool& logged_in)
{
    logged_in = false;
    unsigned char* res = NULL;
    login((unsigned char*)username.c_str(), (unsigned char*)password.c_str(), &token, &res);
    if (!memcmp(res, (unsigned char*)"0", 1))
    {
        logged_in = true;
        return "Hallo " + username + "\n";
    }
    return (string)reinterpret_cast<char*>(res) + "\n";
}

string IMyBankFunctions::newaccount_c(string username, string description)
{
    unsigned char* res = NULL;
    long accountnumber;
    newaccount(token, (unsigned char*)username.c_str(), (unsigned char*)username.c_str(), &accountnumber, &res);
    if (!memcmp(res, (unsigned char*)"0", 1))
    {
        return "Der Account für " + username + " wurde angelegt\n";
    }
    return (string)reinterpret_cast<char*>(res) + "\n";
}

string IMyBankFunctions::newuser_c(string username, string password)
{
    unsigned char* res = NULL;
    newuser(token, (unsigned char*)username.c_str(), (unsigned char*)password.c_str(), &res);
    if (!memcmp(res, (unsigned char*)"0", 1))
    {
        return "Der Nutzer " + username + " wurde angelegt\n";
    }
    return (string)reinterpret_cast<char*>(res) + "\n";
}

string IMyBankFunctions::listaccounts_c()
{
    unsigned char* res = NULL;
    unsigned char* listacc = NULL;
    listaccounts(token,&listacc, &res);
    if (!memcmp(res, (unsigned char*)"0", 1))
    {
        return (string)(reinterpret_cast<char*>(listacc)) + "\n";
    }
    return (string)reinterpret_cast<char*>(res) + "\n";
}

string IMyBankFunctions::transfer_c(long from_account_number,long to_account_number, float amount, string comment)
{
    unsigned char* res = NULL;
    transfer(token, from_account_number, to_account_number, amount, (unsigned char*)comment.c_str(), &res);
    if (!memcmp(res, (unsigned char*)"0", 1))
    {
        return "Das Geld wurde überwiesen\n";
    }
    return (string)reinterpret_cast<char*>(res) + "\n";
}

string IMyBankFunctions::statement_c(long account_number, long detailed)
{
    unsigned char* res = NULL;
    unsigned char* statement_out = NULL;
    statement(token, account_number, detailed, &statement_out, &res);
    if (!memcmp(res, (unsigned char*)"0", 1))
    {
        return (string)(reinterpret_cast<char*>(statement_out)) + "\n";
    }
    return (string)reinterpret_cast<char*>(res) + "\n";
}

string IMyBankFunctions::bye_c()
{
    unsigned char* res = NULL;
    bye(token, &res);
    if (!memcmp(res, (unsigned char*)"0", 1))
    {
        return "Bis zum nächsten mal.\n";
    }
    return (string)reinterpret_cast<char*>(res) + "\n";
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