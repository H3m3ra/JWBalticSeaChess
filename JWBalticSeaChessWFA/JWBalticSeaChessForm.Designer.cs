namespace JWBalticSeaChessWFA
{
    partial class JWBalticSeaChessForm
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
            mainMenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            playerOneToolStripMenuItem = new ToolStripMenuItem();
            playerOneToolStripComboBox = new ToolStripComboBox();
            playerTwoToolStripMenuItem = new ToolStripMenuItem();
            playerTwoToolStripComboBox = new ToolStripComboBox();
            startToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            orientationToolStripMenuItem = new ToolStripMenuItem();
            orientationToolStripComboBox = new ToolStripComboBox();
            eyesToolStripMenuItem = new ToolStripMenuItem();
            eyesToolStripComboBox = new ToolStripComboBox();
            infoToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            mainPanel = new Panel();
            movesListView = new ListView();
            playerTwoPointsLabel = new Label();
            playerTwoNameLabel = new Label();
            canvasPanel = new Panel();
            playerOnePointsLabel = new Label();
            playerOneNameLabel = new Label();
            settingsStripMenuItem = new ToolStripMenuItem();
            AutomaticAIsToolStripMenuItem = new ToolStripMenuItem();
            settingAutomaticAIsStripComboBox = new ToolStripComboBox();
            mainMenuStrip.SuspendLayout();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.BackColor = SystemColors.ControlDark;
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, settingsStripMenuItem, infoToolStripMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Size = new Size(284, 24);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, toolStripSeparator3, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(98, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += FileNewToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(95, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(98, 22);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += FileQuitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { playerOneToolStripMenuItem, playerTwoToolStripMenuItem, startToolStripMenuItem, toolStripSeparator1, undoToolStripMenuItem, redoToolStripMenuItem, toolStripSeparator2, closeToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // playerOneToolStripMenuItem
            // 
            playerOneToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { playerOneToolStripComboBox });
            playerOneToolStripMenuItem.Name = "playerOneToolStripMenuItem";
            playerOneToolStripMenuItem.Size = new Size(180, 22);
            playerOneToolStripMenuItem.Text = "PlayerOne";
            // 
            // playerOneToolStripComboBox
            // 
            playerOneToolStripComboBox.Name = "playerOneToolStripComboBox";
            playerOneToolStripComboBox.Size = new Size(121, 23);
            playerOneToolStripComboBox.TextChanged += PlayerOneToolStripComboBox_TextChanged;
            // 
            // playerTwoToolStripMenuItem
            // 
            playerTwoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { playerTwoToolStripComboBox });
            playerTwoToolStripMenuItem.Name = "playerTwoToolStripMenuItem";
            playerTwoToolStripMenuItem.Size = new Size(180, 22);
            playerTwoToolStripMenuItem.Text = "PlayerTwo";
            // 
            // playerTwoToolStripComboBox
            // 
            playerTwoToolStripComboBox.Name = "playerTwoToolStripComboBox";
            playerTwoToolStripComboBox.Size = new Size(121, 23);
            playerTwoToolStripComboBox.TextChanged += PlayerTwoToolStripComboBox_TextChanged;
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.Size = new Size(180, 22);
            startToolStripMenuItem.Text = "Start";
            startToolStripMenuItem.Click += EditStartToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(180, 22);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += EditUndoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(180, 22);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += EditRedoToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(177, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(180, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += EditCloseToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { orientationToolStripMenuItem, eyesToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // orientationToolStripMenuItem
            // 
            orientationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { orientationToolStripComboBox });
            orientationToolStripMenuItem.Name = "orientationToolStripMenuItem";
            orientationToolStripMenuItem.Size = new Size(180, 22);
            orientationToolStripMenuItem.Text = "Orientation";
            // 
            // orientationToolStripComboBox
            // 
            orientationToolStripComboBox.Items.AddRange(new object[] { "top", "right", "bottom", "left" });
            orientationToolStripComboBox.Name = "orientationToolStripComboBox";
            orientationToolStripComboBox.Size = new Size(121, 23);
            orientationToolStripComboBox.Text = "top";
            orientationToolStripComboBox.TextChanged += OrientationToolStripComboBox_TextChanged;
            // 
            // eyesToolStripMenuItem
            // 
            eyesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { eyesToolStripComboBox });
            eyesToolStripMenuItem.Name = "eyesToolStripMenuItem";
            eyesToolStripMenuItem.Size = new Size(180, 22);
            eyesToolStripMenuItem.Text = "Eyes";
            // 
            // eyesToolStripComboBox
            // 
            eyesToolStripComboBox.Items.AddRange(new object[] { "true", "false" });
            eyesToolStripComboBox.Name = "eyesToolStripComboBox";
            eyesToolStripComboBox.Size = new Size(121, 23);
            eyesToolStripComboBox.Text = "true";
            eyesToolStripComboBox.TextChanged += EyesToolStripComboBox_TextChanged;
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { helpToolStripMenuItem });
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new Size(40, 20);
            infoToolStripMenuItem.Text = "Info";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(99, 22);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += HelpToolStripMenuItem_Click;
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(movesListView);
            mainPanel.Controls.Add(playerTwoPointsLabel);
            mainPanel.Controls.Add(playerTwoNameLabel);
            mainPanel.Controls.Add(canvasPanel);
            mainPanel.Controls.Add(playerOnePointsLabel);
            mainPanel.Controls.Add(playerOneNameLabel);
            mainPanel.Location = new Point(1, 27);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(283, 239);
            mainPanel.TabIndex = 1;
            // 
            // movesListView
            // 
            movesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            movesListView.Location = new Point(217, 0);
            movesListView.Name = "movesListView";
            movesListView.Size = new Size(66, 239);
            movesListView.TabIndex = 5;
            movesListView.UseCompatibleStateImageBehavior = false;
            // 
            // playerTwoPointsLabel
            // 
            playerTwoPointsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            playerTwoPointsLabel.AutoSize = true;
            playerTwoPointsLabel.Location = new Point(129, 16);
            playerTwoPointsLabel.Name = "playerTwoPointsLabel";
            playerTwoPointsLabel.Size = new Size(82, 15);
            playerTwoPointsLabel.TabIndex = 4;
            playerTwoPointsLabel.Text = "JKL MNO PQR";
            // 
            // playerTwoNameLabel
            // 
            playerTwoNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            playerTwoNameLabel.AutoSize = true;
            playerTwoNameLabel.Location = new Point(129, 1);
            playerTwoNameLabel.Name = "playerTwoNameLabel";
            playerTwoNameLabel.Size = new Size(33, 15);
            playerTwoNameLabel.TabIndex = 3;
            playerTwoNameLabel.Text = "TWO";
            // 
            // canvasPanel
            // 
            canvasPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            canvasPanel.Location = new Point(-1, 34);
            canvasPanel.Name = "canvasPanel";
            canvasPanel.Size = new Size(220, 202);
            canvasPanel.TabIndex = 2;
            canvasPanel.Paint += PanelCanvasPaint;
            canvasPanel.MouseClick += CanvasPanel_MouseClick;
            canvasPanel.MouseMove += CanvasPanel_MouseMove;
            canvasPanel.Resize += CanvasPanelResize;
            // 
            // playerOnePointsLabel
            // 
            playerOnePointsLabel.AutoSize = true;
            playerOnePointsLabel.Location = new Point(0, 16);
            playerOnePointsLabel.Name = "playerOnePointsLabel";
            playerOnePointsLabel.Size = new Size(76, 15);
            playerOnePointsLabel.TabIndex = 1;
            playerOnePointsLabel.Text = "ABC DEF GHI";
            // 
            // playerOneNameLabel
            // 
            playerOneNameLabel.AutoSize = true;
            playerOneNameLabel.Location = new Point(0, 1);
            playerOneNameLabel.Name = "playerOneNameLabel";
            playerOneNameLabel.Size = new Size(31, 15);
            playerOneNameLabel.TabIndex = 0;
            playerOneNameLabel.Text = "ONE";
            // 
            // settingsStripMenuItem
            // 
            settingsStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AutomaticAIsToolStripMenuItem });
            settingsStripMenuItem.Name = "settingsStripMenuItem";
            settingsStripMenuItem.Size = new Size(61, 20);
            settingsStripMenuItem.Text = "Settings";
            // 
            // AutomaticAIsToolStripMenuItem
            // 
            AutomaticAIsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingAutomaticAIsStripComboBox });
            AutomaticAIsToolStripMenuItem.Name = "AutomaticAIsToolStripMenuItem";
            AutomaticAIsToolStripMenuItem.Size = new Size(180, 22);
            AutomaticAIsToolStripMenuItem.Text = "Automatic AIs";
            // 
            // settingAutomaticAIsStripComboBox
            // 
            settingAutomaticAIsStripComboBox.Items.AddRange(new object[] { "true", "false" });
            settingAutomaticAIsStripComboBox.Name = "settingAutomaticAIsStripComboBox";
            settingAutomaticAIsStripComboBox.Size = new Size(121, 23);
            settingAutomaticAIsStripComboBox.Text = "true";
            settingAutomaticAIsStripComboBox.TextChanged += SettingAutomaticAIsStripComboBox_TextChanged;
            // 
            // JWBalticSeaChessForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 261);
            Controls.Add(mainPanel);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            MinimumSize = new Size(300, 300);
            Name = "JWBalticSeaChessForm";
            Text = "Baltic Sea Chess";
            Load += JWBalticSeaChessForm_Load;
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private Panel mainPanel;
        private Label playerTwoPointsLabel;
        private Label playerTwoNameLabel;
        private Panel canvasPanel;
        private Label playerOnePointsLabel;
        private Label playerOneNameLabel;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem orientationToolStripMenuItem;
        private ToolStripMenuItem eyesToolStripMenuItem;
        private ToolStripComboBox eyesToolStripComboBox;
        private ToolStripComboBox orientationToolStripComboBox;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem playerOneToolStripMenuItem;
        private ToolStripComboBox playerOneToolStripComboBox;
        private ToolStripMenuItem playerTwoToolStripMenuItem;
        private ToolStripComboBox playerTwoToolStripComboBox;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ListView movesListView;
        private ToolStripMenuItem settingsStripMenuItem;
        private ToolStripMenuItem AutomaticAIsToolStripMenuItem;
        private ToolStripComboBox settingAutomaticAIsStripComboBox;
    }
}