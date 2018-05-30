using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeviceManager.Core;
using DeviceManager.Mapping;
using DeviceManager.Model;
using IWshRuntimeLibrary;
using NewLife.Log;
using NewLife.Serialization;
using NewLife.Xml;

namespace DeviceManager
{
    public partial class Form1 : Form
    {
        private TranslateForm translateForm;

        private Motorparams exp;
        public Form1()
        {
            //Init();

            InitializeComponent();
            this.Name = "DeviceManager";

            XCode.Setting.Current.EntityCacheExpire =2;
            XCode.Setting.Current.SingleCacheExpire = 2;
            AllInit();
        }

        private void Init()
        {
            Motorparams.FromExcel("cc");
            Motorparams.FromExcel("cy");
            Motorparams.FromExcel("hvb");
            Motorparams.FromExcel("ic");
            Motorparams.FromExcel("jc");
            Motorparams.FromExcel("mf");
            Motorparams.FromExcel("vb");
            Motorparams.FromExcel("vc");
            Motorparams.FromExcel("pul");
        }
        private void physicType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var MotorTypeId = (this.motorType.SelectedItem as Motortype).MotorTypeId;
            var physicTypeId = (this.physicType.SelectedItem as Physicfeature).Id;
            var param = this.txtParam.Text;
            var desc = this.txtDesc.Text;

            if(this.btnAdd.Text.EqualIgnoreCase("添 加"))
            {
                var result = MessageBox.Show(this, "确定需要删除此记录？请与软件部门联系后再点击确定！否则后果自负！", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    return;
                }
                return;
                if (param.IsNullOrWhiteSpace())
                {
                    MessageBox.Show($@"{param}不能为空！");
                    return;
                }
                if (desc.IsNullOrWhiteSpace())
                {
                    MessageBox.Show($@"{desc}不能为空！");
                    return;
                }
                var exsit1 = Motorparams.FindByParam(MotorTypeId, param);
                var exsit2 = Motorparams.FindByDesc(MotorTypeId, desc);

                if (exsit1.Any())
                {
                    MessageBox.Show($@"设备参数已存在！请重新编辑！");
                    return;
                }
                if (exsit2.Any())
                {
                    MessageBox.Show($@"设备参数描述已存在！请重新编辑！");
                    return;
                }
                var filedParam = new Motorparams()
                {
                    Param = param,
                    Description = desc,
                    MotorTypeId = MotorTypeId,
                    Time = DateTime.Now
                };
                filedParam.Insert();

                MessageBox.Show($@"添加成功！");
            }
            if (this.btnAdd.Text.EqualIgnoreCase("保 存"))
            {
                if (param.IsNullOrWhiteSpace())
                {
                    MessageBox.Show($@"{param}不能为空！");
                    return;
                }
                if (desc.IsNullOrWhiteSpace())
                {
                    MessageBox.Show($@"{desc}不能为空！");
                    return;
                }
    
                var exsit1 = Motorparams.FindByParam(MotorTypeId, param);
                var exsit2 = Motorparams.FindByDesc(MotorTypeId, desc);

                if (exsit1.Any()&&exsit1.Count==2)
                {
                    MessageBox.Show($@"设备参数已存在！请重新编辑！");
                    return;
                }
                if (exsit2.Any() && exsit2.Count == 2)
                {
                    MessageBox.Show($@"设备参数描述已存在！请重新编辑！");
                    return;
                }
                exp.Param = param;
                exp.Description = desc;
                exp.MotorTypeId = MotorTypeId;
                exp.PhysicId = physicTypeId;
                exp.Time = DateTime.Now;

                exp.Update();
                MessageBox.Show($@"修改成功！");
                this.btnAdd.Text = "添 加";
            }
    
            this.txtDesc.Clear();
            this.txtParam.Clear();
            ListInit();
        }

