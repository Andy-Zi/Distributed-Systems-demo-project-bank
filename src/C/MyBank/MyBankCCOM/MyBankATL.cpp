// MyBankATL.cpp : Implementation of CMyBankATL

#include "pch.h"
#include "MyBankATL.h"
#include <cassert>
#include <ComUtil.h>
// CMyBankATL

MyBankServiceConnector CMyBankATL::bank;

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


HRESULT HandleError(std::exception e,string* lastError) {
    *lastError = e.what();
    return E_INVALIDARG;
}

STDMETHODIMP CMyBankATL::Login(BSTR username, BSTR password, INT* token)
{
    try {
        auto new_token = (CMyBankATL::bank.login(BSTR2String(username), BSTR2String(password)));
        *token = new_token;
    }
    catch (const std::exception& e) {
        return HandleError(e, &(this->lastError));
    }
    return S_OK;
}


STDMETHODIMP CMyBankATL::NewUser(INT token, BSTR username, BSTR password)
{
    try {
        CMyBankATL::bank.newuser(token, BSTR2String(username), BSTR2String(password));
    }
    catch (const std::exception& e) {
        return HandleError(e, &(this->lastError));
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::ListAccounts(INT token, BSTR* Accountdesc) {
    try {
        auto accountdescs = this->bank.listaccounts(token);
        auto j = this->bank.SerializeAccountDescriptions(accountdescs);
        *Accountdesc = String2BSTR(j.dump());
    }catch (const std::exception& e) {
        return HandleError(e, &(this->lastError));
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::Logout(INT token) {
    try {
        this->bank.logout(token);
    }
    catch (const std::exception& e) {
        return HandleError(e, &(this->lastError));
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::NewAccount(INT token, BSTR username, BSTR description, INT* accountNumber) {
    try {
        *accountNumber = this->bank.newaccount(token, BSTR2String(username), BSTR2String(description));
    }
    catch (const std::exception& e) {
        return HandleError(e,&(this->lastError));
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::PayInto(INT token, INT accountNumber, FLOAT amount) {
    try {
        this->bank.payinto(token, accountNumber, amount);
    }
    catch (const std::exception& e) {
        return HandleError(e, &(this->lastError));
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::Transfer(INT token, INT fromAccountNumber, INT toAccountNumber, FLOAT amount, BSTR comment) {
    try {
        this->bank.transfer(token, fromAccountNumber, toAccountNumber, amount, BSTR2String(comment));
    }
    catch (const std::exception& e) {
        return HandleError(e, &(this->lastError));
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::Statement(INT token, INT accountNumber, BOOL detailed, BSTR* statement) {
    try {
        auto statements = this->bank.statement(token, accountNumber, detailed == 1);
        json j = this->bank.SerializeStatements(statements);
        *statement = String2BSTR(j.dump());
    }
    catch (const std::exception& e) {
        return HandleError(e, &(this->lastError));
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::GetError(BSTR* error) {
    *error = String2BSTR(this->lastError);
    return S_OK;
}


