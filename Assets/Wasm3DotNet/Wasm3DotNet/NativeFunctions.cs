using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    public class NativeFunctions
    {
// fix for unity
#if UNITY_IOS && !UNITY_EDITOR
        public const string LIBNAME = "__Internal";
#else
        public const string LIBNAME = "wasm3";
#endif
        // CallingConvention.Cdecl is needed to avoid "Stack Imbalance" error
        // See: https://blog.janjan.net/2017/08/08/csharp-pinvoke-stackimbalance/

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IM3Environment m3_NewEnvironment();

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_FreeEnvironment(IM3Environment environment);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void m3_FreeEnvironment(IntPtr environment);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IM3Runtime m3_NewRuntime(IM3Environment enviroment, uint stackSizeInBytes, IntPtr unused);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_FreeRuntime(IM3Runtime runtime);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void m3_FreeRuntime(IntPtr runtime);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr m3_GetMemory(IM3Runtime runtime, out uint memorySizeInBytes, uint memoryIndex);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_ParseModule(IM3Environment environment, out IM3Module module, byte[] wasmBytes, uint numWasmBytes);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_FreeModule(IM3Module module);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_LoadModule(IM3Runtime runtime, IM3Module module);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_LinkRawFunction(IM3Module module, string moduleName, string functionName, string signature, M3RawCall function);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_Yield();

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_FindFunction(out IM3Function function, IM3Runtime runtime, string functionName);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint m3_GetArgCount(IM3Function function);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint m3_GetRetCount(IM3Function function);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern M3ValueType m3_GetArgType(IM3Function function, uint index);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern M3ValueType m3_GetRetType(IM3Function function, uint index);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_Call(IM3Function function, uint argc, IntPtr[] argPtrs);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_CallArgv(IM3Function function, uint argc, string[] argv);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_GetResults(IM3Function function, uint retc, IntPtr[] retPtrs);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_GetErrorInfo(IM3Runtime runtime, out M3ErrorInfo info);

        [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_ResetErrorInfo(IM3Runtime runtime);
    }
}
