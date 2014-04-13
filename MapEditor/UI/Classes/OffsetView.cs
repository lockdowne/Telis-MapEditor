using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.UI
{
    /// <summary>
    /// Offset UI
    /// </summary>
    public class OffsetView : Form, IOffsetView
    {
        #region Fields

        private System.Windows.Forms.GroupBox groupBox1;
        private Core.Controls.NumericUpDownEx numericUpDownEx2;
        private Core.Controls.NumericUpDownEx numericUpDownEx1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;

        private System.ComponentModel.IContainer components = null;

        #endregion

        #region Properties

        public event EventHandler OnConfirm;
        public event EventHandler OnCancel;

        public int OffsetX
        {
            get { return (int)numericUpDownEx1.Value; }
        }

        public int OffsetY
        {
            get { return (int)numericUpDownEx2.Value; }
        }

        #endregion

        #region Initialize

        public OffsetView()
        {
            InitializeComponent();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownEx2 = new MapEditor.Core.Controls.NumericUpDownEx();
            this.numericUpDownEx1 = new MapEditor.Core.Controls.NumericUpDownEx();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEx2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownEx2);
            this.groupBox1.Controls.Add(this.numericUpDownEx1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Offset";
            // 
            // numericUpDownEx2
            // 
            this.numericUpDownEx2.Location = new System.Drawing.Point(107, 19);
            this.numericUpDownEx2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownEx2.Measure = null;
            this.numericUpDownEx2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEx2.Name = "numericUpDownEx2";
            this.numericUpDownEx2.Size = new System.Drawing.Size(35, 20);
            this.numericUpDownEx2.TabIndex = 3;
            this.numericUpDownEx2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownEx1
            // 
            this.numericUpDownEx1.Location = new System.Drawing.Point(31, 19);
            this.numericUpDownEx1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownEx1.Measure = null;
            this.numericUpDownEx1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEx1.Name = "numericUpDownEx1";
            this.numericUpDownEx1.Size = new System.Drawing.Size(35, 20);
            this.numericUpDownEx1.TabIndex = 2;
            this.numericUpDownEx1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(13, 73);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 1;
            this.buttonConfirm.Text = "OK";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(94, 73);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // OffsetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 109);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.groupBox1);
            this.Name = "OffsetView";
            this.Text = "Offset";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEx2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEx1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (OnConfirm != null)
                OnConfirm(sender, e);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (OnCancel != null)
                OnCancel(sender, e);
        }
        
        #endregion

        #region Methods

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

        public void ShowForm()
        {
            this.ShowDialog();
        }

        public void CloseForm()
        {
            this.Close();
        }


        #endregion
    }
}
