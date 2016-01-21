namespace Poker
{
    public partial class PokerForm
    {
        #region Windows Form Designer generated code

        private System.Windows.Forms.Button buttonFold;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Button bRaise;
        private System.Windows.Forms.ProgressBar progressBarTimer;
        private System.Windows.Forms.TextBox textBoxChips;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxAdd;
        private System.Windows.Forms.TextBox textBoxBot5Chips;
        private System.Windows.Forms.TextBox textBoxBot4Chips;
        private System.Windows.Forms.TextBox textBoxBot3Chips;
        private System.Windows.Forms.TextBox textBoxBot2Chips;
        private System.Windows.Forms.TextBox textBoxBot1Chips;
        private System.Windows.Forms.TextBox textBoxPot;
        private System.Windows.Forms.Button buttonOptions;
        private System.Windows.Forms.Button buttonBB;
        private System.Windows.Forms.TextBox textboxSB;
        private System.Windows.Forms.Button buttonSB;
        private System.Windows.Forms.TextBox textBoxBB;
        private System.Windows.Forms.Label bot5Status;
        private System.Windows.Forms.Label bot4Status;
        private System.Windows.Forms.Label bot3Status;
        private System.Windows.Forms.Label bot2Status;
        private System.Windows.Forms.Label bot1Status;
        private System.Windows.Forms.Label playerStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRaise;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonFold = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonCall = new System.Windows.Forms.Button();
            this.bRaise = new System.Windows.Forms.Button();
            this.progressBarTimer = new System.Windows.Forms.ProgressBar();
            this.textBoxChips = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxAdd = new System.Windows.Forms.TextBox();
            this.textBoxBot5Chips = new System.Windows.Forms.TextBox();
            this.textBoxBot4Chips = new System.Windows.Forms.TextBox();
            this.textBoxBot3Chips = new System.Windows.Forms.TextBox();
            this.textBoxBot2Chips = new System.Windows.Forms.TextBox();
            this.textBoxBot1Chips = new System.Windows.Forms.TextBox();
            this.textBoxPot = new System.Windows.Forms.TextBox();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.buttonBB = new System.Windows.Forms.Button();
            this.textboxSB = new System.Windows.Forms.TextBox();
            this.buttonSB = new System.Windows.Forms.Button();
            this.textBoxBB = new System.Windows.Forms.TextBox();
            this.bot5Status = new System.Windows.Forms.Label();
            this.bot4Status = new System.Windows.Forms.Label();
            this.bot3Status = new System.Windows.Forms.Label();
            this.bot1Status = new System.Windows.Forms.Label();
            this.playerStatus = new System.Windows.Forms.Label();
            this.bot2Status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRaise = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bFold
            // 
            this.buttonFold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFold.Location = new System.Drawing.Point(335, 660);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new System.Drawing.Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.ButtonFoldClick);
            // 
            // bCheck
            // 
            this.buttonCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(494, 660);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.ButtonCheckClick);
            // 
            // bCall
            // 
            this.buttonCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(667, 661);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.ButtonCallClick);
            // 
            // bRaise
            // 
            this.bRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bRaise.Location = new System.Drawing.Point(835, 661);
            this.bRaise.Name = "bRaise";
            this.bRaise.Size = new System.Drawing.Size(124, 62);
            this.bRaise.TabIndex = 4;
            this.bRaise.Text = "Raise";
            this.bRaise.UseVisualStyleBackColor = true;
            this.bRaise.Click += new System.EventHandler(this.ButtonRaiseClick);
            // 
            // pbTimer
            // 
            this.progressBarTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.progressBarTimer.BackColor = System.Drawing.SystemColors.Control;
            this.progressBarTimer.Location = new System.Drawing.Point(335, 631);
            this.progressBarTimer.Maximum = 1000;
            this.progressBarTimer.Name = "progressBarTimer";
            this.progressBarTimer.Size = new System.Drawing.Size(667, 23);
            this.progressBarTimer.TabIndex = 5;
            this.progressBarTimer.Value = 1000;
            // 
            // tbChips
            // 
            this.textBoxChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxChips.Location = new System.Drawing.Point(755, 553);
            this.textBoxChips.Name = "textBoxChips";
            this.textBoxChips.Size = new System.Drawing.Size(163, 23);
            this.textBoxChips.TabIndex = 6;
            this.textBoxChips.Text = "Chips : 0";
            // 
            // bAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(12, 697);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 25);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "AddChips";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
            // 
            // tbAdd
            // 
            this.textBoxAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxAdd.Location = new System.Drawing.Point(93, 700);
            this.textBoxAdd.Name = "textBoxAdd";
            this.textBoxAdd.Size = new System.Drawing.Size(125, 20);
            this.textBoxAdd.TabIndex = 8;
            // 
            // tbBotChips5
            // 
            this.textBoxBot5Chips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBot5Chips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBot5Chips.Location = new System.Drawing.Point(1012, 553);
            this.textBoxBot5Chips.Name = "textBoxBot5Chips";
            this.textBoxBot5Chips.Size = new System.Drawing.Size(152, 23);
            this.textBoxBot5Chips.TabIndex = 9;
            this.textBoxBot5Chips.Text = "Chips : 0";
            // 
            // tbBotChips4
            // 
            this.textBoxBot4Chips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBot4Chips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBot4Chips.Location = new System.Drawing.Point(970, 81);
            this.textBoxBot4Chips.Name = "textBoxBot4Chips";
            this.textBoxBot4Chips.Size = new System.Drawing.Size(123, 23);
            this.textBoxBot4Chips.TabIndex = 10;
            this.textBoxBot4Chips.Text = "Chips : 0";
            // 
            // tbBotChips3
            // 
            this.textBoxBot3Chips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBot3Chips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBot3Chips.Location = new System.Drawing.Point(755, 81);
            this.textBoxBot3Chips.Name = "textBoxBot3Chips";
            this.textBoxBot3Chips.Size = new System.Drawing.Size(125, 23);
            this.textBoxBot3Chips.TabIndex = 11;
            this.textBoxBot3Chips.Text = "Chips : 0";
            // 
            // tbBotChips2
            // 
            this.textBoxBot2Chips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBot2Chips.Location = new System.Drawing.Point(276, 81);
            this.textBoxBot2Chips.Name = "textBoxBot2Chips";
            this.textBoxBot2Chips.Size = new System.Drawing.Size(133, 23);
            this.textBoxBot2Chips.TabIndex = 12;
            this.textBoxBot2Chips.Text = "Chips : 0";
            // 
            // tbBotChips1
            // 
            this.textBoxBot1Chips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxBot1Chips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBot1Chips.Location = new System.Drawing.Point(181, 553);
            this.textBoxBot1Chips.Name = "textBoxBot1Chips";
            this.textBoxBot1Chips.Size = new System.Drawing.Size(142, 23);
            this.textBoxBot1Chips.TabIndex = 13;
            this.textBoxBot1Chips.Text = "Chips : 0";
            // 
            // tbPot
            // 
            this.textBoxPot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPot.Location = new System.Drawing.Point(606, 212);
            this.textBoxPot.Name = "textBoxPot";
            this.textBoxPot.Size = new System.Drawing.Size(125, 23);
            this.textBoxPot.TabIndex = 14;
            this.textBoxPot.Text = "0";
            // 
            // bOptions
            // 
            this.buttonOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOptions.Location = new System.Drawing.Point(12, 12);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(75, 36);
            this.buttonOptions.TabIndex = 15;
            this.buttonOptions.Text = "BB/SB";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.ButtonOptionsClick);
            // 
            // bBB
            // 
            this.buttonBB.Location = new System.Drawing.Point(12, 254);
            this.buttonBB.Name = "buttonBB";
            this.buttonBB.Size = new System.Drawing.Size(75, 23);
            this.buttonBB.TabIndex = 16;
            this.buttonBB.Text = "Big Blind";
            this.buttonBB.UseVisualStyleBackColor = true;
            this.buttonBB.Click += new System.EventHandler(this.ButtonBBClick);
            // 
            // tbSB
            // 
            this.textboxSB.Location = new System.Drawing.Point(12, 228);
            this.textboxSB.Name = "textboxSB";
            this.textboxSB.Size = new System.Drawing.Size(75, 20);
            this.textboxSB.TabIndex = 17;
            this.textboxSB.Text = "250";
            // 
            // bSB
            // 
            this.buttonSB.Location = new System.Drawing.Point(12, 199);
            this.buttonSB.Name = "buttonSB";
            this.buttonSB.Size = new System.Drawing.Size(75, 23);
            this.buttonSB.TabIndex = 18;
            this.buttonSB.Text = "Small Blind";
            this.buttonSB.UseVisualStyleBackColor = true;
            this.buttonSB.Click += new System.EventHandler(this.ButtonSBClick);
            // 
            // tbBB
            // 
            this.textBoxBB.Location = new System.Drawing.Point(12, 283);
            this.textBoxBB.Name = "textBoxBB";
            this.textBoxBB.Size = new System.Drawing.Size(75, 20);
            this.textBoxBB.TabIndex = 19;
            this.textBoxBB.Text = "500";
            // 
            // b5Status
            // 
            this.bot5Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bot5Status.Location = new System.Drawing.Point(1012, 579);
            this.bot5Status.Name = "bot5Status";
            this.bot5Status.Size = new System.Drawing.Size(152, 32);
            this.bot5Status.TabIndex = 26;
            // 
            // b4Status
            // 
            this.bot4Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot4Status.Location = new System.Drawing.Point(970, 107);
            this.bot4Status.Name = "bot4Status";
            this.bot4Status.Size = new System.Drawing.Size(123, 32);
            this.bot4Status.TabIndex = 27;
            // 
            // b3Status
            // 
            this.bot3Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot3Status.Location = new System.Drawing.Point(755, 107);
            this.bot3Status.Name = "bot3Status";
            this.bot3Status.Size = new System.Drawing.Size(125, 32);
            this.bot3Status.TabIndex = 28;
            // 
            // b1Status
            // 
            this.bot1Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bot1Status.Location = new System.Drawing.Point(181, 579);
            this.bot1Status.Name = "bot1Status";
            this.bot1Status.Size = new System.Drawing.Size(142, 32);
            this.bot1Status.TabIndex = 29;
            // 
            // pStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(755, 579);
            this.playerStatus.Name = "playerStatus";
            this.playerStatus.Size = new System.Drawing.Size(163, 32);
            this.playerStatus.TabIndex = 30;
            // 
            // b2Status
            // 
            this.bot2Status.Location = new System.Drawing.Point(276, 107);
            this.bot2Status.Name = "bot2Status";
            this.bot2Status.Size = new System.Drawing.Size(133, 32);
            this.bot2Status.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(654, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pot";
            // 
            // tbRaise
            // 
            this.tbRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbRaise.Location = new System.Drawing.Point(965, 703);
            this.tbRaise.Name = "tbRaise";
            this.tbRaise.Size = new System.Drawing.Size(108, 20);
            this.tbRaise.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.tbRaise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bot2Status);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.bot1Status);
            this.Controls.Add(this.bot3Status);
            this.Controls.Add(this.bot4Status);
            this.Controls.Add(this.bot5Status);
            this.Controls.Add(this.textBoxBB);
            this.Controls.Add(this.buttonSB);
            this.Controls.Add(this.textboxSB);
            this.Controls.Add(this.buttonBB);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.textBoxPot);
            this.Controls.Add(this.textBoxBot1Chips);
            this.Controls.Add(this.textBoxBot2Chips);
            this.Controls.Add(this.textBoxBot3Chips);
            this.Controls.Add(this.textBoxBot4Chips);
            this.Controls.Add(this.textBoxBot5Chips);
            this.Controls.Add(this.textBoxAdd);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxChips);
            this.Controls.Add(this.progressBarTimer);
            this.Controls.Add(this.bRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.LayoutChange);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion    
    }
}