
namespace QLCHCayCanh
{
    partial class frmThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnTKe = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtNgDau = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtNgCuoi = new System.Windows.Forms.DateTimePicker();
            this.bdDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdDoanhThu)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTKe
            // 
            this.btnTKe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTKe.Location = new System.Drawing.Point(413, 24);
            this.btnTKe.Name = "btnTKe";
            this.btnTKe.Size = new System.Drawing.Size(81, 24);
            this.btnTKe.TabIndex = 1;
            this.btnTKe.Text = "Thống kê\r\n";
            this.btnTKe.UseVisualStyleBackColor = true;
            this.btnTKe.Click += new System.EventHandler(this.btnTKe_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Từ ngày:";
            // 
            // dtNgDau
            // 
            this.dtNgDau.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNgDau.Location = new System.Drawing.Point(108, 24);
            this.dtNgDau.Name = "dtNgDau";
            this.dtNgDau.Size = new System.Drawing.Size(102, 22);
            this.dtNgDau.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnTKe);
            this.groupBox1.Controls.Add(this.dtNgCuoi);
            this.groupBox1.Controls.Add(this.dtNgDau);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 66);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn ngày thống kê ";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(226, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày:";
            // 
            // dtNgCuoi
            // 
            this.dtNgCuoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgCuoi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNgCuoi.Location = new System.Drawing.Point(296, 24);
            this.dtNgCuoi.Name = "dtNgCuoi";
            this.dtNgCuoi.Size = new System.Drawing.Size(102, 22);
            this.dtNgCuoi.TabIndex = 3;
            // 
            // bdDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.bdDoanhThu.ChartAreas.Add(chartArea1);
            this.bdDoanhThu.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            this.bdDoanhThu.Legends.Add(legend1);
            this.bdDoanhThu.Location = new System.Drawing.Point(0, 66);
            this.bdDoanhThu.Name = "bdDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.LegendText = "Doanh thu";
            series1.Name = "ChartDT";
            this.bdDoanhThu.Series.Add(series1);
            this.bdDoanhThu.Size = new System.Drawing.Size(603, 370);
            this.bdDoanhThu.TabIndex = 5;
            this.bdDoanhThu.Text = "chart1";
            // 
            // frmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(603, 436);
            this.Controls.Add(this.bdDoanhThu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmThongKe";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thống kê";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdDoanhThu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTKe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtNgDau;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtNgCuoi;
        private System.Windows.Forms.DataVisualization.Charting.Chart bdDoanhThu;
    }
}