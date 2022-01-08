/************************************************************/
/*                                                          */
/* Inhalt:    RPC-Stub-Speicherverwaltung                   */
/*                                                          */
/* Autor(en): Josef Pösl (jp), <XXX>                        */
/* Firma:     Fachhochschule Amberg-Weiden                  */
/* Stand:     18. Sep 2002                                  */
/*                                                          */
/* Historie:  18. Sep 2002 jp  erstellt                     */
/*            xx. xxx xxxx     modifiziert...               */
/*                                                          */
/* Copyright 2001-2050 FH Amberg-Weiden ... usw.            */
/*                                                          */
/************************************************************/

#include "rpc.h"    // Durch MIDL-Compiler erzeugte Header-Datei
#include <windows.h>

/*********************************************************************/
/*                 MIDL allocate and free                            */
/*********************************************************************/

void __RPC_FAR* __RPC_API MIDL_user_allocate(size_t cBytes)
{
    return(malloc(cBytes));
}

void __RPC_USER MIDL_user_free(void __RPC_FAR* p)
{
    free(p);
}
