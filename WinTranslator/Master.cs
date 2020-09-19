using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Serilog;

namespace WinTranslator
{
    public partial class Master : Form
    {
        public Master()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRoundedRectangle(new SolidBrush(Color.White), 20, 20, this.Width - 40, this.Height - 60, 30);
            SolidBrush brush = new SolidBrush(
                Color.White
            );
            g.FillRoundedRectangle(brush, 12, 12, this.Width - 44, this.Height - 64, 10);
            g.DrawRoundedRectangle(new Pen(ControlPaint.Light(Color.White, 0.00f)), 12, 12, this.Width - 44,
                this.Height - 64, 10);
            g.FillRoundedRectangle(new SolidBrush(Color.White), 12, 12 + ((this.Height - 64) / 2), this.Width - 44,
                (this.Height - 64) / 2, 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.Information("Setting up the hook");
            HookToKeyboard();
        }

        private void HookToKeyboard()
        {
            Hook.GlobalEvents().KeyDown += async (s, e) =>
            {
                if (e.Control && e.Shift && e.KeyCode == Keys.T)
                {
                    Log.Information("Sending command to keyboard to copy text.");
                    KeyboardSend.KeyDown(Keys.Control);
                    KeyboardSend.KeyDown(Keys.C);

                    
                    ReadFromClipboard();
                }
            };
        }

        private void ReadFromClipboard()
        {
            Log.Information("Readying from clipboard.");
            if (Clipboard.ContainsText())
            {
                textBox1.Text = Clipboard.GetText();
            }
        }
    }
}