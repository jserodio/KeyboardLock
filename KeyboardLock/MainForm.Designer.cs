namespace KeyboardLock
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variables。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up all resources that are being used。
        /// </summary>
        /// <param name="disposing">True if the managed resource should be freed; otherwise false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows The form designer generates the code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Label = new System.Windows.Forms.Label();
            this.TestBox = new System.Windows.Forms.TextBox();
            this.TestBookTip = new System.Windows.Forms.ToolTip(this.components);
            this.BtnKeyboardLock = new System.Windows.Forms.Button();
            this.BtnTip = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.open = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label
            // 
            this.Label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label.Location = new System.Drawing.Point(62, 185);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(161, 13);
            this.Label.TabIndex = 2;
            this.Label.Text = "The keyboard is not locked yet";
            this.Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TestBox
            // 
            this.TestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestBox.Location = new System.Drawing.Point(92, 78);
            this.TestBox.MaxLength = 10;
            this.TestBox.Name = "TestBox";
            this.TestBox.Size = new System.Drawing.Size(100, 20);
            this.TestBox.TabIndex = 1;
            this.TestBookTip.SetToolTip(this.TestBox, "Try to type!");
            // 
            // BtnKeyboardLock
            // 
            this.BtnKeyboardLock.Location = new System.Drawing.Point(92, 129);
            this.BtnKeyboardLock.Name = "BtnKeyboardLock";
            this.BtnKeyboardLock.Size = new System.Drawing.Size(100, 25);
            this.BtnKeyboardLock.TabIndex = 0;
            this.BtnKeyboardLock.Text = "Lock";
            this.BtnTip.SetToolTip(this.BtnKeyboardLock, "Press F12 button");
            this.BtnKeyboardLock.UseVisualStyleBackColor = true;
            this.BtnKeyboardLock.Click += new System.EventHandler(this.BtnKeyboardLock_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open,
            this.exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // open
            // 
            this.open.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(103, 22);
            this.open.Text = "Open";
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.ShowShortcutKeys = false;
            this.exit.Size = new System.Drawing.Size(103, 22);
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(92, 221);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Shortcut (F12)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.BtnKeyboardLock;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 283);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.TestBox);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.BtnKeyboardLock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyboard Lock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.TextBox TestBox;
        private System.Windows.Forms.ToolTip TestBookTip;
        private System.Windows.Forms.Button BtnKeyboardLock;
        private System.Windows.Forms.ToolTip BtnTip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.ToolStripMenuItem open;
    }
}