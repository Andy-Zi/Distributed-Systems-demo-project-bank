#include "MyBank_i.h"

void __RPC_FAR* __RPC_API midl_user_allocate(size_t len)
{
	return(malloc(len));
}

void __RPC_API midl_user_free(void __RPC_FAR* ptr)
{
	free(ptr);
}