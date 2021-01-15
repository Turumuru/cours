namespace Course
{
  partial class ManagerForm
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
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.поступлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.списаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.инвентаризацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.panel1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(213)))), ((int)(((byte)(202)))));
      this.panel1.Controls.Add(this.menuStrip1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(492, 331);
      this.panel1.TabIndex = 1;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поступлениеToolStripMenuItem,
            this.списаниеToolStripMenuItem,
            this.инвентаризацияToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(492, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // поступлениеToolStripMenuItem
      // 
      this.поступлениеToolStripMenuItem.Name = "поступлениеToolStripMenuItem";
      this.поступлениеToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
      this.поступлениеToolStripMenuItem.Text = "Поступление";
      // 
      // списаниеToolStripMenuItem
      // 
      this.списаниеToolStripMenuItem.Name = "списаниеToolStripMenuItem";
      this.списаниеToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
      this.списаниеToolStripMenuItem.Text = "Списание";
      // 
      // инвентаризацияToolStripMenuItem
      // 
      this.инвентаризацияToolStripMenuItem.Name = "инвентаризацияToolStripMenuItem";
      this.инвентаризацияToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
      this.инвентаризацияToolStripMenuItem.Text = "Инвентаризация";
      // 
      // ManagerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(492, 331);
      this.Controls.Add(this.panel1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "ManagerForm";
      this.Text = "ManagerForm";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManagerForm_FormClosed);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem поступлениеToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem списаниеToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem инвентаризацияToolStripMenuItem;

  }
}