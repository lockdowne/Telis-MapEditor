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
    public class TilePropertiesView : Form
    {
        #region Fields

        private ListBox listBox1;
        private GroupBox groupBox1;
        private Button buttonAdd;
        private Button buttonRemove;
        private Button buttonClose;

        private TilePropertyView tileWindow;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when Add button is clicked
        /// </summary>
        public event Action OnAdd;

        /// <summary>
        /// Occurs when Remove button is clicked
        /// </summary>
        public event TilePropertyEventHandler OnRemove;

        /// <summary>
        /// Occurs when Close button is closed
        /// </summary>
        public event Action OnClose;

        #endregion

        #region Initialize

        public TilePropertiesView()
        {
            InitializeComponent();

            tileWindow = new TilePropertyView();

            
        }        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(179, 173);
            this.listBox1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(10, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 203);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tile Properties";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(10, 222);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(60, 23);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(76, 222);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(60, 23);
            this.buttonRemove.TabIndex = 6;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(142, 222);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(60, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // TilePropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 249);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TilePropertiesView";
            this.Text = "Tile Properties";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (OnAdd != null)
                OnAdd();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (OnRemove != null)
                OnRemove(new TilePropertyEventsArgs(listBox1.SelectedItem));
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (OnClose != null)
                OnClose();
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

        public void ShowWindow()
        {
            ShowDialog();
        }

        public void CloseWindow()
        {
            ShowDialog();
        }

        public void ShowChildWindow()
        {
            tileWindow.ShowDialog();
        }

        public void CloseChildWindow()
        {
            tileWindow.Close();
        }

        #endregion
    }
}
