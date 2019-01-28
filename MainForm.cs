/*
 * CoderScreenSaver
 * Simple screen saver will type some code.
 *
 * Created by SharpDevelop.
 * User: Enikeishik
 * Date: 12.12.2017
 * Time: 10:44
 * 
 * @copyright   Copyright (C) 2017-2018 Enikeishik <enikeishik@gmail.com>. All rights reserved.
 * @author      Enikeishik <enikeishik@gmail.com>
 * @license     GNU General Public License version 2 or later; see LICENSE.txt
 */
using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Text;
using System.IO;

namespace CoderScreenSaver
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        protected Thread drawerThread;
        
        protected Panel panel;
        
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            panel = new Panel();
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Name = "panel";
            panel.Size = new System.Drawing.Size(320, 320);
            panel.TabIndex = 0;
            this.Controls.Add(panel);
        }
        
        void MainFormLoad(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Cursor.Hide();
        }
        
        void MainFormKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Escape) {
                Application.Exit();
            }
        }
        
        protected string[] GetContent()
        {
            return File.ReadAllLines(Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), 
                "..\\..\\MainForm.cs"
            ));
            /*
            Type t = this.GetType();
            MemberInfo[] drawerMembers = t.GetMembers();
            t = panel.GetType();
            MemberInfo[] formMembers = t.GetMembers();
            
            StringBuilder sb = new StringBuilder(100 * drawerMembers.Length + 100 * formMembers.Length);
            foreach (MemberInfo m in drawerMembers) {
                sb.Append(m.ToString() + "Some toooooooooooooooo veeeeeeeeeeeeery loooooooooooooong line" + Environment.NewLine);
            }
            foreach (MemberInfo m in formMembers) {
                sb.Append(m.ToString() + Environment.NewLine);
            }
            return sb.ToString().Split(
                new string[] { Environment.NewLine }, 
                StringSplitOptions.None
            );
            */
        }
        
        void MainFormShown(object sender, EventArgs e)
        {
            Application.DoEvents();
            Drawer drawer = new Drawer(panel, GetContent());
            try {
                drawer.Draw();
            // disable once EmptyGeneralCatchClause
            } catch (Exception ex) {
                Cursor.Show();
                MessageBox.Show(ex.Message);
                Close();
            }
        }
        
        void MainFormPaint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
