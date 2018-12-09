using CSharpFTDI.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFTDI
{
    class Program
    {
        static void Main(string[] args)
        {
            FtdStatus status;

            ulong infoNodeCount = 0;
            Ftd2Interop.FT_CreateDeviceInfoList(ref infoNodeCount);
            if (infoNodeCount > 0)
            {
                Console.WriteLine("Number of connected devices is {0:D}", infoNodeCount);

                FtdDeviceListInfoNode[] infoNodes = new FtdDeviceListInfoNode[infoNodeCount];
                status = Ftd2Interop.FT_GetDeviceInfoList(infoNodes, ref infoNodeCount);

                if (status == FtdStatus.FtdOK)
                {
                    Console.WriteLine("NodeCount={0:D}", infoNodeCount);
                    for (ulong infoNodeIndex = 0; infoNodeIndex < infoNodeCount; ++infoNodeIndex)
                    {
                        FtdDeviceListInfoNode node = infoNodes[infoNodeIndex];

                        int serialNumberCount = System.Array.IndexOf(node.SerialNumber, (byte)0);
                        int descriptionCount = System.Array.IndexOf(node.Description, (byte)0);
                        Console.WriteLine("Node={0:D} {{\n  Flags={1:D}\n  Type={2:D}\n  Id={3:D}\n  LocId={4:D}\n  SerialNumber={5}\n  Description={6}\n}}",
                            infoNodeIndex, node.Flags, node.Type, node.Id, node.LocId,
                            System.Text.Encoding.ASCII.GetString(node.SerialNumber, 0, serialNumberCount),
                            System.Text.Encoding.ASCII.GetString(node.Description, 0, descriptionCount));
                    }
                }
                else
                {
                    Console.WriteLine("Status={0:D}", status);
                }
            }
            else
            {
                Console.WriteLine("No connected device found");
            }

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
    }
}