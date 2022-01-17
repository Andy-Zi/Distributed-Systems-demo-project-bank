#pragma once
#include "IMyBankClientFunctions.h"
#include <atlcomcli.h>
#include <objbase.h>
#include "../MyBankCCOM/MyBankCCOM_i.h"
#include <cassert>
#include <ComUtil.h>
#include <iostream>
#import  "..\x64\Debug\MyBankCCOM.dll"
#include <string>
#include <ComUtil.h>
#include <ClientHelperFunctions.h>


class MyBankClientFunctions : public IMyBankClientFunctions
{
public:
    string connect_c(string netwAddr, string endpoint, long& connected)
    {
        connected = false;
        try {
            Bind((unsigned char*)netwAddr.c_str(), (unsigned char*)endpoint.c_str());
        }
        catch (...) {
            CoUninitialize();
            return "Konnte keine Verbindung zur Bank herstellen!\n";
        }
        if (this->isConnected)
        {
            connected = true;
            return "Bank connected\n";
        }
        else
            return "Konnte keine Verbindung zur Bank herstellen!\n";
    }

    string login_c(string username, string password, bool& logged_in)
    {
        logged_in = false;
        int longToken;
        auto result = comInterfacePtr->Login(String2BSTR(username), String2BSTR(password), &longToken);
        token = longToken;
        if (HandleResult(result)) {
            this->isLoggedIn = true;
            logged_in = true;
            return "Hello " + username + "\n";
        }
        return "Login Failed!\n";
    }

    string newaccount_c(string username, string description)
    {
        int accountnumber;
        auto result = comInterfacePtr->NewAccount(token, String2BSTR(username), String2BSTR(description), &accountnumber);
        if (HandleResult(result)) {
            return "A account for " + username + " has been created\n";
        }
        return "";
    }

    string newuser_c(string username, string password)
    {
        auto result = comInterfacePtr->NewUser(token, String2BSTR(username), String2BSTR(password));
        if (HandleResult(result)) {
            return "User: " + username + " has been created\n";
        }
        return "";
    }

    string listaccounts_c()
    {
        BSTR jsonString;
        auto result = comInterfacePtr->ListAccounts(token, &jsonString);
        if (HandleResult(result)) {
            auto accounts = DeserializeAccountDescriptions(BSTR2String(jsonString));
            PrettyPrintAccountDescriptions(accounts);
        }
        return "";
    }

    string transfer_c(long from_account_number, long to_account_number, string receiver_name, float amount, string comment)
    {
        auto result = comInterfacePtr->Transfer(token, from_account_number, to_account_number, String2BSTR(receiver_name) ,amount, String2BSTR(comment));
        if (HandleResult(result)) {
            return "Money has been transfered\n";
        }
        return "";
    }

    string payinto_c(long account_number, float amount)
    {
        auto result = comInterfacePtr->PayInto(token, account_number, amount);
        if (HandleResult(result)) {
            return "Money has been deposited\n";
        }
        return "";
    }

    string statement_c(long account_number, long detailed)
    {
        BSTR jsonString;
        auto result = comInterfacePtr->Statement(token, account_number, detailed, &jsonString);
        if (HandleResult(result)) {
            auto statements = DeserializeStatements(BSTR2String(jsonString));
            PrettyPrintStatements(statements, detailed);
        }
        return "";
    }

    string bye_c()
    {
        auto result = comInterfacePtr->Logout(token);
        HandleResult(result);
        return "Until next time.\n";
    }

    void UnBind() {
        comInterfacePtr.Release();
        CoUninitialize();
    }

    void Bind(unsigned char* netwAddr, unsigned char* endpoint)
    {
        string netwAddress = (string)reinterpret_cast<char*>(netwAddr);
        BSTR b = _com_util::ConvertStringToBSTR(netwAddress.c_str());
        LPWSTR lp = b;

        COSERVERINFO srvInfo = { 0, lp , 0, 0 };
        MULTI_QI multiQi = { &__uuidof(IMyBankATL), 0, 0 };

        auto initializResult = CoInitialize(NULL);
        if (HandleResult(initializResult)) {
            //auto createResult = comInterfacePtr.CoCreateInstance(__uuidof(MyBankATL));
            auto createResult = CoCreateInstanceEx(__uuidof(MyBankATL), NULL, CLSCTX_REMOTE_SERVER, &srvInfo, 1, &multiQi);

            if (HandleResult(createResult)) {
                comInterfacePtr = (IMyBankATL*)multiQi.pItf;
                this->isConnected = true;
            }
        }
        SysFreeString(b);
    }
private:
    CComPtr<IMyBankATL> comInterfacePtr;

    string BSTR2String(BSTR bs) {
        assert(bs != nullptr);
        wstring  ws(bs, SysStringLen(bs));
        string str(ws.begin(), ws.end());
        return str;
    }

    BSTR String2BSTR(string s) {

        const char* cstr = s.c_str();
        return _bstr_t((char*)cstr).copy();
    }

    bool HandleResult(HRESULT result) {
        if (result != S_OK) {
            if (result == 0x800706ba) {
                throw std::exception("Can't reach Server");
            }
            if (this->isConnected) {
                BSTR errormessage;
                comInterfacePtr->GetError(&errormessage);
                cout << "Error: " << BSTR2String(errormessage) << "\n";
            }
            return false;
        }
        return true;
    }
};