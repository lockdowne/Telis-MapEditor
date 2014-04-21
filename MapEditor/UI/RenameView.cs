using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    public class RenameView : Form, IRenameView
    {
        #region Fields

        private TextBox textBox;
        private Button buttonRename;
        private Button buttonCancel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when the Cancel button is clicked
        /// </summary>
        public event Action OnCancel;

        /// <summary>
        /// Occurs when the Rename button is clicked
        /// </summary>
        public event RenameEventHandler OnConfirm;        

        #endregion

        #region Initialize

        public RenameView()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
        }
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 12);
            this.textBox.MaxLength = 15;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(151, 20);
            this.textBox.TabIndex = 0;
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(12, 38);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(75, 23);
            this.buttonRename.TabIndex = 1;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(88, 38);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // RenameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 70);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RenameView";
            this.Text = "Rename";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void buttonRename_Click(object sender, EventArgs e)
        {
            if (OnConfirm != null)
                OnConfirm(new RenameEventArgs(textBox.Text));
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
        /// Show window as dialog
        /// </summary>
        public void ShowWindow(ILayerView parent)
        {
            ShowDialog((Form)parent);
        }

        /// <summary>
        /// Close window
        /// </summary>
        public void CloseWindow()        
        {
            Close();
        }

        #endregion
    }
}
