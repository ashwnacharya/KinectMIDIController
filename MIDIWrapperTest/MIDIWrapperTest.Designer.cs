namespace MIDIWrapperTest
{
    partial class MIDIWrapperTest
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
            this.txtNoteDuration = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmbTranspose = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.vsVolume = new System.Windows.Forms.VScrollBar();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.pb10 = new System.Windows.Forms.PictureBox();
            this.pb8 = new System.Windows.Forms.PictureBox();
            this.pb6 = new System.Windows.Forms.PictureBox();
            this.pb3 = new System.Windows.Forms.PictureBox();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.pb12 = new System.Windows.Forms.PictureBox();
            this.pb11 = new System.Windows.Forms.PictureBox();
            this.pb9 = new System.Windows.Forms.PictureBox();
            this.pb7 = new System.Windows.Forms.PictureBox();
            this.pb5 = new System.Windows.Forms.PictureBox();
            this.pb4 = new System.Windows.Forms.PictureBox();
            this.pb2 = new System.Windows.Forms.PictureBox();
            this.pb0 = new System.Windows.Forms.PictureBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbGM = new System.Windows.Forms.ComboBox();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNoteDuration
            // 
            this.txtNoteDuration.Location = new System.Drawing.Point(126, 90);
            this.txtNoteDuration.Name = "txtNoteDuration";
            this.txtNoteDuration.Size = new System.Drawing.Size(40, 20);
            this.txtNoteDuration.TabIndex = 83;
            this.txtNoteDuration.Text = "0";
            this.txtNoteDuration.TextChanged += new System.EventHandler(this.txtNoteDuration_TextChanged);
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(119, 69);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(83, 14);
            this.Label5.TabIndex = 82;
            this.Label5.Text = "Note Duration:";
            // 
            // cmbTranspose
            // 
            this.cmbTranspose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTranspose.Items.AddRange(new object[] {
            "+ 1 Octave",
            "11",
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1",
            "0",
            "-1",
            "-2",
            "-3",
            "-4",
            "-5",
            "-6",
            "-7",
            "-8",
            "-9",
            "-10",
            "-11",
            "- 1 Octave"});
            this.cmbTranspose.Location = new System.Drawing.Point(12, 90);
            this.cmbTranspose.Name = "cmbTranspose";
            this.cmbTranspose.Size = new System.Drawing.Size(101, 21);
            this.cmbTranspose.TabIndex = 81;
            this.cmbTranspose.SelectedIndexChanged += new System.EventHandler(this.cmbTranspose_SelectedIndexChanged);
            this.cmbTranspose.TextChanged += new System.EventHandler(this.cmbTranspose_TextChanged);
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(399, 125);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(53, 14);
            this.Label4.TabIndex = 80;
            this.Label4.Text = "Volume:";
            // 
            // vsVolume
            // 
            this.vsVolume.Location = new System.Drawing.Point(402, 146);
            this.vsVolume.Maximum = 127;
            this.vsVolume.Name = "vsVolume";
            this.vsVolume.Size = new System.Drawing.Size(33, 125);
            this.vsVolume.TabIndex = 79;
            this.vsVolume.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsVolume_Scroll);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(212, 69);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(84, 14);
            this.Label3.TabIndex = 78;
            this.Label3.Text = "Patch:";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(12, 69);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(84, 14);
            this.Label2.TabIndex = 77;
            this.Label2.Text = "Transpose:";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(12, 21);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 14);
            this.Label1.TabIndex = 76;
            this.Label1.Text = "Instrument:";
            // 
            // pb10
            // 
            this.pb10.BackColor = System.Drawing.Color.Black;
            this.pb10.Location = new System.Drawing.Point(279, 132);
            this.pb10.Name = "pb10";
            this.pb10.Size = new System.Drawing.Size(33, 69);
            this.pb10.TabIndex = 75;
            this.pb10.TabStop = false;
            // 
            // pb8
            // 
            this.pb8.BackColor = System.Drawing.Color.Black;
            this.pb8.Location = new System.Drawing.Point(232, 132);
            this.pb8.Name = "pb8";
            this.pb8.Size = new System.Drawing.Size(34, 69);
            this.pb8.TabIndex = 74;
            this.pb8.TabStop = false;
            // 
            // pb6
            // 
            this.pb6.BackColor = System.Drawing.Color.Black;
            this.pb6.Location = new System.Drawing.Point(186, 132);
            this.pb6.Name = "pb6";
            this.pb6.Size = new System.Drawing.Size(33, 69);
            this.pb6.TabIndex = 73;
            this.pb6.TabStop = false;
            // 
            // pb3
            // 
            this.pb3.BackColor = System.Drawing.Color.Black;
            this.pb3.Location = new System.Drawing.Point(92, 132);
            this.pb3.Name = "pb3";
            this.pb3.Size = new System.Drawing.Size(34, 69);
            this.pb3.TabIndex = 72;
            this.pb3.TabStop = false;
            // 
            // pb1
            // 
            this.pb1.BackColor = System.Drawing.Color.Black;
            this.pb1.Location = new System.Drawing.Point(46, 132);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(33, 69);
            this.pb1.TabIndex = 71;
            this.pb1.TabStop = false;
            // 
            // pb12
            // 
            this.pb12.BackColor = System.Drawing.Color.White;
            this.pb12.Location = new System.Drawing.Point(346, 132);
            this.pb12.Name = "pb12";
            this.pb12.Size = new System.Drawing.Size(40, 132);
            this.pb12.TabIndex = 70;
            this.pb12.TabStop = false;
            // 
            // pb11
            // 
            this.pb11.BackColor = System.Drawing.Color.White;
            this.pb11.Location = new System.Drawing.Point(299, 132);
            this.pb11.Name = "pb11";
            this.pb11.Size = new System.Drawing.Size(40, 132);
            this.pb11.TabIndex = 69;
            this.pb11.TabStop = false;
            // 
            // pb9
            // 
            this.pb9.BackColor = System.Drawing.Color.White;
            this.pb9.Location = new System.Drawing.Point(252, 132);
            this.pb9.Name = "pb9";
            this.pb9.Size = new System.Drawing.Size(40, 132);
            this.pb9.TabIndex = 68;
            this.pb9.TabStop = false;
            // 
            // pb7
            // 
            this.pb7.BackColor = System.Drawing.Color.White;
            this.pb7.Location = new System.Drawing.Point(206, 132);
            this.pb7.Name = "pb7";
            this.pb7.Size = new System.Drawing.Size(40, 132);
            this.pb7.TabIndex = 67;
            this.pb7.TabStop = false;
            // 
            // pb5
            // 
            this.pb5.BackColor = System.Drawing.Color.White;
            this.pb5.Location = new System.Drawing.Point(159, 132);
            this.pb5.Name = "pb5";
            this.pb5.Size = new System.Drawing.Size(40, 132);
            this.pb5.TabIndex = 66;
            this.pb5.TabStop = false;
            // 
            // pb4
            // 
            this.pb4.BackColor = System.Drawing.Color.White;
            this.pb4.Location = new System.Drawing.Point(112, 132);
            this.pb4.Name = "pb4";
            this.pb4.Size = new System.Drawing.Size(40, 132);
            this.pb4.TabIndex = 65;
            this.pb4.TabStop = false;
            // 
            // pb2
            // 
            this.pb2.BackColor = System.Drawing.Color.White;
            this.pb2.Location = new System.Drawing.Point(66, 132);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(40, 132);
            this.pb2.TabIndex = 64;
            this.pb2.TabStop = false;
            // 
            // pb0
            // 
            this.pb0.BackColor = System.Drawing.Color.White;
            this.pb0.Location = new System.Drawing.Point(19, 132);
            this.pb0.Name = "pb0";
            this.pb0.Size = new System.Drawing.Size(40, 132);
            this.pb0.TabIndex = 63;
            this.pb0.TabStop = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Black;
            this.PictureBox1.Location = new System.Drawing.Point(12, 125);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(380, 146);
            this.PictureBox1.TabIndex = 62;
            this.PictureBox1.TabStop = false;
            // 
            // cmbGM
            // 
            this.cmbGM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGM.Location = new System.Drawing.Point(212, 90);
            this.cmbGM.Name = "cmbGM";
            this.cmbGM.Size = new System.Drawing.Size(307, 21);
            this.cmbGM.TabIndex = 61;
            this.cmbGM.SelectedIndexChanged += new System.EventHandler(this.cmbGM_SelectedIndexChanged);
            // 
            // cmbDevices
            // 
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.Location = new System.Drawing.Point(12, 42);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(427, 21);
            this.cmbDevices.TabIndex = 60;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(452, 42);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(63, 20);
            this.btnOpen.TabIndex = 59;
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // MIDIWrapperTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 313);
            this.Controls.Add(this.txtNoteDuration);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.cmbTranspose);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.vsVolume);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.pb10);
            this.Controls.Add(this.pb8);
            this.Controls.Add(this.pb6);
            this.Controls.Add(this.pb3);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.pb12);
            this.Controls.Add(this.pb11);
            this.Controls.Add(this.pb9);
            this.Controls.Add(this.pb7);
            this.Controls.Add(this.pb5);
            this.Controls.Add(this.pb4);
            this.Controls.Add(this.pb2);
            this.Controls.Add(this.pb0);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.cmbGM);
            this.Controls.Add(this.cmbDevices);
            this.Controls.Add(this.btnOpen);
            this.MaximizeBox = false;
            this.Name = "MIDIWrapperTest";
            this.Text = "MIDIWrapperTest";
            this.Load += new System.EventHandler(this.MIDIWrapperTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtNoteDuration;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cmbTranspose;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.VScrollBar vsVolume;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.PictureBox pb10;
        internal System.Windows.Forms.PictureBox pb8;
        internal System.Windows.Forms.PictureBox pb6;
        internal System.Windows.Forms.PictureBox pb3;
        internal System.Windows.Forms.PictureBox pb1;
        internal System.Windows.Forms.PictureBox pb12;
        internal System.Windows.Forms.PictureBox pb11;
        internal System.Windows.Forms.PictureBox pb9;
        internal System.Windows.Forms.PictureBox pb7;
        internal System.Windows.Forms.PictureBox pb5;
        internal System.Windows.Forms.PictureBox pb4;
        internal System.Windows.Forms.PictureBox pb2;
        internal System.Windows.Forms.PictureBox pb0;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.ComboBox cmbGM;
        internal System.Windows.Forms.ComboBox cmbDevices;
        internal System.Windows.Forms.Button btnOpen;
    }
}

