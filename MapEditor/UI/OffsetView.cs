using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Controls;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    /// <summary>
    /// Represents a window that allows user to offset map
    /// </summary>
    public class OffsetView : Form, IOffsetView
    {
        #region Fields

        private Button buttonCancel;
        private Button buttonConfirm;
        private GroupBox groupBox1;
        private NumericUpDownEx numericY;
        private NumericUpDownEx numericX;
        private Label label2;
        private Label label1; 

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        #endregion

        #region Properties
        
        /// <summary>
        /// Occurs when Confirm button is clicked
        /// </summary>
        public event OffsetEventHandler OnConfirm;

        /// <summary>
        /// Occurs when Cancel button is clicked
        /// </summary>
        public event Action OnCancel;

        /// <summary>
        /// Sets the offset x of numeric control
        /// </summary>
        public int OffsetX
        {
            set { numericX.Value = value; }
        }

        /// <summary>
        /// Sets the offset y of numeric control
        /// </summary>
        public int OffsetY
        {
            set { numericX.Value = value; }
        }

        #endregion

        #region Initialize

        public OffsetView()
        {
            InitializeComponent();

            CenterToScreen();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericY = new MapEditor.Core.Controls.NumericUpDownEx();
            this.numericX = new MapEditor.Core.Controls.NumericUpDownEx();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericX)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 72);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(12, 72);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 4;
            this.buttonConfirm.Text = "OK";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericY);
            this.groupBox1.Controls.Add(this.numericX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 54);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Offset";
            // 
            // numericY
            // 
            this.numericY.Location = new System.Drawing.Point(107, 19);
            this.numericY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericY.Measure = null;
            this.numericY.Name = "numericY";
            this.numericY.Size = new System.Drawing.Size(35, 20);
            this.numericY.TabIndex = 3;
            // 
            // numericX
            // 
            this.numericX.Location = new System.Drawing.Point(31, 19);
            this.numericX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericX.Measure = null;
            this.numericX.Name = "numericX";
            this.numericX.Size = new System.Drawing.Size(35, 20);
            this.numericX.TabIndex = 2;
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
            // OffsetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 107);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OffsetView";
            this.Text = "Offset Map";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (OnConfirm != null)
                OnConfirm(new OffsetEventArgs((int)numericX.Value, (int)numericY.Value));
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (OnCancel != null)
                OnCancel();
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

        /// <summary>
        /// Show window as a dialogbox
        /// </summary>
        public void ShowWindow()
        {
            ShowDialog();
        }

        /// <summary>
        /// Closes window
        /// </summary>
        public void CloseWindow()
        {
            Close();
        }


        #endregion

    }
}
