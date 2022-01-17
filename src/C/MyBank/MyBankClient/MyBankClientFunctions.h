#pragma once
#include "IMyBankClientFunctions.h"
#include "RpcException.h"
#include "MyBank_i.h"
#include <MyBankAcc/ClientHelperFunctions.h>

class MyBankClientFunctions : public IMyBankClientFunctions
{
public:
    string connect_c(string netwAddr, string endpoint, long& connected)
    {
        try
        {
            /* RPC binden */
            Bind((unsigned char*)netwAddr.c_str(), (unsigned char*)endpoint.c_str());
            error_status_t stat = is_connected(&connected);
            handle_rpc_result(stat);
            return "Bank connected\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                return "Can't connect to given server!\n";
            else
                return e.what();
        }
    }

    string login_c(string username, string password, bool& logged_in)
    {
        try
        {
            logged_in = false;
            unsigned char* result = NULL;
            error_status_t stat = login((unsigned char*)username.c_str(), (unsigned char*)password.c_str(), &token, &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                logged_in = true;
                midl_user_free(result);
                return "Hello " + username + "\n";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
        }
    }

    string newaccount_c(string username, string description)
    {
        try
        {
            unsigned char* result = NULL;
            long accountnumber;
            error_status_t stat = newaccount(token, (unsigned char*)username.c_str(), (unsigned char*)description.c_str(), &accountnumber, &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                midl_user_free(result);
                return "A account for " + username + " has been created\n";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
        }
    }

    string newuser_c(string username, string password)
    {
        try
        {
            unsigned char* result = NULL;
            error_status_t stat = newuser(token, (unsigned char*)username.c_str(), (unsigned char*)password.c_str(), &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                midl_user_free(result);
                return "User: " + username + " has been created\n";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
        }
    }

    string listaccounts_c()
    {
        try
        {
            unsigned char* result = NULL;
            unsigned char* listacc = NULL;
            error_status_t stat = listaccounts(token, &listacc, &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                midl_user_free(result);
                list<Accountdesc> accounts = DeserializeAccountDescriptions((string)(reinterpret_cast<char*>(listacc)));
                PrettyPrintAccountDescriptions(accounts);
                midl_user_free(listacc);
                return "";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
        }
    }

    string transfer_c(long from_account_number, long to_account_number, string receiver_name, float amount, string comment)
    {
        try
        {
            unsigned char* result = NULL;
            error_status_t stat = transfer(token, from_account_number, to_account_number, (unsigned char*)receiver_name.c_str(), amount, (unsigned char*)comment.c_str(), &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                midl_user_free(result);
                return "Money has been transfered\n";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
        }
    }

    string payinto_c(long account_number, float amount)
    {
        try
        {
            unsigned char* result = NULL;
            error_status_t stat = payinto(token, account_number, amount, &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                midl_user_free(result);
                return "Money has been deposited\n";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
        }
    }

    string statement_c(long account_number, long detailed)
    {
        try
        {
            unsigned char* result = NULL;
            unsigned char* statement_out = NULL;
            error_status_t stat = statement(token, account_number, detailed, &statement_out, &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                midl_user_free(result);
                list<Statement> statements = DeserializeStatements((string)(reinterpret_cast<char*>(statement_out)));
                PrettyPrintStatements(statements, detailed);
                midl_user_free(statement_out);
                return "";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
        }
    }

    string bye_c()
    {
        try
        {
            unsigned char* result = NULL;
            error_status_t stat = bye(token, &result);
            handle_rpc_result(stat);
            if (!memcmp(result, (unsigned char*)"0", 1))
            {
                midl_user_free(result);
                return "Until next time.\n";
            }
            string out = (string)reinterpret_cast<char*>(result);
            midl_user_free(result);
            return out + "\n";
        }
        catch (std::exception& e)
        {
            string error = string(e.what());
            if (!error.compare(Server_error_code))
                throw;
            else
                return e.what();
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

    void Bind(unsigned char* netwAddr, unsigned char* endpoint)
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
            throw(RpcException(status, (string)"RpcStringBindingCompose failed: Network Addresults = " +
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
    private:
        const char* Server_error_code = "-1";
        void handle_rpc_result(error_status_t stat)
        {
            if (stat != 0)
            {
                if (stat == RPC_S_SERVER_UNAVAILABLE) throw std::invalid_argument(Server_error_code);
                else throw std::invalid_argument(Server_error_code);
            }
        }
};