using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFTDI.Libraries
{
    public enum FtdStatus : ulong
    {
        FtdOK,
        FtdInvalidHandle,
        FtdDeviceNotFound,
        FtdDeviceNotOpened,
        FtdIOError,
        FtdInsufficientResources,
        FtdInvalidParameter,
        FtdInvalidBaudRate,
        FtdDeviceNotOpenedForErase,
        FtdDeviceNotOpenedForWrite,
        FtdFailedToWriteDevice,
        FtdEEPROMReadFailed,
        FtdEEPROMWriteFailed,
        FtdEEPROMEraseFailed,
        FtdEEPROMNorPresent,
        FtdEEPROMNotProgrammed,
        FtdInvalidArgs,
        FtdNotSupported,
        FtdOtherError
    };

    public enum FtdDevice : ulong
    {
        FtdDeviceBM,
        FtdDeviceAM,
        FtdDevice100AX,
        FtdDeviceUnknown,
        FtdDevice2232C,
        FtdDevice232R
    };

    public static class Ftd2
    {
        //
        // FT_OpenEx Flags
        //
        public const uint FtdListNumberOnly = 0x80000000;
        public const uint FtdListByIndex = 0x40000000;
        public const uint FtdListAll = 0x20000000;
        public const uint FtdListMask = (FtdListNumberOnly | FtdListByIndex | FtdListAll);

        //
        // Baud Rates
        //
        public const uint FtdBaudRate_300 = 300;
        public const uint FtdBaudRate_600 = 600;
        public const uint FtdBaudRate_1200 = 1200;
        public const uint FtdBaudRate_2400 = 2400;
        public const uint FtdBaudRate_4800 = 4800;
        public const uint FtdBaudRate_9600 = 9600;
        public const uint FtdBaudRate_14400 = 14400;
        public const uint FtdBaudRate_19200 = 19200;
        public const uint FtdBaudRate_38400 = 38400;
        public const uint FtdBaudRate_57600 = 57600;
        public const uint FtdBaudRate_115200 = 115200;
        public const uint FtdBaudRate_230400 = 230400;
        public const uint FtdBaudRate_460800 = 460800;
        public const uint FtdBaudRate_921600 = 921600;

        //
        // Word Lengths
        //
        public const byte FtdBits_8 = 8;
        public const byte FtdBits_7 = 7;
        public const byte FtdBits_6 = 6;
        public const byte FtdBits_5 = 5;

        //
        // Stop Bits
        //
        public const byte FtdStopBits_1 = 0;
        public const byte FtdStopBits_1_5 = 1;
        public const byte FtdStopBits_2 = 2;

        //
        // Parity
        //
        public const byte FtdParityNone = 0;
        public const byte FtdParityOdd = 1;
        public const byte FtdParityEven = 2;
        public const byte FtdParityMark = 3;
        public const byte FtdParitySpace = 4;

        //
        // Flow Control
        //
        public const ushort FtdFlowNone = 0x0000;
        public const ushort FtdFlowRtsCts = 0x0100;
        public const ushort FtdFlowDtrDsr = 0x0200;
        public const ushort FtdFlowXonXoff = 0x0100;

        //
        // Purge rx and tx buffers
        //
        public const uint FtdPurgeRx = 1;
        public const uint FtdPurgeTx = 2;

        //
        // Events
        //
        public const uint FtdEventRxChar = 1;
        public const uint FtdEventModemStatus = 2;

        //
        // Timeouts
        //
        public const uint FtdDefaultRxTimeout = 300;
        public const uint FtdDefaultTxTimeout = 300;
        
        /// <summary>
        /// This function builds a device information list and returns the number of D2XX devices connected to the
        /// system. The list contains information about both unopen and open devices.
        /// </summary>
        /// <param name="deviceCount">Pointer to unsigned long to store the number of device connected.</param>
        /// <returns>FtdOK if successful, otherwise the return value is an Ftd error code</returns>
        [DllImport("ftd2xx.dll", SetLastError = true)]
        public static extern FtdStatus FT_CreateDeviceInfoList(
            ref ulong deviceCount);

        [DllImport("ftd2xx.dll", SetLastError = true)]
        public static extern FtdStatus FT_GetDeviceInfoList(
            [Out] FtdDeviceListInfoNode[] devices,
            ref ulong deviceCount);

        [DllImport("ftd2xx.dll", SetLastError = true)]
        public static extern FtdStatus FT_GetDeviceInfoDetail(
            uint index,
            ref uint flags,
            ref uint id,
            ref uint locId,
            [Out] byte[] serialNumber,
            [Out] byte[] description,
            ref uint ftdHandle);

        [DllImport("ftd2xx.dll", SetLastError = true)]
        public static extern FtdStatus FT_ListDevices(
            );
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public struct FtdDeviceListInfoNode
    {
        public uint Flags;
        public uint Type;
        public uint Id;
        public uint LocId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] SerialNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] Description;
        public uint FtdHandle;
    }
}
