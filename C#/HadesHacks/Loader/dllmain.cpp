#include "pch.h"
#include <metahost.h>
#pragma comment(lib, "mscoree.lib")

BOOL APIENTRY DllMain(HMODULE /* hModule */, DWORD ul_reason_for_call, LPVOID /* lpReserved */)
{
    ICLRDebugging* pCLRDebugging = NULL;
    HRESULT hr;
    hr = CLRCreateInstance
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
