
namespace QLChamCong
{
    partial class HomePage
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ctTextBox1 = new QLChamCong.Control.ctTextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(420, 219);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 0;
            // 
            // ctTextBox1
            // 
            this.ctTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.ctTextBox1.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.ctTextBox1.BorderFocusColor = System.Drawing.Color.HotPink;
            this.ctTextBox1.BorderRadius = 0;
            this.ctTextBox1.BorderSize = 2;
            this.ctTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.ctTextBox1.Location = new System.Drawing.Point(376, 284);
            this.ctTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctTextBox1.Multiline = false;
            this.ctTextBox1.Name = "ctTextBox1";
            this.ctTextBox1.Padding = new System.Windows.Forms.Padding(7);
            this.ctTextBox1.PasswordChar = false;
            this.ctTextBox1.Size = new System.Drawing.Size(250, 35);
            this.ctTextBox1.TabIndex = 1;
            this.ctTextBox1.Texts = "";
            this.ctTextBox1.UnderlinedStyle = false;
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ctTextBox1);
            this.Controls.Add(this.textBox1);
            this.Name = "HomePage";
            this.Text = "HomePage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private Control.ctTextBox ctTextBox1;
    }
}