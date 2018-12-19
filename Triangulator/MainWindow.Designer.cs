namespace Triangulator
{
	partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_File_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Operations = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Operations_Monotone = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenPolyDialog = new System.Windows.Forms.OpenFileDialog();
            this.SavePolyDialog = new System.Windows.Forms.SaveFileDialog();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _timer
            // 
            this._timer.Interval = 33;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.MainMenu_Operations});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(965, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File_Open,
            this.MainMenu_File_Save,
            this.toolStripSeparator1,
            this.MainMenu_File_Clear,
            this.toolStripSeparator2,
            this.MainMenu_File_Exit});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // MainMenu_File_Open
            // 
            this.MainMenu_File_Open.Name = "MainMenu_File_Open";
            this.MainMenu_File_Open.Size = new System.Drawing.Size(132, 22);
            this.MainMenu_File_Open.Text = "Открыть";
            this.MainMenu_File_Open.Click += new System.EventHandler(this.MainMenu_File_Open_Click);
            // 
            // MainMenu_File_Save
            // 
            this.MainMenu_File_Save.Name = "MainMenu_File_Save";
            this.MainMenu_File_Save.Size = new System.Drawing.Size(132, 22);
            this.MainMenu_File_Save.Text = "Сохранить";
            this.MainMenu_File_Save.Click += new System.EventHandler(this.MainMenu_File_Save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(129, 6);
            // 
            // MainMenu_File_Clear
            // 
            this.MainMenu_File_Clear.Name = "MainMenu_File_Clear";
            this.MainMenu_File_Clear.Size = new System.Drawing.Size(132, 22);
            this.MainMenu_File_Clear.Text = "Очистить";
            this.MainMenu_File_Clear.Click += new System.EventHandler(this.MainMenu_File_Clear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(129, 6);
            // 
            // MainMenu_File_Exit
            // 
            this.MainMenu_File_Exit.Name = "MainMenu_File_Exit";
            this.MainMenu_File_Exit.Size = new System.Drawing.Size(132, 22);
            this.MainMenu_File_Exit.Text = "Выход";
            this.MainMenu_File_Exit.Click += new System.EventHandler(this.MainMenu_File_Exit_Click);
            // 
            // MainMenu_Operations
            // 
            this.MainMenu_Operations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Operations_Monotone});
            this.MainMenu_Operations.Name = "MainMenu_Operations";
            this.MainMenu_Operations.Size = new System.Drawing.Size(75, 20);
            this.MainMenu_Operations.Text = "Операции";
            // 
            // MainMenu_Operations_Monotone
            // 
            this.MainMenu_Operations_Monotone.Name = "MainMenu_Operations_Monotone";
            this.MainMenu_Operations_Monotone.Size = new System.Drawing.Size(204, 22);
            this.MainMenu_Operations_Monotone.Text = "Монотонный алгоритм";
            this.MainMenu_Operations_Monotone.Click += new System.EventHandler(this.MainMenu_Operations_Monotone_Click);
            // 
            // OpenPolyDialog
            // 
            this.OpenPolyDialog.Filter = "Polygons files|*.poly";
            // 
            // SavePolyDialog
            // 
            this.SavePolyDialog.Filter = "Polygons files|*.poly";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 605);
            this.Controls.Add(this.MainMenu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainWindow";
            this.Text = "Триангуляция";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Click += new System.EventHandler(this.MainWindow_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer _timer;
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem MainMenu_Operations;
		private System.Windows.Forms.ToolStripMenuItem MainMenu_Operations_Monotone;
		private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MainMenu_File_Open;
		private System.Windows.Forms.ToolStripMenuItem MainMenu_File_Save;
		private System.Windows.Forms.OpenFileDialog OpenPolyDialog;
		private System.Windows.Forms.SaveFileDialog SavePolyDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem MainMenu_File_Exit;
		private System.Windows.Forms.ToolStripMenuItem MainMenu_File_Clear;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}

