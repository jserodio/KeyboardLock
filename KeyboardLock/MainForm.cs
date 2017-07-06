using System;
using System.Windows.Forms;

namespace KeyboardLock
{
    public partial class MainForm : Form
    {
        private bool lockKeyboard = false;              // Keyboard status flag
        private KeyboardHook k_hook = new KeyboardHook();

        public MainForm()
        {
            InitializeComponent();

            k_hook.KeyDownEvent += new KeyEventHandler(Hook_KeyDown); // Hook the key to press 

            k_hook.Start();
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (Properties.Settings.Default.shortcut)
            {
                if (e.KeyCode == Keys.F12)
                {
                    if (!lockKeyboard)
                    {
                        Lock("Unlock", "The keyboard is locked.", false, true);
                    }
                    else
                    {
                        Lock("Lock", "The keyboard is not locked yet.", true, false);
                    }

                }
            }
        }

        private void BtnKeyboardLock_Click(object sender, EventArgs e)
        {
            if (!lockKeyboard)
            {
                Lock("Unlock", "The keyboard is locked.", false, true);
            }
            else
            {
                Lock("Lock", "The keyboard is not locked yet.", true, false);
            }
        }

        /// <summary>
        /// Lock the keyboard or unlock the keyboard
        /// </summary>
        /// <param name="BtnText">Key text</param>
        /// <param name="LabelText">Prompt text</param>
        /// <param name="hook_state">Hook state</param>
        /// <param name="keyboard_state">Keyboard status</param>
        private void Lock(string BtnText, string LabelText, bool hook_state, bool keyboard_state)
        {
            BtnKeyboardLock.Text = BtnText;
            Label.Text = LabelText;
            k_hook.Go = hook_state;
            lockKeyboard = keyboard_state;
        }
                
        ~MainForm()
        {
            k_hook.Stop();
        }

        /// <summary>
        /// Event: Double click in the tray icon brings back the app to the front
        /// </summary>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            //this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Event: Change behaviour, closing now minimizes to the tray icon in the taskbar menu and prevents app from closing.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.Hide();
                //this.WindowState = FormWindowState.Minimized;
            }
            e.Cancel = true; // Stop the app from terminating.
        }


        /// <summary>
        /// Event: CheckBox reads from Properties.Settings the 'shortcut' variable. If it's True, untick the checkbox; If it's False, tick. Saves the new values to the Properties.Settings.
        /// </summary>
        private void checkBox1_Click(object sender, EventArgs e)
        {
            this.checkBox1.Checked = !Properties.Settings.Default.shortcut; // if !true = false or !false = true
            Properties.Settings.Default.shortcut = !Properties.Settings.Default.shortcut; // if !true = false or !false = true
            Properties.Settings.Default.Save();
        }
        

        /// <summary>
        /// Event: Display the context menu with left click.
        /// </summary>
        //private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        //{
        //    contextMenuStrip1.Show(Control.MousePosition);
        //}

        /// <summary>
        /// Event: Exit in the tray icons context menu.
        /// </summary>
        private void exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Event: Brings back the app.
        /// </summary>
        private void open_Click(object sender, EventArgs e)
        {
            this.Show();
            //this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// On main load, gets the current value from the Properties.Settings and loads the checkbox with the value.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.checkBox1.Checked = Properties.Settings.Default.shortcut; // true or false
        }


    }
}