#define USEPYD

using Python.Runtime;
using System.Diagnostics;

namespace pythonbridge
{
    public class PythonBridge
    {
        public string ModuleName = "";
        public dynamic Module;
        public void Init()
        {
#if !USEPYD
            return;
#endif
            PythonEngine.Initialize();
            using (Py.GIL())
            {
                Module = Py.Import(ModuleName);
                var Output = Module.Test();
                Debug.WriteLine("Test: "+Output);
            }
        }
        public static void Shutdown()
        {
            PythonEngine.Shutdown();
        }
        public void SetModule(string Name)
        {
            ModuleName = Name;
        }
        public string GetModuleName()
        {
            return ModuleName;
        }
    }
}
