using System;
using InTheHand.Net.Bluetooth;
using System.Threading;

namespace BtAuth
{
    class Program
    {
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        static string deviceName = "*";

        static string pin = "1234";

        static void Main(string[] args)
        {
            if (BluetoothRadio.PrimaryRadio == null)
            {
                Console.Error.WriteLine("BluetoothRadio.PrimaryRadio is NULL! No Bluetooth adapter?");
                Environment.Exit(1);
            }

            if (args.Length > 0 && args[0].Length > 0)
            {
                deviceName = args[0].ToUpperInvariant();
            }

            if (args.Length > 1)
            {
                pin = args[1];
            }

            Console.WriteLine("001\tRadio\tName={0}", BluetoothRadio.PrimaryRadio.Name);
            Console.WriteLine("002\tRadio\tLocalAddress={0}", BluetoothRadio.PrimaryRadio.LocalAddress);
            Console.WriteLine("003\tRadio\tManufacturer={0}", BluetoothRadio.PrimaryRadio.Manufacturer);
            Console.WriteLine("004\tRadio\tSoftwareManufacturer={0}", BluetoothRadio.PrimaryRadio.SoftwareManufacturer);
            Console.WriteLine("005\tRadio\tHardwareStatus={0}", BluetoothRadio.PrimaryRadio.HardwareStatus);
            Console.WriteLine("006\tRadio\tMode={0}", BluetoothRadio.PrimaryRadio.Mode);

            var authentication = new BluetoothWin32Authentication(HandleAuth);

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(stateInfo => {
                    Thread.Sleep(Timeout.Infinite);
                    ((AutoResetEvent)stateInfo).Set();
                }),
                autoEvent
            );

            autoEvent.WaitOne();
            autoEvent.Close();
            authentication.Dispose();
        }

        static void HandleAuth(object sender, BluetoothWin32AuthenticationEventArgs e)
        {
            Console.WriteLine("101\tAuth\tDeviceName={0}", e.Device.DeviceName);
            Console.WriteLine("102\tAuth\tDeviceAddress={0}", e.Device.DeviceAddress.ToString());
            Console.WriteLine("103\tAuth\tMethod={0}", e.AuthenticationMethod);

            if (!deviceName.Equals("*") && !e.Device.DeviceName.ToUpperInvariant().Contains(deviceName))
            {
                Console.WriteLine("104\tAuth\tIgnoring={0}", deviceName);

                return;
            }

            if (e.AuthenticationMethod == BluetoothAuthenticationMethod.Legacy)
            {
                Console.WriteLine("105\tAuth\tPin={0}", pin);

                e.Pin = pin;
            }

            e.Confirm = true;
        }
    }
}
