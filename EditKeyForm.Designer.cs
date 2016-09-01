namespace KeyGenerator
{
    partial class EditKeyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1Edit = new System.Windows.Forms.TextBox();
            this.button1A = new System.Windows.Forms.Button();
            this.button1C = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Element to Edit: ";
            // 
            // textBox1Edit
            // 
            this.textBox1Edit.Location = new System.Drawing.Point(29, 55);
            this.textBox1Edit.Name = "textBox1Edit";
            this.textBox1Edit.Size = new System.Drawing.Size(256, 20);
            this.textBox1Edit.TabIndex = 1;
            // 
            // button1A
            // 
            this.button1A.Location = new System.Drawing.Point(69, 100);
            this.button1A.Name = "button1A";
            this.button1A.Size = new System.Drawing.Size(75, 23);
            this.button1A.TabIndex = 2;
            this.button1A.Text = "Accept";
            this.button1A.UseVisualStyleBackColor = true;
            this.button1A.Click += new System.EventHandler(this.button1A_Click);
            // 
            // button1C
            // 
            this.button1C.Location = new System.Drawing.Point(202, 100);
            this.button1C.Name = "button1C";
            this.button1C.Size = new System.Drawing.Size(75, 23);
            this.button1C.TabIndex = 3;
            this.button1C.Text = "Cancel";
            this.button1C.UseVisualStyleBackColor = true;
            this.button1C.Click += new System.EventHandler(this.button1C_Click);
            // 
            // EditKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 147);
            this.Controls.Add(this.button1C);
            this.Controls.Add(this.button1A);
            this.Controls.Add(this.textBox1Edit);
            this.Controls.Add(this.label1);
            this.Name = "EditKeyForm";
            this.Text = "Edit Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1Edit;
        private System.Windows.Forms.Button button1A;
        private System.Windows.Forms.Button button1C;
    }
}