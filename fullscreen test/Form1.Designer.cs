namespace fullscreen_test
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
			components = new System.ComponentModel.Container();
			button1 = new Button();
			label1 = new Label();
			timer1 = new System.Windows.Forms.Timer(components);
			button2 = new Button();
			button3 = new Button();
			button4 = new Button();
			listBox1 = new ListBox();
			button5 = new Button();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(293, 112);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 0;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			button1.KeyPress += KeyPressChecker;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(264, 60);
			label1.Name = "label1";
			label1.Size = new Size(38, 15);
			label1.TabIndex = 1;
			label1.Text = "label1";
			// 
			// timer1
			// 
			timer1.Enabled = true;
			timer1.Interval = 1000;
			timer1.Tick += timer1_Tick;
			// 
			// button2
			// 
			button2.Location = new Point(326, 264);
			button2.Name = "button2";
			button2.Size = new Size(75, 23);
			button2.TabIndex = 2;
			button2.Text = "topmost";
			button2.UseVisualStyleBackColor = true;
			button2.Click += topmost;
			// 
			// button3
			// 
			button3.Location = new Point(585, 143);
			button3.Name = "button3";
			button3.Size = new Size(75, 23);
			button3.TabIndex = 3;
			button3.Text = "button3";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button4
			// 
			button4.Location = new Point(525, 331);
			button4.Name = "button4";
			button4.Size = new Size(115, 59);
			button4.TabIndex = 4;
			button4.Text = "Topmost simple";
			button4.UseVisualStyleBackColor = true;
			button4.Click += topmost_simple;
			// 
			// listBox1
			// 
			listBox1.FormattingEnabled = true;
			listBox1.ItemHeight = 15;
			listBox1.Location = new Point(12, 41);
			listBox1.Name = "listBox1";
			listBox1.Size = new Size(120, 349);
			listBox1.TabIndex = 5;
			// 
			// button5
			// 
			button5.Location = new Point(639, 37);
			button5.Name = "button5";
			button5.Size = new Size(75, 23);
			button5.TabIndex = 6;
			button5.Text = "reset";
			button5.UseVisualStyleBackColor = true;
			button5.Click += button5_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(button5);
			Controls.Add(listBox1);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(label1);
			Controls.Add(button1);
			Name = "Form1";
			Text = "Form1";
			KeyPress += KeyPressChecker;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button button1;
		private Label label1;
		private System.Windows.Forms.Timer timer1;
		private Button button2;
		private Button button3;
		private Button button4;
		private ListBox listBox1;
		private Button button5;
	}
}
