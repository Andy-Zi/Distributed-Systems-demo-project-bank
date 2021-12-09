// dllmain.h : Declaration of module class.

class CMyBankCCOMModule : public ATL::CAtlDllModuleT< CMyBankCCOMModule >
{
public :
	DECLARE_LIBID(LIBID_MyBankCCOMLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_MYBANKCCOM, "{3feedad1-8c16-4a85-9440-b24d37ddfa2f}")
};

extern class CMyBankCCOMModule _AtlModule;
