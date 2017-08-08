using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace GeometryAI
{
    public class MouseMacro
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public struct POINT
        {
            public int x;
            public int y;
        };

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        };

        private POINT GetMousePosition()
        {
            POINT point;
            GetCursorPos(out point);
            return point;
        }

        public void LeftDown()
        {
            POINT point = GetMousePosition();
            mouse_event((int)MouseEventFlags.LEFTDOWN, point.x, point.y, 0, 0);
        }

        public void LeftUp()
        {
            POINT point = GetMousePosition();
            mouse_event((int)MouseEventFlags.LEFTUP, point.x, point.y, 0, 0);
        }
    }
}
