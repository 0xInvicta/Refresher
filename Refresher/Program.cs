using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Refresher
{
    internal class Program
    {
        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        static void Main(string[] args)
        {
            String browserName = "chrome"; //eg: msedge

            printTitile();
            while (true)
            {
                Process[] process = Process.GetProcessesByName(browserName);
                foreach (var instance in process)
                {
                    if (instance != null)
                    {
                        var _process = Process.GetProcessById(instance.Id);
                        bringProccessToFront(_process);
                    }
                    else
                    {
                        SystemSounds.Asterisk.Play();

                        MessageBox.Show(browserName + " was not found  running?\nTry again.", "Process ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                Process[] processes = Process.GetProcessesByName(browserName);
                 

                 foreach (Process proc in processes)      
                    PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK_F5, 0);  
                    updateTerminal();

                 Thread.Sleep(180000); //180000 = 3mins

            }                
        }
        public static void updateTerminal()
        {
            string datetime = DateTime.Now.ToString("hh:mm:ss tt");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[+] Refreshed - ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(datetime + "\n");

        }
        public static void printTitile()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\r______      __               _               \r\n| ___ \\    / _|             | |              \r\n| |_/ /___| |_ _ __ ___  ___| |__   ___ _ __ \r\n|    // _ \\  _| '__/ _ \\/ __| '_ \\ / _ \\ '__|\r\n| |\\ \\  __/ | | | |  __/\\__ \\ | | |  __/ |   \r\n\\_| \\_\\___|_| |_|  \\___||___/_| |_|\\___|_|   \r");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Author: Matuesz Peplinski\n");
        }

        public static void bringProccessToFront(Process process)
        {
            IntPtr handle = process.MainWindowHandle;
            SetForegroundWindow(handle);
        }
    }
    //NOTES:
    //Keys Codes: https://docs.microsoft.com/en-gb/windows/win32/inputdev/virtual-key-codes?redirectedfrom=MSDN

}

