#include <atlcomcli.h>
#include <objbase.h>
#include "../MyBankCCOM/MyBankCCOM_i.h"
#include <cassert>
#include <ComUtil.h>
#include <iostream>
#include "ClientHelperFunctions.cpp"
#import  "..\x64\Debug\MyBankCCOM.dll"
#include <string>
#include <ComUtil.h>
#include "IMyBankClientFunctions.h"
#include "JsonUtilityFunctions.cpp"

using namespace std;
class MyBankClientFunctions : public IMyBankClientFunctions
{
public:
    string BSTR2String(BSTR bs);
    BSTR String2BSTR(string s);
    bool HandleResult(HRESULT result);
    void Bind(unsigned char* netwAddr, unsigned char* endpoint);
    string connect_c(string netadr, string endpoint, bool& connected);
    string login_c(string username, string password, bool& connected);
    string newaccount_c(string username, string description);
    string newuser_c(string username, string password);
    string listaccounts_c();
    string transfer_c(long from_account_number, long to_account_number, float amount, string comment);
    string payinto_c(long account_number, float amount);
    string statement_c(long account_number, long detailed);
    string bye_c();
private:
    long token;
    CComPtr<IMyBankATL> comInterfacePtr;
};

string MyBankClientFunctions::BSTR2String(BSTR bs) {
    assert(bs != nullptr);
    wstring  ws(bs, SysStringLen(bs));
    string str(ws.begin(), ws.end());
    return str;
}

BSTR MyBankClientFunctions::String2BSTR(string s) {

    const char* cstr = s.c_str();
    return _bstr_t((char*)cstr).copy();
}

bool MyBankClientFunctions::HandleResult(HRESULT result) {
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

string MyBankClientFunctions::connect_c(string netwAddr, string endpoint, bool& connected)
{
    connected = false;
	Bind((unsigned char*)netwAddr.c_str(), (unsigned char*)endpoint.c_str());
    if (this->isConnected)
    {
        connected = true;
        return "Bank verbunden\n";
    }
    else
        return "Konnte keine Verbindung zur Bank herstellen!\n";
}

string MyBankClientFunctions::login_c(string username, string password, bool& logged_in)
{
    logged_in = false;
    auto result = comInterfacePtr->Login(String2BSTR(username) , String2BSTR(password), &token);
    if (HandleResult(result)) {
        this->isLoggedIn = true;
        logged_in = true;
        return "Hallo " + username + "\n";
    }
    return "Login Failed!\n";
}

string MyBankClientFunctions::newaccount_c(string username, string description)
{
    long accountnumber;
    auto result = comInterfacePtr->NewAccount(token, String2BSTR(username) , String2BSTR(description), &accountnumber);
    if (HandleResult(result)) {
        return "Der Account für " + username + " wurde angelegt";
    }
    return "";
}

string MyBankClientFunctions::newuser_c(string username, string password)
{
    auto result = comInterfacePtr->NewUser(token, String2BSTR(username), String2BSTR(password));
    if (HandleResult(result)) {
        return "Der Nutzer " + username + " wurde angelegt";
    }
    return "";
}


string MyBankClientFunctions::transfer_c(long from_account_number, long to_account_number, float amount, string comment)
{
    auto result = comInterfacePtr->Transfer(token, from_account_number,to_account_number,amount, String2BSTR(comment));
    if (HandleResult(result)) {
        return "Das Geld wurde überwiesen";
    }
    return "";
}

string MyBankClientFunctions::payinto_c(long account_number, float amount)
{
    return "Not Implemented! :(";
}

string MyBankClientFunctions::listaccounts_c()
{
    BSTR jsonString;
    auto result = comInterfacePtr->ListAccounts(token,&jsonString);
    if (HandleResult(result)) {
        auto accounts = DeserializeAccountDescriptions(BSTR2String(jsonString));
        PrettyPrlongAccountDescriptions(accounts);
    }
    return "";
}

string MyBankClientFunctions::statement_c(long account_number, long detailed)
{
    BSTR jsonString;
    auto result = comInterfacePtr->Statement(token, account_number, detailed, &jsonString);
    if (HandleResult(result)) {
        auto statements = DeserializeStatements(BSTR2String(jsonString));
        PrettyPrlongStatements(statements);
    }
    return "";
}

string MyBankClientFunctions::bye_c()
{
    auto result = comInterfacePtr->Logout(token);
    HandleResult(result);
    comInterfacePtr.Release();
    CoUninitialize();
	return "Bis zum nächsten mal.\n";
}

void MyBankClientFunctions::Bind(unsigned char* netwAddr, unsigned char* endpoint)
{

    string netwAddress = (string)reinterpret_cast<char*>(netwAddr) + ":" + (string)reinterpret_cast<char*>(endpoint);
    BSTR b = _com_util::ConvertStringToBSTR(netwAddress.c_str());
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