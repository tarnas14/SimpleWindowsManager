namespace SimpleWindowsManager.WindowGrid
{
    partial class GridSwitcher
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
            this._gridList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _gridList
            // 
            this._gridList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gridList.FormattingEnabled = true;
            this._gridList.Location = new System.Drawing.Point(12, 12);
            this._gridList.Name = "_gridList";
            this._gridList.Size = new System.Drawing.Size(264, 21);
            this._gridList.TabIndex = 0;
            // 
            // GridSwitcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 45);
            this.Controls.Add(this._gridList);
            this.Name = "GridSwitcher";
            this.Text = "Choose grid";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox _gridList;
    }
}