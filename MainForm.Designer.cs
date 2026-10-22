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
			rbWartime = new RadioButton();
			rbPeasetime = new RadioButton();
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
			groupBox1.Controls.Add(rbWartime);
			groupBox1.Controls.Add(rbPeasetime);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(129, 86);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Situation";
			// 
			// rbWartime
			// 
			rbWartime.AutoSize = true;
			rbWartime.Location = new Point(6, 61);
			rbWartime.Name = "rbWartime";
			rbWartime.Size = new Size(70, 19);
			rbWartime.TabIndex = 1;
			rbWartime.Text = "Wartime";
			rbWartime.UseVisualStyleBackColor = true;
			rbWartime.CheckedChanged += Situation_CheckedChanged;
			// 
			// rbPeasetime
			// 
			rbPeasetime.AutoSize = true;
			rbPeasetime.Checked = true;
			rbPeasetime.Location = new Point(6, 22);
			rbPeasetime.Name = "rbPeasetime";
			rbPeasetime.Size = new Size(80, 19);
			rbPeasetime.TabIndex = 0;
			rbPeasetime.TabStop = true;
			rbPeasetime.Text = "Peacetime";
			rbPeasetime.UseVisualStyleBackColor = true;
			rbPeasetime.CheckedChanged += Situation_CheckedChanged;
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
		private RadioButton rbWartime;
		private RadioButton rbPeasetime;
	}
}
