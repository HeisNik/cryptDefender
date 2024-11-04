using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cryptDefender
{
    public partial class PortSkanner : Form
    {
        private TextBox? textBoxIp;
        private TextBox? textBoxStartPort;
        private TextBox? textBoxEndPort;
        private Button? buttonScan;
        private Button? buttonStop;
        private ListBox? listBoxOpenPorts;
        private ListBox? listBoxClosedPorts;
        private Label? labelScanTime;
        private Stopwatch? scanStopwatch;
        private CancellationTokenSource? cancellationTokenSource;

        public PortSkanner()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            textBoxIp = new TextBox();
            textBoxStartPort = new TextBox();
            textBoxEndPort = new TextBox();
            buttonScan = new Button();
            buttonStop = new Button();
            listBoxOpenPorts = new ListBox();
            listBoxClosedPorts = new ListBox();
            labelScanTime = new Label();
            SuspendLayout();
            // 
            // textBoxIp
            // 
            textBoxIp.Location = new Point(30, 59);
            textBoxIp.Name = "textBoxIp";
            textBoxIp.Size = new Size(280, 55);
            textBoxIp.TabIndex = 0;
            textBoxIp.Text = "127.0.0.1";
            // 
            // textBoxStartPort
            // 
            textBoxStartPort.Location = new Point(30, 147);
            textBoxStartPort.Name = "textBoxStartPort";
            textBoxStartPort.Size = new Size(133, 55);
            textBoxStartPort.TabIndex = 1;
            textBoxStartPort.Text = "1";
            // 
            // textBoxEndPort
            // 
            textBoxEndPort.Location = new Point(227, 147);
            textBoxEndPort.Name = "textBoxEndPort";
            textBoxEndPort.Size = new Size(151, 55);
            textBoxEndPort.TabIndex = 2;
            textBoxEndPort.Text = "100";
            // 
            // buttonScan
            // 
            buttonScan.Location = new Point(30, 240);
            buttonScan.Name = "buttonScan";
            buttonScan.Size = new Size(150, 71);
            buttonScan.TabIndex = 3;
            buttonScan.Text = "Scan Ports";
            buttonScan.Click += buttonScan_Click;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Location = new Point(279, 234);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(150, 83);
            buttonStop.TabIndex = 4;
            buttonStop.Text = "Stop";
            buttonStop.Click += buttonStop_Click;
            // 
            // listBoxOpenPorts
            // 
            listBoxOpenPorts.FormattingEnabled = true;
            listBoxOpenPorts.ItemHeight = 48;
            listBoxOpenPorts.Location = new Point(30, 351);
            listBoxOpenPorts.Name = "listBoxOpenPorts";
            listBoxOpenPorts.Size = new Size(344, 484);
            listBoxOpenPorts.TabIndex = 5;
            // 
            // listBoxClosedPorts
            // 
            listBoxClosedPorts.FormattingEnabled = true;
            listBoxClosedPorts.ItemHeight = 48;
            listBoxClosedPorts.Location = new Point(466, 351);
            listBoxClosedPorts.Name = "listBoxClosedPorts";
            listBoxClosedPorts.Size = new Size(332, 484);
            listBoxClosedPorts.TabIndex = 6;
            // 
            // labelScanTime
            // 
            labelScanTime.AutoSize = true;
            labelScanTime.Location = new Point(30, 932);
            labelScanTime.Name = "labelScanTime";
            labelScanTime.Size = new Size(429, 48);
            labelScanTime.TabIndex = 7;
            labelScanTime.Text = "Estimated Scan Time: N/A";
            // 
            // PortSkanner
            // 
            ClientSize = new Size(1289, 1030);
            Controls.Add(textBoxIp);
            Controls.Add(textBoxStartPort);
            Controls.Add(textBoxEndPort);
            Controls.Add(buttonScan);
            Controls.Add(buttonStop);
            Controls.Add(listBoxOpenPorts);
            Controls.Add(listBoxClosedPorts);
            Controls.Add(labelScanTime);
            Name = "PortSkanner";
            Text = "Port Scanner";
            ResumeLayout(false);
            PerformLayout();
        }

        private async void buttonScan_Click(object? sender, EventArgs e)
        {

            if (textBoxIp == null || textBoxStartPort == null || textBoxEndPort == null ||
                buttonScan == null || buttonStop == null ||
                listBoxOpenPorts == null || listBoxClosedPorts == null || labelScanTime == null)
            {
                return;
            }

            string target = textBoxIp.Text;
            int startPort, endPort;

            if (!int.TryParse(textBoxStartPort.Text, out startPort) || !int.TryParse(textBoxEndPort.Text, out endPort))
            {
                MessageBox.Show("Invalid port range. Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listBoxOpenPorts.Items.Clear();
            listBoxClosedPorts.Items.Clear();
            labelScanTime.Text = "Estimated Scan Time: Calculating...";

            buttonScan.Enabled = false;
            buttonStop.Enabled = true;

            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            scanStopwatch = Stopwatch.StartNew();

            await Task.Run(() =>
            {
                Parallel.For(startPort, endPort + 1, port =>
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    bool isOpen = IsPortOpen(target, port);
                    string result = isOpen ? $"Port {port} is open" : $"Port {port} is closed";

                    listBoxOpenPorts.Invoke(new Action(() =>
                    {
                        if (isOpen)
                            listBoxOpenPorts.Items.Add(result);
                        else
                            listBoxClosedPorts.Items.Add(result);
                    }));
                });
            }, cancellationToken);

            labelScanTime.Text = $"Scan completed in: {scanStopwatch.Elapsed.TotalSeconds:F2} seconds";
            scanStopwatch.Stop();

            buttonScan.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void buttonStop_Click(object? sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        private bool IsPortOpen(string target, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(target, port);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}

