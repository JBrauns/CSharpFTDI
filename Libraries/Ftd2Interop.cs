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

    public static class Ftd2Interop
    {
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
