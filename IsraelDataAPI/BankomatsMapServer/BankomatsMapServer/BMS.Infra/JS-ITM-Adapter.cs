using System;
using System.IO;
using System.Reflection;
using BMS.Infra.DataTypes;
using Jurassic;

namespace BMS.Infra
{
    public class JS_ITM_Adapter
    {
        private readonly ScriptEngine m_engine;
        public JS_ITM_Adapter()
        {
            m_engine = new ScriptEngine();
            m_engine.ExecuteFile(Path.Combine(AssemblyDirectory, @"JS-ITEM\js-itm.js"));
        }
        public LatLonPoint Convert_ITM_to_LatLon(ITMPoint itmPoint)
        {
            //var engine = new Jurassic.ScriptEngine();
            //engine.SetGlobalValue("x", itmPoint.X);
            //engine.SetGlobalValue("y", itmPoint.Y);
            //engine.ExecuteFile(Path.Combine(AssemblyDirectory, @"JS-ITEM\js-itm.js"));
            //string result = engine.GetGlobalValue<string>("result");
            
            string result = m_engine.CallGlobalFunction<string>("Run", itmPoint.X, itmPoint.Y);
            string[] resultValues = result.Split(' ');
            return new LatLonPoint(Double.Parse(resultValues[0]), Double.Parse(resultValues[1]));
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
