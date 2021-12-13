

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0626 */
/* at Tue Jan 19 04:14:07 2038
 */
/* Compiler settings for MyBankCCOM.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.01.0626 
    protocol : all , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __MyBankCCOM_i_h__
#define __MyBankCCOM_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef DECLSPEC_XFGVIRT
#if _CONTROL_FLOW_GUARD_XFG
#define DECLSPEC_XFGVIRT(base, func) __declspec(xfg_virtual(base, func))
#else
#define DECLSPEC_XFGVIRT(base, func)
#endif
#endif

/* Forward Declarations */ 

#ifndef __IMyBankATL_FWD_DEFINED__
#define __IMyBankATL_FWD_DEFINED__
typedef interface IMyBankATL IMyBankATL;

#endif 	/* __IMyBankATL_FWD_DEFINED__ */


#ifndef __MyBankATL_FWD_DEFINED__
#define __MyBankATL_FWD_DEFINED__

#ifdef __cplusplus
typedef class MyBankATL MyBankATL;
#else
typedef struct MyBankATL MyBankATL;
#endif /* __cplusplus */

#endif 	/* __MyBankATL_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "shobjidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IMyBankATL_INTERFACE_DEFINED__
#define __IMyBankATL_INTERFACE_DEFINED__

/* interface IMyBankATL */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IMyBankATL;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("fed0c2f3-0b42-40bb-aae9-d1f510c6ac0d")
    IMyBankATL : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Login( 
            /* [in] */ BSTR username,
            /* [in] */ BSTR password,
            /* [retval][out] */ INT *token) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE NewUser( 
            /* [in] */ INT token,
            /* [in] */ BSTR username,
            /* [in] */ BSTR password) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE ListAccounts( 
            /* [in] */ INT token,
            /* [retval][out] */ BSTR *Accountdesc) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Logout( 
            /* [in] */ INT token) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE NewAccount( 
            /* [in] */ INT token,
            /* [in] */ BSTR username,
            /* [in] */ BSTR description,
            /* [retval][out] */ INT *accountNumber) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE PayInto( 
            /* [in] */ INT token,
            /* [in] */ INT accountNumber,
            /* [in] */ FLOAT amount) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Transfer( 
            /* [in] */ INT token,
            /* [in] */ INT fromAccountNumber,
            /* [in] */ INT toAccountNumber,
            /* [in] */ FLOAT amount,
            /* [in] */ BSTR comment) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Statement( 
            /* [in] */ INT token,
            /* [in] */ INT accountNumber,
            /* [in] */ BOOL detailed,
            /* [retval][out] */ BSTR *statement) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IMyBankATLVtbl
    {
        BEGIN_INTERFACE
        
        DECLSPEC_XFGVIRT(IUnknown, QueryInterface)
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IMyBankATL * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        DECLSPEC_XFGVIRT(IUnknown, AddRef)
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IMyBankATL * This);
        
        DECLSPEC_XFGVIRT(IUnknown, Release)
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IMyBankATL * This);
        
        DECLSPEC_XFGVIRT(IDispatch, GetTypeInfoCount)
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IMyBankATL * This,
            /* [out] */ UINT *pctinfo);
        
        DECLSPEC_XFGVIRT(IDispatch, GetTypeInfo)
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IMyBankATL * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        DECLSPEC_XFGVIRT(IDispatch, GetIDsOfNames)
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IMyBankATL * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        DECLSPEC_XFGVIRT(IDispatch, Invoke)
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IMyBankATL * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        DECLSPEC_XFGVIRT(IMyBankATL, Login)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Login )( 
            IMyBankATL * This,
            /* [in] */ BSTR username,
            /* [in] */ BSTR password,
            /* [retval][out] */ INT *token);
        
        DECLSPEC_XFGVIRT(IMyBankATL, NewUser)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *NewUser )( 
            IMyBankATL * This,
            /* [in] */ INT token,
            /* [in] */ BSTR username,
            /* [in] */ BSTR password);
        
        DECLSPEC_XFGVIRT(IMyBankATL, ListAccounts)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *ListAccounts )( 
            IMyBankATL * This,
            /* [in] */ INT token,
            /* [retval][out] */ BSTR *Accountdesc);
        
        DECLSPEC_XFGVIRT(IMyBankATL, Logout)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Logout )( 
            IMyBankATL * This,
            /* [in] */ INT token);
        
        DECLSPEC_XFGVIRT(IMyBankATL, NewAccount)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *NewAccount )( 
            IMyBankATL * This,
            /* [in] */ INT token,
            /* [in] */ BSTR username,
            /* [in] */ BSTR description,
            /* [retval][out] */ INT *accountNumber);
        
        DECLSPEC_XFGVIRT(IMyBankATL, PayInto)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *PayInto )( 
            IMyBankATL * This,
            /* [in] */ INT token,
            /* [in] */ INT accountNumber,
            /* [in] */ FLOAT amount);
        
        DECLSPEC_XFGVIRT(IMyBankATL, Transfer)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Transfer )( 
            IMyBankATL * This,
            /* [in] */ INT token,
            /* [in] */ INT fromAccountNumber,
            /* [in] */ INT toAccountNumber,
            /* [in] */ FLOAT amount,
            /* [in] */ BSTR comment);
        
        DECLSPEC_XFGVIRT(IMyBankATL, Statement)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Statement )( 
            IMyBankATL * This,
            /* [in] */ INT token,
            /* [in] */ INT accountNumber,
            /* [in] */ BOOL detailed,
            /* [retval][out] */ BSTR *statement);
        
        END_INTERFACE
    } IMyBankATLVtbl;

    interface IMyBankATL
    {
        CONST_VTBL struct IMyBankATLVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IMyBankATL_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IMyBankATL_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IMyBankATL_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IMyBankATL_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IMyBankATL_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IMyBankATL_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IMyBankATL_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IMyBankATL_Login(This,username,password,token)	\
    ( (This)->lpVtbl -> Login(This,username,password,token) ) 

#define IMyBankATL_NewUser(This,token,username,password)	\
    ( (This)->lpVtbl -> NewUser(This,token,username,password) ) 

#define IMyBankATL_ListAccounts(This,token,Accountdesc)	\
    ( (This)->lpVtbl -> ListAccounts(This,token,Accountdesc) ) 

#define IMyBankATL_Logout(This,token)	\
    ( (This)->lpVtbl -> Logout(This,token) ) 

#define IMyBankATL_NewAccount(This,token,username,description,accountNumber)	\
    ( (This)->lpVtbl -> NewAccount(This,token,username,description,accountNumber) ) 

#define IMyBankATL_PayInto(This,token,accountNumber,amount)	\
    ( (This)->lpVtbl -> PayInto(This,token,accountNumber,amount) ) 

#define IMyBankATL_Transfer(This,token,fromAccountNumber,toAccountNumber,amount,comment)	\
    ( (This)->lpVtbl -> Transfer(This,token,fromAccountNumber,toAccountNumber,amount,comment) ) 

#define IMyBankATL_Statement(This,token,accountNumber,detailed,statement)	\
    ( (This)->lpVtbl -> Statement(This,token,accountNumber,detailed,statement) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IMyBankATL_INTERFACE_DEFINED__ */



#ifndef __MyBankCCOMLib_LIBRARY_DEFINED__
#define __MyBankCCOMLib_LIBRARY_DEFINED__

/* library MyBankCCOMLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_MyBankCCOMLib;

EXTERN_C const CLSID CLSID_MyBankATL;

#ifdef __cplusplus

class DECLSPEC_UUID("e96db8ed-48d8-47ae-b9d6-9cdf447ccc57")
MyBankATL;
#endif
#endif /* __MyBankCCOMLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

unsigned long             __RPC_USER  BSTR_UserSize64(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal64(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal64(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree64(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


