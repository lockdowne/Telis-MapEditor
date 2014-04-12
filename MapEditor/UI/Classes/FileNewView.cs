using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.UI;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    public class FileNewView : Form, IFileNewView
    {
        #region Fields

        private GroupBox groupBox1;
        private Button buttonBrowse;
        private TextBox textBoxTileset;
        private TextBox textBoxName;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private NumericUpDownEx numericUpDownExMapWidth;
        private Label label4;
        private Label label3;
        private NumericUpDownEx numericUpDownExMapHeight;
        private GroupBox groupBox3;
        private NumericUpDownEx numericUpDownExTileHeight;
        private Label label6;
        private NumericUpDownEx numericUpDownExTileWidth;
        private Label label5;
        private Label labelDisplay;
        private Button buttonCancel;
        private Button buttonOK;
        private IContainer components = null;

        #endregion

        #region Properties

        public event EventHandler Browse;
        public event EventHandler Confirm;
        public event EventHandler Cancel;
        public event EventHandler ValueChanged;

        public string FileName
        {
            get { return textBoxName.Text; }
        }

        public string TilesetPath
        {
            get { return textBoxTileset.Text; }
        }

        public string Display
        {
            set { labelDisplay.Text = value; }
        }

        public int MapWidth
        {
            get { return (int)numericUpDownExMapWidth.Value; }
        }

        public int MapHeight
        {
            get { return (int)numericUpDownExMapHeight.Value; }
        }

        public int TileWidth
        {
            get { return (int)numericUpDownExTileWidth.Value; }
        }

        public int TileHeight
        {
            get { return (int)numericUpDownExTileHeight.Value; }
        }

        #endregion

        #region Initialize

        public FileNewView()
        {
            InitializeComponent();

            labelDisplay.Text = MapWidth * TileWidth + " x " + MapHeight * TileHeight + " pixels";
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxTileset = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelDisplay = new System.Windows.Forms.Label();
            this.numericUpDownExMapHeight = new MapEditor.Core.Controls.NumericUpDownEx();
            this.numericUpDownExMapWidth = new MapEditor.Core.Controls.NumericUpDownEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownExTileHeight = new MapEditor.Core.Controls.NumericUpDownEx();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownExTileWidth = new MapEditor.Core.Controls.NumericUpDownEx();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExMapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExMapWidth)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExTileWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowse);
            this.groupBox1.Controls.Add(this.textBoxTileset);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 110);
            this.groupBox1.TabIndex = 0;
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
            this.buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelDisplay);
            this.groupBox2.Controls.Add(this.numericUpDownExMapHeight);
            this.groupBox2.Controls.Add(this.numericUpDownExMapWidth);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(13, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Map size";
            // 
            // labelDisplay
            // 
            this.labelDisplay.AutoSize = true;
            this.labelDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisplay.Location = new System.Drawing.Point(9, 75);
            this.labelDisplay.Name = "labelDisplay";
            this.labelDisplay.Size = new System.Drawing.Size(0, 12);
            this.labelDisplay.TabIndex = 4;
            // 
            // numericUpDownExMapHeight
            // 
            this.numericUpDownExMapHeight.Location = new System.Drawing.Point(61, 46);
            this.numericUpDownExMapHeight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownExMapHeight.Measure = "tiles";
            this.numericUpDownExMapHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownExMapHeight.Name = "numericUpDownExMapHeight";
            this.numericUpDownExMapHeight.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownExMapHeight.TabIndex = 4;
            this.numericUpDownExMapHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownExMapHeight.ValueChanged += new EventHandler(numericUpDownExMapHeight_ValueChanged);
            // 
            // numericUpDownExMapWidth
            // 
            this.numericUpDownExMapWidth.Location = new System.Drawing.Point(61, 20);
            this.numericUpDownExMapWidth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownExMapWidth.Measure = "tiles";
            this.numericUpDownExMapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownExMapWidth.Name = "numericUpDownExMapWidth";
            this.numericUpDownExMapWidth.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownExMapWidth.TabIndex = 3;
            this.numericUpDownExMapWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownExMapWidth.ValueChanged += new EventHandler(numericUpDownExMapWidth_ValueChanged);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownExTileHeight);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numericUpDownExTileWidth);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(160, 129);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(140, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tile size";
            // 
            // numericUpDownExTileHeight
            // 
            this.numericUpDownExTileHeight.Location = new System.Drawing.Point(60, 46);
            this.numericUpDownExTileHeight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownExTileHeight.Measure = "px";
            this.numericUpDownExTileHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownExTileHeight.Name = "numericUpDownExTileHeight";
            this.numericUpDownExTileHeight.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownExTileHeight.TabIndex = 6;
            this.numericUpDownExTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownExTileHeight.ValueChanged += new EventHandler(numericUpDownExTileHeight_ValueChanged);
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
            // numericUpDownExTileWidth
            // 
            this.numericUpDownExTileWidth.Location = new System.Drawing.Point(60, 20);
            this.numericUpDownExTileWidth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownExTileWidth.Measure = "px";
            this.numericUpDownExTileWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownExTileWidth.Name = "numericUpDownExTileWidth";
            this.numericUpDownExTileWidth.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownExTileWidth.TabIndex = 5;
            this.numericUpDownExTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownExTileWidth.ValueChanged += new EventHandler(numericUpDownExTileWidth_ValueChanged);
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
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(224, 236);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new EventHandler(buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(143, 235);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new EventHandler(buttonOK_Click);
            // 
            // FileNewView
            // 
            this.AcceptButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(314, 272);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileNewView";
            this.Text = "New Project";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExMapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExMapWidth)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExTileWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        #region Windows Form Designer generated events

        void numericUpDownExTileWidth_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }

        void numericUpDownExMapHeight_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }

        void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (Browse != null)
                Browse(sender, e);
        }

        void numericUpDownExMapWidth_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }

        void numericUpDownExTileHeight_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }

        void buttonCancel_Click(object sender, EventArgs e)
        {
            if (Cancel != null)
                Cancel(sender, e);
        }

        void buttonOK_Click(object sender, EventArgs e)
        {
            if (Confirm != null)
                Confirm(sender, e);
        }

        #endregion

        #region Methods

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
            ShowDialog();
        }

        public void CloseForm()
        {
            Close();
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void SetTilesetPath(string path)
        {
            textBoxTileset.Text = path;
        }

        #endregion        
    }
}
