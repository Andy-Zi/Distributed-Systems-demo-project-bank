#include "MyBank_i.h"
#include "MyBankServiceConnector.h"
#include <string>
#include <malloc.h>


using namespace std;


MyBankServiceConnector bank;

error_status_t is_connected(long* connected)
{
    *connected = long(1);
    return RPC_S_OK;
}

error_status_t login(unsigned char* username, unsigned char* password, long* token, unsigned char** res)
{
    try {
        auto new_token = bank.login((string) reinterpret_cast<char*>(username), (string) reinterpret_cast<char*>(password));
        *token = new_token;
        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()),strResLen);
    }
    return RPC_S_OK;
}

error_status_t newaccount(long token,unsigned char* username,unsigned char* description, long* account_number, unsigned char** res)
{
    try {
        *account_number = bank.newaccount(token, (string) reinterpret_cast<char*>(username), (string) reinterpret_cast<char*>(description));
        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()), strResLen);
    }
    return RPC_S_OK;
}

error_status_t newuser(long token, unsigned char* username, unsigned char* password, unsigned char** res)
{
    try {
        bank.newuser(token, (string) reinterpret_cast<char*>(username), (string) reinterpret_cast<char*>(password));
        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()), strResLen);
    }
    return RPC_S_OK;
}

error_status_t listaccounts(long token, unsigned char** descriptions, unsigned char** res)
{
    try {
        auto accountdescs = bank.listaccounts(token);
        auto j = bank.SerializeAccountDescriptions(accountdescs);
        int strdescLen = strlen(j.dump().c_str()) + 1;
        *descriptions = (unsigned char*)MIDL_user_allocate(strdescLen);
        memcpy(*descriptions, (unsigned char*)(j.dump().c_str()), strdescLen);

        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()), strResLen);
    }
    return RPC_S_OK;
}

error_status_t payinto(long token,long account_number,float amount, unsigned char** res)
{
    try {
        bank.payinto(token, account_number, amount);
        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()), strResLen);
    }
    return RPC_S_OK;
}

error_status_t transfer(long token, long from_account_number, long to_account_number, float amount, unsigned char* comment, unsigned char** res)
{
    try {
        bank.transfer(token, from_account_number, to_account_number, amount, (string) reinterpret_cast<char*>(comment));
        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()), strResLen);
    }
    return RPC_S_OK;
}

error_status_t statement(long token, long account_number, long detailed, unsigned char** statement_out, unsigned char** res)
{
    try {
        auto statements = bank.statement(token, account_number, detailed == 1);
        json j = bank.SerializeStatements(statements);
        int strstatLen = strlen(j.dump().c_str()) + 1;
        *statement_out = (unsigned char*)MIDL_user_allocate(strstatLen);
        memcpy(*statement_out, (unsigned char*)(j.dump().c_str()), strstatLen);

        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()), strResLen);
    }
    return RPC_S_OK;
}

error_status_t bye(long token, unsigned char** res)
{
    try {
        bank.logout(token);
        int strResLen = strlen("0") + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)("0"), strResLen);
    }
    catch (const std::exception& e) {
        int strResLen = strlen((e.what())) + 1;
        *res = (unsigned char*)MIDL_user_allocate(strResLen);
        memcpy(*res, (unsigned char*)(e.what()), strResLen);
    }
    return RPC_S_OK;
}