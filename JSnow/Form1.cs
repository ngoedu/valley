using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snow.Syntax;
using System.Diagnostics;
using Snow.Syntax.Entity;
using System.IO;
using Snow.XUnitTest;


namespace Snow
{
    public partial class Form1 : Form, Matchable
    {
        public Form1()
        {
            InitializeComponent();

            
            MessageBox.Show("Take it easy, play with it... ;)");

            DirectoryInfo dinfo = new DirectoryInfo(SrcPath);
            FileInfo[] Files = dinfo.GetFiles("*.html");

            foreach (FileInfo file in Files)
            {
                this.cb_sample.Items.Add(file.Name);
            }

        }

        static string SrcPath = Environment.CurrentDirectory.ToString().Replace(@"\bin\Debug", @"\X\Sample");
        static string TestJavascriptContent = string.Empty;
        static string TestAcMachineWords = "function ,if,else,for,while";
        static string SampleFile = @"\Sample3.html";

        


        private void button1_Click(object sender, EventArgs e)
        {
            SampleFile = this.cb_sample.SelectedItem == null ? SampleFile : this.cb_sample.SelectedItem.ToString();
            TestJavascriptContent = TestUtil.ExtractJSContent(TestUtil.GetFileContent(SrcPath, SampleFile));
        	TestFormalizerMethod();		
        }
        
        void Button2Click(object sender, EventArgs e)
		{
        	TestAcMachineMethod();
		}

        public int OnMatch(Source src, string Key, int position)
        {
            richTextBox2.Text += string.Format("key [{0}] was found at {1} \r\n", Key, position);
            return 0;
        }

        private void TestAcMachineMethod()
        {
            richTextBox1.Text = TestJavascriptContent;
        	richTextBox2.Text = TestAcMachineWords + "\r\n==================\r\n";
        	
        	var machine = new Snow.Syntax.Matcher(TestAcMachineWords.Split(','));
            machine.Scan(new Source(TestJavascriptContent.ToCharArray()), this);      	
        }
        
        private void TestFormalizerMethod()
        {
            richTextBox1.Text = TestJavascriptContent;
            var src = new Source(TestJavascriptContent);

            System.Diagnostics.Debug.WriteLine("[{0}] source code to be parsed now...", TestJavascriptContent.Length);

            //C# measure of action performance
            //https://msdn.microsoft.com/en-us/library/windows/desktop/dn553408%28v=vs.85%29.aspx
            var stopWatch = Stopwatch.StartNew();

            //initilaze context
            Context.Init();
            
            //process of the scrip parse
            Formalizer.Process(src, "GLOBAL", Context.GLOBAL);

            stopWatch.Stop();
            System.Diagnostics.Debug.WriteLine("source code was formalized within {0}ms", stopWatch.Elapsed.TotalMilliseconds);

            
            richTextBox2.Text = Context.GLOBAL.ToString();

            //TestASCIIScope(src.cArray);
        }

        /// <summary>
        /// calculate the max and min char in the array
        /// </summary>
        /// <param name="array"></param>
        private void TestASCIIScope(char[] array)
        {
            char max = array[0], min = array[0];
            for (int i=1; i< array.Length; i++)
            {
                max = array[i] > max ? array[i] : max;
                min = array[i] < min ? array[i] : min;
            }
            richTextBox2.Text = string.Format("max={0} , min={1}", Convert.ToInt32(max), Convert.ToInt32(min));
        }

        /// <summary>
        /// https://msdn.microsoft.com/da-dk/library/x9h8tsay.aspx
        /// 
        /// dump char map range from u0000 to uffff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();

            for (int i=0; i< 65535; i++)
                //richTextBox2.Text +=i+ "\t" + (char)i + "\r\n";
                System.Diagnostics.Debug.WriteLine("{0}:\t[{1}]", i, (char)i);
        }
    }
}
