#include "IMyBankFunctions.h"
#include <atlcomcli.h>
#include <objbase.h>
#include "../MyBankCCOM/MyBankCCOM_i.h"
#include <cassert>
#include <ComUtil.h>
#include <iostream>
#include "ClientHelperFunctions.cpp"
#import  "..\x64\Debug\MyBankCCOM.dll"


CComPtr<IMyBankATL> comInterfacePtr;

std::string BSTR2String(BSTR bs) {
    assert(bs != nullptr);
    std::wstring  ws(bs, SysStringLen(bs));
    std::string str(ws.begin(), ws.end());
    return str;
}

BSTR String2BSTR(string s) {

    const char* cstr = s.c_str();
    return _bstr_t((char*)cstr).copy();
}

bool IMyBankFunctions::HandleResult(HRESULT result) {
    if (result != S_OK) {

        if (this->isConnected) {
            BSTR errormessage;
            comInterfacePtr->GetError(&errormessage);
            cout << "Error: " << BSTR2String(errormessage) << "\n";
        }else{
            _com_error err(result);
            cout << "CCOM Error: " << err.ErrorMessage() << "\n";
        }
        return false;
    }
    return true;
}



string IMyBankFunctions::connect_c(string netwAddr)
{

	Bind(netwAddr);
    if (this->isConnected)
        return "Bank verbunden\n";
    else
        return "Konnte keine Verbindung zur Bank herstellen!\n";
}

string IMyBankFunctions::login_c(string username, string password)
{
    auto result = comInterfacePtr->Login(String2BSTR(username) , String2BSTR(password), &token);
    if (HandleResult(result)) {
        this->isLoggedIn = true;
        return "Hallo " + username + "\n";
    }
    return "Login Failed!\n";
}

string IMyBankFunctions::newaccount_c(string username, string description)
{
    int accountnumber;
    auto result = comInterfacePtr->NewAccount(token, String2BSTR(username) , String2BSTR(description), &accountnumber);
    if (HandleResult(result)) {
        return "Der Account für " + username + " wurde angelegt";
    }
    return "";
}

string IMyBankFunctions::newuser_c(string username, string password)
{
    auto result = comInterfacePtr->NewUser(token, String2BSTR(username), String2BSTR(password));
    if (HandleResult(result)) {
        return "Der Nutzer " + username + " wurde angelegt";
    }
    return "";
}


string IMyBankFunctions::transfer_c(int from_account_number, int to_account_number, float amount, string comment)
{
    auto result = comInterfacePtr->Transfer(token, from_account_number,to_account_number,amount, String2BSTR(comment));
    if (HandleResult(result)) {
        return "Das Geld wurde überwiesen";
    }
    return "";
}

string IMyBankFunctions::listaccounts_c()
{
    BSTR jsonString;
    auto result = comInterfacePtr->ListAccounts(token,&jsonString);
    if (HandleResult(result)) {
        auto accounts = DeserializeAccountDescriptions(BSTR2String(jsonString));
        PrettyPrintAccountDescriptions(accounts);
    }
    return "";
}

string IMyBankFunctions::statement_c(int account_number, bool detailed)
{
    BSTR jsonString;
    auto result = comInterfacePtr->Statement(token, account_number, detailed, &jsonString);
    if (HandleResult(result)) {
        auto statements = DeserializeStatements(BSTR2String(jsonString));
        PrettyPrintStatements(statements);
    }
    return "";
}

string IMyBankFunctions::bye_c()
{
    auto result = comInterfacePtr->Logout(token);
    HandleResult(result);
    comInterfacePtr.Release();
    CoUninitialize();
	return "Bis zum nächsten mal.\n";
}

void IMyBankFunctions::Bind(string netwAddr)
{
    BSTR b = _com_util::ConvertStringToBSTR(netwAddr.c_str());
    LPWSTR lp = b;

    COSERVERINFO srvInfo = { 0, lp , 0, 0 };
    MULTI_QI multiQi = { &__uuidof(IMyBankATL), 0, 0 };

    auto initializResult = CoInitialize(NULL);
    if (HandleResult(initializResult)) {
        //auto createResult = comInterfacePtr.CoCreateInstance(__uuidof(MyBankATL));
        auto createResult = CoCreateInstanceEx( __uuidof(MyBankATL), NULL, CLSCTX_REMOTE_SERVER, &srvInfo, 1, &multiQi);

        if (HandleResult(createResult)) {
            comInterfacePtr = (IMyBankATL*)multiQi.pItf;
            this->isConnected = true;
        }
    }
    SysFreeString(b);
}