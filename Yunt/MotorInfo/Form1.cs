using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotorInfo.Models;
using NewLife.Log;

namespace MotorInfo
{
    
    public partial class Form1 : Form
    {
        private string CollectDevice = "";
        private string Productionline = "";
        private string UpdateMotorId;
        public Form1()
        {
            InitializeComponent();
            ListInit();
        }

        private void ListInit()
        {
            //this.txtCollectDevice.Text = "";
            //this.txtProductionline = "WDD-P001";
            this.listView1.BeginUpdate();
            this.listView1.Items.Clear();
            //this.listView1.Items.Clear();
            var motorList = Motor.FindAll();
            foreach (var motor in motorList)
            {

                var strings = new string[]
                 {
                    motor.Name,motor.SerialNumber,motor.StandValue.ToString(),motor.MotorPower.ToString(),
                    motor.ProductSpecification,motor.FeedSize.ToString(),motor.FinalSize.ToString(),motor.MotorTypeId,motor.MotorId,motor.ProductionLineId,
                    motor.EmbeddedDeviceId.ToString()
                 };
                var listViewItem = new ListViewItem(strings) { Tag = motor };
                this.listView1.Items.Add(listViewItem);
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lv in listView1.SelectedItems)
            {
                if (lv.Selected)
                {
                    this.txtName.Text = lv.Text;
                    this.txtSerialNum.Text = lv.SubItems[1].Text;
                    this.txtStandValue.Text = lv.SubItems[2].Text;
                    this.txtPower.Text = lv.SubItems[3].Text;
                    this.txtProType.Text = lv.SubItems[4].Text;
                    this.txtFeedSize.Text = lv.SubItems[5].Text;
                    this.txtFinalSize.Text = lv.SubItems[6].Text;
                    this.txtMotorType.Text = lv.SubItems[7].Text;
                    this.txtMotorId.Text = lv.SubItems[8].Text;
                  
                    this.txtProductionline.Text =lv.SubItems[9].Text;
                    this.txtCollectDevice.Text = lv.SubItems[10].Text;
                    UpdateMotorId = this.txtMotorId.Text;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var serialNum = this.txtSerialNum.Text;
            var standValue = this.txtStandValue.Text;
            var power = this.txtPower.Text;
            var proType = this.txtProType.Text;
            var feed = this.txtFeedSize.Text;
            var final = this.txtFinalSize.Text;


            if (serialNum.IsNullOrWhiteSpace())
            {
               // MessageBox.Show($@"{serialNum}不能为空！");
                //return;
                serialNum = "";
            }
            if (standValue.IsNullOrWhiteSpace())
            {
                //MessageBox.Show($@"{standValue}不能为空！");
                //return;
                standValue = "0";
            }
            if (power.IsNullOrWhiteSpace())
            {
                //MessageBox.Show($@"{power}不能为空！");
                //return;
                power = "0";
            }
            if (proType.IsNullOrWhiteSpace())
            {
                //MessageBox.Show($@"{proType}不能为空！");
                //return;
                proType = "";
            }
            if (feed.IsNullOrWhiteSpace())
            {
                //MessageBox.Show($@"{feed}不能为空！");
                //return;
                feed = "0";
            }
            if (final.IsNullOrWhiteSpace())
            {
                //MessageBox.Show($@"{final}不能为空！");
                //return;
                final = "0";
            }
            try
            {
                var motor = Motor.Find("MotorId", UpdateMotorId);
                motor.SerialNumber = serialNum;
                motor.StandValue = Convert.ToSingle(standValue);
                motor.MotorPower = Convert.ToSingle(power);
                motor.ProductSpecification = proType;
                motor.FeedSize = Convert.ToSingle(feed);
                motor.FinalSize = Convert.ToSingle(final);
      
                motor.SaveAsync();

                MessageBox.Show($@"修改成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"非数字类型{ex.Message}");
            }

            this.txtName.Clear();
            this.txtSerialNum.Clear();
            this.txtStandValue.Clear();
            this.txtPower.Clear();
            this.txtProType.Clear();
            this.txtFeedSize.Clear();
            this.txtFinalSize.Clear();
            this.txtMotorType.Clear();
            this.txtMotorId.Clear();
            this.txtCollectDevice.Clear();
            this.txtProductionline.Clear();
            ListInit();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var motorInfo = Motor.FindAll().FirstOrDefault(x => x.StandValue == 0);
            if (motorInfo != null)
            {
                this.txtName.Text = motorInfo.Name;
                this.txtSerialNum.Text = motorInfo.SerialNumber;
                this.txtStandValue.Text = motorInfo.StandValue.ToString();
                this.txtPower.Text = motorInfo.MotorPower.ToString();
                this.txtProType.Text = motorInfo.ProductSpecification;
                this.txtFeedSize.Text = motorInfo.FeedSize.ToString();
                this.txtFinalSize.Text = motorInfo.FinalSize.ToString();
                this.txtMotorType.Text = motorInfo.MotorTypeId;
                this.txtMotorId.Text = motorInfo.MotorId;
                this.txtCollectDevice.Text = motorInfo.ProductionLineId;
                this.txtProductionline.Text = motorInfo.ProductionLineId;
            }

        }
        /// <summary>
        /// 从excel中导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var stream=new StreamReader("./File/motorinfo.txt",Encoding.Default))
            {
                //排除第一行
                stream.ReadLine();
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if(line.IsNullOrWhiteSpace())
                        continue;
                    var str= line.Split(new char[] { ' ', '\t', ';' }, StringSplitOptions.RemoveEmptyEntries);
                  
                    var motorId = str[3];
                    var motor = Motor.Find("MotorId", motorId);
                    if (motor == null)
                    {
                        XTrace.Log.Error($"不存在该电机设备：{motorId}");
                        continue;
                    }
                    if(!str[4].IsNullOrWhiteSpace())
                        motor.MotorPower = Convert.ToSingle(str[4]);
                    if (!str[8].IsNullOrWhiteSpace())
                        motor.StandValue = Convert.ToSingle(str[8]);
                    if (!str[9].IsNullOrWhiteSpace())
                        motor.IsBeltWeight = Convert.ToInt64(str[9]);
                    if (!str[10].IsNullOrWhiteSpace())
                        motor.IsMainBeltWeight = Convert.ToInt64(str[10]);
                    if (str.Length == 12)
                    {
                        if (!str[11].IsNullOrWhiteSpace())
                            motor.SerialNumber =str[11];
                    }
                    motor.SaveAsync();
                }
            }
        }
    }
}
