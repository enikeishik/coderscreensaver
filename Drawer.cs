/*
 * CoderScreenSaver
 * Simple screen saver will type some code.
 *
 * Created by SharpDevelop.
 * User: Enikeishik
 * Date: 12.12.2017
 * Time: 11:44
 * 
 * @copyright   Copyright (C) 2017-2018 Enikeishik <enikeishik@gmail.com>. All rights reserved.
 * @author      Enikeishik <enikeishik@gmail.com>
 * @license     GNU General Public License version 2 or later; see LICENSE.txt
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace CoderScreenSaver
{
    /// <summary>
    /// Description of Drawer.
    /// </summary>
    public class Drawer
    {
        protected const float FONT_WIDTH_SCALE = 0.9f;
        protected const int TIME_SCALE = 1;
        protected MainForm form;
        protected Panel canvas;
        protected Graphics formGraphics;
        
        protected Font drawFont = new Font(FontFamily.GenericMonospace, 12);
        protected SolidBrush drawBrush = new SolidBrush(Color.Green);
        protected int xStart = 50;
        protected int yStart = 50;
        protected StringFormat drawFormat = new StringFormat();
        
        protected string[] content;
        
        protected int xOffset;
        protected int yOffset;
        protected int width;
        protected int height;
        protected int maxChars = 80;
        protected int maxLines = 20;
        
        public Drawer(Panel canvas, string[] content)
        {
            this.form = (MainForm) canvas.Parent;
            this.canvas = canvas;
            this.formGraphics = this.canvas.CreateGraphics();
            
            xOffset = (int) Math.Floor(drawFont.Size * FONT_WIDTH_SCALE);
            yOffset = (int) drawFont.Height;
            width = maxChars * xOffset + xStart - xOffset;
            height = maxLines * yOffset + yStart - yOffset;
            if (width < xStart || height < yStart) {
                throw new Exception("Output area too small");
            }
            
            this.content = content;
        }
        
        public void Draw()
        {
            int x = xStart;
            int y = yStart;
            int lineNum = 0;
            int charNum = 0;
            string line = content[lineNum++];
            while (true) {
                Application.DoEvents();
                Thread.Sleep(new Random().Next(1, 20) * TIME_SCALE);
                
                if (line.Length > 0)
                    DrawOnForm(line[charNum++].ToString(), x, y);
                
                if (charNum >= line.Length) {
                    charNum = 0;
                    line = content[lineNum++];
                    if (lineNum >= content.Length)
                        lineNum = 0;
                    x = xStart;
                    y += yOffset;
                    if (y > height) {
                        ShiftUp();
                        y -= yOffset;
                    }
                    continue;
                }
                
                x += xOffset;
                if (x <= width)
                    continue;
                
                x = xStart;
                y += yOffset;
                if (y <= height)
                    continue;
                
                ShiftUp();
                y -= yOffset;
            }
        }
        
        protected void DrawOnForm(string text, int x, int y)
        {
            formGraphics.DrawString(
                text,
                drawFont, 
                drawBrush, 
                x, 
                y, 
                drawFormat
            );
        }
        
        protected void ShiftUp()
        {
            formGraphics.CopyFromScreen(
                xStart, 
                yStart + yOffset, 
                xStart, 
                yStart, 
                new Size(width, height)
            );
            formGraphics.FillRectangle(
                new SolidBrush(Color.Black), 
                xStart, 
                yStart + yOffset * maxLines - yOffset, 
                width, 
                yOffset
            );
        }
    }
}
