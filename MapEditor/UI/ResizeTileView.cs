﻿using System;
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
    /// Represents a window that allows user to resize tiles
    /// </summary>
    public class ResizeTileView : Form, IResizeTileView
    {
        #region Fields

        private Button buttonCancel;
        private Button buttonConfirm;
        private GroupBox groupBox1;
        private NumericUpDownEx numericHeight;
        private NumericUpDownEx numericWidth;
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
        public event ResizeTileEventHandler OnConfirm;

        /// <summary>
        /// Occurs when Cancel button is clicked
        /// </summary>
        public event Action OnCancel;

        /// <summary>
        /// Sets the tile width of numeric control
        /// </summary>
        public int TileWidth
        {
            get { return (int)numericWidth.Value; }
            set { numericWidth.Value = value; }
        }

        /// <summary>
        /// Sets the tile height of numeric control
        /// </summary>
        public int TileHeight
        {
            get { return (int)numericHeight.Value; }
            set { numericHeight.Value = value; }
        }

        #endregion

        #region Initialize

        public ResizeTileView()
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
            this.numericHeight = new MapEditor.Core.Controls.NumericUpDownEx();
            this.numericWidth = new MapEditor.Core.Controls.NumericUpDownEx();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(185, 72);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(101, 73);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 4;
            this.buttonConfirm.Text = "OK";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericHeight);
            this.groupBox1.Controls.Add(this.numericWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 54);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tile";
            // 
            // numericHeight
            // 
            this.numericHeight.Location = new System.Drawing.Point(170, 21);
            this.numericHeight.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericHeight.Measure = "px";
            this.numericHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(65, 20);
            this.numericHeight.TabIndex = 3;
            this.numericHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // numericWidth
            // 
            this.numericWidth.Location = new System.Drawing.Point(52, 21);
            this.numericWidth.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericWidth.Measure = "px";
            this.numericWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(65, 20);
            this.numericWidth.TabIndex = 2;
            this.numericWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width:";
            // 
            // ResizeTileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 107);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ResizeTileView";
            this.Text = "Resize Tiles";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (OnConfirm != null)
                OnConfirm(new ResizeTileEventArgs((int)numericWidth.Value, (int)numericHeight.Value));
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
