using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

namespace MyBrowser
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    class GetSystemIcon
    {
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        /// <summary>  
        /// 返回系统设置的图标  
        /// </summary>  
        /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>  
        /// <param name="dwFileAttributes">0</param>  
        /// <param name="psfi">结构体</param>  
        /// <param name="cbSizeFileInfo">结构体大小</param>  
        /// <param name="uFlags">枚举类型</param>  
        /// <returns>-1失败</returns>  
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref   SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public enum SHGFI
        {
            SHGFI_ICON = 0x100,
            SHGFI_LARGEICON = 0x0,
            SHGFI_USEFILEATTRIBUTES = 0x10
        }


        /// <summary>  
        /// 获取文件图标 zgke@sina.com qq:116149  
        /// </summary>  
        /// <param name="p_Path">文件全路径</param>  
        /// <returns>图标</returns>  
        public static Icon GetIconByFileName(string p_Path)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON | SHGFI.SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }
        /// <summary>  
        /// 获取文件夹图标  zgke@sina.com qq:116149  
        /// </summary>  
        /// <returns>图标</returns>  
        public static Icon GetDirectoryIcon()
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }
        //[StructLayout(LayoutKind.Sequential)]
        //public struct SHFILEINFO
        //{
        //    public IntPtr hIcon;
        //    public IntPtr iIcon;
        //    public uint dwAttributes;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        //    public string szDisplayName;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        //    public string szTypeName;
        //}

        ///// <summary>
        ///// 返回系统设置的图标
        ///// </summary>
        ///// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>
        ///// <param name="dwFileAttributes">0</param>
        ///// <param name="psfi">结构体</param>
        ///// <param name="cbSizeFileInfo">结构体大小</param>
        ///// <param name="uFlags">枚举类型</param>
        ///// <returns>-1失败</returns>
        //[DllImport("shell32.dll")]
        //public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref   SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        //public enum SHGFI
        //{
        //    SHGFI_ICON = 0x100,
        //    SHGFI_LARGEICON = 0x0,
        //    SHGFI_USEFILEATTRIBUTES = 0x10
        //}


        ///// <summary>
        ///// 获取文件图标 zgke@sina.com qq:116149
        ///// </summary>
        ///// <param name="p_Path">文件全路径</param>
        ///// <returns>图标</returns>
        //public static Icon GetIconByFileName(string p_Path)
        //{
        //    SHFILEINFO _SHFILEINFO = new SHFILEINFO();
        //    IntPtr _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON | SHGFI.SHGFI_USEFILEATTRIBUTES));
        //    if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
        //    Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
        //    return _Icon;
        //}
        ///// <summary>
        ///// 获取文件夹图标  zgke@sina.com qq:116149
        ///// </summary>
        ///// <returns>图标</returns>
        //public static Icon GetDirectoryIcon()
        //{
        //    SHFILEINFO _SHFILEINFO = new SHFILEINFO();
        //    IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
        //    if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
        //    Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
        //    return _Icon;
        //}

    //    public static Icon GetDirectoryIcon()
    //    {
    //        SHFILEINFO _SHFILEINFO = new SHFILEINFO();
    //        //IntPtr _IconIntPtr = Win32.SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON));
    //        //if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
    //        //Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
    //        //return _Icon;
    //        Win32.SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON));
    //        System.Drawing.Icon dirIcon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
    //        return dirIcon;
    //    } 

    //    /// <summary>
    //    /// 依据文件名读取图标，若指定文件不存在，则返回空值。
    //    /// </summary>
    //    /// <param name="fileName"></param>
    //    /// <returns></returns>
    //    public static Icon GetIconByFileName(string fileName)
    //    {
    //        if (fileName == null || fileName.Equals(string.Empty)) return null;
    //        if (!File.Exists(fileName)) return null;

    //        SHFILEINFO shinfo = new SHFILEINFO();
    //        //Use this to get the small Icon
    //        Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
    //        //The icon is returned in the hIcon member of the shinfo struct
    //        System.Drawing.Icon myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
    //        return myIcon;
    //    }

    //    /// <summary>
    //    /// 给出文件扩展名（.*），返回相应图标
    //    /// 若不以"."开头则返回文件夹的图标。
    //    /// </summary>
    //    /// <param name="fileType"></param>
    //    /// <param name="isLarge"></param>
    //    /// <returns></returns>
    //    public static Icon GetIconByFileType(string fileType, bool isLarge)
    //    {
    //        if (fileType == null || fileType.Equals(string.Empty)) return null;

    //        RegistryKey regVersion = null;
    //        string regFileType = null;
    //        string regIconString = null;
    //        string systemDirectory = Environment.SystemDirectory + "\\";

    //        if (fileType[0] == '.')
    //        {
    //            //读系统注册表中文件类型信息
    //            regVersion = Registry.ClassesRoot.OpenSubKey(fileType, true);
    //            if (regVersion != null)
    //            {
    //                regFileType = regVersion.GetValue("") as string;
    //                regVersion.Close();
    //                regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon", true);
    //                if (regVersion != null)
    //                {
    //                    regIconString = regVersion.GetValue("") as string;
    //                    regVersion.Close();
    //                }
    //            }
    //            if (regIconString == null)
    //            {
    //                //没有读取到文件类型注册信息，指定为未知文件类型的图标
    //                regIconString = systemDirectory + "shell32.dll,0";
    //            }
    //        }
    //        else
    //        {
    //            //直接指定为文件夹图标
    //            regIconString = systemDirectory + "shell32.dll,3";
    //        }
    //        string[] fileIcon = regIconString.Split(new char[] { ',' });
    //        if (fileIcon.Length != 2)
    //        {
    //            //系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标
    //            fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
    //        }
    //        Icon resultIcon = null;
    //        try
    //        {
    //            //调用API方法读取图标
    //            int[] phiconLarge = new int[1];
    //            int[] phiconSmall = new int[1];
    //            uint count = Win32.ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
    //            IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
    //            resultIcon = Icon.FromHandle(IconHnd);
    //        }
    //        catch { }
    //        return resultIcon;
    //    }
    //}

    //[StructLayout(LayoutKind.Sequential)]
    //public struct SHFILEINFO
    //{
    //    public IntPtr hIcon;
    //    public IntPtr iIcon;
    //    public uint dwAttributes;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    //    public string szDisplayName;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
    //    public string szTypeName;
    //};

    ///// <summary>
    ///// 定义调用的API方法
    ///// </summary>
        //class Win32
        //{
        //    public const uint SHGFI_ICON = 0x100;
        //    public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        //    public const uint SHGFI_SMALLICON = 0x1; // 'Small icon

        //    [DllImport("shell32.dll")]
        //    public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        //    [DllImport("shell32.dll")]
        //    public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);
        //}

    }
}
