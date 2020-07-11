namespace SvartaJump
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.playButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.scoreLabel = new System.Windows.Forms.Label();
            this.highScoresList = new System.Windows.Forms.Label();
            this.scoreAfterDeath = new System.Windows.Forms.Label();
            this.playAgainButton = new System.Windows.Forms.Button();
            this.warningLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.SystemColors.Desktop;
            this.playButton.Font = new System.Drawing.Font("Comic Sans MS", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.playButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.playButton.Location = new System.Drawing.Point(169, 243);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(261, 110);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "HRAJ!";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.scoreLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.scoreLabel.Location = new System.Drawing.Point(472, 781);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(93, 38);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "label1";
            // 
            // highScoresList
            // 
            this.highScoresList.AutoSize = true;
            this.highScoresList.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.highScoresList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.highScoresList.Location = new System.Drawing.Point(162, 482);
            this.highScoresList.Name = "highScoresList";
            this.highScoresList.Size = new System.Drawing.Size(92, 38);
            this.highScoresList.TabIndex = 2;
            this.highScoresList.Text = "label2";
            // 
            // scoreAfterDeath
            // 
            this.scoreAfterDeath.AutoSize = true;
            this.scoreAfterDeath.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.scoreAfterDeath.Location = new System.Drawing.Point(97, 399);
            this.scoreAfterDeath.Name = "scoreAfterDeath";
            this.scoreAfterDeath.Size = new System.Drawing.Size(166, 67);
            this.scoreAfterDeath.TabIndex = 3;
            this.scoreAfterDeath.Text = "label3";
            this.scoreAfterDeath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playAgainButton
            // 
            this.playAgainButton.BackColor = System.Drawing.SystemColors.Desktop;
            this.playAgainButton.Font = new System.Drawing.Font("Comic Sans MS", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.playAgainButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.playAgainButton.Location = new System.Drawing.Point(121, 651);
            this.playAgainButton.Name = "playAgainButton";
            this.playAgainButton.Size = new System.Drawing.Size(368, 115);
            this.playAgainButton.TabIndex = 4;
            this.playAgainButton.Text = "ZNOVA!";
            this.playAgainButton.UseVisualStyleBackColor = false;
            this.playAgainButton.Click += new System.EventHandler(this.playAgainButton_Click);
            // 
            // warningLabel
            // 
            this.warningLabel.AutoSize = true;
            this.warningLabel.Font = new System.Drawing.Font("Comic Sans MS", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.warningLabel.Location = new System.Drawing.Point(98, 603);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(413, 135);
            this.warningLabel.TabIndex = 5;
            this.warningLabel.Text = "POZOR!";
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Comic Sans MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exitButton.Location = new System.Drawing.Point(216, 781);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(163, 60);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pauseButton.Location = new System.Drawing.Point(38, 820);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(91, 34);
            this.pauseButton.TabIndex = 7;
            this.pauseButton.Text = "PAUSE";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 881);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.warningLabel);
            this.Controls.Add(this.playAgainButton);
            this.Controls.Add(this.scoreAfterDeath);
            this.Controls.Add(this.highScoresList);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.playButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label highScoresList;
        private System.Windows.Forms.Label scoreAfterDeath;
        private System.Windows.Forms.Button playAgainButton;
        private System.Windows.Forms.Label warningLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button pauseButton;
    }
}

