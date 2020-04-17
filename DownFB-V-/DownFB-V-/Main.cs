using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Compiler;

namespace DownFB_V_
{
    public partial class Main : Form  
    {
        StreamWriter stdin = null;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void ghostButton1_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void ghostButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void StartCmdProcess()
        {
            ProcessStartInfo pStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "start /WAIT",
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            Process cmdProcess = new Process
            {
                StartInfo = pStartInfo,
                EnableRaisingEvents = true,
            };

            cmdProcess.Start();
            cmdProcess.BeginErrorReadLine();
            cmdProcess.BeginOutputReadLine();
            stdin = cmdProcess.StandardInput;

            cmdProcess.OutputDataReceived += (s, evt) =>
            {
                if (evt.Data != null)
                {
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        textBox1.AppendText(evt.Data + Environment.NewLine);
                        textBox1.ScrollToCaret();
                        ghostTextBox2.Text = "";
                    }));
                }
            };

            cmdProcess.ErrorDataReceived += (s, evt) =>
            {
                if (evt.Data != null)
                {
                    BeginInvoke(new Action(() =>
                    {
                        //rtbStdErr.AppendText(evt.Data + Environment.NewLine);
                        //rtbStdErr.ScrollToCaret();
                    }));
                }
            };

            cmdProcess.Exited += (s, evt) =>
            {
                // cmdProcess?.Dispose();
            };
        }

        private void ghostButton3_Click(object sender, EventArgs e)
        {
          

            textBox1.ForeColor = Color.LawnGreen;
            textBox1.Text = "";
            StartCmdProcess();
            //stdin.WriteLine();
           stdin.Write(ghostTextBox2.Text + Environment.NewLine);
            stdin.WriteLine();


           }
    }
}
