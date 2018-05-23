namespace MotorInfo
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMotorType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSerialNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStandValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFeedSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFinalSize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtProType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Name1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StandValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MotorPower = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProductSpecification = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FeedSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FinalSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MotorTypeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MotorId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.line = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.collect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label11 = new System.Windows.Forms.Label();
            this.txtCollectDevice = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProductionline = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMotorId = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(336, 192);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保  存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(141, 28);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(120, 21);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "设备名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "设备类型";
            // 
            // txtMotorType
            // 
            this.txtMotorType.Location = new System.Drawing.Point(363, 28);
            this.txtMotorType.Name = "txtMotorType";
            this.txtMotorType.ReadOnly = true;
            this.txtMotorType.Size = new System.Drawing.Size(120, 21);
            this.txtMotorType.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "序列号";
            // 
            // txtSerialNum
            // 
            this.txtSerialNum.Location = new System.Drawing.Point(363, 75);
            this.txtSerialNum.Name = "txtSerialNum";
            this.txtSerialNum.Size = new System.Drawing.Size(120, 21);
            this.txtSerialNum.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(536, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "额定值";
            // 
            // txtStandValue
            // 
            this.txtStandValue.Location = new System.Drawing.Point(595, 75);
            this.txtStandValue.Name = "txtStandValue";
            this.txtStandValue.Size = new System.Drawing.Size(120, 21);
            this.txtStandValue.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(304, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "进料尺寸";
            // 
            // txtFeedSize
            // 
            this.txtFeedSize.Location = new System.Drawing.Point(363, 131);
            this.txtFeedSize.Name = "txtFeedSize";
            this.txtFeedSize.Size = new System.Drawing.Size(120, 21);
            this.txtFeedSize.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(537, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "出料尺寸";
            // 
            // txtFinalSize
            // 
            this.txtFinalSize.Location = new System.Drawing.Point(596, 134);
            this.txtFinalSize.Name = "txtFinalSize";
            this.txtFinalSize.Size = new System.Drawing.Size(120, 21);
            this.txtFinalSize.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(82, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "产品类型";
            // 
            // txtProType
            // 
            this.txtProType.Location = new System.Drawing.Point(141, 131);
            this.txtProType.Name = "txtProType";
            this.txtProType.Size = new System.Drawing.Size(120, 21);
            this.txtProType.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(755, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "电机功率";
            // 
            // txtPower
            // 
            this.txtPower.Location = new System.Drawing.Point(826, 75);
            this.txtPower.Name = "txtPower";
            this.txtPower.Size = new System.Drawing.Size(120, 21);
            this.txtPower.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(755, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "备  注";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(826, 134);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(120, 21);
            this.txtRemark.TabIndex = 17;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name1,
            this.SerialNumber,
            this.StandValue,
            this.MotorPower,
            this.ProductSpecification,
            this.FeedSize,
            this.FinalSize,
            this.MotorTypeId,
            this.MotorId,
            this.line,
            this.collect});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(84, 249);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(862, 307);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 19;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Name1
            // 
            this.Name1.Text = "设备名称";
            this.Name1.Width = 100;
            // 
            // SerialNumber
            // 
            this.SerialNumber.Text = "序列号";
            this.SerialNumber.Width = 91;
            // 
            // StandValue
            // 
            this.StandValue.Text = "负荷值";
            this.StandValue.Width = 61;
            // 
            // MotorPower
            // 
            this.MotorPower.Text = "电机功率";
            this.MotorPower.Width = 74;
            // 
            // ProductSpecification
            // 
            this.ProductSpecification.Text = "产品类型";
            this.ProductSpecification.Width = 91;
            // 
            // FeedSize
            // 
            this.FeedSize.Text = "进料尺寸";
            this.FeedSize.Width = 71;
            // 
            // FinalSize
            // 
            this.FinalSize.Text = "出料尺寸";
            this.FinalSize.Width = 66;
            // 
            // MotorTypeId
            // 
            this.MotorTypeId.Text = "电机类型";
            this.MotorTypeId.Width = 71;
            // 
            // MotorId
            // 
            this.MotorId.Text = "设备标识";
            this.MotorId.Width = 96;
            // 
            // line
            // 
            this.line.Text = "产线";
            // 
            // collect
            // 
            this.collect.Text = "嵌入式设备";
            this.collect.Width = 77;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(755, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "嵌入式设备";
            // 
            // txtCollectDevice
            // 
            this.txtCollectDevice.Location = new System.Drawing.Point(826, 28);
            this.txtCollectDevice.Name = "txtCollectDevice";
            this.txtCollectDevice.ReadOnly = true;
            this.txtCollectDevice.Size = new System.Drawing.Size(120, 21);
            this.txtCollectDevice.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(536, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "产  线";
            // 
            // txtProductionline
            // 
            this.txtProductionline.Location = new System.Drawing.Point(595, 28);
            this.txtProductionline.Name = "txtProductionline";
            this.txtProductionline.ReadOnly = true;
            this.txtProductionline.Size = new System.Drawing.Size(120, 21);
            this.txtProductionline.TabIndex = 24;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(474, 192);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 26;
            this.btnNext.Text = "下一个";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(82, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "设备标识";
            // 
            // txtMotorId
            // 
            this.txtMotorId.Location = new System.Drawing.Point(141, 75);
            this.txtMotorId.Name = "txtMotorId";
            this.txtMotorId.ReadOnly = true;
            this.txtMotorId.Size = new System.Drawing.Size(120, 21);
            this.txtMotorId.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(536, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(205, 10);
            this.label13.TabIndex = 29;
            this.label13.Text = "（例如，额定电流，额定频率，额定电压等）";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 568);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMotorId);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtProductionline);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCollectDevice);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPower);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtProType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFinalSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFeedSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStandValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSerialNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMotorType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Name = "Form1";
            this.Text = "设备信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMotorType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSerialNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStandValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFeedSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFinalSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtProType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Name1;
        private System.Windows.Forms.ColumnHeader SerialNumber;
        private System.Windows.Forms.ColumnHeader StandValue;
        private System.Windows.Forms.ColumnHeader MotorPower;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCollectDevice;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProductionline;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMotorId;
        private System.Windows.Forms.ColumnHeader ProductSpecification;
        private System.Windows.Forms.ColumnHeader FeedSize;
        private System.Windows.Forms.ColumnHeader FinalSize;
        private System.Windows.Forms.ColumnHeader MotorTypeId;
        private System.Windows.Forms.ColumnHeader MotorId;
        private System.Windows.Forms.ColumnHeader line;
        private System.Windows.Forms.ColumnHeader collect;
        private System.Windows.Forms.Label label13;
    }
}

