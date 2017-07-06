using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyboardLock
{
    /// <summary>
    /// Keyboard hook
    /// </summary>
    class KeyboardHook
    {
        public event KeyEventHandler KeyDownEvent;
        public event KeyPressEventHandler KeyPressEvent;
        public event KeyEventHandler KeyUpEvent;

        public bool Go = true;      // Let the keyboard data go by default (do not intercept)

        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        static int hKeyboardHook = 0; // Declare the initial value of the keyboard hook processing

        // Values are located in Winuser.h whitin the Microsoft SDK
        public const int WH_KEYBOARD_LL = 13;   // Thread Keyboard Hook Monitor The mouse message is set to 2, and the global keyboard listens for the mouse message to 13
        HookProc KeyboardHookProcedure; // Declare KeyboardHookProcedure as the HookProc type

        // Keyboard structure
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;  // Set a virtual key code. The code must have a value range of 1 to 254
            public int scanCode; // Specifies the key to the hardware scan code
            public int flags;  // Key sign
            public int time; // Specify the timestamp of the message
            public int dwExtraInfo; // Specify additional information
        }

        // Use this feature to install a hook
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        // Call this function to unload the hook
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);


        // Use this function to continue the next hook through the message hook
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        // Get the current thread number (thread hook required)
        [DllImport("kernel32.dll")]
        static extern int GetCurrentThreadId();

        // Use the WINDOWS API function instead of the function that gets the current instance to prevent the hook from failing
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        public void Start()
        {
            // Install the keyboard hook
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
                //hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                //************************************
                //Keyboard thread hook
                //SetWindowsHookEx( 2,KeyboardHookProcedure, IntPtr.Zero, GetCurrentThreadId());
                //idGetCurrentThreadId(), specifies the thread to be monitored,
                //Keyboard global hook, needs to reference space (using System.Reflection;)
                //SetWindowsHookEx( 13,MouseHookProcedure,Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),0);
                //
                //On the SetWindowsHookEx (int idHook, HookProc lpfn, IntPtr hInstance, int threadId) function will hook to the hook list
                //Four parameters explaining:
                //IdHook hook type, that is, to determine what kind of message hooks, the above code is set to 2, that is, listen to the keyboard message and is a thread hook, if the global hook to listen to the keyboard message should be set to 13,
                //Thread Hook Monitor The mouse message is set to 7, and the global hooks monitor the mouse message to 14. Lpfn The address pointer of the hook subroutine. If the dwThreadId parameter is 0 or is created by another process
                //The identity of the thread, lpfn must point to the hook in the DLL. In addition, lpfn can point to the current process of a hook subroutine code. The hook function is the entry address when the hook is hooked to any
                //This function is called after the message. HInstance handle to the application instance. Identifies the DLL containing the subroutine pointed to by lpfn. If threadId identifies the current process to create a thread, and the child
                //The code is in the current process and hInstance must be NULL. You can simply set it as an instance handle for this application. Threaded The identifier of the thread associated with the installed hook subroutine
                //If it is 0, the hook subroutine is associated with all threads, the global hook
                //************************************
                //If SetWindowsHookEx fails
                if (hKeyboardHook == 0)
                {
                    Stop();
                    throw new Exception("Error: Keyboard hook failed :(");
                }
            }
        }

        public void Stop()
        {
            bool retKeyboard = true;

            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }

            if (!(retKeyboard)) throw new Exception("Error: hook unload failed :(");
        }

        // The ToAscii function translates the specified character or character of the specified virtual key and keyboard state
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, // [in] Specifies that the virtual key code is translated.
                                         int uScanCode, // [in] The key to the specified hardware scan code must be translated into english. High-order bit of this value set the key, if it is (not pressure)
                                         byte[] lpbKeyState, // [in] Pointer to a 256-byte array containing the current state of the keyboard. An array of each element (byte) contains a key to the state. If the high order bit of the byte is set, the key is to fall (press). At low bits, if the setting indicates that the key is on the switch. In this function, only the elbow CAPS LOCK key is relevant. NUM locks and scroll lock keys in the switched state are ignored.
                                         byte[] lpwTransKey, // [out] The pointer to the buffer receives the translated character or character.
                                         int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise.

        // Gets the status of the key
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        private const int WM_KEYDOWN = 0x100; // KEYDOWN
        private const int WM_KEYUP = 0x101; // KEYUP
        private const int WM_SYSKEYDOWN = 0x104; // SYSKEYDOWN
        private const int WM_SYSKEYUP = 0x105; // SYSKEYUP

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // Listen to keyboard events
            if ((nCode >= 0) && (KeyDownEvent != null || KeyUpEvent != null || KeyPressEvent != null))
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                // raise KeyDown
                if (KeyDownEvent != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyDownEvent(this, e);

                    // Completely intercepted key
                    if (e.KeyCode == Keys.F12)
                    {
                        return 1;
                    }
                }

                // Keyboard press
                if (KeyPressEvent != null && wParam == WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);

                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkCode, MyKeyboardHookStruct.scanCode, keyState, inBuffer, MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        KeyPressEvent(this, e);
                    }
                }

                // Keyboard lift
                if (KeyUpEvent != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyUpEvent(this, e);
                }

            }
            // If it returns 1, the message ends, and the message is no longer passed.
            // If you return 0 or call the CallNextHookEx function, the message goes out of this hook and continues to pass, that is, to the true recipient of the message
            if (Go)
            {
                return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
            }
            else
            {
                return 1;
            }
        }

        ~KeyboardHook()
        {
            Stop();
        }
    }
}