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
    /// Represents a window that allows user to create a new map
    /// </summary>
    public class NewMapView : Form, INewMapView
    { 
        #region Fields

        private Label labelPixelDisplayer;
        private Button buttonOK;
        private Button buttonCancel;
        private GroupBox groupBox3;
        private Label label6;
        private Label label5;
        private GroupBox groupBox2;
        private Label label4;
        private Label label3;
        private GroupBox groupBox1;
        private Button buttonBrowse;
        private TextBox textBoxTileset;
        private TextBox textBoxName;
        private Label label2;
        private Label label1;
        private NumericUpDownEx numericTileHeight;
        private NumericUpDownEx numericTileWidth;
        private NumericUpDownEx numericMapHeight;
        private NumericUpDownEx numericMapWidth;
       
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when the Browse button is clicked
        /// </summary>
        public event Action OnBrowse;

        /// <summary>
        /// Occurs when the Confirm button is clicked
        /// </summary>
        public event NewMapEventHandler OnConfirm;

        /// <summary>
        /// Occurs when the Cancel button is clicked
        /// </summary>
        public event Action OnCancel;

        /// <summary>
        /// Occurs when a numeric box values are changed
        /// </summary>
        public event NewMapEventHandler OnNumericValueChanged;
        
        /// <summary>
        /// Occurs when window has loaded
        /// </summary>
        public event NewMapEventHandler OnWindowLoaded;

        /// <summary>
        /// Sets the label
        /// </summary>
        public string PixelDisplayer
        {
            set { labelPixelDisplayer.Text = value; }
        }

        /// <summary>
        /// Sets the tileset path
        /// </summary>
        public string TilesetPath
        {
            set { textBoxTileset.Text = value; }
        }

        #endregion 

        #region Initialize

        public NewMapView()
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericTileHeight = new MapEditor.Core.Controls.NumericUpDownEx();
            this.numericTileWidth = new MapEditor.Core.Controls.NumericUpDownEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericMapHeight = new MapEditor.Core.Controls.NumericUpDownEx();
            this.numericMapWidth = new MapEditor.Core.Controls.NumericUpDownEx();
            this.labelPixelDisplayer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxTileset = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTileWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(142, 234);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(223, 235);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericTileHeight);
            this.groupBox3.Controls.Add(this.numericTileWidth);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(159, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(140, 100);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tile size";
            // 
            // numericTileHeight
            // 
            this.numericTileHeight.Location = new System.Drawing.Point(58, 45);
            this.numericTileHeight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericTileHeight.Measure = "px";
            this.numericTileHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTileHeight.Name = "numericTileHeight";
            this.numericTileHeight.Size = new System.Drawing.Size(65, 20);
            this.numericTileHeight.TabIndex = 8;
            this.numericTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericTileHeight.ValueChanged += new System.EventHandler(this.numericTileHeight_ValueChanged);
            // 
            // numericTileWidth
            // 
            this.numericTileWidth.Location = new System.Drawing.Point(58, 19);
            this.numericTileWidth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericTileWidth.Measure = "px";
            this.numericTileWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTileWidth.Name = "numericTileWidth";
            this.numericTileWidth.Size = new System.Drawing.Size(65, 20);
            this.numericTileWidth.TabIndex = 7;
            this.numericTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericTileWidth.ValueChanged += new System.EventHandler(this.numericTileWidth_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Width:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Height:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericMapHeight);
            this.groupBox2.Controls.Add(this.numericMapWidth);
            this.groupBox2.Controls.Add(this.labelPixelDisplayer);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Map size";
            // 
            // numericMapHeight
            // 
            this.numericMapHeight.Location = new System.Drawing.Point(61, 46);
            this.numericMapHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericMapHeight.Measure = "tiles";
            this.numericMapHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapHeight.Name = "numericMapHeight";
            this.numericMapHeight.Size = new System.Drawing.Size(65, 20);
            this.numericMapHeight.TabIndex = 6;
            this.numericMapHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericMapHeight.ValueChanged += new System.EventHandler(this.numericMapHeight_ValueChanged);
            // 
            // numericMapWidth
            // 
            this.numericMapWidth.Location = new System.Drawing.Point(61, 20);
            this.numericMapWidth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericMapWidth.Measure = "tiles";
            this.numericMapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMapWidth.Name = "numericMapWidth";
            this.numericMapWidth.Size = new System.Drawing.Size(65, 20);
            this.numericMapWidth.TabIndex = 5;
            this.numericMapWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericMapWidth.ValueChanged += new System.EventHandler(this.numericMapWidth_ValueChanged);
            // 
            // labelPixelDisplayer
            // 
            this.labelPixelDisplayer.AutoSize = true;
            this.labelPixelDisplayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPixelDisplayer.Location = new System.Drawing.Point(9, 75);
            this.labelPixelDisplayer.Name = "labelPixelDisplayer";
            this.labelPixelDisplayer.Size = new System.Drawing.Size(0, 12);
            this.labelPixelDisplayer.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Width:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowse);
            this.groupBox1.Controls.Add(this.textBoxTileset);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 110);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(195, 72);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxTileset
            // 
            this.textBoxTileset.Location = new System.Drawing.Point(61, 46);
            this.textBoxTileset.Name = "textBoxTileset";
            this.textBoxTileset.ReadOnly = true;
            this.textBoxTileset.Size = new System.Drawing.Size(209, 20);
            this.textBoxTileset.TabIndex = 3;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(61, 20);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(209, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tileset:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // NewMapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 271);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewMapView";
            this.Text = "New Map";
            this.Load += new System.EventHandler(this.NewFileView_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTileWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMapWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (OnConfirm != null)
                OnConfirm(new NewMapEventArgs(textBoxName.Text, textBoxTileset.Text, (int)numericMapWidth.Value, (int)numericMapHeight.Value, (int)numericTileWidth.Value, (int)numericTileHeight.Value));
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (OnBrowse != null)
                OnBrowse();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (OnCancel != null)
                OnCancel();
        }

        private void numericMapWidth_ValueChanged(object sender, EventArgs e)
        {
            if (OnNumericValueChanged != null)
                OnNumericValueChanged(new NewMapEventArgs(textBoxName.Text, textBoxTileset.Text, (int)numericMapWidth.Value, (int)numericMapHeight.Value, (int)numericTileWidth.Value, (int)numericTileHeight.Value));
        }

        private void numericMapHeight_ValueChanged(object sender, EventArgs e)
        {
            if (OnNumericValueChanged != null)
                OnNumericValueChanged(new NewMapEventArgs(textBoxName.Text, textBoxTileset.Text, (int)numericMapWidth.Value, (int)numericMapHeight.Value, (int)numericTileWidth.Value, (int)numericTileHeight.Value));
        }

        private void numericTileWidth_ValueChanged(object sender, EventArgs e)
        {
            if (OnNumericValueChanged != null)
                OnNumericValueChanged(new NewMapEventArgs(textBoxName.Text, textBoxTileset.Text, (int)numericMapWidth.Value, (int)numericMapHeight.Value, (int)numericTileWidth.Value, (int)numericTileHeight.Value));
        }

        private void numericTileHeight_ValueChanged(object sender, EventArgs e)
        {
            if (OnNumericValueChanged != null)
                OnNumericValueChanged(new NewMapEventArgs(textBoxName.Text, textBoxTileset.Text, (int)numericMapWidth.Value, (int)numericMapHeight.Value, (int)numericTileWidth.Value, (int)numericTileHeight.Value));
        }
        
        private void NewFileView_Load(object sender, EventArgs e)
        {
            if (OnWindowLoaded != null)
                OnWindowLoaded(new NewMapEventArgs(textBoxName.Text, textBoxTileset.Text, (int)numericMapWidth.Value, (int)numericMapHeight.Value, (int)numericTileWidth.Value, (int)numericTileHeight.Value)); 
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

        /// <summary>
        /// Displays a message box
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        public void DisplayMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(text, caption, buttons, icon);            
        }
        
        /// <summary>
        /// Resets all controls to default values
        /// </summary>
        public void Reset()
        {
            textBoxName.Text = string.Empty;
            textBoxTileset.Text = string.Empty;

            numericMapHeight.Value = 100;
            numericMapWidth.Value = 100;
            numericTileHeight.Value = 32;
            numericTileWidth.Value = 32;
        }

        #endregion               
    }
}
