/************************************************************/
/*                                                          */
/* Inhalt:    RPC-Ausnahmeklasse / RPC-Ausnahmebehandlung   */
/*                                                          */
/* Autor(en): Josef P�sl (jp), <XXX>                        */
/* Firma:     Fachhochschule Amberg-Weiden                  */
/* Stand:     18. Sep 2002                                  */
/*                                                          */
/* Historie:  18. Sep 2002 jp  erstellt                     */
/*            xx. xxx xxxx     modifiziert...               */
/*                                                          */
/* Copyright 2001-2050 FH Amberg-Weiden ... usw.            */
/*                                                          */
/************************************************************/

#ifndef RPC_EXCEPTION_H
#define RPC_EXCEPTION_H

#include <rpc.h>

#define RPC_STATUS_ASSERT(fct, stat) {                      \
   if (status)                                              \
     {                                                      \
      fprintf(stderr, "Error calling "fct": 0x%x\n", stat); \
      exit(status);                                         \
     }                                                      \
  }
  

#ifdef __cplusplus

#include <string>

using std::string;


class RpcException
  {
 public:
   RpcException(RPC_STATUS status, const string& errorText, const string& errorType)
    : status(status),errorText(errorText),errorType(errorType){}

   RPC_STATUS  GetStatus(void)    { return(status); }
   const char *GetErrorText(void) { return(errorText.data());}
   const char *GetErrorType(void) { return(errorType.data());}

   static void Raise(RPC_STATUS status, const char *errText, const char *errType)
     {
      throw(RpcException(status,errText,errType));
     }
   
 private:
   RPC_STATUS status;
   string errorText;
   string errorType;
  };

#endif  // __cplusplus

#endif  // RPC_EXCEPTION_H
