﻿namespace SimpleWindowsManager
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
            this._windowTitles = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _windowTitles
            // 
            this._windowTitles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._windowTitles.FormattingEnabled = true;
            this._windowTitles.Location = new System.Drawing.Point(12, 12);
            this._windowTitles.Name = "_windowTitles";
            this._windowTitles.Size = new System.Drawing.Size(209, 21);
            this._windowTitles.TabIndex = 0;
            // 
            // Switcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 48);
            this.Controls.Add(this._windowTitles);
            this.Name = "Switcher";
            this.Text = "Switcher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox _windowTitles;
    }
}

