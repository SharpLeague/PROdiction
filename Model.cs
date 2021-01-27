using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace PROdiction
{
    public class Model
    {
        const string DLL_PATH = "libkeras2cpp.dll";

        [DllImport(@DLL_PATH, SetLastError=true, CharSet = CharSet.Ansi)]
        private static extern IntPtr load_model([MarshalAs(UnmanagedType.LPStr)] string fileName);

        [DllImport(@DLL_PATH, SetLastError=true)]
        private static extern IntPtr predict(IntPtr model, float[] data, UIntPtr size);
        
        private IntPtr _model;

        public static Model Load(string fileName)
        {
            return new Model
            {
                _model = load_model(fileName)
            };
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