        private void AllInit()
        {
            MotorTypeInit();
            PhysicTypeInit();
            ListInit();
        }
    


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lv in listView1.SelectedItems)
            {
                if (lv.Selected)
                {
                    var MotorTypeCode = "";
                    var physicCode = "";
                    if (!lv.SubItems[2].Text.IsNullOrWhiteSpace())
                    {
                        var motorT = Motortype.Find("MotorTypeId", lv.SubItems[3].Text);
                        if (motorT != null)
                        {
                            motorType.SelectedValue = motorT.MotorTypeId; //根据索引修改选中项
                            motorType.SelectedItem = motorT; //根据Key得到选中项

                            MotorTypeCode = motorT.MotorTypeId;
                        }
                    }
                    var phy = Physicfeature.Find("Id", lv.SubItems[2].Text);
                    if (phy != null)
                    {

                        physicType.SelectedValue = phy.Id;    //根据索引修改选中项
                        physicType.SelectedItem = phy; //根据Key得到选中项
                        physicCode = phy.PhysicType;
                    }

                    this.txtParam.Text = lv.Text;
                    this.txtDesc.Text = lv.SubItems[1].Text;

                    exp = Motorparams.FindByParam(MotorTypeCode, lv.Text).SingleOrDefault();
                }
            

            }
        }



        private void ListInit()
        {
            this.listView1.BeginUpdate();
            this.listView1.Items.Clear();
            var paramList = Motorparams.FindAll();
            foreach (var param in paramList)
            {
                var strings = new string[]
               {
                    param.Param,param.Description,param.PhysicId.ToString(),param.MotorTypeId,param.Time.ToString()
               };
                var listViewItem = new ListViewItem(strings) { Tag = param };
                this.listView1.Items.Add(listViewItem);
            }
            
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        private void MotorTypeInit()
        {
            var types = Motortype.FindAll();

            this.motorType.DataSource = types;
            this.motorType.ValueMember = "MotorTypeId";
            this.motorType.DisplayMember = "MotorTypeName";
        }

        private void PhysicTypeInit()
        {
            var types = Physicfeature.FindAll();

            this.physicType.DataSource = types;
            this.physicType.ValueMember = "Id";
            this.physicType.DisplayMember = "PhysicType";
        }
        /// <summary>
        /// 参考文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                var path =AppDomain.CurrentDomain.BaseDirectory+ @"Dll\CabinetParamter.xls";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Messagebox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Seacher();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        private void Seacher()
        {
            var txt = this.txtKey.Text;
            var MotorTypeId = (this.motorType.SelectedItem as Motortype).MotorTypeId;
            //var physicType = (this.physicType.SelectedItem as Physicfeature).PhysicType;

            IList<Motorparams> list = null;
            //if (txt.IsEnlish())
            //{
            //    //list = Motorparams.FindByParam(MotorTypeId, txt);
            //}

            //if (txt.IsChina())
            //{
                
            //}
            list = Motorparams.FindByDesc(MotorTypeId, txt);
            if (txt.IsNullOrWhiteSpace())
            {
                list = Motorparams.FindAll();

            }
            this.listView1.BeginUpdate();
            this.listView1.Items.Clear();
        
            if (list != null)
            {
                var paramList = list;
                foreach (var param in paramList)
                {
                    var strings = new string[]
                    {
                        param.Param, param.Description,  param.MotorTypeId, param.Time.ToString()
                    };
                    var listViewItem = new ListViewItem(strings) {Tag = param};
                    this.listView1.Items.Add(listViewItem);
                }
            }
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 创建桌面快捷方式并开机启动的方法

            //获取当前系统用户启动目录
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            //获取当前系统用户桌面目录
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            FileInfo fileStartup = new FileInfo(startupPath + "\\DeviceManager.exe.lnk");
            FileInfo fileDesktop = new FileInfo(desktopPath + "\\DeviceManager.exe.lnk");

            if (!fileDesktop.Exists)
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(
                      Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) +
                       "\\" + "DeviceManager.exe.lnk"
                       );
                shortcut.TargetPath = Application.StartupPath + "\\" + "DeviceManager.exe";//启动更新程序★
                shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
                shortcut.WindowStyle = 1;
                shortcut.Description = "设备管理工具";
                shortcut.IconLocation = Application.ExecutablePath;
                shortcut.Save();
            }

            if (!fileStartup.Exists)
            {
                //获取可执行文件快捷方式的全部路径
                string exeDir = desktopPath + "\\DeviceManager.exe.lnk";
                //把程序快捷方式复制到启动目录
                //System.IO.File.Copy(exeDir, startupPath + "\\DeviceDebugTool.exe.lnk", true);
            }
            #endregion
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            Seacher();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListInit();
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            if (translateForm == null || translateForm.IsDisposed)   //注意先判断null，再判断IsDisposed，不能先判断IsDisposed
            {
                translateForm = new TranslateForm();
                translateForm.Show();
            }
            else
            {
                translateForm.Show();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.btnAdd.Text = "保 存";
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var result=MessageBox.Show(this, "确定需要删除此记录？请与软件部门联系后再点击确定！否则后果自负！", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                //    exp.Delete();
                //    ListInit();
                //}
                return;
            }
            return;
        }

        private void btnJsonIn_Click(object sender, EventArgs e)
        {
            var info = GetJsonInfo("File\\configure.v0.2.1.json");
            var phsics = info.phy;
            if(phsics.Any())
                phsics.ForEach(p =>
                {
                    if(Physicfeature.Find("Id",p.id)==null)
                        new Physicfeature()
                        {
                            Accur = p.accur,
                            Id = p.id,
                            Format = p.format,
                            Unit = p.unit,
                            PhysicType = p.name,
                            Time = DateTime.Now
                        }.SaveAsync();


                    if (p.func!=null&&p.func.Any())
                    {
                        p.func.ForEach(f =>
                        {
                            var paramList=Motorparams.FindAll("Param", f);
                            if (paramList == null||!paramList.Any()) return;
                            var list = paramList;
                            foreach (var t in list)
                            {
                                var update=paramList.FirstOrDefault(x=> x.Id ==t.Id);
                                update.PhysicId = p.id;
                                update.SaveAsync();
                            }
                          
                        });
                    }
                });
            MessageBox.Show("配置初始化完成！");

        }

        #region private extend method

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="jsonPath"></param>
        public static ConfigureJsonInfo GetJsonInfo(string jsonPath)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + jsonPath;
                if (!System.IO.File.Exists(path)) return new ConfigureJsonInfo();
                var fs = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
                var sr = new StreamReader(fs);
                var text = sr.ReadToEnd();
                return text.ToJsonEntity<ConfigureJsonInfo>();
            }
            catch (Exception e)
            {
                XTrace.Log.Error(e.Message);
                // throw;
                return new ConfigureJsonInfo();
            }


        }

        #endregion

    }

}
