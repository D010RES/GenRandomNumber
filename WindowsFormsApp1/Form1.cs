using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }
        private void btnGenerate_Click(object sender, EventArgs e) {
            long randomTick, result;
            int min, max;
            listBox1.Items.Clear();
            List<long> myList = new List<long>();
            min = int.Parse(txtMin.Text);
            max = int.Parse(txtMax.Text);
            int count = int.Parse(txtCount.Text.ToString());
            for (int i = 1; i < count; i++) {
            runAgaing:
                randomTick = DateTime.Now.Millisecond;
                randomTick = randomTick % max;
                randomTick = (9463 * randomTick + 5461) % 233280;
                result = randomTick % max + min;
                if (!(result > min && result < max)) {
                    goto runAgaing;
                }
                myList.Add(result);
                Thread.Sleep(1);
            }
            foreach (var item in myList) {
                listBox1.Items.Add(item);
            }
            fillChart(myList);
        }
        private void fillChart(List<long> numbers) {
            CleanChart();
            var myDict = new Dictionary<long, int>();
            
            foreach (var number in numbers) {
                int count = numbers.Count(i=> i == number);
                myDict[number] = count;   
            }

            foreach(KeyValuePair<long, int> entry in myDict) {
               chrtRandomNumber.Series["Rep"].Points.AddXY(entry.Key, entry.Value);
            } 
        }

        private void CleanChart() { 
            foreach (var series in chrtRandomNumber.Series) {
                series.Points.Clear();
            }
        }
        private void Form1_Load(object sender, EventArgs e) {
            txtCount.Text = "30";
            txtMax.Text = "100000";
            txtMin.Text = "50000";
            chrtRandomNumber.Titles.Add("تعداد دفعات تکرار اعداد تصادفی");

        }
        private void fontDialog1_Apply(object sender, EventArgs e) {

        }
    }
}
