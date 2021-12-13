// MyBankATL.h : Declaration of the CMyBankATL

#pragma once
#include "resource.h"       // main symbols



#include "MyBankCCOM_i.h"
#include "atlstr.h"
#include "../MyBankAcc/MyBankServiceConnector.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CMyBankATL

class ATL_NO_VTABLE CMyBankATL :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CMyBankATL, &CLSID_MyBankATL>,
	public IDispatchImpl<IMyBankATL, &IID_IMyBankATL, &LIBID_MyBankCCOMLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	MyBankServiceConnector bank;
	CMyBankATL()
	{
	}

DECLARE_REGISTRY_RESOURCEID(106)


BEGIN_COM_MAP(CMyBankATL)
	COM_INTERFACE_ENTRY(IMyBankATL)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:

	STDMETHOD(Login)(BSTR username, BSTR password, INT* token);
	STDMETHOD(NewUser)(INT token, BSTR username, BSTR password);
	STDMETHOD(ListAccounts)(INT token, BSTR* accountdesc);
	STDMETHOD(Logout)(INT token);
	STDMETHOD(NewAccount)(INT token, BSTR username, BSTR description, INT* accountNumber);
	STDMETHOD(PayInto)(INT token, INT accountNumber, FLOAT amount);
	STDMETHOD(Transfer)(INT token, INT fromAccountNumber, INT toAccountNumber, FLOAT amount, BSTR comment);
	STDMETHOD(Statement)(INT token, INT accountNumber, BOOL detailed, BSTR* statement);
};

OBJECT_ENTRY_AUTO(__uuidof(MyBankATL), CMyBankATL)

