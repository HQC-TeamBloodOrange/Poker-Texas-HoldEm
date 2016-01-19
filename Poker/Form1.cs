namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        //Variables
        #region Variables

        public int Nm; //What the fuck is this ??

        //Constatns
        private readonly Panel playerPanel = new Panel();

        private readonly Panel bot1Panel = new Panel();

        private readonly Panel bot2Panel = new Panel();

        private readonly Panel bot3Panel = new Panel();

        private readonly Panel bot4Panel = new Panel();

        private readonly Panel bot5Panel = new Panel();

        private readonly List<bool?> bools = new List<bool?>();

        private readonly List<Type> Win = new List<Type>();

        private readonly List<string> CheckWinners = new List<string>();

        private readonly List<int> ints = new List<int>();

        private readonly int[] Reserve = new int[17];

        private readonly Image[] Deck = new Image[52];

        private readonly PictureBox[] Holder = new PictureBox[52];

        private readonly Timer timer = new Timer();

        private readonly Timer Updates = new Timer();

        //Fields

        private ProgressBar progressBar = new ProgressBar();

        private int call = 500, foldedPlayers = 5;

        public int playerChips = 10000, bot1Chips = 10000, bot2Chips = 10000, bot3Chips = 10000, bot4Chips = 10000, bot5Chips = 10000;

        private double type, rounds, bot1Power, bot2Power, bot3Power, bot4Power, bot5Power, playerPower, playerType = -1, raise, bot1Type = -1, bot2Type = -1, bot3Type = -1, bot4Type = -1, bot5Type = -1;

        private bool isBot1Turn, isBot2Turn, isBot3Turn, isBot4Turn, isBot5Turn;

        private bool B1Fturn, B2Fturn, B3Fturn, B4Fturn, B5Fturn;

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private bool playerFolded, bot1Folded, bot2Folded, bot3Folded, bot4Folded, bot5Folded, intsadded, changed;

        private int playerCall, bot1Call, bot2Call, bot3Call, bot4Call, bot5Call, playerRaise, bot1Raise, bot2Raise, bot3Raise, bot4Raise, bot5Raise;

        private int height, width, winners, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;

        private int last = 123, raisedTurn = 1;

        

        private bool PFturn, isPlayerTurn = true, restart, raising;

        private Type sorted;

        private string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);

        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/

        

        private int time = 60, i, bigBlind = 500, smallBlind = 250, maxBlind = 10000000, turnCount; // TODO: Make them in constants;

        #endregion

        public Form1()
        {
            ////bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            this.call = this.bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Updates.Start();
            this.InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            this.Shuffle();
            this.tbPot.Enabled = false;
            this.tbChips.Enabled = false;
            this.tbBotChips1.Enabled = false;
            this.tbBotChips2.Enabled = false;
            this.tbBotChips3.Enabled = false;
            this.tbBotChips4.Enabled = false;
            this.tbBotChips5.Enabled = false;
            this.tbChips.Text = "Chips : " + this.playerChips;
            this.tbBotChips1.Text = "Chips : " + this.bot1Chips;
            this.tbBotChips2.Text = "Chips : " + this.bot2Chips;
            this.tbBotChips3.Text = "Chips : " + this.bot3Chips;
            this.tbBotChips4.Text = "Chips : " + this.bot4Chips;
            this.tbBotChips5.Text = "Chips : " + this.bot5Chips;
            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += this.timer_Tick;
            this.Updates.Interval = (1 * 1 * 100);
            this.Updates.Tick += this.Update_Tick;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.bSB.Visible = true;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.bSB.Visible = true;
            this.tbBB.Visible = false;
            this.tbSB.Visible = false;
            this.bBB.Visible = false;
            this.bSB.Visible = false;
            this.tbRaise.Text = (this.bigBlind * 2).ToString();
        }

        private async Task Shuffle()
        {
            this.bools.Add(this.PFturn);
            this.bools.Add(this.B1Fturn);
            this.bools.Add(this.B2Fturn);
            this.bools.Add(this.B3Fturn);
            this.bools.Add(this.B4Fturn);
            this.bools.Add(this.B5Fturn);
            this.bCall.Enabled = false;
            this.bRaise.Enabled = false;
            this.bFold.Enabled = false;
            this.bCheck.Enabled = false;
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            var check = false;
            var backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            var r = new Random();

            for (this.i = this.ImgLocation.Length; this.i > 0; this.i--)
            {
                var j = r.Next(this.i);
                var k = this.ImgLocation[j];
                this.ImgLocation[j] = this.ImgLocation[this.i - 1];
                this.ImgLocation[this.i - 1] = k;
            }

            for (this.i = 0; this.i < 17; this.i++)
            {
                this.Deck[this.i] = Image.FromFile(this.ImgLocation[this.i]);
                var charsToRemove = new[]
                {
                    "Assets\\Cards\\", ".png"
                };

                foreach (var c in charsToRemove)
                {
                    this.ImgLocation[this.i] = this.ImgLocation[this.i].Replace(c, string.Empty);
                }

                this.Reserve[this.i] = int.Parse(this.ImgLocation[this.i]) - 1;
                this.Holder[this.i] = new PictureBox();
                this.Holder[this.i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Holder[this.i].Height = 130;
                this.Holder[this.i].Width = 80;
                this.Controls.Add(this.Holder[this.i]);
                this.Holder[this.i].Name = "pb" + this.i;
                await Task.Delay(200);

                #region Throwing Cards

                if (this.i < 2)
                {
                    if (this.Holder[0].Tag != null)
                    {
                        this.Holder[1].Tag = this.Reserve[1];
                    }

                    this.Holder[0].Tag = this.Reserve[0];
                    this.Holder[this.i].Image = this.Deck[this.i];
                    this.Holder[this.i].Anchor = (AnchorStyles.Bottom);

                    ////Holder[i].Dock = DockStyle.Top;
                    this.Holder[this.i].Location = new Point(horizontal, vertical);
                    horizontal += this.Holder[this.i].Width;
                    this.Controls.Add(this.playerPanel);
                    this.playerPanel.Location = new Point(this.Holder[0].Left - 10, this.Holder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }

                if (this.bot1Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 2 && this.i < 4)
                    {
                        if (this.Holder[2].Tag != null)
                        {
                            this.Holder[3].Tag = this.Reserve[3];
                        }

                        this.Holder[2].Tag = this.Reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot1Panel);
                        this.bot1Panel.Location = new Point(this.Holder[2].Left - 10, this.Holder[2].Top - 10);
                        this.bot1Panel.BackColor = Color.DarkBlue;
                        this.bot1Panel.Height = 150;
                        this.bot1Panel.Width = 180;
                        this.bot1Panel.Visible = false;
                        if (this.i == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot2Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 4 && this.i < 6)
                    {
                        if (this.Holder[4].Tag != null)
                        {
                            this.Holder[5].Tag = this.Reserve[5];
                        }

                        this.Holder[4].Tag = this.Reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot2Panel);
                        this.bot2Panel.Location = new Point(this.Holder[4].Left - 10, this.Holder[4].Top - 10);
                        this.bot2Panel.BackColor = Color.DarkBlue;
                        this.bot2Panel.Height = 150;
                        this.bot2Panel.Width = 180;
                        this.bot2Panel.Visible = false;
                        if (this.i == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot3Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 6 && this.i < 8)
                    {
                        if (this.Holder[6].Tag != null)
                        {
                            this.Holder[7].Tag = this.Reserve[7];
                        }

                        this.Holder[6].Tag = this.Reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot3Panel);
                        this.bot3Panel.Location = new Point(this.Holder[6].Left - 10, this.Holder[6].Top - 10);
                        this.bot3Panel.BackColor = Color.DarkBlue;
                        this.bot3Panel.Height = 150;
                        this.bot3Panel.Width = 180;
                        this.bot3Panel.Visible = false;
                        if (this.i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (this.bot4Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 8 && this.i < 10)
                    {
                        if (this.Holder[8].Tag != null)
                        {
                            this.Holder[9].Tag = this.Reserve[9];
                        }

                        this.Holder[8].Tag = this.Reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot4Panel);
                        this.bot4Panel.Location = new Point(this.Holder[8].Left - 10, this.Holder[8].Top - 10);
                        this.bot4Panel.BackColor = Color.DarkBlue;
                        this.bot4Panel.Height = 150;
                        this.bot4Panel.Width = 180;
                        this.bot4Panel.Visible = false;
                        if (this.i == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot5Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 10 && this.i < 12)
                    {
                        if (this.Holder[10].Tag != null)
                        {
                            this.Holder[11].Tag = this.Reserve[11];
                        }

                        this.Holder[10].Tag = this.Reserve[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot5Panel);
                        this.bot5Panel.Location = new Point(this.Holder[10].Left - 10, this.Holder[10].Top - 10);
                        this.bot5Panel.BackColor = Color.DarkBlue;
                        this.bot5Panel.Height = 150;
                        this.bot5Panel.Width = 180;
                        this.bot5Panel.Visible = false;
                        if (this.i == 11)
                        {
                            check = false;
                        }
                    }
                }

                /// TODO: Refactor the list of if's. Replace the magic numbers.
                if (this.i >= 12)
                {
                    this.Holder[12].Tag = this.Reserve[12];

                    if (this.i > 12)
                    {
                        this.Holder[13].Tag = this.Reserve[13];
                    }

                    if (this.i > 13)
                    {
                        this.Holder[14].Tag = this.Reserve[14];
                    }

                    if (this.i > 14)
                    {
                        this.Holder[15].Tag = this.Reserve[15];
                    }

                    if (this.i > 15)
                    {
                        this.Holder[16].Tag = this.Reserve[16];
                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;
                    if (this.Holder[this.i] != null)
                    {
                        this.Holder[this.i].Anchor = AnchorStyles.None;
                        this.Holder[this.i].Image = backImage;

                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                #endregion

                if (this.bot1Chips <= 0)
                {
                    this.B1Fturn = true;
                    this.Holder[2].Visible = false;
                    this.Holder[3].Visible = false;
                }
                else
                {
                    this.B1Fturn = false;
                    if (this.i == 3)
                    {
                        if (this.Holder[3] != null)
                        {
                            this.Holder[2].Visible = true;
                            this.Holder[3].Visible = true;
                        }
                    }
                }

                if (this.bot2Chips <= 0)
                {
                    this.B2Fturn = true;
                    this.Holder[4].Visible = false;
                    this.Holder[5].Visible = false;
                }
                else
                {
                    this.B2Fturn = false;
                    if (this.i == 5)
                    {
                        if (this.Holder[5] != null)
                        {
                            this.Holder[4].Visible = true;
                            this.Holder[5].Visible = true;
                        }
                    }
                }

                if (this.bot3Chips <= 0)
                {
                    this.B3Fturn = true;
                    this.Holder[6].Visible = false;
                    this.Holder[7].Visible = false;
                }
                else
                {
                    this.B3Fturn = false;
                    if (this.i == 7)
                    {
                        if (this.Holder[7] != null)
                        {
                            this.Holder[6].Visible = true;
                            this.Holder[7].Visible = true;
                        }
                    }
                }

                if (this.bot4Chips <= 0)
                {
                    this.B4Fturn = true;
                    this.Holder[8].Visible = false;
                    this.Holder[9].Visible = false;
                }
                else
                {
                    this.B4Fturn = false;
                    if (this.i == 9)
                    {
                        if (this.Holder[9] != null)
                        {
                            this.Holder[8].Visible = true;
                            this.Holder[9].Visible = true;
                        }
                    }
                }

                if (this.bot5Chips <= 0)
                {
                    this.B5Fturn = true;
                    this.Holder[10].Visible = false;
                    this.Holder[11].Visible = false;
                }
                else
                {
                    this.B5Fturn = false;
                    if (this.i == 11)
                    {
                        if (this.Holder[11] != null)
                        {
                            this.Holder[10].Visible = true;
                            this.Holder[11].Visible = true;
                        }
                    }
                }

                if (this.i == 16)
                {
                    if (!this.restart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }

                    this.timer.Start();
                }
            }

            if (this.foldedPlayers == 5)
            {
                var dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                this.foldedPlayers = 5;
            }

            if (this.i == 17)
            {
                this.bRaise.Enabled = true;
                this.bCall.Enabled = true;
                this.bRaise.Enabled = true;
                this.bRaise.Enabled = true;
                this.bFold.Enabled = true;
            }
        }

        private async Task Turns()
        {
            #region Rotating

            if (!this.PFturn)
            {
                if (this.isPlayerTurn)
                {
                    this.FixCall(this.pStatus, ref this.playerCall, ref this.playerRaise, 1);
                    ////MessageBox.Show("Player's Turn");
                    this.pbTimer.Visible = true;
                    this.pbTimer.Value = 1000;
                    this.time = 60;
                    this.maxBlind = 10000000;
                    this.timer.Start();
                    this.bRaise.Enabled = true;
                    this.bCall.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.bFold.Enabled = true;
                    this.turnCount++;
                    this.FixCall(this.pStatus, ref this.playerCall, ref this.playerRaise, 2);
                }
            }

            if (this.PFturn || !this.isPlayerTurn)
            {
                await this.AllIn();
                if (this.PFturn && !this.playerFolded)
                {
                    if (this.bCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.playerFolded = true;
                    }
                }

                await this.CheckRaise(0, 0);
                this.pbTimer.Visible = false;
                this.bRaise.Enabled = false;
                this.bCall.Enabled = false;
                this.bRaise.Enabled = false;
                this.bRaise.Enabled = false;
                this.bFold.Enabled = false;
                this.timer.Stop();
                this.isBot1Turn = true;
                if (!this.B1Fturn)
                {
                    if (this.isBot1Turn)
                    {
                        this.FixCall(this.b1Status, ref this.bot1Call, ref this.bot1Raise, 1);
                        this.FixCall(this.b1Status, ref this.bot1Call, ref this.bot1Raise, 2);
                        this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.B1Fturn);
                        MessageBox.Show("Bot 1's Turn");
                        this.AI(2, 3, ref this.bot1Chips, ref this.isBot1Turn, ref this.B1Fturn, this.b1Status, 0, this.bot1Power, this.bot1Type);
                        this.turnCount++;
                        this.last = 1;
                        this.isBot1Turn = false;
                        this.isBot2Turn = true;
                    }
                }

                if (this.B1Fturn && !this.bot1Folded)
                {
                    this.bools.RemoveAt(1);
                    this.bools.Insert(1, null);
                    this.maxLeft--;
                    this.bot1Folded = true;
                }

                if (this.B1Fturn || !this.isBot1Turn)
                {
                    await this.CheckRaise(1, 1);
                    this.isBot2Turn = true;
                }

                if (!this.B2Fturn)
                {
                    if (this.isBot2Turn)
                    {
                        this.FixCall(this.b2Status, ref this.bot2Call, ref this.bot2Raise, 1);
                        this.FixCall(this.b2Status, ref this.bot2Call, ref this.bot2Raise, 2);
                        this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.B2Fturn);
                        MessageBox.Show("Bot 2's Turn");
                        this.AI(4, 5, ref this.bot2Chips, ref this.isBot2Turn, ref this.B2Fturn, this.b2Status, 1, this.bot2Power, this.bot2Type);
                        this.turnCount++;
                        this.last = 2;
                        this.isBot2Turn = false;
                        this.isBot3Turn = true;
                    }
                }

                if (this.B2Fturn && !this.bot2Folded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.maxLeft--;
                    this.bot2Folded = true;
                }

                if (this.B2Fturn || !this.isBot2Turn)
                {
                    await this.CheckRaise(2, 2);
                    this.isBot3Turn = true;
                }

                if (!this.B3Fturn)
                {
                    if (this.isBot3Turn)
                    {
                        this.FixCall(this.b3Status, ref this.bot3Call, ref this.bot3Raise, 1);
                        this.FixCall(this.b3Status, ref this.bot3Call, ref this.bot3Raise, 2);
                        this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.B3Fturn);
                        MessageBox.Show("Bot 3's Turn");
                        this.AI(6, 7, ref this.bot3Chips, ref this.isBot3Turn, ref this.B3Fturn, this.b3Status, 2, this.bot3Power, this.bot3Type);
                        this.turnCount++;
                        this.last = 3;
                        this.isBot3Turn = false;
                        this.isBot4Turn = true;
                    }
                }

                if (this.B3Fturn && !this.bot3Folded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.maxLeft--;
                    this.bot3Folded = true;
                }

                if (this.B3Fturn || !this.isBot3Turn)
                {
                    await this.CheckRaise(3, 3);
                    this.isBot4Turn = true;
                }

                if (!this.B4Fturn)
                {
                    if (this.isBot4Turn)
                    {
                        this.FixCall(this.b4Status, ref this.bot4Call, ref this.bot4Raise, 1);
                        this.FixCall(this.b4Status, ref this.bot4Call, ref this.bot4Raise, 2);
                        this.Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.B4Fturn);
                        MessageBox.Show("Bot 4's Turn");
                        this.AI(8, 9, ref this.bot4Chips, ref this.isBot4Turn, ref this.B4Fturn, this.b4Status, 3, this.bot4Power, this.bot4Type);
                        this.turnCount++;
                        this.last = 4;
                        this.isBot4Turn = false;
                        this.isBot5Turn = true;
                    }
                }

                if (this.B4Fturn && !this.bot4Folded)
                {
                    this.bools.RemoveAt(4);
                    this.bools.Insert(4, null);
                    this.maxLeft--;
                    this.bot4Folded = true;
                }

                if (this.B4Fturn || !this.isBot4Turn)
                {
                    await this.CheckRaise(4, 4);
                    this.isBot5Turn = true;
                }

                if (!this.B5Fturn)
                {
                    if (this.isBot5Turn)
                    {
                        this.FixCall(this.b5Status, ref this.bot5Call, ref this.bot5Raise, 1);
                        this.FixCall(this.b5Status, ref this.bot5Call, ref this.bot5Raise, 2);
                        this.Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.B5Fturn);
                        MessageBox.Show("Bot 5's Turn");
                        this.AI(10, 11, ref this.bot5Chips, ref this.isBot5Turn, ref this.B5Fturn, this.b5Status, 4, this.bot5Power, this.bot5Type);
                        this.turnCount++;
                        this.last = 5;
                        this.isBot5Turn = false;
                    }
                }

                if (this.B5Fturn && !this.bot5Folded)
                {
                    this.bools.RemoveAt(5);
                    this.bools.Insert(5, null);
                    this.maxLeft--;
                    this.bot5Folded = true;
                }

                if (this.B5Fturn || !this.isBot5Turn)
                {
                    await this.CheckRaise(5, 5);
                    this.isPlayerTurn = true;
                }

                if (this.PFturn && !this.playerFolded)
                {
                    if (this.bCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.playerFolded = true;
                    }
                }

                #endregion

                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }

                this.restart = false;
            }
        }
        // ----------------------------------------------------------------------
        private void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }

            if (!foldedTurn || c1 == 0 && c2 == 1 && this.pStatus.Text.Contains("Fold") == false)
            {
                #region Variables

                bool done = false, vf = false;
                var straight1 = new int[5];
                var straight = new int[7];
                straight[0] = this.Reserve[c1];
                straight[1] = this.Reserve[c2];
                straight1[0] = straight[2] = this.Reserve[12];
                straight1[1] = straight[3] = this.Reserve[13];
                straight1[2] = straight[4] = this.Reserve[14];
                straight1[3] = straight[5] = this.Reserve[15];
                straight1[4] = straight[6] = this.Reserve[16];
                var a = straight.Where(o => o % 4 == 0).ToArray();
                var b = straight.Where(o => o % 4 == 1).ToArray();
                var c = straight.Where(o => o % 4 == 2).ToArray();
                var d = straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);

                #endregion

                for (this.i = 0; this.i < 16; this.i++)
                {
                    if (this.Reserve[this.i] == int.Parse(this.Holder[c1].Tag.ToString()) && this.Reserve[this.i + 1] == int.Parse(this.Holder[c2].Tag.ToString()))
                    {
                        ////Pair from Hand current = 1

                        this.rPairFromHand(ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0

                        this.rPairTwoPair(ref current, ref Power);

                        #endregion

                        #region Two Pair current = 2

                        this.rTwoPair(ref current, ref Power);

                        #endregion

                        #region Three of a kind current = 3

                        this.rThreeOfAKind(ref current, ref Power, straight);

                        #endregion

                        #region Straight current = 4

                        this.rStraight(ref current, ref Power, straight);

                        #endregion

                        #region Flush current = 5 || 5.5

                        this.rFlush(ref current, ref Power, ref vf, straight1);

                        #endregion

                        #region Full House current = 6

                        this.rFullHouse(ref current, ref Power, ref done, straight);

                        #endregion

                        #region Four of a Kind current = 7

                        this.rFourOfAKind(ref current, ref Power, straight);

                        #endregion

                        #region Straight Flush current = 8 || 9

                        this.rStraightFlush(ref current, ref Power, st1, st2, st3, st4);

                        #endregion

                        #region High Card current = -1

                        this.rHighCard(ref current, ref Power);

                        #endregion
                    }
                }
            }
        }
        // TODO: RoyalFlush
        // ----------------------------------------------------------------------
        // TODO: Milen
        private void rStraightFlush(ref double current, ref double power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        power = st1.Max() / 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        power = st1.Max() / 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        power = (st2.Max()) / 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        power = (st2.Max()) / 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        power = st3.Max() / 4 + (current * 100);
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        power = st3.Max() / 4 + (current * 100);
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        power = st4.Max() / 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        power = st4.Max() / 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFourOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (var j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 && Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 7
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 7
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        // TODO: Aleks
        private void rFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                this.type = Power;
                for (var j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                this.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 6
                                });
                                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                this.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 6
                                });
                                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    Power = this.type;
                }
            }
        }

        private void rFlush(ref double current, ref double Power, ref bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (this.Reserve[this.i] % 4 == this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f1[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (this.Reserve[this.i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.Reserve[this.i] / 4 < f1.Max() / 4 && this.Reserve[this.i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4) ////different cards in hand
                {
                    if (this.Reserve[this.i] % 4 != this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f1[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.Reserve[this.i + 1] % 4 != this.Reserve[this.i] % 4 && this.Reserve[this.i + 1] % 4 == f1[0] % 4)
                    {
                        if (this.Reserve[this.i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (this.Reserve[this.i] % 4 == f1[0] % 4 && this.Reserve[this.i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.Reserve[this.i + 1] % 4 == f1[0] % 4 && this.Reserve[this.i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i + 1] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.Reserve[this.i] / 4 < f1.Min() / 4 && this.Reserve[this.i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (this.Reserve[this.i] % 4 == this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f2[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (this.Reserve[this.i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.Reserve[this.i] / 4 < f2.Max() / 4 && this.Reserve[this.i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4) ////different cards in hand
                {
                    if (this.Reserve[this.i] % 4 != this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f2[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.Reserve[this.i + 1] % 4 != this.Reserve[this.i] % 4 && this.Reserve[this.i + 1] % 4 == f2[0] % 4)
                    {
                        if (this.Reserve[this.i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (this.Reserve[this.i] % 4 == f2[0] % 4 && this.Reserve[this.i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.Reserve[this.i + 1] % 4 == f2[0] % 4 && this.Reserve[this.i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i + 1] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.Reserve[this.i] / 4 < f2.Min() / 4 && this.Reserve[this.i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (this.Reserve[this.i] % 4 == this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f3[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (this.Reserve[this.i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.Reserve[this.i] / 4 < f3.Max() / 4 && this.Reserve[this.i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4) ////different cards in hand
                {
                    if (this.Reserve[this.i] % 4 != this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f3[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.Reserve[this.i + 1] % 4 != this.Reserve[this.i] % 4 && this.Reserve[this.i + 1] % 4 == f3[0] % 4)
                    {
                        if (this.Reserve[this.i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (this.Reserve[this.i] % 4 == f3[0] % 4 && this.Reserve[this.i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.Reserve[this.i + 1] % 4 == f3[0] % 4 && this.Reserve[this.i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i + 1] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.Reserve[this.i] / 4 < f3.Min() / 4 && this.Reserve[this.i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (this.Reserve[this.i] % 4 == this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f4[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + (current * 100);
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.Reserve[this.i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.Reserve[this.i] / 4 < f4.Max() / 4 && this.Reserve[this.i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4) ////different cards in hand
                {
                    if (this.Reserve[this.i] % 4 != this.Reserve[this.i + 1] % 4 && this.Reserve[this.i] % 4 == f4[0] % 4)
                    {
                        if (this.Reserve[this.i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.Reserve[this.i + 1] % 4 != this.Reserve[this.i] % 4 && this.Reserve[this.i + 1] % 4 == f4[0] % 4)
                    {
                        if (this.Reserve[this.i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.Reserve[this.i + 1] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (this.Reserve[this.i] % 4 == f4[0] % 4 && this.Reserve[this.i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.Reserve[this.i + 1] % 4 == f4[0] % 4 && this.Reserve[this.i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.Reserve[this.i + 1] + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.Reserve[this.i] / 4 < f4.Min() / 4 && this.Reserve[this.i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                ////ace
                if (f1.Length > 0)
                {
                    if (this.Reserve[this.i] / 4 == 0 && this.Reserve[this.i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.Reserve[this.i + 1] / 4 == 0 && this.Reserve[this.i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (this.Reserve[this.i] / 4 == 0 && this.Reserve[this.i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.Reserve[this.i + 1] / 4 == 0 && this.Reserve[this.i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (this.Reserve[this.i] / 4 == 0 && this.Reserve[this.i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.Reserve[this.i + 1] / 4 == 0 && this.Reserve[this.i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (this.Reserve[this.i] / 4 == 0 && this.Reserve[this.i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.Reserve[this.i + 1] / 4 == 0 && this.Reserve[this.i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        // TODO: Tisho
        private void rStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (var j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 4
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 4
                            });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 4
                        });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (var j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 3
                            });
                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 3
                            });
                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        // TODO: Tedi
        private void rTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                var msgbox = false;
                for (var tc = 16; tc >= 12; tc--)
                {
                    var max = tc - 12;
                    if (this.Reserve[this.i] / 4 != this.Reserve[this.i + 1] / 4)
                    {
                        for (var k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (this.Reserve[this.i] / 4 == this.Reserve[tc] / 4 && this.Reserve[this.i + 1] / 4 == this.Reserve[tc - k] / 4 || 
                                    this.Reserve[this.i + 1] / 4 == this.Reserve[tc] / 4 && this.Reserve[this.i] / 4 == this.Reserve[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.Reserve[this.i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.Reserve[this.i + 1] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.Reserve[this.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.Reserve[this.i] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.Reserve[this.i + 1] / 4 != 0 && this.Reserve[this.i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.Reserve[this.i] / 4) * 2 + (this.Reserve[this.i + 1] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }

                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                var msgbox = false;
                var msgbox1 = false;
                for (var tc = 16; tc >= 12; tc--)
                {
                    var max = tc - 12;
                    for (var k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (this.Reserve[tc] / 4 == this.Reserve[tc - k] / 4)
                            {
                                if (this.Reserve[tc] / 4 != this.Reserve[this.i] / 4 && this.Reserve[tc] / 4 != this.Reserve[this.i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.Reserve[this.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.Reserve[this.i] / 4) * 2 + 13 * 4 + current * 100;
                                            this.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.Reserve[this.i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.Reserve[this.i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            this.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.Reserve[this.i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.Reserve[tc] / 4) * 2 + (this.Reserve[this.i + 1] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.Reserve[this.i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.Reserve[tc] / 4) * 2 + (this.Reserve[this.i] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (this.Reserve[this.i] / 4 > this.Reserve[this.i + 1] / 4)
                                        {
                                            if (this.Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + this.Reserve[this.i] / 4 + current * 100;
                                                this.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });
                                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.Reserve[tc] / 4 + this.Reserve[this.i] / 4 + current * 100;
                                                this.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });
                                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (this.Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + this.Reserve[this.i + 1] + current * 100;
                                                this.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });
                                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.Reserve[tc] / 4 + this.Reserve[this.i + 1] / 4 + current * 100;
                                                this.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });

                                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }

                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        // TODO: Stoqn
        private void rPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                var msgbox = false;
                if (this.Reserve[this.i] / 4 == this.Reserve[this.i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (this.Reserve[this.i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 1
                            });
                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (this.Reserve[this.i + 1] / 4) * 4 + current * 100;
                            this.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 1
                            });
                            this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (var tc = 16; tc >= 12; tc--)
                {
                    if (this.Reserve[this.i + 1] / 4 == this.Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.Reserve[this.i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + this.Reserve[this.i] / 4 + current * 100;
                                this.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 1
                                });
                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.Reserve[this.i + 1] / 4) * 4 + this.Reserve[this.i] / 4 + current * 100;
                                this.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 1
                                });
                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (this.Reserve[this.i] / 4 == this.Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.Reserve[this.i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + this.Reserve[this.i + 1] / 4 + current * 100;
                                this.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 1
                                });
                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.Reserve[tc] / 4) * 4 + this.Reserve[this.i + 1] / 4 + current * 100;
                                this.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 1
                                });
                                this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (this.Reserve[this.i] / 4 > this.Reserve[this.i + 1] / 4)
                {
                    current = -1;
                    Power = this.Reserve[this.i] / 4;

                    this.Win.Add(new Type
                    {
                        Power = Power,
                        Current = -1
                    });
                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = this.Reserve[this.i + 1] / 4;

                    this.Win.Add(new Type
                    {
                        Power = Power,
                        Current = -1
                    });

                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }

                if (this.Reserve[this.i] / 4 == 0 || this.Reserve[this.i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;

                    this.Win.Add(new Type
                    {
                        Power = Power,
                        Current = -1
                    });

                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }
        // ----------------------------------------------------------------------
        private void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (var j = 0; j <= 16; j++)
            {
                ////await Task.Delay(5);
                if (this.Holder[j].Visible)
                {
                    this.Holder[j].Image = this.Deck[j];
                }
            }
            if (current == this.sorted.Current)
            {
                if (Power == this.sorted.Power)
                {
                    this.winners++;
                    this.CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly) ////lastfixed
            {
                if (this.winners > 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(this.tbPot.Text) / this.winners;
                        this.tbChips.Text = this.playerChips.ToString();
                        ////pPanel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.bot1Chips += int.Parse(this.tbPot.Text) / this.winners;
                        this.tbBotChips1.Text = this.bot1Chips.ToString();
                        ////b1Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.bot2Chips += int.Parse(this.tbPot.Text) / this.winners;
                        this.tbBotChips2.Text = this.bot2Chips.ToString();
                        ////b2Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.bot3Chips += int.Parse(this.tbPot.Text) / this.winners;
                        this.tbBotChips3.Text = this.bot3Chips.ToString();
                        ////b3Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.bot4Chips += int.Parse(this.tbPot.Text) / this.winners;
                        this.tbBotChips4.Text = this.bot4Chips.ToString();
                        ////b4Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.bot5Chips += int.Parse(this.tbPot.Text) / this.winners;
                        this.tbBotChips5.Text = this.bot5Chips.ToString();
                        ////b5Panel.Visible = true;
                    }
                    ////await Finish(1);
                }
                if (this.winners == 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(this.tbPot.Text);
                        ////await Finish(1);
                        ////pPanel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.bot1Chips += int.Parse(this.tbPot.Text);
                        ////await Finish(1);
                        ////b1Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.bot2Chips += int.Parse(this.tbPot.Text);
                        ////await Finish(1);
                        ////b2Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.bot3Chips += int.Parse(this.tbPot.Text);
                        ////await Finish(1);
                        ////b3Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.bot4Chips += int.Parse(this.tbPot.Text);
                        ////await Finish(1);
                        ////b4Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.bot5Chips += int.Parse(this.tbPot.Text);
                        ////await Finish(1);
                        ////b5Panel.Visible = true;
                    }
                }
            }
        }

        private async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.maxLeft - 1 || !this.changed && this.turnCount == this.maxLeft)
                {
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.raise = 0;
                        this.call = 0;
                        this.raisedTurn = 123;
                        this.rounds++;
                        if (!this.PFturn)
                        {
                            this.pStatus.Text = "";
                        }
                        if (!this.B1Fturn)
                        {
                            this.b1Status.Text = "";
                        }
                        if (!this.B2Fturn)
                        {
                            this.b2Status.Text = "";
                        }
                        if (!this.B3Fturn)
                        {
                            this.b3Status.Text = "";
                        }
                        if (!this.B4Fturn)
                        {
                            this.b4Status.Text = "";
                        }
                        if (!this.B5Fturn)
                        {
                            this.b5Status.Text = "";
                        }
                    }
                }
            }
            if (this.rounds == this.Flop)
            {
                for (var j = 12; j <= 14; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1Call = 0;
                        this.bot1Raise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (this.rounds == this.Turn)
            {
                for (var j = 14; j <= 15; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1Call = 0;
                        this.bot1Raise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (this.rounds == this.River)
            {
                for (var j = 15; j <= 16; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1Call = 0;
                        this.bot1Raise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (this.rounds == this.End && this.maxLeft == 6)
            {
                var fixedLast = "qwerty";
                if (!this.pStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
                }
                if (!this.b1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.B1Fturn);
                }
                if (!this.b2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.B2Fturn);
                }
                if (!this.b3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.B3Fturn);
                }
                if (!this.b4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    this.Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.B4Fturn);
                }
                if (!this.b5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    this.Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.B5Fturn);
                }
                this.Winner(this.playerType, this.playerPower, "Player", this.playerChips, fixedLast);
                this.Winner(this.bot1Type, this.bot1Power, "Bot 1", this.bot1Chips, fixedLast);
                this.Winner(this.bot2Type, this.bot2Power, "Bot 2", this.bot2Chips, fixedLast);
                this.Winner(this.bot3Type, this.bot3Power, "Bot 3", this.bot3Chips, fixedLast);
                this.Winner(this.bot4Type, this.bot4Power, "Bot 4", this.bot4Chips, fixedLast);
                this.Winner(this.bot5Type, this.bot5Power, "Bot 5", this.bot5Chips, fixedLast);
                this.restart = true;
                this.isPlayerTurn = true;
                this.PFturn = false;
                this.B1Fturn = false;
                this.B2Fturn = false;
                this.B3Fturn = false;
                this.B4Fturn = false;
                this.B5Fturn = false;
                if (this.playerChips <= 0)
                {
                    var f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.playerChips = f2.a;
                        this.bot1Chips += f2.a;
                        this.bot2Chips += f2.a;
                        this.bot3Chips += f2.a;
                        this.bot4Chips += f2.a;
                        this.bot5Chips += f2.a;
                        this.PFturn = false;
                        this.isPlayerTurn = true;
                        this.bRaise.Enabled = true;
                        this.bFold.Enabled = true;
                        this.bCheck.Enabled = true;
                        this.bRaise.Text = "Raise";
                    }
                }
                this.playerPanel.Visible = false;
                this.bot1Panel.Visible = false;
                this.bot2Panel.Visible = false;
                this.bot3Panel.Visible = false;
                this.bot4Panel.Visible = false;
                this.bot5Panel.Visible = false;
                this.playerCall = 0;
                this.playerRaise = 0;
                this.bot1Call = 0;
                this.bot1Raise = 0;
                this.bot2Call = 0;
                this.bot2Raise = 0;
                this.bot3Call = 0;
                this.bot3Raise = 0;
                this.bot4Call = 0;
                this.bot4Raise = 0;
                this.bot5Call = 0;
                this.bot5Raise = 0;
                this.last = 0;
                this.call = this.bigBlind;
                this.raise = 0;
                this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.bools.Clear();
                this.rounds = 0;
                this.playerPower = 0;
                this.playerType = -1;
                this.type = 0;
                this.bot1Power = 0;
                this.bot2Power = 0;
                this.bot3Power = 0;
                this.bot4Power = 0;
                this.bot5Power = 0;
                this.bot1Type = -1;
                this.bot2Type = -1;
                this.bot3Type = -1;
                this.bot4Type = -1;
                this.bot5Type = -1;
                this.ints.Clear();
                this.CheckWinners.Clear();
                this.winners = 0;
                this.Win.Clear();
                this.sorted.Current = 0;
                this.sorted.Power = 0;
                for (var os = 0; os < 17; os++)
                {
                    this.Holder[os].Image = null;
                    this.Holder[os].Invalidate();
                    this.Holder[os].Visible = false;
                }
                this.tbPot.Text = "0";
                this.pStatus.Text = "";
                await this.Shuffle();
                await this.Turns();
            }
        }

        private void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (this.rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != this.raise && cRaise <= this.raise)
                    {
                        this.call = Convert.ToInt32(this.raise) - cRaise;
                    }
                    if (cCall != this.call || cCall <= this.call)
                    {
                        this.call = this.call - cCall;
                    }
                    if (cRaise == this.raise && this.raise > 0)
                    {
                        this.call = 0;
                        this.bCall.Enabled = false;
                        this.bCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        private async Task AllIn()
        {
            #region All in

            if (this.playerChips <= 0 && !this.intsadded)
            {
                if (this.pStatus.Text.Contains("Raise"))
                {
                    this.ints.Add(this.playerChips);
                    this.intsadded = true;
                }
                if (this.pStatus.Text.Contains("Call"))
                {
                    this.ints.Add(this.playerChips);
                    this.intsadded = true;
                }
            }
            this.intsadded = false;
            if (this.bot1Chips <= 0 && !this.B1Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.bot1Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot2Chips <= 0 && !this.B2Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.bot2Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot3Chips <= 0 && !this.B3Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.bot3Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot4Chips <= 0 && !this.B4Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.bot4Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot5Chips <= 0 && !this.B5Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.bot5Chips);
                    this.intsadded = true;
                }
            }
            if (this.ints.ToArray().Length == this.maxLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.ints.Clear();
            }

            #endregion

            var abc = this.bools.Count(x => x == false);

            #region LastManStanding

            if (abc == 1)
            {
                var index = this.bools.IndexOf(false);
                if (index == 0)
                {
                    this.playerChips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.playerChips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    this.bot1Chips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.bot1Chips.ToString();
                    this.bot1Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    this.bot2Chips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.bot2Chips.ToString();
                    this.bot2Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    this.bot3Chips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.bot3Chips.ToString();
                    this.bot3Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    this.bot4Chips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.bot4Chips.ToString();
                    this.bot4Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    this.bot5Chips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.bot5Chips.ToString();
                    this.bot5Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (var j = 0; j <= 16; j++)
                {
                    this.Holder[j].Visible = false;
                }
                await this.Finish(1);
            }
            this.intsadded = false;

            #endregion

            #region FiveOrLessLeft

            if (abc < 6 && abc > 1 && this.rounds >= this.End)
            {
                await this.Finish(2);
            }

            #endregion
        }

        private async Task Finish(int n)
        {
            if (n == 2)
            {
                this.FixWinners();
            }
            this.playerPanel.Visible = false;
            this.bot1Panel.Visible = false;
            this.bot2Panel.Visible = false;
            this.bot3Panel.Visible = false;
            this.bot4Panel.Visible = false;
            this.bot5Panel.Visible = false;
            this.call = this.bigBlind;
            this.raise = 0;
            this.foldedPlayers = 5;
            this.type = 0;
            this.rounds = 0;
            this.bot1Power = 0;
            this.bot2Power = 0;
            this.bot3Power = 0;
            this.bot4Power = 0;
            this.bot5Power = 0;
            this.playerPower = 0;
            this.playerType = -1;
            this.raise = 0;
            this.bot1Type = -1;
            this.bot2Type = -1;
            this.bot3Type = -1;
            this.bot4Type = -1;
            this.bot5Type = -1;
            this.isBot1Turn = false;
            this.isBot2Turn = false;
            this.isBot3Turn = false;
            this.isBot4Turn = false;
            this.isBot5Turn = false;
            this.B1Fturn = false;
            this.B2Fturn = false;
            this.B3Fturn = false;
            this.B4Fturn = false;
            this.B5Fturn = false;
            this.playerFolded = false;
            this.bot1Folded = false;
            this.bot2Folded = false;
            this.bot3Folded = false;
            this.bot4Folded = false;
            this.bot5Folded = false;
            this.PFturn = false;
            this.isPlayerTurn = true;
            this.restart = false;
            this.raising = false;
            this.playerCall = 0;
            this.bot1Call = 0;
            this.bot2Call = 0;
            this.bot3Call = 0;
            this.bot4Call = 0;
            this.bot5Call = 0;
            this.playerRaise = 0;
            this.bot1Raise = 0;
            this.bot2Raise = 0;
            this.bot3Raise = 0;
            this.bot4Raise = 0;
            this.bot5Raise = 0;
            this.height = 0;
            this.width = 0;
            this.winners = 0;
            this.Flop = 1;
            this.Turn = 2;
            this.River = 3;
            this.End = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.bools.Clear();
            this.CheckWinners.Clear();
            this.ints.Clear();
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.tbPot.Text = "0";
            this.time = 60;
            this.maxBlind = 10000000;
            this.turnCount = 0;
            this.pStatus.Text = "";
            this.b1Status.Text = "";
            this.b2Status.Text = "";
            this.b3Status.Text = "";
            this.b4Status.Text = "";
            this.b5Status.Text = "";
            if (this.playerChips <= 0)
            {
                var f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.playerChips = f2.a;
                    this.bot1Chips += f2.a;
                    this.bot2Chips += f2.a;
                    this.bot3Chips += f2.a;
                    this.bot4Chips += f2.a;
                    this.bot5Chips += f2.a;
                    this.PFturn = false;
                    this.isPlayerTurn = true;
                    this.bRaise.Enabled = true;
                    this.bFold.Enabled = true;
                    this.bCheck.Enabled = true;
                    this.bRaise.Text = "Raise";
                }
            }
            this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (var os = 0; os < 17; os++)
            {
                this.Holder[os].Image = null;
                this.Holder[os].Invalidate();
                this.Holder[os].Visible = false;
            }
            await this.Shuffle();
            ////await Turns();
        }

        private void FixWinners()
        {
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            var fixedLast = "qwerty";
            if (!this.pStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            }
            if (!this.b1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.B1Fturn);
            }
            if (!this.b2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.B2Fturn);
            }
            if (!this.b3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.B3Fturn);
            }
            if (!this.b4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                this.Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.B4Fturn);
            }
            if (!this.b5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                this.Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.B5Fturn);
            }
            this.Winner(this.playerType, this.playerPower, "Player", this.playerChips, fixedLast);
            this.Winner(this.bot1Type, this.bot1Power, "Bot 1", this.bot1Chips, fixedLast);
            this.Winner(this.bot2Type, this.bot2Power, "Bot 2", this.bot2Chips, fixedLast);
            this.Winner(this.bot3Type, this.bot3Power, "Bot 3", this.bot3Chips, fixedLast);
            this.Winner(this.bot4Type, this.bot4Power, "Bot 4", this.bot4Chips, fixedLast);
            this.Winner(this.bot5Type, this.bot5Power, "Bot 5", this.bot5Chips, fixedLast);
        }
        // ----------------------------------------------------------------------
        private void AI(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            // TODO: make it with switch case.
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    this.HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 0)
                {
                    this.PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 1)
                {
                    this.PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 2)
                {
                    this.TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 3)
                {
                    this.ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 4)
                {
                    this.Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    this.Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 6)
                {
                    this.FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 7)
                {
                    this.FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    this.StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }
            if (sFTurn)
            {
                this.Holder[c1].Visible = false;
                this.Holder[c2].Visible = false;
            }
        }

        // TODO: Stoqn
        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            this.HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }

        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            this.HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }

        // TODO: Tedi - edited
        private void PairHand(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            var randomPair = new Random();
            var randomCall = randomPair.Next(10, 16);
            var randomRaise = randomPair.Next(10, 13);

            if (botPower <= 199 && botPower >= 140)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 6, randomRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 7, randomRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 9, randomRaise);
            }
        }

        private void TwoPair(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            var randomPair = new Random();
            var randomCall = randomPair.Next(6, 11);
            var randomRaise = randomPair.Next(6, 11);

            if (botPower <= 290 && botPower >= 246)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 3, randomRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 4, randomRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 4, randomRaise);
            }
        }

        // TODO: Tisho
        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var tk = new Random();
            var tCall = tk.Next(3, 7);
            var tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower <= 327 && botPower >= 321) ////10  8
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower < 321 && botPower >= 303) ////7 2
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
        }

        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var str = new Random();
            var sCall = str.Next(3, 6);
            var sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower <= 409 && botPower >= 407) ////10  8
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower < 407 && botPower >= 404)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
        }

        // TODO: Aleks
        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var fsh = new Random();
            var fCall = fsh.Next(2, 6);
            var fRaise = fsh.Next(3, 7);
            this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise);
        }

        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var flh = new Random();
            var fhCall = flh.Next(1, 5);
            var fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
            if (botPower < 620 && botPower >= 602)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }

        // TODO: Milen
        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var fk = new Random();
            var fkCall = fk.Next(1, 4);
            var fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }

        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var sf = new Random();
            var sfCall = sf.Next(1, 3);
            var sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }
        //-----------------------------------------------------------------------------------------
        // TODO: Add to namespace Statuses.
        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            this.raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            this.raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            this.raising = false;
            sTurn = false;
            sChips -= this.call;
            sStatus.Text = "Call " + this.call;
            this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.call).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(this.raise);
            sStatus.Text = "Raise " + this.raise;
            this.tbPot.Text = (int.Parse(this.tbPot.Text) + Convert.ToInt32(this.raise)).ToString();
            this.call = Convert.ToInt32(this.raise);
            this.raising = true;
            sTurn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            var a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 4);
            if (this.call <= 0)
            {
                this.Check(ref sTurn, sStatus);
            }
            if (this.call > 0)
            {
                if (rnd == 1)
                {
                    if (this.call <= RoundN(sChips, n))
                    {
                        this.Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
                if (rnd == 2)
                {
                    if (this.call <= RoundN(sChips, n1))
                    {
                        this.Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (rnd == 3)
            {
                if (this.raise == 0)
                {
                    this.raise = this.call * 2;
                    this.Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (this.raise <= RoundN(sChips, n))
                    {
                        this.raise = this.call * 2;
                        this.Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int randomCall, int n1, int randomRaise)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.rounds < 2)
            {
                if (this.call <= 0)
                {
                    this.Check(ref sTurn, sStatus);
                }
                if (this.call > 0)
                {
                    if (this.call >= RoundN(sChips, n1))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (this.raise > RoundN(sChips, randomCall))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (this.call >= RoundN(sChips, randomCall) && this.call <= RoundN(sChips, n1))
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= RoundN(sChips, randomCall) && this.raise >= (RoundN(sChips, randomCall)) / 2)
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= (RoundN(sChips, randomCall)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, randomCall);
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
            }
            if (this.rounds >= 2)
            {
                if (this.call > 0)
                {
                    if (this.call >= RoundN(sChips, n1 - rnd))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (this.raise > RoundN(sChips, randomCall - rnd))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (this.call >= RoundN(sChips, randomCall - rnd) && this.call <= RoundN(sChips, n1 - rnd))
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= RoundN(sChips, randomCall - rnd) && this.raise >= (RoundN(sChips, randomCall - rnd)) / 2)
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= (RoundN(sChips, randomCall - rnd)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, randomCall - rnd);
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (this.call <= 0)
                {
                    this.raise = RoundN(sChips, randomRaise - rnd);
                    this.Raised(ref sChips, ref sTurn, sStatus);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.call <= 0)
            {
                this.Check(ref botTurn, botStatus);
            }
            else
            {
                if (this.call >= RoundN(botChips, n))
                {
                    if (botChips > this.call)
                    {
                        this.Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= this.call)
                    {
                        this.raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.tbPot.Text = (int.Parse(this.tbPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.raise > 0)
                    {
                        if (botChips >= this.raise * 2)
                        {
                            this.raise *= 2;
                            this.Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            this.Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        this.raise = this.call * 2;
                        this.Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }


        #region UI

        private async void timer_Tick(object sender, object e)
        {
            if (this.pbTimer.Value <= 0)
            {
                this.PFturn = true;
                await this.Turns();
            }
            if (this.time > 0)
            {
                this.time--;
                this.pbTimer.Value = (this.time / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (this.playerChips <= 0)
            {
                this.tbChips.Text = "Chips : 0";
            }
            if (this.bot1Chips <= 0)
            {
                this.tbBotChips1.Text = "Chips : 0";
            }
            if (this.bot2Chips <= 0)
            {
                this.tbBotChips2.Text = "Chips : 0";
            }
            if (this.bot3Chips <= 0)
            {
                this.tbBotChips3.Text = "Chips : 0";
            }
            if (this.bot4Chips <= 0)
            {
                this.tbBotChips4.Text = "Chips : 0";
            }
            if (this.bot5Chips <= 0)
            {
                this.tbBotChips5.Text = "Chips : 0";
            }
            this.tbChips.Text = "Chips : " + this.playerChips;
            this.tbBotChips1.Text = "Chips : " + this.bot1Chips;
            this.tbBotChips2.Text = "Chips : " + this.bot2Chips;
            this.tbBotChips3.Text = "Chips : " + this.bot3Chips;
            this.tbBotChips4.Text = "Chips : " + this.bot4Chips;
            this.tbBotChips5.Text = "Chips : " + this.bot5Chips;
            if (this.playerChips <= 0)
            {
                this.isPlayerTurn = false;
                this.PFturn = true;
                this.bCall.Enabled = false;
                this.bRaise.Enabled = false;
                this.bFold.Enabled = false;
                this.bCheck.Enabled = false;
            }
            if (this.maxBlind > 0)
            {
                this.maxBlind--;
            }
            if (this.playerChips >= this.call)
            {
                this.bCall.Text = "Call " + this.call;
            }
            else
            {
                this.bCall.Text = "All in";
                this.bRaise.Enabled = false;
            }
            if (this.call > 0)
            {
                this.bCheck.Enabled = false;
            }
            if (this.call <= 0)
            {
                this.bCheck.Enabled = true;
                this.bCall.Text = "Call";
                this.bCall.Enabled = false;
            }
            if (this.playerChips <= 0)
            {
                this.bRaise.Enabled = false;
            }
            int parsedValue;

            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.playerChips <= int.Parse(this.tbRaise.Text))
                {
                    this.bRaise.Text = "All in";
                }
                else
                {
                    this.bRaise.Text = "Raise";
                }
            }
            if (this.playerChips < this.call)
            {
                this.bRaise.Enabled = false;
            }
        }
        //------------------------------------------------------------------
        // TODO: Add to namespace Events.
        private async void bFold_Click(object sender, EventArgs e)
        {
            this.pStatus.Text = "Fold";
            this.isPlayerTurn = false;
            this.PFturn = true;
            await this.Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (this.call <= 0)
            {
                this.isPlayerTurn = false;
                this.pStatus.Text = "Check";
            }
            else
            {
                ////pStatus.Text = "All in " + Chips;

                this.bCheck.Enabled = false;
            }
            await this.Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            if (this.playerChips >= this.call)
            {
                this.playerChips -= this.call;
                this.tbChips.Text = "Chips : " + this.playerChips;
                if (this.tbPot.Text != "")
                {
                    this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.call).ToString();
                }
                else
                {
                    this.tbPot.Text = this.call.ToString();
                }
                this.isPlayerTurn = false;
                this.pStatus.Text = "Call " + this.call;
                this.playerCall = this.call;
            }
            else if (this.playerChips <= this.call && this.call > 0)
            {
                this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.playerChips).ToString();
                this.pStatus.Text = "All in " + this.playerChips;
                this.playerChips = 0;
                this.tbChips.Text = "Chips : " + this.playerChips;
                this.isPlayerTurn = false;
                this.bFold.Enabled = false;
                this.playerCall = this.playerChips;
            }
            await this.Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            int parsedValue;
            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.playerChips > this.call)
                {
                    if (this.raise * 2 > int.Parse(this.tbRaise.Text))
                    {
                        this.tbRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    if (this.playerChips >= int.Parse(this.tbRaise.Text))
                    {
                        this.call = int.Parse(this.tbRaise.Text);
                        this.raise = int.Parse(this.tbRaise.Text);
                        this.pStatus.Text = "Raise " + this.call;
                        this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.call).ToString();
                        this.bCall.Text = "Call";
                        this.playerChips -= int.Parse(this.tbRaise.Text);
                        this.raising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.raise);
                    }
                    else
                    {
                        this.call = this.playerChips;
                        this.raise = this.playerChips;
                        this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.playerChips).ToString();
                        this.pStatus.Text = "Raise " + this.call;
                        this.playerChips = 0;
                        this.raising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.raise);
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            this.isPlayerTurn = false;
            await this.Turns();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (this.tbAdd.Text == "")
            {
            }
            else
            {
                this.playerChips += int.Parse(this.tbAdd.Text);
                this.bot1Chips += int.Parse(this.tbAdd.Text);
                this.bot2Chips += int.Parse(this.tbAdd.Text);
                this.bot3Chips += int.Parse(this.tbAdd.Text);
                this.bot4Chips += int.Parse(this.tbAdd.Text);
                this.bot5Chips += int.Parse(this.tbAdd.Text);
            }
            this.tbChips.Text = "Chips : " + this.playerChips;
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            this.tbBB.Text = this.bigBlind.ToString();
            this.tbSB.Text = this.smallBlind.ToString();
            if (this.tbBB.Visible == false)
            {
                this.tbBB.Visible = true;
                this.tbSB.Visible = true;
                this.bBB.Visible = true;
                this.bSB.Visible = true;
            }
            else
            {
                this.tbBB.Visible = false;
                this.tbSB.Visible = false;
                this.bBB.Visible = false;
                this.bSB.Visible = false;
            }
        }

        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.tbSB.Text.Contains(",") || this.tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.tbSB.Text = this.smallBlind.ToString();
                return;
            }
            if (!int.TryParse(this.tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.tbSB.Text = this.smallBlind.ToString();
                return;
            }
            if (int.Parse(this.tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.tbSB.Text = this.smallBlind.ToString();
            }
            if (int.Parse(this.tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(this.tbSB.Text) >= 250 && int.Parse(this.tbSB.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.tbBB.Text.Contains(",") || this.tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.tbBB.Text = this.bigBlind.ToString();
                return;
            }
            if (!int.TryParse(this.tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.tbSB.Text = this.bigBlind.ToString();
                return;
            }
            if (int.Parse(this.tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.tbBB.Text = this.bigBlind.ToString();
            }
            if (int.Parse(this.tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(this.tbBB.Text) >= 500 && int.Parse(this.tbBB.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }

        #endregion
    }
}