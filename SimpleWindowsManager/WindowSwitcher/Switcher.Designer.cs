namespace SimpleWindowsManager.WindowSwitcher
{
    partial class Switcher
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
            this._windowTitles = new AutoCompleteCombobox();
            this.SuspendLayout();
            // 
            // _windowTitles
            // 
            this._windowTitles.AutoCompleteAfterCharacterCount = 3;
            this._windowTitles.Location = new System.Drawing.Point(12, 12);
            this._windowTitles.Name = "_windowTitles";
            this._windowTitles.Size = new System.Drawing.Size(322, 29);
            this._windowTitles.TabIndex = 1;
            // 
            // Switcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 50);
            this.ControlBox = false;
            this.Controls.Add(this._windowTitles);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(362, 89);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(0, 89);
            this.Name = "Switcher";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Switcher";
            this.ResumeLayout(false);

        }

        #endregion

        private AutoCompleteCombobox _windowTitles;

    }
}

