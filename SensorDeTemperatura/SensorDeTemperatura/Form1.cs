using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace SensorDeTemperatura
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        public Form1()
        {
            InitializeComponent();
            // Initialize SerialPort
            //serialPort = new SerialPort("COM7", 9600); // Replace with your COM port
            serialPort = new SerialPort();
            serialPort.BaudRate = 9600;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            //serialPort.Open();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            /*string data = serialPort.ReadLine(); // Read the incoming data
            this.Invoke(new MethodInvoker(delegate {
                label3.Text = data; // Update label with temperature value
            }));*/
            try
            {
                string data = serialPort.ReadLine(); // Read the incoming data
                this.Invoke(new MethodInvoker(delegate {
                    label3.Text = data; // Update label with temperature value
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            buscapuerto();
        }

        private void buscapuerto()
        {
            try
            {
                comboBox1.Items.Clear();
                foreach(string puerto in SerialPort.GetPortNames())
                    comboBox1.Items.Add(puerto);
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
                else
                    MessageBox.Show("No hay puertos disponibles.");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnConectar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    // Set the selected port name from the ComboBox
                    serialPort.PortName = comboBox1.Text;
                    serialPort.Open();

                    if (serialPort.IsOpen)
                    {
                        label6.Text = "Conectado"; // Update connection status
                    }
                    else
                    {
                        MessageBox.Show("Conexión Fallida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {

        }

        private void btnDesconectar_Click_1(object sender, EventArgs e)
        {
            /*serialPort1.Close();
            label6.Text = "Desconectado";*/
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    label6.Text = "Desconectado"; // Update connection status
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            Close();
        }
    }
}
