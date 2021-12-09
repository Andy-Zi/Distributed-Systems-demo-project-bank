// MyBankATL.cpp : Implementation of CMyBankATL

#include "pch.h"
#include "MyBankATL.h"
#include <cassert>
#include <ComUtil.h>
// CMyBankATL

std::string BSTR2String(BSTR bs) {
    assert(bs != nullptr);
    std::wstring  ws(bs, SysStringLen(bs));
    std::string str(ws.begin(), ws.end());
    return str;
}

BSTR String2BSTR(string s) {
    std:wstring ws(s.begin(), s.end());
    return SysAllocStringLen(ws.data(), ws.size());
}

STDMETHODIMP CMyBankATL::Login(BSTR username, BSTR password, LONG* token)
{
    try {
        auto new_token = (long)(this->bank.login(BSTR2String(username), BSTR2String(password)));
        token = &new_token;
    }
    catch (const std::exception& e) {
        token = 0;
        return E_ACCESSDENIED;
    }
    return S_OK;
}


STDMETHODIMP CMyBankATL::NewUser(LONG token, BSTR username, BSTR password)
{
    try {
        this->bank.newuser(token, BSTR2String(username), BSTR2String(password));
    }
    catch (const std::exception& e) {
        return E_ACCESSDENIED;
    }
    return S_OK;
}

STDMETHODIMP CMyBankATL::ListAccounts(LONG token, BSTR* Accountdesc) {
   /* try {

        auto accountdescs = this->bank.listaccounts(token);
        ATLAccountdesc* atlAccountDescs = new ATLAccountdesc[accountdescs.size()];
        int i = 0;
        for (auto it = accountdescs.begin(); it != accountdescs.end(); it++) {
            ATLAccountdesc atlAccountDesc;
            atlAccountDesc.Account_Number = (*it).Account_Number;
            atlAccountDesc.Description = String2BSTR((*it).Description);
            atlAccountDescs[i] = atlAccountDesc;
            i++;
        }

        Accountdesc = &atlAccountDescs;

    }catch (const std::exception& e) {
        return E_ACCESSDENIED;
    }*/
    return S_OK;
}

STDMETHODIMP CMyBankATL::Test(LONG token)
{
    return S_OK;
}

