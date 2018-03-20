namespace DeviceManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.motorType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.physicType = new System.Windows.Forms.ComboBox();
            this.txtParam = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Param = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Physic = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MotorTypeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.menuOperate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOperate.SuspendLayout();
            this.SuspendLayout();
            // 
            // motorType
            // 
            this.motorType.DisplayMember = "MotorTypeName";
            this.motorType.FormattingEnabled = true;
            this.motorType.Location = new System.Drawing.Point(169, 75);
            this.motorType.Name = "motorType";
            this.motorType.Size = new System.Drawing.Size(121, 20);
            this.motorType.TabIndex = 0;
            this.motorType.ValueMember = "MotorType";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "电机设备类型：";
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(374, 78);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(65, 12);
            this.lb2.TabIndex = 3;
            this.lb2.Text = "物理特性：";
            // 
            // physicType
            // 
            this.physicType.DisplayMember = "PhysicType";
            this.physicType.FormattingEnabled = true;
            this.physicType.Location = new System.Drawing.Point(445, 75);
            this.physicType.Name = "physicType";
            this.physicType.Size = new System.Drawing.Size(121, 20);
            this.physicType.TabIndex = 2;
            this.physicType.ValueMember = "Id";
            this.physicType.SelectedIndexChanged += new System.EventHandler(this.physicType_SelectedIndexChanged);
            // 
            // txtParam
            // 
            this.txtParam.Location = new System.Drawing.Point(169, 121);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(121, 21);
            this.txtParam.TabIndex = 4;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(445, 121);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(121, 21);
            this.txtDesc.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "参数（英文）：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(398, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "描述：";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(611, 119);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "添 加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Param,
            this.Desc,
            this.Physic,
            this.MotorTypeId,
            this.Time});
            this.listView1.ContextMenuStrip = this.menuOperate;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(76, 228);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(610, 286);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Param
            // 
            this.Param.Text = "参数";
            this.Param.Width = 122;
            // 
            // Desc
            // 
            this.Desc.Text = "描述";
            this.Desc.Width = 122;
            // 
            // Physic
            // 
            this.Physic.Text = "物理特性";
            this.Physic.Width = 100;
            // 
            // MotorTypeId
            // 
            this.MotorTypeId.Text = "电机设备类型";
            this.MotorTypeId.Width = 122;
            // 
            // Time
            // 
            this.Time.Text = "时间";
            this.Time.Width = 144;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(611, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "参考文档";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtKey
            // 
            this.txtKey.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKey.Location = new System.Drawing.Point(479, 201);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(121, 21);
            this.txtKey.TabIndex = 11;
            this.txtKey.TextChanged += new System.EventHandler(this.txtKey_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(611, 199);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "搜 索";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(76, 199);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "刷 新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(307, 119);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(58, 23);
            this.btnTranslate.TabIndex = 14;
            this.btnTranslate.Text = "翻 译";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // menuOperate
            // 
            this.menuOperate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuOperate.Name = "menuOperate";
            this.menuOperate.Size = new System.Drawing.Size(153, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "编 辑";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "删 除";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 546);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.physicType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.motorType);
            this.Name = "Form1";
            this.Text = "设备配置";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuOperate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox motorType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.ComboBox physicType;
        private System.Windows.Forms.TextBox txtParam;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Param;
        private System.Windows.Forms.ColumnHeader Desc;
        private System.Windows.Forms.ColumnHeader Physic;
        private System.Windows.Forms.ColumnHeader MotorTypeId;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.ContextMenuStrip menuOperate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}

