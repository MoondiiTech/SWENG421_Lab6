
namespace Lab6
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.DrawingArea = new System.Windows.Forms.PictureBox();
            this.create = new System.Windows.Forms.Button();
            this.graphsDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.revise = new System.Windows.Forms.Button();
            this.copy = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.copy);
            this.panel1.Controls.Add(this.revise);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.graphsDropDown);
            this.panel1.Controls.Add(this.create);
            this.panel1.Controls.Add(this.DrawingArea);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1146, 563);
            this.panel1.TabIndex = 0;
            // 
            // DrawingArea
            // 
            this.DrawingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawingArea.Location = new System.Drawing.Point(0, -1);
            this.DrawingArea.Name = "DrawingArea";
            this.DrawingArea.Size = new System.Drawing.Size(1146, 518);
            this.DrawingArea.TabIndex = 0;
            this.DrawingArea.TabStop = false;
            // 
            // create
            // 
            this.create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.create.Location = new System.Drawing.Point(437, 530);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(75, 23);
            this.create.TabIndex = 1;
            this.create.Text = "Create";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.create_Click);
            // 
            // graphsDropDown
            // 
            this.graphsDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.graphsDropDown.FormattingEnabled = true;
            this.graphsDropDown.Location = new System.Drawing.Point(270, 530);
            this.graphsDropDown.Name = "graphsDropDown";
            this.graphsDropDown.Size = new System.Drawing.Size(121, 21);
            this.graphsDropDown.TabIndex = 2;
            this.graphsDropDown.SelectedIndexChanged += new System.EventHandler(this.graphsDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Location = new System.Drawing.Point(200, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Graph IDs";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // revise
            // 
            this.revise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.revise.Location = new System.Drawing.Point(550, 530);
            this.revise.Name = "revise";
            this.revise.Size = new System.Drawing.Size(75, 23);
            this.revise.TabIndex = 3;
            this.revise.Text = "Revise";
            this.revise.UseVisualStyleBackColor = true;
            this.revise.Click += new System.EventHandler(this.revise_Click);
            // 
            // copy
            // 
            this.copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.copy.Location = new System.Drawing.Point(667, 529);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(75, 23);
            this.copy.TabIndex = 4;
            this.copy.Text = "Copy";
            this.copy.UseVisualStyleBackColor = true;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 563);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox DrawingArea;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.ComboBox graphsDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button revise;
        private System.Windows.Forms.Button copy;
    }
}

