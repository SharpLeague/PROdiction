using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using LeagueSharp;
using LeagueSharp.Common;
using PROdiction.Properties;
using SharpDX;

namespace PROdiction
{
    public class Model
    {
        const string DLL_PATH = "libkeras2cpp.dll";

        [DllImport(@DLL_PATH, SetLastError=true, CharSet = CharSet.Ansi)]
        private static extern IntPtr load_model_bytes(IntPtr byteArray, UIntPtr size);


        [DllImport(@DLL_PATH, SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr load_model([MarshalAs(UnmanagedType.LPStr)] string fileName);


        [DllImport(@DLL_PATH, SetLastError=true)]
        private static extern IntPtr predict(IntPtr model, float[] data, UIntPtr size);
        
        private IntPtr _model;

        public static Model Load(string fileName)
        {
            object obj = Resources.ResourceManager.GetObject(fileName, Resources.Culture);
            
            var bytes = (byte[]) obj;
            
            IntPtr unmanagedArray = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, unmanagedArray, bytes.Length);
            
            var model = new Model
            {
                _model = load_model_bytes(unmanagedArray, (UIntPtr) bytes.Length)
                // _model = load_model(fileName)
            };
            
            Marshal.FreeHGlobal(unmanagedArray);
            
            return model;
        }

        public float[] Predict(float[] input)
        {
            var output = predict(_model, input, (UIntPtr) input.Length);

            float[] buffer = new float[1];
            Marshal.Copy(output, buffer, 0, buffer.Length);
            return buffer;
        }
    }
}