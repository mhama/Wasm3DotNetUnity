using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using Wasm3DotNet.Wrapper;
using System;
using AOT;

public class RunWasmSample : MonoBehaviour
{
    [SerializeField]
    Text outputText;

    [SerializeField]
    TextAsset wasmText;

    // Start is called before the first frame update
    void Start()
    {
        RunWasm();
    }

    void RunWasm()
    {
        using (var environment = new Wasm3DotNet.Wrapper.Environment())
        using (var runtime = new Runtime(environment))
        {
            /*
            string wasmPath = Application.streamingAssetsPath + "/wasm/test.wasm";
            byte[] wasmData = File.ReadAllBytes(wasmPath);
            */

            byte[] wasmData = wasmText.bytes;
            Log("wasmData: " + wasmData);
            Log("wasmData length: " + wasmData.Length);

            var module = runtime.ParseModule(wasmData);

            runtime.LoadModule(module);

            module.LinkRawFunction("externals", "print_add", "i(ii)", Output);

            var func = runtime.FindFunction("test");

            var ret = func.Call(10);
            Log($"Result: {ret}");
        }
    }

    void Log(string msg)
    {
        Debug.Log(msg);
        outputText.text += msg + "\n";
    }

    delegate IntPtr OutputFuncType(IntPtr runtime, IntPtr ctx, IntPtr sp, IntPtr mem);

    // for IL2CPP environment, MonoPInvokeCallback and static is needed.
    [MonoPInvokeCallback(typeof(OutputFuncType))]
    static IntPtr Output(IntPtr runtime, IntPtr ctx, IntPtr sp, IntPtr mem)
    {
        // Read values from WASM stack.
        // See: m3_api_defs.h in wasm3
        var x = Marshal.ReadInt32(sp, 8);
        var y = Marshal.ReadInt32(sp, 16);
        Debug.Log($"x={x}, y={y}");
        // Write result to WASM stack.
        Marshal.WriteInt32(sp, x + y);

        return IntPtr.Zero; // No error
    }
}
