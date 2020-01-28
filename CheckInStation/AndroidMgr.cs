using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CheckInStation
{
    class AndroidMgr
    {
        static string SDKLocation;
        static bool isRunning;

        public enum COMMANDS
        {
            MANUFACTURER,
            SERIALNO,
            IMEI,
            DEVICENAME,
            MODEL

        }

        public static void KillADBService()
        {
            string sdk = @"C:\Users\Vince\Libraries\Downloads\imobileconfig.0.132.77.win7-x64\adb.exe";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = sdk;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "kill-service";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();


                }
            }
            catch
            {
                // Log error.
                Console.WriteLine("FAILED");
            }
        }


        public static void InitiateADBService()
        {
            isRunning = false;

            string sdk = @"C:\Users\Vince\Libraries\Downloads\imobileconfig.0.132.77.win7-x64\adb.exe";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = sdk;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "start-service";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();

                    
                }
            }
            catch
            {
                // Log error.
                Console.WriteLine("FAILED");
            }

            int port = 5037; //<--- This is your value
            bool isListening = false;

            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();

            foreach (IPEndPoint tcpi in tcpConnInfoArray)
            {
                if (tcpi.Port == port)
                {
                    isListening = true;
                    Trace.WriteLine("SERVER STATUS: " + isListening + " " + tcpi.Address + " " + tcpi.AddressFamily + " PORT: " + tcpi.Port);
                    isRunning = true;
                    break;
                }
            }


        }


        public static List<string> GetConnectedDevices()
        {

            List<string> connectedDevices = new List<string>();

            if (isRunning)
            {

                string command = "devices -l";

                int exitCode;
                ProcessStartInfo processInfo;
                Process process;
                List<string> device = new List<string>();

                processInfo = new ProcessStartInfo(@"C:\Users\Vince\Libraries\Downloads\imobileconfig.0.132.77.win7-x64\adb.exe", command);
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                // *** Redirect the output ***
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                process = Process.Start(processInfo);
                process.WaitForExit();

                // *** Read the streams ***
                // Warning: This approach can lead to deadlocks, see Edit #2
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                exitCode = process.ExitCode;

                process.Close();

                foreach (var myString in output.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    device.Add(myString);
                }

                for (int i =  0;i< device.Count;i++)
                {
                    if (i != 0)
                    {
                        connectedDevices.Add(device[i].ToString().Split(' ')[0]);
                    }
                }
                    

            }

            return connectedDevices;
        }

        public static List<aDevice> LoadDevices(List<string> deviceList)
        {
            List<aDevice> aDeviceList = new List<aDevice>();
            

            if (isRunning)
            {
                foreach (string deviceID in deviceList)
                {

                    aDevice device = new aDevice();
                    Dictionary<string, COMMANDS> cmds = new Dictionary<string, COMMANDS>();
                    string f = @"\""'\""";
                    cmds.Add("-s " + deviceID.Trim() + " shell \"service call iphonesubinfo 1 | awk -F " + f +" '{print $2}' | sed '1 d' | tr -d '.' | awk '{print}' ORS=\"", COMMANDS.IMEI);

                    cmds.Add($"-s {deviceID} shell getprop ro.product.model", COMMANDS.MODEL);
                    cmds.Add($"-s {deviceID} shell getprop ro.product.name", COMMANDS.DEVICENAME);
                    cmds.Add($"-s {deviceID} shell getprop ro.product.manufacturer", COMMANDS.MANUFACTURER);
                    cmds.Add($"-s {deviceID} shell getprop ril.serialnumber", COMMANDS.SERIALNO);
                    
                    foreach (KeyValuePair<string,COMMANDS> cmd in cmds)
                    {
                        string command = cmd.Key;
                        Console.WriteLine("LONE LINE:" + command);

                        int exitCode;
                        ProcessStartInfo processInfo;
                        Process process;
                        StringBuilder stringBuilder = new StringBuilder();

                        processInfo = new ProcessStartInfo(@"C:\Users\Vince\Libraries\Downloads\imobileconfig.0.132.77.win7-x64\adb.exe", command);
                        processInfo.CreateNoWindow = true;
                        processInfo.UseShellExecute = false;
                        // *** Redirect the output ***
                        processInfo.RedirectStandardError = true;
                        processInfo.RedirectStandardOutput = true;

                        process = Process.Start(processInfo);
                        process.WaitForExit();

                        // *** Read the streams ***
                        // Warning: This approach can lead to deadlocks, see Edit #2
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        exitCode = process.ExitCode;
                        process.Close();

                        switch (cmd.Value)
                        {
                            case COMMANDS.DEVICENAME:
                                device.deviceName = String.IsNullOrEmpty(output) ? " " : output;
                                break;
                            case COMMANDS.MANUFACTURER:
                                device.manufacturer = String.IsNullOrEmpty(output) ? " " : output;
                                break;
                            case COMMANDS.MODEL:
                                device.model = String.IsNullOrEmpty(output) ? " " : output;
                                break;
                            case COMMANDS.SERIALNO:
                                device.serialNo = String.IsNullOrEmpty(output) ? " " : output;
                                break;
                            case COMMANDS.IMEI:
                                device.imeiNo = String.IsNullOrEmpty(output) ? " " : output;
                                break;
                            default:
                                break;
                        }

                    }

                    aDeviceList.Add(device);
                    
                }
            }


            return aDeviceList;
        }

    }

    

    class aDevice
    {
        public string manufacturer;
        public string model;
        public string deviceName;
        public string serialNo;
        public string imeiNo;


    }


}
