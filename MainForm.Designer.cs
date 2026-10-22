namespace IntegerIndexedPriorityQueue
{
    partial class MainForm
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
			DisplayListBox = new ListBox();
			groupBox1 = new GroupBox();
			radioButton2 = new RadioButton();
			radioButton1 = new RadioButton();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// DisplayListBox
			// 
			DisplayListBox.FormattingEnabled = true;
			DisplayListBox.ItemHeight = 15;
			DisplayListBox.Location = new Point(147, 12);
			DisplayListBox.Name = "DisplayListBox";
			DisplayListBox.Size = new Size(289, 199);
			DisplayListBox.TabIndex = 2;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(radioButton2);
			groupBox1.Controls.Add(radioButton1);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(129, 86);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Situation";
			// 
			// radioButton2
			// 
			radioButton2.AutoSize = true;
			radioButton2.Location = new Point(6, 61);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new Size(70, 19);
			radioButton2.TabIndex = 1;
			radioButton2.Text = "Wartime";
			radioButton2.UseVisualStyleBackColor = true;
			radioButton2.CheckedChanged += Situation_CheckedChanged;
			// 
			// radioButton1
			// 
			radioButton1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton1.Location = new Point(6, 22);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new Size(80, 19);
			radioButton1.TabIndex = 0;
			radioButton1.TabStop = true;
			radioButton1.Text = "Peacetime";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton1.CheckedChanged += Situation_CheckedChanged;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(439, 217);
			Controls.Add(groupBox1);
			Controls.Add(DisplayListBox);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "MainForm";
			Text = "Main Form";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion
		private ListBox DisplayListBox;
		private GroupBox groupBox1;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
	}
}
