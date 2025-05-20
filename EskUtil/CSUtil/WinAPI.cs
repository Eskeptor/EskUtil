// ======================================================================================================
// File Name        : WinAPI.cs
// Project          : CSUtil
// Last Update      : 2025.05.20 - yc.jeon
// ======================================================================================================

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CSUtil
{
    /// <summary>
    /// Windows API 함수
    /// </summary>
    public static class WinAPI
    {
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public const string KERNEL32 = "kernel32.dll";
        public const string USER32 = "user32.dll";
        public const string WINMM = "winmm.dll";

        public const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        public const int ERROR_SUCCESS = 0x00;
        public const int ERROR_ALREADY_EXISTS = 0xB7;

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_NCHITTEST = 0x84;

        public const int WS_VSCROLL = 0x00200000;
        public const int WS_HSCROLL = 0x00100000;
        public const int GWL_STYLE = -16;

        public const int HT_CAPTION = 0x2;
        public const int HT_BOTTOMRIGHT = 17;

        public const int SB_VERT = 1;
        public const int SB_HORZ = 0;
        public const uint SIF_ALL = 0x17;

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const int KEYEVENTF_KEYUP = 0x0002;

        public const int VK_LBUTTON = 0x01;
        public const int VK_RBUTTON = 0x02;
        public const int VK_CANCEL = 0x03;
        public const int VK_MBUTTON = 0x04;
        public const int VK_XBUTTON1 = 0x05;
        public const int VK_XBUTTON2 = 0x06;
        public const int VK_BACK = 0x08;
        public const int VK_TAB = 0x09;
        public const int VK_CLEAR = 0x0C;
        public const int VK_RETURN = 0x0D;
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_PAUSE = 0x13;
        public const int VK_CAPITAL = 0x14;
        public const int VK_KANA = 0x15;
        public const int VK_HANGUEL = 0x15;
        public const int VK_HANGUL = 0x15;
        public const int VK_JUNJA = 0x17;
        public const int VK_FINAL = 0x18;
        public const int VK_HANJA = 0x19;
        public const int VK_KANJI = 0x19;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_CONVERT = 0x1C;
        public const int VK_NONCONVERT = 0x1D;
        public const int VK_ACCEPT = 0x1E;
        public const int VK_MODECHANGE = 0x1F;
        public const int VK_SPACE = 0x20;
        public const int VK_PRIOR = 0x21;
        public const int VK_NEXT = 0x22;
        public const int VK_END = 0x23;
        public const int VK_HOME = 0x24;
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;
        public const int VK_SELECT = 0x29;
        public const int VK_PRINT = 0x2A;
        public const int VK_EXECUTE = 0x2B;
        public const int VK_SNAPSHOT = 0x2C;
        public const int VK_INSERT = 0x2D;
        public const int VK_DELETE = 0x2E;
        public const int VK_HELP = 0x2F;
        public const int VK_0 = 0x30;
        public const int VK_1 = 0x31;
        public const int VK_2 = 0x32;
        public const int VK_3 = 0x33;
        public const int VK_4 = 0x34;
        public const int VK_5 = 0x35;
        public const int VK_6 = 0x36;
        public const int VK_7 = 0x37;
        public const int VK_8 = 0x38;
        public const int VK_9 = 0x39;
        public const int VK_A = 0x41;
        public const int VK_B = 0x42;
        public const int VK_C = 0x43;
        public const int VK_D = 0x44;
        public const int VK_E = 0x45;
        public const int VK_F = 0x46;
        public const int VK_G = 0x47;
        public const int VK_H = 0x48;
        public const int VK_I = 0x49;
        public const int VK_J = 0x4A;
        public const int VK_K = 0x4B;
        public const int VK_L = 0x4C;
        public const int VK_M = 0x4D;
        public const int VK_N = 0x4E;
        public const int VK_O = 0x4F;
        public const int VK_P = 0x50;
        public const int VK_Q = 0x51;
        public const int VK_R = 0x52;
        public const int VK_S = 0x53;
        public const int VK_T = 0x54;
        public const int VK_U = 0x55;
        public const int VK_V = 0x56;
        public const int VK_W = 0x57;
        public const int VK_X = 0x58;
        public const int VK_Y = 0x59;
        public const int VK_Z = 0x5A;
        public const int VK_LWIN = 0x5B;
        public const int VK_RWIN = 0x5C;
        public const int VK_APPS = 0x5D;
        public const int VK_SLEEP = 0x5F;
        public const int VK_NUMPAD0 = 0x60;
        public const int VK_NUMPAD1 = 0x61;
        public const int VK_NUMPAD2 = 0x62;
        public const int VK_NUMPAD3 = 0x63;
        public const int VK_NUMPAD4 = 0x64;
        public const int VK_NUMPAD5 = 0x65;
        public const int VK_NUMPAD6 = 0x66;
        public const int VK_NUMPAD7 = 0x67;
        public const int VK_NUMPAD8 = 0x68;
        public const int VK_NUMPAD9 = 0x69;
        public const int VK_MULTIPLY = 0x6A;
        public const int VK_ADD = 0x6B;
        public const int VK_SEPARATOR = 0x6C;
        public const int VK_SUBTRACT = 0x6D;
        public const int VK_DECIMAL = 0x6E;
        public const int VK_DIVIDE = 0x6F;
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;
        public const int VK_F13 = 0x7C;
        public const int VK_F14 = 0x7D;
        public const int VK_F15 = 0x7E;
        public const int VK_F16 = 0x7F;
        public const int VK_F17 = 0x80;
        public const int VK_F18 = 0x81;
        public const int VK_F19 = 0x82;
        public const int VK_F20 = 0x83;
        public const int VK_F21 = 0x84;
        public const int VK_F22 = 0x85;
        public const int VK_F23 = 0x86;
        public const int VK_F24 = 0x87;
        public const int VK_NUMLOCK = 0x90;
        public const int VK_SCROLL = 0x91;
        public const int VK_LSHIFT = 0xA0;
        public const int VK_RSHIFT = 0xA1;
        public const int VK_LCONTROL = 0xA2;
        public const int VK_RCONTROL = 0xA3;
        public const int VK_LMENU = 0xA4;
        public const int VK_RMENU = 0xA5;
        public const int VK_BROWSER_BACK = 0xA6;
        public const int VK_BROWSER_FORWARD = 0xA7;
        public const int VK_BROWSER_REFRESH = 0xA8;
        public const int VK_BROWSER_STOP = 0xA9;
        public const int VK_BROWSER_SEARCH = 0xAA;
        public const int VK_BROWSER_FAVORITES = 0xAB;
        public const int VK_BROWSER_HOME = 0xAC;
        public const int VK_VOLUMN_MUTE = 0xAD;
        public const int VK_VOLUMN_DOWN = 0xAE;
        public const int VK_VOLUMN_UP = 0xAF;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;
        public const int VK_MEDIA_STOP = 0xB2;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_LAUNCH_MAIL = 0xB4;
        public const int VK_LAUNCH_MEDIA_SELECT = 0xB5;
        public const int VK_LAUNCH_APP1 = 0xB6;
        public const int VK_LAUNCH_APP2 = 0xB7;
        public const int VK_OEM_1 = 0xBA;
        public const int VK_OEM_PLUS = 0xBB;
        public const int VK_OEM_COMMA = 0xBC;
        public const int VK_OEM_MINUS = 0xBD;
        public const int VK_OEM_PERIOD = 0xBE;
        public const int VK_OEM_2 = 0xBF;
        public const int VK_OEM_3 = 0xC0;
        public const int VK_OEM_4 = 0xDB;
        public const int VK_OEM_5 = 0xDC;
        public const int VK_OEM_6 = 0xDD;
        public const int VK_OEM_7 = 0xDE;
        public const int VK_OEM_8 = 0xDF;
        public const int VK_OEM_102 = 0xE2;
        public const int VK_PROCESSKEY = 0xE5;
        public const int VK_PACKET = 0xE7;
        public const int VK_ATTN = 0xF6;
        public const int VK_CRSEL = 0xF7;
        public const int VK_EXSEL = 0xF8;
        public const int VK_EREOF = 0xF9;
        public const int VK_PLAY = 0xFA;
        public const int VK_ZOOM = 0xFB;
        public const int VK_NONAME = 0xFC;
        public const int VK_PA1 = 0xFD;
        public const int VK_OEM_CLEAR = 0xFE;

        public const int SW_SHOWNORMAL = 1;
        public const int SW_HIDE = 0;
        public const int SW_MINIMIZE = 2;
        public const int SW_MAXIMIZE = 3;
        public const int SW_RESTORE = 9;

        public const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        public const int TVS_EX_DOUBLEBUFFER = 0x0004;

        /// <summary>
        /// program timer for single event
        /// </summary>
        public const int TIME_ONESHOT = 0x0000;
        /// <summary>
        /// program for continuous periodic event
        /// </summary>
        public const int TIME_PERIODIC = 0x0001;
        /// <summary>
        /// callback is function
        /// </summary>
        public const int TIME_CALLBACK_FUNCTION = 0x0000;
        /// <summary>
        /// callback is event - use SetEvent
        /// </summary>
        public const int TIME_CALLBACK_EVENT_SET = 0x0010;
        /// <summary>
        /// callback is event - use PulseEvent
        /// </summary>
        public const int TIME_CALLBACK_EVENT_PULSE = 0x0020;

        /// <summary>
        /// WinAPI의 MAX_PATH 값과 동일
        /// </summary>
        public const int MAX_PATH = 260;

        /// <summary>
        /// Timer Callback
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <param name="userCtx"></param>
        /// <param name="rsv1"></param>
        /// <param name="rsv2"></param>
        public delegate void TimerCallback(uint id, uint msg, ref uint userCtx, uint rsv1, uint rsv2);

#pragma warning disable CA1051 // 표시되는 인스턴스 필드를 선언하지 마세요. (Win32 API용 Structure 항목 이름 그대로 사용을 위함)
        /// <summary>
        /// System Time 구조체
        /// </summary>
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            // <summary>
            // See your Windows API docs.
            // </summary>
            public int x;
            // <summary>
            // See your Windows API docs.
            // </summary>
            public int y;
        }

        public struct MOUSEHOOKSTRUCT
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }
#pragma warning restore CA1051 // 표시되는 인스턴스 필드를 선언하지 마세요. (Win32 API용 Structure 항목 이름 그대로 사용을 위함)


        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport(KERNEL32, SetLastError = true, EntryPoint = "SetThreadExecutionState")]
        private static extern EXECUTION_STATE Win32_SetThreadExecutionState(EXECUTION_STATE esFlags);
        [DllImport(KERNEL32, ExactSpelling = true, EntryPoint = "GetConsoleWindow")]
        private static extern IntPtr Win32_GetConsoleWindow();
        [DllImport(KERNEL32, EntryPoint = "SetSystemTime")]
        private static extern bool Win32_SetSystemTime(ref SYSTEMTIME time);
        [DllImport(KERNEL32, EntryPoint = "GetLocalTime")]
        private static extern void Win32_GetLocalTime(out SYSTEMTIME localTime);
        [DllImport(KERNEL32, CharSet = CharSet.Unicode, EntryPoint = "WinExec")]
        private static extern int Win32_WinExec(string lpCmdLine, uint uCmdShow);

        [DllImport(WINMM, SetLastError = true, EntryPoint = "timeSetEvent")]
        private static extern uint Win32_TimeSetEvent(uint msDelay, uint msResolution, TimerCallback callback, ref uint userCtx, uint eventType);
        [DllImport(WINMM, SetLastError = true, EntryPoint = "timeKillEvent")]
        private static extern void Win32_TimeKillEvent(uint uTimerId);

        [DllImport(USER32, EntryPoint = "DeleteMenu")]
        private static extern int Win32_DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport(USER32, EntryPoint = "GetSystemMenu")]
        private static extern IntPtr Win32_GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport(USER32, EntryPoint = "SendMessage")]
        private static extern int Win32_SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport(USER32, EntryPoint = "PostMessage")]
        private static extern int Win32_PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport(USER32, EntryPoint = "ReleaseCapture")]
        private static extern bool Win32_ReleaseCapture();
        [DllImport(USER32, SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "FindWindow")]
        private static extern IntPtr Win32_FindWindow(string lpClassName, string lpWindowName);
        [DllImport(USER32, SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "FindWindowEx")]
        private static extern IntPtr Win32_FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport(USER32, SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "SetWindowText")]
        private static extern bool Win32_SetWindowText(IntPtr hWnd, string lpString);
        [DllImport(USER32, EntryPoint = "GetKeyboardState")]
        private static extern bool Win32_GetKeyboardState(ref byte lppbKeyState);
        [DllImport(USER32, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi, EntryPoint = "GetKeyState")]
        private static extern short Win32_GetKeyState(int keyCode);
        [DllImport(USER32, EntryPoint = "keybd_event")]
        private static extern void Win32_keybd_event(byte bVk, byte bScan, int dwFlags, ref int dwExtraInfo);
        [DllImport(USER32, CharSet = CharSet.Auto, EntryPoint = "SetWindowsHookEx")]
        private static extern int Win32_SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hmod, int dwThreadId);
        [DllImport(USER32, CharSet = CharSet.Auto, ExactSpelling = true, EntryPoint = "UnhookWindowsHookEx")]
        private static extern bool Win32_UnhookWindowsHookEx(IntPtr hhk);
        [DllImport(USER32, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall, EntryPoint = "CallNextHookEx")]
        private static extern int Win32_CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport(USER32, EntryPoint = "HideCaret")]
        private static extern bool Win32_HideCaret(IntPtr hWnd);
        [DllImport(USER32, EntryPoint = "GetWindowLong")]
        private static extern int Win32_GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport(USER32, SetLastError = true, EntryPoint = "GetScrollInfo")]
        private static extern bool Win32_GetScrollInfo(IntPtr hWnd, int nBar, ref SCROLLINFO lpScrollInfo);

        /// <summary>
        /// 절전 모드로 들어가지 않도록 방지하는 함수
        /// </summary>
        public static void KeepAliveScreen()
        {
            Win32_SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
        }

        /// <summary>
        /// 절전 모드로 들어가지 못하게 설정했던 것을 원래대로 되돌리는 함수
        /// </summary>
        public static void ReleaseKeepAliveScreen()
        {
            Win32_SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        /// <summary>
        /// 콘솔창의 종료 버튼을 제거하는 함수
        /// </summary>
        public static void DeleteConsoleCloseMenu()
        {
            _ = Win32_DeleteMenu(Win32_GetSystemMenu(Win32_GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
        }

        /// <summary>
        /// GetLastError 호출 함수
        /// </summary>
        /// <returns></returns>
        public static int GetLastError()
        {
            return Marshal.GetLastWin32Error();
        }

        /// <summary>
        /// System Time Setting
        /// </summary>
        /// <param name="time">셋팅할 시간 값</param>
        /// <returns></returns>
        public static bool SetWindowsSystemTime(ref SYSTEMTIME time)
        {
            return Win32_SetSystemTime(ref time);
        }

        /// <summary>
        /// Local Time Get
        /// </summary>
        /// <param name="localTime">현재 시간 값</param>
        public static void GetWindowsLocalTime(out SYSTEMTIME localTime)
        {
            Win32_GetLocalTime(out localTime);
        }

        public static uint TimeSetEvent(uint msDelay, uint msResolution, TimerCallback callback, ref uint userCtx, uint eventType)
        {
            return Win32_TimeSetEvent(msDelay, msResolution, callback, ref userCtx, eventType);
        }

        public static void TimeKillEvent(uint uTimerId)
        {
            Win32_TimeKillEvent(uTimerId);
        }

        public static int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return Win32_SendMessage(hWnd, Msg, wParam, lParam);
        }

        public static int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam)
        {
            return Win32_PostMessage(hWnd, Msg, wParam, lParam);
        }

        public static bool ReleaseCapture()
        {
            return Win32_ReleaseCapture();
        }

        public static IntPtr FindWindow(string lpClassName, string lpWindowName)
        {
            return Win32_FindWindow(lpClassName, lpWindowName);
        }

        public static IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow)
        {
            return Win32_FindWindowEx(hwndParent, hwndChildAfter, lpszClass, lpszWindow);
        }

        public static bool SetWindowText(IntPtr hWnd, string lpString)
        {
            return Win32_SetWindowText(hWnd, lpString);
        }

        /// <summary>
        /// MessageBox의 Title 및 버튼 Text를 변경하는 함수
        /// </summary>
        /// <param name="title">변경할 MessageBox의 Title</param>
        /// <param name="buttonTexts">변경할 Button 텍스트들</param>
        public static void SetMessageBox(string title, string[] buttonTexts)
        {
            if (buttonTexts == null ||
                buttonTexts.Length == 0)
            {
                return;
            }

            Task.Run(() =>
            {
                IntPtr messageBoxHandle = IntPtr.Zero;
                int elapsedTime = 0;
                while (elapsedTime < 100)
                {
                    // #32770 = MessageBox 클래스
                    messageBoxHandle = Win32_FindWindow("#32770", title);
                    if (messageBoxHandle != IntPtr.Zero)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(10);
                    elapsedTime += 10;
                }

                if (messageBoxHandle == IntPtr.Zero)
                {
                    return;
                }

                IntPtr buttonHandle = IntPtr.Zero;
                buttonHandle = Win32_FindWindowEx(messageBoxHandle, buttonHandle, "Button", null);
                int index = 0;

                while (buttonHandle != IntPtr.Zero &&
                       index < buttonTexts.Length)
                {
                    Win32_SetWindowText(buttonHandle, buttonTexts[index]);

                    buttonHandle = Win32_FindWindowEx(messageBoxHandle, buttonHandle, "Button", null);
                    ++index;
                }
            });
        }

        public static bool GetKeyboardState(ref byte lppbKeyState)
        {
            return Win32_GetKeyboardState(ref lppbKeyState);
        }

        public static short GetKeyState(int keyCode)
        {
            return Win32_GetKeyState(keyCode);
        }

        public static void keybd_event(byte bVk, byte bScan, int dwFlags, ref int dwExtraInfo)
        {
            Win32_keybd_event(bVk, bScan, dwFlags, ref dwExtraInfo);
        }

        public static int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hmod, int dwThreadId)
        {
            return Win32_SetWindowsHookEx(idHook, lpfn, hmod, dwThreadId);
        }

        public static bool UnhookWindowsHookEx(IntPtr hhk)
        {
            return Win32_UnhookWindowsHookEx(hhk);
        }

        public static int CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam)
        {
            return Win32_CallNextHookEx(hhk, nCode, wParam, lParam);
        }

        public static int WinExec(string lpCmdLine, uint uCmdShow)
        {
            return Win32_WinExec(lpCmdLine, uCmdShow);
        }

        public static bool HideCaret(IntPtr hWnd)
        {
            return Win32_HideCaret(hWnd);
        }

        public static int GetWindowLong(IntPtr hWnd, int nIndex)
        {
            return Win32_GetWindowLong(hWnd, nIndex);
        }

        public static bool GetScrollInfo(IntPtr hWnd, int nBar, ref SCROLLINFO lpScrollInfo)
        {
            return Win32_GetScrollInfo(hWnd, nBar, ref lpScrollInfo);
        }
    }
}
