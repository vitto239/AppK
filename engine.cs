using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Appk
{
    internal static class Extensions
    {
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);
        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            try
            {
                var fileNameBuilder = new StringBuilder(buffer);
                uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
                var x = QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
                    fileNameBuilder.ToString() :
                    null;
                return x;
            }
            catch (Exception)
            {
                return "error";
            }

        }
    }
    public class InfoClass
    {

    }
    internal class engine
    {
        private ServiceController[] servicesTable 
        { 
            get
            {
                return ServiceController.GetServices();
            }            
        }
        private Process[] processesTable
        {
            get
            {
                return Process.GetProcesses();
            }
        }


        //internal string[] SearchIn(ModeSelector selector, string processName)
        //{
        //    switch (selector)
        //    {
        //        case ModeSelector.services:
        //            //return servicesTable.Where(c => c.ServiceName.Contains(processName)).ToArray();

        //        case ModeSelector.processes:
        //            //return processesTable.Where(c => c.Contains(processName)).ToArray();
        //        default:
        //            return null;
        //    }
            
        //}

        internal string[] SearchIn(string text)
        {
            var z = GetNames();
            return z.Where(a => a.Contains(text)).ToArray();
        }

        //internal object GetInfo(ModeSelector selector, string processName)
        //{
        //    var prc = SearchIn(selector, processName);
        //    switch (selector)
        //    {
        //        case ModeSelector.services:
        //            foreach (var item in prc)
        //            {

        //            }
        //            return null;
        //        case ModeSelector.processes:
        //            foreach (var item in prc)
        //            {

        //            }
        //            return null;
        //        default:
        //            return null;
        //    }
        //}
        internal string [] GetNames()
        {
            var x = servicesTable.Select(a => a.ServiceName).ToArray();            
            var y = processesTable.Select(a => a.ProcessName).ToArray();            
            var lengthX = x.Length;
            var lengthY = y.Length;
            var z = new string[lengthX + lengthY];
            var lengthZ = z.Length;
            
            for (int i = 0; i < lengthX; i++)
            {
                z[i] = x[i].ToLower();
            }
            var u = 0;
            for (int i = lengthX; i < lengthZ; i++)
            {
                z[i] = y[u++].ToLower();
            }
            return z;
        }
    }
}
