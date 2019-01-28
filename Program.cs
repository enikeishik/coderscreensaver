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

namespace CoderScreenSaver
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        
    }
}
