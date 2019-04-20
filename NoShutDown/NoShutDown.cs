using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NoShutdown
{
    public partial class NoShutDown : Form
    {
        [DllImport("user32.dll")]
        public extern static bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

        public NoShutDown()
        {
            InitializeComponent();
            //ShutdownBlockReasonCreate(Handle, "Don't let you shut down :P");
        }

        protected override void WndProc(ref Message aMessage)
        {
            const int WM_QUERYENDSESSION = 0x0011;
            const int WM_ENDSESSION = 0x0016;

            if (aMessage.Msg == WM_QUERYENDSESSION || aMessage.Msg == WM_ENDSESSION)
                return;

            base.WndProc(ref aMessage);
        }

        protected override CreateParams CreateParams //Hide from Task Manager
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }
    }
}