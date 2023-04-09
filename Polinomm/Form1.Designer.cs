namespace Polinomm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            textBox1 = new TextBox();
            axisXMaxTextBox = new TextBox();
            axisYMaxTextBox = new TextBox();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(372, 84);
            panel1.Name = "panel1";
            panel1.Size = new Size(819, 525);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick_1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(45, 248);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(271, 99);
            textBox1.TabIndex = 1;
            // 
            // axisXMaxTextBox
            // 
            axisXMaxTextBox.Location = new Point(45, 203);
            axisXMaxTextBox.Name = "axisXMaxTextBox";
            axisXMaxTextBox.Size = new Size(100, 27);
            axisXMaxTextBox.TabIndex = 0;
            axisXMaxTextBox.TextChanged += axisXMaxTextBox_TextChanged;
            // 
            // axisYMaxTextBox
            // 
            axisYMaxTextBox.Location = new Point(216, 203);
            axisYMaxTextBox.Name = "axisYMaxTextBox";
            axisYMaxTextBox.Size = new Size(100, 27);
            axisYMaxTextBox.TabIndex = 1;
            axisYMaxTextBox.TextChanged += axisYMaxTextBox_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1240, 648);
            Controls.Add(axisYMaxTextBox);
            Controls.Add(axisXMaxTextBox);
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TextBox textBox1;
        private TextBox axisXMaxTextBox;
        private TextBox axisYMaxTextBox;
    }
}