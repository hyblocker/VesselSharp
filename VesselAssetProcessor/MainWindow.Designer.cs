
namespace VesselAssetProcessor
{
	partial class MainWindow
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileMenuStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newAssetBankFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openRecentFilesMenuDropDown = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editMenuStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.addItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.excludeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewMenuStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildMenuStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rebuildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cleanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cancelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpMenuStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuStripMenuItem,
            this.editMenuStripMenuItem,
            this.viewMenuStripMenuItem,
            this.buildMenuStripMenuItem,
            this.helpMenuStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileMenuStripMenuItem
			// 
			this.fileMenuStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAssetBankFileMenuItem,
            this.openMenuItem,
            this.openRecentFilesMenuDropDown,
            this.toolStripSeparator1,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.toolStripSeparator2,
            this.exitMenuItem});
			this.fileMenuStripMenuItem.Name = "fileMenuStripMenuItem";
			this.fileMenuStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileMenuStripMenuItem.Text = "&File";
			// 
			// newAssetBankFileMenuItem
			// 
			this.newAssetBankFileMenuItem.Name = "newAssetBankFileMenuItem";
			this.newAssetBankFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newAssetBankFileMenuItem.Size = new System.Drawing.Size(218, 22);
			this.newAssetBankFileMenuItem.Text = "&New Asset Bank...";
			this.newAssetBankFileMenuItem.Click += new System.EventHandler(this.newAssetBankFileMenuItem_Click);
			// 
			// openMenuItem
			// 
			this.openMenuItem.Name = "openMenuItem";
			this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openMenuItem.Size = new System.Drawing.Size(218, 22);
			this.openMenuItem.Text = "&Open...";
			this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
			// 
			// openRecentFilesMenuDropDown
			// 
			this.openRecentFilesMenuDropDown.Enabled = false;
			this.openRecentFilesMenuDropDown.Name = "openRecentFilesMenuDropDown";
			this.openRecentFilesMenuDropDown.Size = new System.Drawing.Size(218, 22);
			this.openRecentFilesMenuDropDown.Text = "Open Recent";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
			// 
			// saveMenuItem
			// 
			this.saveMenuItem.Name = "saveMenuItem";
			this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveMenuItem.Size = new System.Drawing.Size(218, 22);
			this.saveMenuItem.Text = "&Save...";
			this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
			// 
			// saveAsMenuItem
			// 
			this.saveAsMenuItem.Name = "saveAsMenuItem";
			this.saveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsMenuItem.Size = new System.Drawing.Size(218, 22);
			this.saveAsMenuItem.Text = "Sa&ve As";
			this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitMenuItem.Size = new System.Drawing.Size(218, 22);
			this.exitMenuItem.Text = "Exit";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// editMenuStripMenuItem
			// 
			this.editMenuStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem,
            this.toolStripSeparator5,
            this.addItemMenuItem,
            this.toolStripSeparator6,
            this.excludeMenuItem,
            this.renameMenuItem,
            this.toolStripSeparator4,
            this.settingsMenuItem});
			this.editMenuStripMenuItem.Name = "editMenuStripMenuItem";
			this.editMenuStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editMenuStripMenuItem.Text = "&Edit";
			// 
			// undoMenuItem
			// 
			this.undoMenuItem.Name = "undoMenuItem";
			this.undoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoMenuItem.Size = new System.Drawing.Size(218, 22);
			this.undoMenuItem.Text = "&Undo";
			this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
			// 
			// redoMenuItem
			// 
			this.redoMenuItem.Name = "redoMenuItem";
			this.redoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoMenuItem.Size = new System.Drawing.Size(218, 22);
			this.redoMenuItem.Text = "&Redo";
			this.redoMenuItem.Click += new System.EventHandler(this.redoMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(215, 6);
			// 
			// addItemMenuItem
			// 
			this.addItemMenuItem.Name = "addItemMenuItem";
			this.addItemMenuItem.Size = new System.Drawing.Size(218, 22);
			this.addItemMenuItem.Text = "&Add";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(215, 6);
			// 
			// excludeMenuItem
			// 
			this.excludeMenuItem.Name = "excludeMenuItem";
			this.excludeMenuItem.Size = new System.Drawing.Size(218, 22);
			this.excludeMenuItem.Text = "&Exclude From Project";
			this.excludeMenuItem.Click += new System.EventHandler(this.excludeMenuItem_Click);
			// 
			// renameMenuItem
			// 
			this.renameMenuItem.Name = "renameMenuItem";
			this.renameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.renameMenuItem.Size = new System.Drawing.Size(218, 22);
			this.renameMenuItem.Text = "Re&name";
			this.renameMenuItem.Click += new System.EventHandler(this.renameMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(215, 6);
			// 
			// settingsMenuItem
			// 
			this.settingsMenuItem.Name = "settingsMenuItem";
			this.settingsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.settingsMenuItem.Size = new System.Drawing.Size(218, 22);
			this.settingsMenuItem.Text = "&Edit Asset Bank Settings";
			this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
			// 
			// viewMenuStripMenuItem
			// 
			this.viewMenuStripMenuItem.Name = "viewMenuStripMenuItem";
			this.viewMenuStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewMenuStripMenuItem.Text = "&View";
			// 
			// buildMenuStripMenuItem
			// 
			this.buildMenuStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildMenuItem,
            this.rebuildMenuItem,
            this.cleanMenuItem,
            this.cancelMenuItem});
			this.buildMenuStripMenuItem.Name = "buildMenuStripMenuItem";
			this.buildMenuStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.buildMenuStripMenuItem.Text = "&Build";
			// 
			// buildMenuItem
			// 
			this.buildMenuItem.Name = "buildMenuItem";
			this.buildMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.buildMenuItem.Size = new System.Drawing.Size(215, 22);
			this.buildMenuItem.Text = "&Build";
			this.buildMenuItem.Click += new System.EventHandler(this.buildMenuItem_Click);
			// 
			// rebuildMenuItem
			// 
			this.rebuildMenuItem.Name = "rebuildMenuItem";
			this.rebuildMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
			this.rebuildMenuItem.Size = new System.Drawing.Size(215, 22);
			this.rebuildMenuItem.Text = "&Rebuild";
			this.rebuildMenuItem.Click += new System.EventHandler(this.rebuildMenuItem_Click);
			// 
			// cleanMenuItem
			// 
			this.cleanMenuItem.Name = "cleanMenuItem";
			this.cleanMenuItem.Size = new System.Drawing.Size(215, 22);
			this.cleanMenuItem.Text = "&Clean";
			this.cleanMenuItem.Click += new System.EventHandler(this.cleanMenuItem_Click);
			// 
			// cancelMenuItem
			// 
			this.cancelMenuItem.Name = "cancelMenuItem";
			this.cancelMenuItem.Size = new System.Drawing.Size(215, 22);
			this.cancelMenuItem.Text = "Ca&ncel";
			this.cancelMenuItem.Click += new System.EventHandler(this.cancelMenuItem_Click);
			// 
			// helpMenuStripMenuItem
			// 
			this.helpMenuStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpMenuItem,
            this.toolStripSeparator3,
            this.aboutMenuItem});
			this.helpMenuStripMenuItem.Name = "helpMenuStripMenuItem";
			this.helpMenuStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpMenuStripMenuItem.Text = "&Help";
			// 
			// helpMenuItem
			// 
			this.helpMenuItem.Name = "helpMenuItem";
			this.helpMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.helpMenuItem.Size = new System.Drawing.Size(146, 22);
			this.helpMenuItem.Text = "View &Help";
			this.helpMenuItem.Click += new System.EventHandler(this.helpMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
			// 
			// aboutMenuItem
			// 
			this.aboutMenuItem.Name = "aboutMenuItem";
			this.aboutMenuItem.Size = new System.Drawing.Size(146, 22);
			this.aboutMenuItem.Text = "&About";
			this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainWindow";
			this.Text = "Asset Processor";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileMenuStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editMenuStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewMenuStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem buildMenuStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpMenuStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newAssetBankFileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openRecentFilesMenuDropDown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addItemMenuItem;
		private System.Windows.Forms.ToolStripMenuItem excludeMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem buildMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rebuildMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cleanMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cancelMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
	}
}

