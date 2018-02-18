namespace CTP_交易
{
	partial class FormTrade
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("未知", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("未成交还在队列中", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("未成交不在队列中", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("部分成交还在队列中", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("部分成交不在队列中", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("已撤单", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("全部成交", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("尚未触发", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("已触发", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("当日持仓", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("历史持仓", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("套利", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "工商银行"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "农业银行"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "3",
            "中国银行"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "4",
            "建设银行"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "5",
            "交通银行"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Z",
            "其它银行"}, -1);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrade));
            this.comboBoxErrMsg = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelSHFE = new System.Windows.Forms.Label();
            this.labelCZCE = new System.Windows.Forms.Label();
            this.labelDCE = new System.Windows.Forms.Label();
            this.labelFFEX = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.buttonResetSetting = new System.Windows.Forms.Button();
            this.listViewOrder = new CTP_交易.HFListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader43 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewPosition = new CTP_交易.HFListView();
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader34 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader44 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader32 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hfListViewParkedOrder = new CTP_交易.HFListView();
            this.columnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader35 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader36 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader37 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader38 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader39 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader41 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader42 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hfListViewParkedAction = new CTP_交易.HFListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonTrade = new System.Windows.Forms.RadioButton();
            this.radioButtonMd = new System.Windows.Forms.RadioButton();
            this.panelTop = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSHFE = new System.Windows.Forms.TabPage();
            this.dataGridViewSHFE = new CTP_交易.DoubleBufferDGV();
            this.tabPageCZCE = new System.Windows.Forms.TabPage();
            this.dataGridViewCZCE = new CTP_交易.DoubleBufferDGV();
            this.tabPageDCE = new System.Windows.Forms.TabPage();
            this.dataGridViewDCE = new CTP_交易.DoubleBufferDGV();
            this.tabPageCFFEX = new System.Windows.Forms.TabPage();
            this.dataGridViewCFFEX = new CTP_交易.DoubleBufferDGV();
            this.tabPageSelected = new System.Windows.Forms.TabPage();
            this.dataGridViewSelected = new CTP_交易.DoubleBufferDGV();
            this.tabPageArbitrage = new System.Windows.Forms.TabPage();
            this.dataGridViewArbitrage = new CTP_交易.DoubleBufferDGV();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControSystem = new System.Windows.Forms.TabControl();
            this.tabPageOrder = new System.Windows.Forms.TabPage();
            this.tabPagePosition = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonQuickLock = new System.Windows.Forms.Button();
            this.buttonCovert = new System.Windows.Forms.Button();
            this.buttonQuickClose = new System.Windows.Forms.Button();
            this.tabPageParked = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.tabPageAccount = new System.Windows.Forms.TabPage();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.buttonQryAccount = new System.Windows.Forms.Button();
            this.userControlTradeAccount1 = new CTP_交易.UserControlTradeAccount();
            this.tabPageInstrument = new System.Windows.Forms.TabPage();
            this.dataGridViewInstruments = new CTP_交易.DoubleBufferDGV();
            this.tabPageInstrumentSelf = new System.Windows.Forms.TabPage();
            this.dataGridViewInstrumentsSelected = new CTP_交易.DoubleBufferDGV();
            this.tabPageBankFuture = new System.Windows.Forms.TabPage();
            this.listViewSeries = new CTP_交易.HFListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewBank = new CTP_交易.HFListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonTransfer = new System.Windows.Forms.Button();
            this.buttonQryTransferSeries = new System.Windows.Forms.Button();
            this.comboBoxTransferType = new System.Windows.Forms.ComboBox();
            this.textBoxBankName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxTradeAmount = new System.Windows.Forms.TextBox();
            this.textBoxBankPwd = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxAccountPwd = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxFutureFetchAmount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxBankID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageSystem = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSaveLog = new System.Windows.Forms.Button();
            this.hfListViewLog = new CTP_交易.HFListView();
            this.columnHeader67 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader68 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonShowLog = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonExportRate = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.textBoxOldPassword = new System.Windows.Forms.TextBox();
            this.radioButtonUser = new System.Windows.Forms.RadioButton();
            this.label34 = new System.Windows.Forms.Label();
            this.radioButtonAccount = new System.Windows.Forms.RadioButton();
            this.buttonChangePwd = new System.Windows.Forms.Button();
            this.textBoxNewPwdConfirm = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.textBoxNewPassWord = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tabPageSetting = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBoxResetAll = new System.Windows.Forms.CheckBox();
            this.checkBoxClearLog = new System.Windows.Forms.CheckBox();
            this.checkBoxClearSelected = new System.Windows.Forms.CheckBox();
            this.checkBoxClearCosumVolume = new System.Windows.Forms.CheckBox();
            this.checkBoxClearServers = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridViewCustomVolume = new CTP_交易.DoubleBufferDGV();
            this.合约 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.手数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxKeepInstrument = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownPriceTick = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.numericUpDownFlowPrice = new System.Windows.Forms.NumericUpDown();
            this.checkBoxFlowPrice = new System.Windows.Forms.CheckBox();
            this.checkBoxFastCloseAddTick = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxPlaySound = new System.Windows.Forms.CheckBox();
            this.checkBoxShowTootip = new System.Windows.Forms.CheckBox();
            this.tabPageHelp = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPageQryTrade = new System.Windows.Forms.TabPage();
            this.buttonSaveTrade = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.buttonQryPosition = new System.Windows.Forms.Button();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.hfListViewTrade = new CTP_交易.HFListView();
            this.columnHeader45 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader53 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader46 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader47 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader48 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader49 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader50 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader51 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader54 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageQrySettleInfo = new System.Windows.Forms.TabPage();
            this.label42 = new System.Windows.Forms.Label();
            this.buttonSaveInfo = new System.Windows.Forms.Button();
            this.buttonQrySettleInfo = new System.Windows.Forms.Button();
            this.dateTimePickerQrySettleInfo = new System.Windows.Forms.DateTimePicker();
            this.richTextBoxSettleInfo = new System.Windows.Forms.RichTextBox();
            this.splitContainerOrder = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.labelAskPrice = new System.Windows.Forms.Label();
            this.labelAskVolume = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.labelBidPrice = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.labelBidVolume = new System.Windows.Forms.Label();
            this.labelInstrumentName = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labelLastPrice = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.labelUpperLimit = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.labelLowerLimit = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.labelWeiBi = new System.Windows.Forms.Label();
            this.labelWeiCha = new System.Windows.Forms.Label();
            this.labelSettlementPrice = new System.Windows.Forms.Label();
            this.labelPreSettlementPrice = new System.Windows.Forms.Label();
            this.labelUpDown = new System.Windows.Forms.Label();
            this.labelTotalVolume = new System.Windows.Forms.Label();
            this.labelOpenPrice = new System.Windows.Forms.Label();
            this.labelHighest = new System.Windows.Forms.Label();
            this.labelLowest = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelOpenInstetest = new System.Windows.Forms.Label();
            this.labelPreOI = new System.Windows.Forms.Label();
            this.labelOIDiff = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.comboBoxInstrument = new System.Windows.Forms.ComboBox();
            this.numericUpDownVolume = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPrice = new System.Windows.Forms.NumericUpDown();
            this.buttonPrice = new System.Windows.Forms.Button();
            this.comboBoxOffset = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelUpper = new System.Windows.Forms.Label();
            this.labelLower = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelVolumeMax = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxDirector = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.buttonMarketPrice = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.btnParked = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSHFE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSHFE)).BeginInit();
            this.tabPageCZCE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCZCE)).BeginInit();
            this.tabPageDCE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDCE)).BeginInit();
            this.tabPageCFFEX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCFFEX)).BeginInit();
            this.tabPageSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelected)).BeginInit();
            this.tabPageArbitrage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArbitrage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControSystem.SuspendLayout();
            this.tabPageOrder.SuspendLayout();
            this.tabPagePosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabPageParked.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tabPageAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.tabPageInstrument.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstruments)).BeginInit();
            this.tabPageInstrumentSelf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstrumentsSelected)).BeginInit();
            this.tabPageBankFuture.SuspendLayout();
            this.tabPageSystem.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageSetting.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomVolume)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriceTick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlowPrice)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabPageHelp.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tabPageQryTrade.SuspendLayout();
            this.tabPageQrySettleInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOrder)).BeginInit();
            this.splitContainerOrder.Panel1.SuspendLayout();
            this.splitContainerOrder.Panel2.SuspendLayout();
            this.splitContainerOrder.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxErrMsg
            // 
            this.comboBoxErrMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxErrMsg.FormattingEnabled = true;
            this.comboBoxErrMsg.Location = new System.Drawing.Point(4, 1);
            this.comboBoxErrMsg.Name = "comboBoxErrMsg";
            this.comboBoxErrMsg.Size = new System.Drawing.Size(689, 20);
            this.comboBoxErrMsg.TabIndex = 3;
            // 
            // toolTip1
            // 
            this.toolTip1.Active = global::CTP_交易.Properties.Settings.Default.ShowTootip;
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
            // 
            // labelSHFE
            // 
            this.labelSHFE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSHFE.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSHFE.Location = new System.Drawing.Point(819, 3);
            this.labelSHFE.Name = "labelSHFE";
            this.labelSHFE.Size = new System.Drawing.Size(65, 19);
            this.labelSHFE.TabIndex = 4;
            this.labelSHFE.Text = "hh:mm:ss";
            this.toolTip1.SetToolTip(this.labelSHFE, "上期所");
            // 
            // labelCZCE
            // 
            this.labelCZCE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCZCE.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCZCE.Location = new System.Drawing.Point(884, 3);
            this.labelCZCE.Name = "labelCZCE";
            this.labelCZCE.Size = new System.Drawing.Size(65, 19);
            this.labelCZCE.TabIndex = 4;
            this.labelCZCE.Text = "hh:mm:ss";
            this.toolTip1.SetToolTip(this.labelCZCE, "郑商所");
            // 
            // labelDCE
            // 
            this.labelDCE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDCE.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDCE.Location = new System.Drawing.Point(949, 3);
            this.labelDCE.Name = "labelDCE";
            this.labelDCE.Size = new System.Drawing.Size(65, 19);
            this.labelDCE.TabIndex = 4;
            this.labelDCE.Text = "hh:mm:ss";
            this.toolTip1.SetToolTip(this.labelDCE, "大商所");
            // 
            // labelFFEX
            // 
            this.labelFFEX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFFEX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFFEX.Location = new System.Drawing.Point(1014, 3);
            this.labelFFEX.Name = "labelFFEX";
            this.labelFFEX.Size = new System.Drawing.Size(65, 19);
            this.labelFFEX.TabIndex = 4;
            this.labelFFEX.Text = "hh:mm:ss";
            this.toolTip1.SetToolTip(this.labelFFEX, "中金所");
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter2.Cursor = System.Windows.Forms.Cursors.PanNorth;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 255);
            this.splitter2.MinExtra = 0;
            this.splitter2.MinSize = 0;
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1084, 8);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            this.toolTip1.SetToolTip(this.splitter2, "单击显示/隐藏行情栏");
            this.splitter2.SplitterMoving += new System.Windows.Forms.SplitterEventHandler(this.splitter2_SplitterMoving);
            this.splitter2.Click += new System.EventHandler(this.splitter2_Click);
            // 
            // buttonResetSetting
            // 
            this.buttonResetSetting.Location = new System.Drawing.Point(164, 126);
            this.buttonResetSetting.Name = "buttonResetSetting";
            this.buttonResetSetting.Size = new System.Drawing.Size(75, 23);
            this.buttonResetSetting.TabIndex = 3;
            this.buttonResetSetting.Text = "提  交";
            this.toolTip1.SetToolTip(this.buttonResetSetting, "自选合约/登录密码\r\n也将被重置");
            this.buttonResetSetting.UseVisualStyleBackColor = true;
            this.buttonResetSetting.Click += new System.EventHandler(this.buttonResetSetting_Click_1);
            // 
            // listViewOrder
            // 
            this.listViewOrder.AllowColumnReorder = true;
            this.listViewOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader22,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader43,
            this.columnHeader23});
            this.listViewOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewOrder.FullRowSelect = true;
            listViewGroup1.Header = "未知";
            listViewGroup1.Name = "Unknown";
            listViewGroup2.Header = "未成交还在队列中";
            listViewGroup2.Name = "NoTradeQueueing";
            listViewGroup3.Header = "未成交不在队列中";
            listViewGroup3.Name = "NoTradeNotQueueing";
            listViewGroup4.Header = "部分成交还在队列中";
            listViewGroup4.Name = "PartTradedQueueing";
            listViewGroup5.Header = "部分成交不在队列中";
            listViewGroup5.Name = "PartTradedNotQueueing";
            listViewGroup6.Header = "已撤单";
            listViewGroup6.Name = "Canceled";
            listViewGroup7.Header = "全部成交";
            listViewGroup7.Name = "AllTraded";
            listViewGroup8.Header = "尚未触发";
            listViewGroup8.Name = "NotTouched";
            listViewGroup9.Header = "已触发";
            listViewGroup9.Name = "Touched";
            this.listViewOrder.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8,
            listViewGroup9});
            this.listViewOrder.HideSelection = false;
            this.listViewOrder.Location = new System.Drawing.Point(3, 3);
            this.listViewOrder.MultiSelect = false;
            this.listViewOrder.Name = "listViewOrder";
            this.listViewOrder.Size = new System.Drawing.Size(682, 241);
            this.listViewOrder.SortColumn = 7;
            this.listViewOrder.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listViewOrder.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listViewOrder, "双击撤单");
            this.listViewOrder.UseCompatibleStateImageBehavior = false;
            this.listViewOrder.View = System.Windows.Forms.View.Details;
            this.listViewOrder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewOrder_MouseDoubleClick);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "合约";
            this.columnHeader13.Width = 50;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "买卖";
            this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader14.Width = 41;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "开平";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 43;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "报单价格";
            this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "成交均价";
            this.columnHeader22.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "报单手数";
            this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "成交手数";
            this.columnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "报单时间";
            this.columnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader19.Width = 69;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "响应时间";
            this.columnHeader20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader43
            // 
            this.columnHeader43.Text = "报单编号";
            this.columnHeader43.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "报单状态";
            this.columnHeader23.Width = 160;
            // 
            // listViewPosition
            // 
            this.listViewPosition.AllowColumnReorder = true;
            this.listViewPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewPosition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader21,
            this.columnHeader24,
            this.columnHeader28,
            this.columnHeader29,
            this.columnHeader26,
            this.columnHeader27,
            this.columnHeader34,
            this.columnHeader44,
            this.columnHeader30,
            this.columnHeader31,
            this.columnHeader32,
            this.columnHeader25});
            this.listViewPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPosition.FullRowSelect = true;
            this.listViewPosition.GridLines = true;
            listViewGroup10.Header = "当日持仓";
            listViewGroup10.Name = "Today";
            listViewGroup11.Header = "历史持仓";
            listViewGroup11.Name = "History";
            listViewGroup12.Header = "套利";
            listViewGroup12.Name = "Arbitrage";
            this.listViewPosition.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup10,
            listViewGroup11,
            listViewGroup12});
            this.listViewPosition.HideSelection = false;
            this.listViewPosition.Location = new System.Drawing.Point(0, 0);
            this.listViewPosition.MultiSelect = false;
            this.listViewPosition.Name = "listViewPosition";
            this.listViewPosition.Size = new System.Drawing.Size(682, 206);
            this.listViewPosition.SortColumn = 0;
            this.listViewPosition.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listViewPosition, "单击选择合约,下单区平仓\r\n双击快速平仓");
            this.listViewPosition.UseCompatibleStateImageBehavior = false;
            this.listViewPosition.View = System.Windows.Forms.View.Details;
            this.listViewPosition.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewPosition_MouseClick);
            this.listViewPosition.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewPosition_MouseDoubleClick);
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "合约";
            this.columnHeader21.Width = 50;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "多空";
            this.columnHeader24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader24.Width = 40;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "昨持";
            this.columnHeader28.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader28.Width = 40;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "今持";
            this.columnHeader29.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader29.Width = 40;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "持仓均价";
            this.columnHeader26.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader26.Width = 80;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "持仓盈亏";
            this.columnHeader27.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader34
            // 
            this.columnHeader34.Text = "开仓均价";
            this.columnHeader34.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader34.Width = 80;
            // 
            // columnHeader44
            // 
            this.columnHeader44.Text = "手数";
            this.columnHeader44.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader44.Width = 40;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "平仓均价";
            this.columnHeader30.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader30.Width = 80;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "手数";
            this.columnHeader31.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader31.Width = 40;
            // 
            // columnHeader32
            // 
            this.columnHeader32.Text = "平仓盈亏";
            this.columnHeader32.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader32.Width = 70;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "手续费";
            this.columnHeader25.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hfListViewParkedOrder
            // 
            this.hfListViewParkedOrder.AllowColumnReorder = true;
            this.hfListViewParkedOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hfListViewParkedOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader33,
            this.columnHeader35,
            this.columnHeader36,
            this.columnHeader37,
            this.columnHeader38,
            this.columnHeader39,
            this.columnHeader40,
            this.columnHeader41,
            this.columnHeader42});
            this.hfListViewParkedOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hfListViewParkedOrder.FullRowSelect = true;
            this.hfListViewParkedOrder.GridLines = true;
            this.hfListViewParkedOrder.HideSelection = false;
            this.hfListViewParkedOrder.Location = new System.Drawing.Point(0, 0);
            this.hfListViewParkedOrder.MultiSelect = false;
            this.hfListViewParkedOrder.Name = "hfListViewParkedOrder";
            this.hfListViewParkedOrder.Size = new System.Drawing.Size(682, 211);
            this.hfListViewParkedOrder.SortColumn = 0;
            this.hfListViewParkedOrder.TabIndex = 0;
            this.toolTip1.SetToolTip(this.hfListViewParkedOrder, "预埋单\r\n双击删除");
            this.hfListViewParkedOrder.UseCompatibleStateImageBehavior = false;
            this.hfListViewParkedOrder.View = System.Windows.Forms.View.Details;
            this.hfListViewParkedOrder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.hfListViewParkedOrder_MouseDoubleClick);
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "合约";
            this.columnHeader33.Width = 50;
            // 
            // columnHeader35
            // 
            this.columnHeader35.Text = "触发价格";
            this.columnHeader35.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader35.Width = 80;
            // 
            // columnHeader36
            // 
            this.columnHeader36.Text = "买卖";
            this.columnHeader36.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader36.Width = 40;
            // 
            // columnHeader37
            // 
            this.columnHeader37.Text = "开平";
            this.columnHeader37.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader37.Width = 40;
            // 
            // columnHeader38
            // 
            this.columnHeader38.Text = "价格条件";
            this.columnHeader38.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader38.Width = 80;
            // 
            // columnHeader39
            // 
            this.columnHeader39.Text = "报单价格";
            this.columnHeader39.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader39.Width = 80;
            // 
            // columnHeader40
            // 
            this.columnHeader40.Text = "报单数量";
            this.columnHeader40.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader41
            // 
            this.columnHeader41.Text = "埋单状态";
            this.columnHeader41.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader41.Width = 100;
            // 
            // columnHeader42
            // 
            this.columnHeader42.Text = "错误信息";
            this.columnHeader42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader42.Width = 150;
            // 
            // hfListViewParkedAction
            // 
            this.hfListViewParkedAction.AllowColumnReorder = true;
            this.hfListViewParkedAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hfListViewParkedAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hfListViewParkedAction.FullRowSelect = true;
            this.hfListViewParkedAction.GridLines = true;
            this.hfListViewParkedAction.HideSelection = false;
            this.hfListViewParkedAction.Location = new System.Drawing.Point(0, 0);
            this.hfListViewParkedAction.MultiSelect = false;
            this.hfListViewParkedAction.Name = "hfListViewParkedAction";
            this.hfListViewParkedAction.Size = new System.Drawing.Size(682, 26);
            this.hfListViewParkedAction.SortColumn = 0;
            this.hfListViewParkedAction.TabIndex = 0;
            this.toolTip1.SetToolTip(this.hfListViewParkedAction, "预埋撤单\r\n双击删除");
            this.hfListViewParkedAction.UseCompatibleStateImageBehavior = false;
            this.hfListViewParkedAction.View = System.Windows.Forms.View.Details;
            this.hfListViewParkedAction.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.hfListViewParkedAction_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonTrade);
            this.panel1.Controls.Add(this.radioButtonMd);
            this.panel1.Controls.Add(this.labelSHFE);
            this.panel1.Controls.Add(this.labelCZCE);
            this.panel1.Controls.Add(this.labelDCE);
            this.panel1.Controls.Add(this.labelFFEX);
            this.panel1.Controls.Add(this.comboBoxErrMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 538);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 24);
            this.panel1.TabIndex = 4;
            // 
            // radioButtonTrade
            // 
            this.radioButtonTrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonTrade.AutoCheck = false;
            this.radioButtonTrade.AutoSize = true;
            this.radioButtonTrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonTrade.ForeColor = System.Drawing.Color.Red;
            this.radioButtonTrade.Location = new System.Drawing.Point(753, 3);
            this.radioButtonTrade.Name = "radioButtonTrade";
            this.radioButtonTrade.Size = new System.Drawing.Size(46, 16);
            this.radioButtonTrade.TabIndex = 5;
            this.radioButtonTrade.Text = "交易";
            this.radioButtonTrade.UseVisualStyleBackColor = true;
            // 
            // radioButtonMd
            // 
            this.radioButtonMd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonMd.AutoCheck = false;
            this.radioButtonMd.AutoSize = true;
            this.radioButtonMd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonMd.ForeColor = System.Drawing.Color.Red;
            this.radioButtonMd.Location = new System.Drawing.Point(704, 3);
            this.radioButtonMd.Name = "radioButtonMd";
            this.radioButtonMd.Size = new System.Drawing.Size(46, 16);
            this.radioButtonMd.TabIndex = 5;
            this.radioButtonMd.Text = "行情";
            this.radioButtonMd.UseVisualStyleBackColor = true;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tabControl1);
            this.panelTop.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CTP_交易.Properties.Settings.Default, "TopVisable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1084, 255);
            this.panelTop.TabIndex = 8;
            this.panelTop.Visible = global::CTP_交易.Properties.Settings.Default.TopVisable;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPageSHFE);
            this.tabControl1.Controls.Add(this.tabPageCZCE);
            this.tabControl1.Controls.Add(this.tabPageDCE);
            this.tabControl1.Controls.Add(this.tabPageCFFEX);
            this.tabControl1.Controls.Add(this.tabPageSelected);
            this.tabControl1.Controls.Add(this.tabPageArbitrage);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1084, 255);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageSHFE
            // 
            this.tabPageSHFE.Controls.Add(this.dataGridViewSHFE);
            this.tabPageSHFE.Location = new System.Drawing.Point(4, 4);
            this.tabPageSHFE.Name = "tabPageSHFE";
            this.tabPageSHFE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSHFE.Size = new System.Drawing.Size(1076, 229);
            this.tabPageSHFE.TabIndex = 0;
            this.tabPageSHFE.Text = "上期所";
            this.tabPageSHFE.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSHFE
            // 
            this.dataGridViewSHFE.AllowUserToAddRows = false;
            this.dataGridViewSHFE.AllowUserToDeleteRows = false;
            this.dataGridViewSHFE.AllowUserToOrderColumns = true;
            this.dataGridViewSHFE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSHFE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSHFE.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewSHFE.Name = "dataGridViewSHFE";
            this.dataGridViewSHFE.RowTemplate.Height = 23;
            this.dataGridViewSHFE.Size = new System.Drawing.Size(1070, 223);
            this.dataGridViewSHFE.TabIndex = 0;
            // 
            // tabPageCZCE
            // 
            this.tabPageCZCE.Controls.Add(this.dataGridViewCZCE);
            this.tabPageCZCE.Location = new System.Drawing.Point(4, 4);
            this.tabPageCZCE.Name = "tabPageCZCE";
            this.tabPageCZCE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCZCE.Size = new System.Drawing.Size(1076, 229);
            this.tabPageCZCE.TabIndex = 1;
            this.tabPageCZCE.Text = "郑商所";
            this.tabPageCZCE.UseVisualStyleBackColor = true;
            // 
            // dataGridViewCZCE
            // 
            this.dataGridViewCZCE.AllowUserToAddRows = false;
            this.dataGridViewCZCE.AllowUserToDeleteRows = false;
            this.dataGridViewCZCE.AllowUserToOrderColumns = true;
            this.dataGridViewCZCE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCZCE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCZCE.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewCZCE.Name = "dataGridViewCZCE";
            this.dataGridViewCZCE.ReadOnly = true;
            this.dataGridViewCZCE.RowTemplate.Height = 23;
            this.dataGridViewCZCE.Size = new System.Drawing.Size(1070, 223);
            this.dataGridViewCZCE.TabIndex = 0;
            // 
            // tabPageDCE
            // 
            this.tabPageDCE.Controls.Add(this.dataGridViewDCE);
            this.tabPageDCE.Location = new System.Drawing.Point(4, 4);
            this.tabPageDCE.Name = "tabPageDCE";
            this.tabPageDCE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDCE.Size = new System.Drawing.Size(1076, 229);
            this.tabPageDCE.TabIndex = 2;
            this.tabPageDCE.Text = "大商所";
            this.tabPageDCE.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDCE
            // 
            this.dataGridViewDCE.AllowUserToAddRows = false;
            this.dataGridViewDCE.AllowUserToDeleteRows = false;
            this.dataGridViewDCE.AllowUserToOrderColumns = true;
            this.dataGridViewDCE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDCE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDCE.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewDCE.Name = "dataGridViewDCE";
            this.dataGridViewDCE.ReadOnly = true;
            this.dataGridViewDCE.RowTemplate.Height = 23;
            this.dataGridViewDCE.Size = new System.Drawing.Size(1070, 223);
            this.dataGridViewDCE.TabIndex = 0;
            // 
            // tabPageCFFEX
            // 
            this.tabPageCFFEX.Controls.Add(this.dataGridViewCFFEX);
            this.tabPageCFFEX.Location = new System.Drawing.Point(4, 4);
            this.tabPageCFFEX.Name = "tabPageCFFEX";
            this.tabPageCFFEX.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCFFEX.Size = new System.Drawing.Size(1076, 229);
            this.tabPageCFFEX.TabIndex = 3;
            this.tabPageCFFEX.Text = "中金所";
            this.tabPageCFFEX.UseVisualStyleBackColor = true;
            // 
            // dataGridViewCFFEX
            // 
            this.dataGridViewCFFEX.AllowUserToAddRows = false;
            this.dataGridViewCFFEX.AllowUserToDeleteRows = false;
            this.dataGridViewCFFEX.AllowUserToOrderColumns = true;
            this.dataGridViewCFFEX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCFFEX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCFFEX.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewCFFEX.Name = "dataGridViewCFFEX";
            this.dataGridViewCFFEX.ReadOnly = true;
            this.dataGridViewCFFEX.RowTemplate.Height = 23;
            this.dataGridViewCFFEX.Size = new System.Drawing.Size(1070, 223);
            this.dataGridViewCFFEX.TabIndex = 0;
            // 
            // tabPageSelected
            // 
            this.tabPageSelected.Controls.Add(this.dataGridViewSelected);
            this.tabPageSelected.Location = new System.Drawing.Point(4, 4);
            this.tabPageSelected.Name = "tabPageSelected";
            this.tabPageSelected.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSelected.Size = new System.Drawing.Size(1076, 229);
            this.tabPageSelected.TabIndex = 4;
            this.tabPageSelected.Text = "自选合约";
            this.tabPageSelected.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSelected
            // 
            this.dataGridViewSelected.AllowUserToAddRows = false;
            this.dataGridViewSelected.AllowUserToDeleteRows = false;
            this.dataGridViewSelected.AllowUserToOrderColumns = true;
            this.dataGridViewSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSelected.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewSelected.Name = "dataGridViewSelected";
            this.dataGridViewSelected.ReadOnly = true;
            this.dataGridViewSelected.RowTemplate.Height = 23;
            this.dataGridViewSelected.Size = new System.Drawing.Size(1070, 223);
            this.dataGridViewSelected.TabIndex = 0;
            // 
            // tabPageArbitrage
            // 
            this.tabPageArbitrage.Controls.Add(this.dataGridViewArbitrage);
            this.tabPageArbitrage.Location = new System.Drawing.Point(4, 4);
            this.tabPageArbitrage.Name = "tabPageArbitrage";
            this.tabPageArbitrage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageArbitrage.Size = new System.Drawing.Size(1076, 229);
            this.tabPageArbitrage.TabIndex = 5;
            this.tabPageArbitrage.Text = "套利合约";
            this.tabPageArbitrage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewArbitrage
            // 
            this.dataGridViewArbitrage.AllowUserToAddRows = false;
            this.dataGridViewArbitrage.AllowUserToDeleteRows = false;
            this.dataGridViewArbitrage.AllowUserToOrderColumns = true;
            this.dataGridViewArbitrage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewArbitrage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewArbitrage.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewArbitrage.Name = "dataGridViewArbitrage";
            this.dataGridViewArbitrage.ReadOnly = true;
            this.dataGridViewArbitrage.RowTemplate.Height = 23;
            this.dataGridViewArbitrage.Size = new System.Drawing.Size(1070, 223);
            this.dataGridViewArbitrage.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1076, 229);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPage1_Paint);
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 263);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControSystem);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainerOrder);
            this.splitContainer1.Size = new System.Drawing.Size(1084, 275);
            this.splitContainer1.SplitterDistance = 698;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControSystem
            // 
            this.tabControSystem.Controls.Add(this.tabPageOrder);
            this.tabControSystem.Controls.Add(this.tabPagePosition);
            this.tabControSystem.Controls.Add(this.tabPageParked);
            this.tabControSystem.Controls.Add(this.tabPageAccount);
            this.tabControSystem.Controls.Add(this.tabPageInstrument);
            this.tabControSystem.Controls.Add(this.tabPageInstrumentSelf);
            this.tabControSystem.Controls.Add(this.tabPageBankFuture);
            this.tabControSystem.Controls.Add(this.tabPageSystem);
            this.tabControSystem.Controls.Add(this.tabPageSetting);
            this.tabControSystem.Controls.Add(this.tabPageHelp);
            this.tabControSystem.Controls.Add(this.tabPageAbout);
            this.tabControSystem.Controls.Add(this.tabPageQryTrade);
            this.tabControSystem.Controls.Add(this.tabPageQrySettleInfo);
            this.tabControSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControSystem.Location = new System.Drawing.Point(0, 0);
            this.tabControSystem.Name = "tabControSystem";
            this.tabControSystem.SelectedIndex = 0;
            this.tabControSystem.Size = new System.Drawing.Size(696, 273);
            this.tabControSystem.TabIndex = 0;
            // 
            // tabPageOrder
            // 
            this.tabPageOrder.Controls.Add(this.listViewOrder);
            this.tabPageOrder.Location = new System.Drawing.Point(4, 22);
            this.tabPageOrder.Name = "tabPageOrder";
            this.tabPageOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOrder.Size = new System.Drawing.Size(688, 247);
            this.tabPageOrder.TabIndex = 0;
            this.tabPageOrder.Text = "委  托";
            this.tabPageOrder.UseVisualStyleBackColor = true;
            // 
            // tabPagePosition
            // 
            this.tabPagePosition.Controls.Add(this.splitContainer4);
            this.tabPagePosition.Location = new System.Drawing.Point(4, 22);
            this.tabPagePosition.Name = "tabPagePosition";
            this.tabPagePosition.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePosition.Size = new System.Drawing.Size(688, 247);
            this.tabPagePosition.TabIndex = 2;
            this.tabPagePosition.Text = "持  仓";
            this.tabPagePosition.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.button4);
            this.splitContainer4.Panel1.Controls.Add(this.buttonQuickLock);
            this.splitContainer4.Panel1.Controls.Add(this.buttonCovert);
            this.splitContainer4.Panel1.Controls.Add(this.buttonQuickClose);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.listViewPosition);
            this.splitContainer4.Size = new System.Drawing.Size(682, 241);
            this.splitContainer4.SplitterDistance = 31;
            this.splitContainer4.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(541, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "市价反手";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // buttonQuickLock
            // 
            this.buttonQuickLock.Location = new System.Drawing.Point(387, 4);
            this.buttonQuickLock.Name = "buttonQuickLock";
            this.buttonQuickLock.Size = new System.Drawing.Size(75, 23);
            this.buttonQuickLock.TabIndex = 3;
            this.buttonQuickLock.Text = "快速锁仓";
            this.buttonQuickLock.UseVisualStyleBackColor = true;
            this.buttonQuickLock.Click += new System.EventHandler(this.buttonQuickLock_Click);
            // 
            // buttonCovert
            // 
            this.buttonCovert.Location = new System.Drawing.Point(233, 4);
            this.buttonCovert.Name = "buttonCovert";
            this.buttonCovert.Size = new System.Drawing.Size(75, 23);
            this.buttonCovert.TabIndex = 2;
            this.buttonCovert.Text = "快速反手";
            this.buttonCovert.UseVisualStyleBackColor = true;
            this.buttonCovert.Click += new System.EventHandler(this.buttonCovert_Click);
            // 
            // buttonQuickClose
            // 
            this.buttonQuickClose.Location = new System.Drawing.Point(79, 4);
            this.buttonQuickClose.Name = "buttonQuickClose";
            this.buttonQuickClose.Size = new System.Drawing.Size(75, 23);
            this.buttonQuickClose.TabIndex = 1;
            this.buttonQuickClose.Text = "快速平仓";
            this.buttonQuickClose.UseVisualStyleBackColor = true;
            this.buttonQuickClose.Click += new System.EventHandler(this.buttonQuickClose_Click);
            // 
            // tabPageParked
            // 
            this.tabPageParked.Controls.Add(this.splitContainer5);
            this.tabPageParked.Location = new System.Drawing.Point(4, 22);
            this.tabPageParked.Name = "tabPageParked";
            this.tabPageParked.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParked.Size = new System.Drawing.Size(688, 247);
            this.tabPageParked.TabIndex = 3;
            this.tabPageParked.Text = "预  埋";
            this.tabPageParked.UseVisualStyleBackColor = true;
            this.tabPageParked.Enter += new System.EventHandler(this.tabPageParked_Enter);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(3, 3);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.hfListViewParkedOrder);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.hfListViewParkedAction);
            this.splitContainer5.Size = new System.Drawing.Size(682, 241);
            this.splitContainer5.SplitterDistance = 211;
            this.splitContainer5.TabIndex = 1;
            // 
            // tabPageAccount
            // 
            this.tabPageAccount.Controls.Add(this.splitContainer7);
            this.tabPageAccount.Location = new System.Drawing.Point(4, 22);
            this.tabPageAccount.Name = "tabPageAccount";
            this.tabPageAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAccount.Size = new System.Drawing.Size(688, 247);
            this.tabPageAccount.TabIndex = 4;
            this.tabPageAccount.Text = "资  金";
            this.tabPageAccount.UseVisualStyleBackColor = true;
            this.tabPageAccount.Enter += new System.EventHandler(this.tabPageAccount_Enter);
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(3, 3);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.buttonQryAccount);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.userControlTradeAccount1);
            this.splitContainer7.Size = new System.Drawing.Size(682, 241);
            this.splitContainer7.SplitterDistance = 29;
            this.splitContainer7.TabIndex = 0;
            // 
            // buttonQryAccount
            // 
            this.buttonQryAccount.Location = new System.Drawing.Point(163, 5);
            this.buttonQryAccount.Name = "buttonQryAccount";
            this.buttonQryAccount.Size = new System.Drawing.Size(75, 23);
            this.buttonQryAccount.TabIndex = 0;
            this.buttonQryAccount.Text = "刷  新";
            this.buttonQryAccount.UseVisualStyleBackColor = true;
            this.buttonQryAccount.Click += new System.EventHandler(this.buttonQryAccount_Click);
            // 
            // userControlTradeAccount1
            // 
            this.userControlTradeAccount1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlTradeAccount1.IsActive = false;
            this.userControlTradeAccount1.Location = new System.Drawing.Point(0, 0);
            this.userControlTradeAccount1.Name = "userControlTradeAccount1";
            this.userControlTradeAccount1.Size = new System.Drawing.Size(682, 208);
            this.userControlTradeAccount1.TabIndex = 0;
            // 
            // tabPageInstrument
            // 
            this.tabPageInstrument.Controls.Add(this.dataGridViewInstruments);
            this.tabPageInstrument.Location = new System.Drawing.Point(4, 22);
            this.tabPageInstrument.Name = "tabPageInstrument";
            this.tabPageInstrument.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInstrument.Size = new System.Drawing.Size(688, 247);
            this.tabPageInstrument.TabIndex = 5;
            this.tabPageInstrument.Text = "合  约";
            this.tabPageInstrument.UseVisualStyleBackColor = true;
            // 
            // dataGridViewInstruments
            // 
            this.dataGridViewInstruments.AllowUserToAddRows = false;
            this.dataGridViewInstruments.AllowUserToDeleteRows = false;
            this.dataGridViewInstruments.AllowUserToOrderColumns = true;
            this.dataGridViewInstruments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInstruments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInstruments.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewInstruments.Name = "dataGridViewInstruments";
            this.dataGridViewInstruments.ReadOnly = true;
            this.dataGridViewInstruments.RowTemplate.Height = 23;
            this.dataGridViewInstruments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInstruments.Size = new System.Drawing.Size(682, 241);
            this.dataGridViewInstruments.TabIndex = 0;
            // 
            // tabPageInstrumentSelf
            // 
            this.tabPageInstrumentSelf.Controls.Add(this.dataGridViewInstrumentsSelected);
            this.tabPageInstrumentSelf.Location = new System.Drawing.Point(4, 22);
            this.tabPageInstrumentSelf.Name = "tabPageInstrumentSelf";
            this.tabPageInstrumentSelf.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInstrumentSelf.Size = new System.Drawing.Size(688, 247);
            this.tabPageInstrumentSelf.TabIndex = 6;
            this.tabPageInstrumentSelf.Text = "自选合约";
            this.tabPageInstrumentSelf.UseVisualStyleBackColor = true;
            // 
            // dataGridViewInstrumentsSelected
            // 
            this.dataGridViewInstrumentsSelected.AllowUserToAddRows = false;
            this.dataGridViewInstrumentsSelected.AllowUserToDeleteRows = false;
            this.dataGridViewInstrumentsSelected.AllowUserToOrderColumns = true;
            this.dataGridViewInstrumentsSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInstrumentsSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInstrumentsSelected.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewInstrumentsSelected.Name = "dataGridViewInstrumentsSelected";
            this.dataGridViewInstrumentsSelected.ReadOnly = true;
            this.dataGridViewInstrumentsSelected.RowTemplate.Height = 23;
            this.dataGridViewInstrumentsSelected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInstrumentsSelected.Size = new System.Drawing.Size(682, 241);
            this.dataGridViewInstrumentsSelected.TabIndex = 0;
            // 
            // tabPageBankFuture
            // 
            this.tabPageBankFuture.Controls.Add(this.listViewSeries);
            this.tabPageBankFuture.Controls.Add(this.listViewBank);
            this.tabPageBankFuture.Controls.Add(this.buttonTransfer);
            this.tabPageBankFuture.Controls.Add(this.buttonQryTransferSeries);
            this.tabPageBankFuture.Controls.Add(this.comboBoxTransferType);
            this.tabPageBankFuture.Controls.Add(this.textBoxBankName);
            this.tabPageBankFuture.Controls.Add(this.label6);
            this.tabPageBankFuture.Controls.Add(this.textBox3);
            this.tabPageBankFuture.Controls.Add(this.label7);
            this.tabPageBankFuture.Controls.Add(this.textBoxTradeAmount);
            this.tabPageBankFuture.Controls.Add(this.textBoxBankPwd);
            this.tabPageBankFuture.Controls.Add(this.label12);
            this.tabPageBankFuture.Controls.Add(this.textBoxAccountPwd);
            this.tabPageBankFuture.Controls.Add(this.label11);
            this.tabPageBankFuture.Controls.Add(this.textBoxFutureFetchAmount);
            this.tabPageBankFuture.Controls.Add(this.label10);
            this.tabPageBankFuture.Controls.Add(this.label9);
            this.tabPageBankFuture.Controls.Add(this.label8);
            this.tabPageBankFuture.Controls.Add(this.textBoxBankID);
            this.tabPageBankFuture.Controls.Add(this.label2);
            this.tabPageBankFuture.Location = new System.Drawing.Point(4, 22);
            this.tabPageBankFuture.Name = "tabPageBankFuture";
            this.tabPageBankFuture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBankFuture.Size = new System.Drawing.Size(688, 247);
            this.tabPageBankFuture.TabIndex = 8;
            this.tabPageBankFuture.Text = "银期转帐";
            this.tabPageBankFuture.UseVisualStyleBackColor = true;
            // 
            // listViewSeries
            // 
            this.listViewSeries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.listViewSeries.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewSeries.Location = new System.Drawing.Point(3, 149);
            this.listViewSeries.Name = "listViewSeries";
            this.listViewSeries.Size = new System.Drawing.Size(682, 95);
            this.listViewSeries.SortColumn = 0;
            this.listViewSeries.TabIndex = 6;
            this.listViewSeries.UseCompatibleStateImageBehavior = false;
            this.listViewSeries.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "流水号";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "银行代码";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "操作时间";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 120;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "交易代码";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "金    额";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "处理状态";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "处理结果";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader12.Width = 198;
            // 
            // listViewBank
            // 
            this.listViewBank.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewBank.FullRowSelect = true;
            this.listViewBank.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.listViewBank.Location = new System.Drawing.Point(317, 14);
            this.listViewBank.MultiSelect = false;
            this.listViewBank.Name = "listViewBank";
            this.listViewBank.Size = new System.Drawing.Size(369, 131);
            this.listViewBank.SortColumn = 0;
            this.listViewBank.TabIndex = 5;
            this.listViewBank.UseCompatibleStateImageBehavior = false;
            this.listViewBank.View = System.Windows.Forms.View.Details;
            this.listViewBank.SelectedIndexChanged += new System.EventHandler(this.listViewBank_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "银行代码";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "银行简称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 109;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "银行分中心代码";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 122;
            // 
            // buttonTransfer
            // 
            this.buttonTransfer.Location = new System.Drawing.Point(155, 124);
            this.buttonTransfer.Name = "buttonTransfer";
            this.buttonTransfer.Size = new System.Drawing.Size(75, 23);
            this.buttonTransfer.TabIndex = 4;
            this.buttonTransfer.Text = "发 送";
            this.buttonTransfer.UseVisualStyleBackColor = true;
            this.buttonTransfer.Click += new System.EventHandler(this.buttonTransfer_Click);
            // 
            // buttonQryTransferSeries
            // 
            this.buttonQryTransferSeries.Location = new System.Drawing.Point(236, 124);
            this.buttonQryTransferSeries.Name = "buttonQryTransferSeries";
            this.buttonQryTransferSeries.Size = new System.Drawing.Size(75, 23);
            this.buttonQryTransferSeries.TabIndex = 3;
            this.buttonQryTransferSeries.Text = "转帐查询";
            this.buttonQryTransferSeries.UseVisualStyleBackColor = true;
            this.buttonQryTransferSeries.Click += new System.EventHandler(this.buttonQryTransferSeries_Click);
            // 
            // comboBoxTransferType
            // 
            this.comboBoxTransferType.FormattingEnabled = true;
            this.comboBoxTransferType.Items.AddRange(new object[] {
            "保证金=>银行卡",
            "银行卡=>保证金"});
            this.comboBoxTransferType.Location = new System.Drawing.Point(211, 70);
            this.comboBoxTransferType.Name = "comboBoxTransferType";
            this.comboBoxTransferType.Size = new System.Drawing.Size(100, 20);
            this.comboBoxTransferType.TabIndex = 2;
            this.comboBoxTransferType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBoxBankName
            // 
            this.textBoxBankName.Enabled = false;
            this.textBoxBankName.Location = new System.Drawing.Point(211, 15);
            this.textBoxBankName.Name = "textBoxBankName";
            this.textBoxBankName.Size = new System.Drawing.Size(100, 21);
            this.textBoxBankName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(152, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "银行简称";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(70, 41);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(241, 21);
            this.textBox3.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "银行帐号";
            // 
            // textBoxTradeAmount
            // 
            this.textBoxTradeAmount.Location = new System.Drawing.Point(211, 97);
            this.textBoxTradeAmount.Name = "textBoxTradeAmount";
            this.textBoxTradeAmount.Size = new System.Drawing.Size(100, 21);
            this.textBoxTradeAmount.TabIndex = 1;
            // 
            // textBoxBankPwd
            // 
            this.textBoxBankPwd.Enabled = false;
            this.textBoxBankPwd.Location = new System.Drawing.Point(70, 124);
            this.textBoxBankPwd.Name = "textBoxBankPwd";
            this.textBoxBankPwd.PasswordChar = '*';
            this.textBoxBankPwd.Size = new System.Drawing.Size(75, 21);
            this.textBoxBankPwd.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(152, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "转帐金额";
            // 
            // textBoxAccountPwd
            // 
            this.textBoxAccountPwd.Location = new System.Drawing.Point(70, 97);
            this.textBoxAccountPwd.Name = "textBoxAccountPwd";
            this.textBoxAccountPwd.PasswordChar = '*';
            this.textBoxAccountPwd.Size = new System.Drawing.Size(75, 21);
            this.textBoxAccountPwd.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "银行密码";
            // 
            // textBoxFutureFetchAmount
            // 
            this.textBoxFutureFetchAmount.Enabled = false;
            this.textBoxFutureFetchAmount.Location = new System.Drawing.Point(70, 70);
            this.textBoxFutureFetchAmount.Name = "textBoxFutureFetchAmount";
            this.textBoxFutureFetchAmount.Size = new System.Drawing.Size(75, 21);
            this.textBoxFutureFetchAmount.TabIndex = 1;
            this.textBoxFutureFetchAmount.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "资金密码";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(152, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "转帐类别";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "可转资金";
            // 
            // textBoxBankID
            // 
            this.textBoxBankID.Enabled = false;
            this.textBoxBankID.Location = new System.Drawing.Point(70, 14);
            this.textBoxBankID.Name = "textBoxBankID";
            this.textBoxBankID.Size = new System.Drawing.Size(75, 21);
            this.textBoxBankID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "银行代码";
            // 
            // tabPageSystem
            // 
            this.tabPageSystem.Controls.Add(this.groupBox2);
            this.tabPageSystem.Controls.Add(this.groupBox1);
            this.tabPageSystem.Location = new System.Drawing.Point(4, 22);
            this.tabPageSystem.Name = "tabPageSystem";
            this.tabPageSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSystem.Size = new System.Drawing.Size(688, 247);
            this.tabPageSystem.TabIndex = 7;
            this.tabPageSystem.Text = "系  统";
            this.tabPageSystem.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSaveLog);
            this.groupBox2.Controls.Add(this.hfListViewLog);
            this.groupBox2.Controls.Add(this.buttonShowLog);
            this.groupBox2.Controls.Add(this.buttonClearLog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(214, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(471, 241);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "日志";
            // 
            // buttonSaveLog
            // 
            this.buttonSaveLog.Location = new System.Drawing.Point(295, 14);
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveLog.TabIndex = 6;
            this.buttonSaveLog.Text = "保  存";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            this.buttonSaveLog.Click += new System.EventHandler(this.buttonSaveLog_Click);
            // 
            // hfListViewLog
            // 
            this.hfListViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader67,
            this.columnHeader68});
            this.hfListViewLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hfListViewLog.FullRowSelect = true;
            this.hfListViewLog.GridLines = true;
            this.hfListViewLog.Location = new System.Drawing.Point(3, 43);
            this.hfListViewLog.Name = "hfListViewLog";
            this.hfListViewLog.Size = new System.Drawing.Size(465, 195);
            this.hfListViewLog.SortColumn = 0;
            this.hfListViewLog.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.hfListViewLog.TabIndex = 3;
            this.hfListViewLog.UseCompatibleStateImageBehavior = false;
            this.hfListViewLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader67
            // 
            this.columnHeader67.Text = "          时 间";
            this.columnHeader67.Width = 160;
            // 
            // columnHeader68
            // 
            this.columnHeader68.Text = "     日 志";
            this.columnHeader68.Width = 300;
            // 
            // buttonShowLog
            // 
            this.buttonShowLog.Location = new System.Drawing.Point(67, 14);
            this.buttonShowLog.Name = "buttonShowLog";
            this.buttonShowLog.Size = new System.Drawing.Size(75, 23);
            this.buttonShowLog.TabIndex = 4;
            this.buttonShowLog.Text = "显示日志";
            this.buttonShowLog.UseVisualStyleBackColor = true;
            this.buttonShowLog.Click += new System.EventHandler(this.buttonShowLog_Click);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(181, 14);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLog.TabIndex = 5;
            this.buttonClearLog.Text = "清除日志";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonExportRate);
            this.groupBox1.Controls.Add(this.buttonReset);
            this.groupBox1.Controls.Add(this.textBoxOldPassword);
            this.groupBox1.Controls.Add(this.radioButtonUser);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.radioButtonAccount);
            this.groupBox1.Controls.Add(this.buttonChangePwd);
            this.groupBox1.Controls.Add(this.textBoxNewPwdConfirm);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.textBoxNewPassWord);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 238);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "修改密码";
            // 
            // buttonExportRate
            // 
            this.buttonExportRate.Location = new System.Drawing.Point(96, 200);
            this.buttonExportRate.Name = "buttonExportRate";
            this.buttonExportRate.Size = new System.Drawing.Size(75, 23);
            this.buttonExportRate.TabIndex = 8;
            this.buttonExportRate.Text = "导出保证金";
            this.buttonExportRate.UseVisualStyleBackColor = true;
            this.buttonExportRate.Visible = false;
            this.buttonExportRate.Click += new System.EventHandler(this.buttonExportRate_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(96, 171);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "重 置";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // textBoxOldPassword
            // 
            this.textBoxOldPassword.Location = new System.Drawing.Point(69, 59);
            this.textBoxOldPassword.Name = "textBoxOldPassword";
            this.textBoxOldPassword.PasswordChar = '*';
            this.textBoxOldPassword.Size = new System.Drawing.Size(101, 21);
            this.textBoxOldPassword.TabIndex = 1;
            // 
            // radioButtonUser
            // 
            this.radioButtonUser.AutoSize = true;
            this.radioButtonUser.Checked = true;
            this.radioButtonUser.Location = new System.Drawing.Point(20, 27);
            this.radioButtonUser.Name = "radioButtonUser";
            this.radioButtonUser.Size = new System.Drawing.Size(71, 16);
            this.radioButtonUser.TabIndex = 2;
            this.radioButtonUser.TabStop = true;
            this.radioButtonUser.Text = "用户密码";
            this.radioButtonUser.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(24, 62);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(41, 12);
            this.label34.TabIndex = 6;
            this.label34.Text = "原密码";
            // 
            // radioButtonAccount
            // 
            this.radioButtonAccount.AutoSize = true;
            this.radioButtonAccount.Location = new System.Drawing.Point(97, 27);
            this.radioButtonAccount.Name = "radioButtonAccount";
            this.radioButtonAccount.Size = new System.Drawing.Size(71, 16);
            this.radioButtonAccount.TabIndex = 2;
            this.radioButtonAccount.Text = "资金密码";
            this.radioButtonAccount.UseVisualStyleBackColor = true;
            // 
            // buttonChangePwd
            // 
            this.buttonChangePwd.Location = new System.Drawing.Point(14, 171);
            this.buttonChangePwd.Name = "buttonChangePwd";
            this.buttonChangePwd.Size = new System.Drawing.Size(75, 23);
            this.buttonChangePwd.TabIndex = 4;
            this.buttonChangePwd.Text = "修改密码";
            this.buttonChangePwd.UseVisualStyleBackColor = true;
            this.buttonChangePwd.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // textBoxNewPwdConfirm
            // 
            this.textBoxNewPwdConfirm.Location = new System.Drawing.Point(69, 135);
            this.textBoxNewPwdConfirm.Name = "textBoxNewPwdConfirm";
            this.textBoxNewPwdConfirm.PasswordChar = '*';
            this.textBoxNewPwdConfirm.Size = new System.Drawing.Size(101, 21);
            this.textBoxNewPwdConfirm.TabIndex = 3;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(12, 138);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 12);
            this.label38.TabIndex = 6;
            this.label38.Text = "确认密码";
            // 
            // textBoxNewPassWord
            // 
            this.textBoxNewPassWord.Location = new System.Drawing.Point(69, 97);
            this.textBoxNewPassWord.Name = "textBoxNewPassWord";
            this.textBoxNewPassWord.PasswordChar = '*';
            this.textBoxNewPassWord.Size = new System.Drawing.Size(101, 21);
            this.textBoxNewPassWord.TabIndex = 2;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(24, 100);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(41, 12);
            this.label35.TabIndex = 6;
            this.label35.Text = "新密码";
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.Controls.Add(this.groupBox6);
            this.tabPageSetting.Controls.Add(this.groupBox5);
            this.tabPageSetting.Controls.Add(this.groupBox4);
            this.tabPageSetting.Controls.Add(this.groupBox3);
            this.tabPageSetting.Location = new System.Drawing.Point(4, 22);
            this.tabPageSetting.Name = "tabPageSetting";
            this.tabPageSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetting.Size = new System.Drawing.Size(688, 247);
            this.tabPageSetting.TabIndex = 11;
            this.tabPageSetting.Text = "设  置";
            this.tabPageSetting.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBoxResetAll);
            this.groupBox6.Controls.Add(this.checkBoxClearLog);
            this.groupBox6.Controls.Add(this.checkBoxClearSelected);
            this.groupBox6.Controls.Add(this.checkBoxClearCosumVolume);
            this.groupBox6.Controls.Add(this.checkBoxClearServers);
            this.groupBox6.Controls.Add(this.buttonResetSetting);
            this.groupBox6.Location = new System.Drawing.Point(7, 75);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(290, 164);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "恢复设置";
            // 
            // checkBoxResetAll
            // 
            this.checkBoxResetAll.AutoSize = true;
            this.checkBoxResetAll.Location = new System.Drawing.Point(164, 95);
            this.checkBoxResetAll.Name = "checkBoxResetAll";
            this.checkBoxResetAll.Size = new System.Drawing.Size(96, 16);
            this.checkBoxResetAll.TabIndex = 4;
            this.checkBoxResetAll.Text = "恢复所有设置";
            this.checkBoxResetAll.UseVisualStyleBackColor = true;
            // 
            // checkBoxClearLog
            // 
            this.checkBoxClearLog.AutoSize = true;
            this.checkBoxClearLog.Location = new System.Drawing.Point(164, 66);
            this.checkBoxClearLog.Name = "checkBoxClearLog";
            this.checkBoxClearLog.Size = new System.Drawing.Size(96, 16);
            this.checkBoxClearLog.TabIndex = 4;
            this.checkBoxClearLog.Text = "清除日志记录";
            this.checkBoxClearLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxClearSelected
            // 
            this.checkBoxClearSelected.AutoSize = true;
            this.checkBoxClearSelected.Location = new System.Drawing.Point(37, 95);
            this.checkBoxClearSelected.Name = "checkBoxClearSelected";
            this.checkBoxClearSelected.Size = new System.Drawing.Size(96, 16);
            this.checkBoxClearSelected.TabIndex = 4;
            this.checkBoxClearSelected.Text = "清除自选合约";
            this.checkBoxClearSelected.UseVisualStyleBackColor = true;
            // 
            // checkBoxClearCosumVolume
            // 
            this.checkBoxClearCosumVolume.AutoSize = true;
            this.checkBoxClearCosumVolume.Location = new System.Drawing.Point(37, 66);
            this.checkBoxClearCosumVolume.Name = "checkBoxClearCosumVolume";
            this.checkBoxClearCosumVolume.Size = new System.Drawing.Size(108, 16);
            this.checkBoxClearCosumVolume.TabIndex = 4;
            this.checkBoxClearCosumVolume.Text = "清除自定义手数";
            this.checkBoxClearCosumVolume.UseVisualStyleBackColor = true;
            // 
            // checkBoxClearServers
            // 
            this.checkBoxClearServers.AutoSize = true;
            this.checkBoxClearServers.Location = new System.Drawing.Point(37, 42);
            this.checkBoxClearServers.Name = "checkBoxClearServers";
            this.checkBoxClearServers.Size = new System.Drawing.Size(108, 16);
            this.checkBoxClearServers.TabIndex = 4;
            this.checkBoxClearServers.Text = "清除服务器列表";
            this.checkBoxClearServers.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridViewCustomVolume);
            this.groupBox5.Location = new System.Drawing.Point(303, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(183, 238);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "自定义手数";
            // 
            // dataGridViewCustomVolume
            // 
            this.dataGridViewCustomVolume.AllowUserToAddRows = false;
            this.dataGridViewCustomVolume.AllowUserToDeleteRows = false;
            this.dataGridViewCustomVolume.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCustomVolume.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCustomVolume.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomVolume.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.合约,
            this.手数});
            this.dataGridViewCustomVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCustomVolume.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewCustomVolume.Name = "dataGridViewCustomVolume";
            this.dataGridViewCustomVolume.RowTemplate.Height = 23;
            this.dataGridViewCustomVolume.Size = new System.Drawing.Size(177, 218);
            this.dataGridViewCustomVolume.TabIndex = 0;
            this.dataGridViewCustomVolume.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomVolume_CellEndEdit);
            // 
            // 合约
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.合约.DefaultCellStyle = dataGridViewCellStyle2;
            this.合约.HeaderText = "合约";
            this.合约.Name = "合约";
            this.合约.ReadOnly = true;
            this.合约.Width = 60;
            // 
            // 手数
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "-";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.手数.DefaultCellStyle = dataGridViewCellStyle3;
            this.手数.HeaderText = "手数";
            this.手数.Name = "手数";
            this.手数.Width = 60;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxKeepInstrument);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.numericUpDownPriceTick);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.numericUpDownFlowPrice);
            this.groupBox4.Controls.Add(this.checkBoxFlowPrice);
            this.groupBox4.Controls.Add(this.checkBoxFastCloseAddTick);
            this.groupBox4.Location = new System.Drawing.Point(492, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(189, 238);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "下单";
            // 
            // checkBoxKeepInstrument
            // 
            this.checkBoxKeepInstrument.AutoSize = true;
            this.checkBoxKeepInstrument.Checked = global::CTP_交易.Properties.Settings.Default.KeepInstrument;
            this.checkBoxKeepInstrument.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeepInstrument.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CTP_交易.Properties.Settings.Default, "KeepInstrument", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxKeepInstrument.Location = new System.Drawing.Point(9, 152);
            this.checkBoxKeepInstrument.Name = "checkBoxKeepInstrument";
            this.checkBoxKeepInstrument.Size = new System.Drawing.Size(108, 16);
            this.checkBoxKeepInstrument.TabIndex = 6;
            this.checkBoxKeepInstrument.Text = "下单后保留合约";
            this.checkBoxKeepInstrument.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::CTP_交易.Properties.Settings.Default.KeepLots;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CTP_交易.Properties.Settings.Default, "KeepLots", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(9, 112);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "下单后保留手数";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(139, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "个波动";
            // 
            // numericUpDownPriceTick
            // 
            this.numericUpDownPriceTick.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CTP_交易.Properties.Settings.Default, "FastCloseTick", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownPriceTick.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDownPriceTick.Location = new System.Drawing.Point(105, 29);
            this.numericUpDownPriceTick.Name = "numericUpDownPriceTick";
            this.numericUpDownPriceTick.Size = new System.Drawing.Size(30, 23);
            this.numericUpDownPriceTick.TabIndex = 4;
            this.numericUpDownPriceTick.Value = global::CTP_交易.Properties.Settings.Default.FastCloseTick;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(139, 74);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(41, 12);
            this.label39.TabIndex = 5;
            this.label39.Text = "个波动";
            // 
            // numericUpDownFlowPrice
            // 
            this.numericUpDownFlowPrice.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CTP_交易.Properties.Settings.Default, "FlowPriceTick", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownFlowPrice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDownFlowPrice.Location = new System.Drawing.Point(105, 69);
            this.numericUpDownFlowPrice.Name = "numericUpDownFlowPrice";
            this.numericUpDownFlowPrice.Size = new System.Drawing.Size(30, 23);
            this.numericUpDownFlowPrice.TabIndex = 4;
            this.numericUpDownFlowPrice.Value = global::CTP_交易.Properties.Settings.Default.FlowPriceTick;
            // 
            // checkBoxFlowPrice
            // 
            this.checkBoxFlowPrice.AutoSize = true;
            this.checkBoxFlowPrice.Checked = global::CTP_交易.Properties.Settings.Default.FlowPriceAddTick;
            this.checkBoxFlowPrice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFlowPrice.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CTP_交易.Properties.Settings.Default, "FlowPriceAddTick", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxFlowPrice.Location = new System.Drawing.Point(9, 72);
            this.checkBoxFlowPrice.Name = "checkBoxFlowPrice";
            this.checkBoxFlowPrice.Size = new System.Drawing.Size(96, 16);
            this.checkBoxFlowPrice.TabIndex = 2;
            this.checkBoxFlowPrice.Text = "跟盘价下单加";
            this.checkBoxFlowPrice.UseVisualStyleBackColor = true;
            // 
            // checkBoxFastCloseAddTick
            // 
            this.checkBoxFastCloseAddTick.AutoSize = true;
            this.checkBoxFastCloseAddTick.Checked = global::CTP_交易.Properties.Settings.Default.FastCloseAddTick;
            this.checkBoxFastCloseAddTick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFastCloseAddTick.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CTP_交易.Properties.Settings.Default, "FastCloseAddTick", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxFastCloseAddTick.Location = new System.Drawing.Point(9, 32);
            this.checkBoxFastCloseAddTick.Name = "checkBoxFastCloseAddTick";
            this.checkBoxFastCloseAddTick.Size = new System.Drawing.Size(90, 16);
            this.checkBoxFastCloseAddTick.TabIndex = 2;
            this.checkBoxFastCloseAddTick.Text = "快速平仓,加";
            this.checkBoxFastCloseAddTick.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxPlaySound);
            this.groupBox3.Controls.Add(this.checkBoxShowTootip);
            this.groupBox3.Location = new System.Drawing.Point(3, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 55);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "界面";
            // 
            // checkBoxPlaySound
            // 
            this.checkBoxPlaySound.AutoSize = true;
            this.checkBoxPlaySound.Checked = global::CTP_交易.Properties.Settings.Default.PlaySound;
            this.checkBoxPlaySound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlaySound.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CTP_交易.Properties.Settings.Default, "PlaySound", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxPlaySound.Location = new System.Drawing.Point(168, 27);
            this.checkBoxPlaySound.Name = "checkBoxPlaySound";
            this.checkBoxPlaySound.Size = new System.Drawing.Size(72, 16);
            this.checkBoxPlaySound.TabIndex = 2;
            this.checkBoxPlaySound.Text = "播放声音";
            this.checkBoxPlaySound.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowTootip
            // 
            this.checkBoxShowTootip.AutoSize = true;
            this.checkBoxShowTootip.Checked = global::CTP_交易.Properties.Settings.Default.ShowTootip;
            this.checkBoxShowTootip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowTootip.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CTP_交易.Properties.Settings.Default, "ShowTootip", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxShowTootip.Location = new System.Drawing.Point(41, 27);
            this.checkBoxShowTootip.Name = "checkBoxShowTootip";
            this.checkBoxShowTootip.Size = new System.Drawing.Size(96, 16);
            this.checkBoxShowTootip.TabIndex = 2;
            this.checkBoxShowTootip.Text = "显示操作提示";
            this.checkBoxShowTootip.UseVisualStyleBackColor = true;
            this.checkBoxShowTootip.CheckedChanged += new System.EventHandler(this.checkBoxShowTootip_CheckedChanged);
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.Controls.Add(this.webBrowser1);
            this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHelp.Size = new System.Drawing.Size(688, 247);
            this.tabPageHelp.TabIndex = 9;
            this.tabPageHelp.Text = "帮  助";
            this.tabPageHelp.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(682, 241);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.richTextBox1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(688, 247);
            this.tabPageAbout.TabIndex = 10;
            this.tabPageAbout.Text = "关  于";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.AutoWordSelection = true;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(682, 241);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tabPageQryTrade
            // 
            this.tabPageQryTrade.Controls.Add(this.buttonSaveTrade);
            this.tabPageQryTrade.Controls.Add(this.label41);
            this.tabPageQryTrade.Controls.Add(this.label40);
            this.tabPageQryTrade.Controls.Add(this.buttonQryPosition);
            this.tabPageQryTrade.Controls.Add(this.dateTimePickerEnd);
            this.tabPageQryTrade.Controls.Add(this.dateTimePickerStart);
            this.tabPageQryTrade.Controls.Add(this.hfListViewTrade);
            this.tabPageQryTrade.Location = new System.Drawing.Point(4, 22);
            this.tabPageQryTrade.Name = "tabPageQryTrade";
            this.tabPageQryTrade.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQryTrade.Size = new System.Drawing.Size(688, 247);
            this.tabPageQryTrade.TabIndex = 12;
            this.tabPageQryTrade.Text = "历史成交";
            this.tabPageQryTrade.UseVisualStyleBackColor = true;
            // 
            // buttonSaveTrade
            // 
            this.buttonSaveTrade.Location = new System.Drawing.Point(530, 14);
            this.buttonSaveTrade.Name = "buttonSaveTrade";
            this.buttonSaveTrade.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveTrade.TabIndex = 6;
            this.buttonSaveTrade.Text = "保  存";
            this.buttonSaveTrade.UseVisualStyleBackColor = true;
            this.buttonSaveTrade.Click += new System.EventHandler(this.buttonSaveTrade_Click);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(248, 18);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(53, 12);
            this.label41.TabIndex = 4;
            this.label41.Text = "结束时间";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(85, 18);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(53, 12);
            this.label40.TabIndex = 4;
            this.label40.Text = "开始时间";
            // 
            // buttonQryPosition
            // 
            this.buttonQryPosition.Location = new System.Drawing.Point(433, 13);
            this.buttonQryPosition.Name = "buttonQryPosition";
            this.buttonQryPosition.Size = new System.Drawing.Size(75, 23);
            this.buttonQryPosition.TabIndex = 3;
            this.buttonQryPosition.Text = "查  询";
            this.buttonQryPosition.UseVisualStyleBackColor = true;
            this.buttonQryPosition.Click += new System.EventHandler(this.buttonQryPosition_Click);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "HH:mm:ss";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(307, 14);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.ShowUpDown = true;
            this.dateTimePickerEnd.Size = new System.Drawing.Size(89, 21);
            this.dateTimePickerEnd.TabIndex = 2;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "HH:mm:ss";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(141, 14);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.ShowUpDown = true;
            this.dateTimePickerStart.Size = new System.Drawing.Size(87, 21);
            this.dateTimePickerStart.TabIndex = 1;
            // 
            // hfListViewTrade
            // 
            this.hfListViewTrade.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader45,
            this.columnHeader53,
            this.columnHeader46,
            this.columnHeader47,
            this.columnHeader48,
            this.columnHeader49,
            this.columnHeader50,
            this.columnHeader51,
            this.columnHeader54});
            this.hfListViewTrade.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hfListViewTrade.Location = new System.Drawing.Point(3, 44);
            this.hfListViewTrade.Name = "hfListViewTrade";
            this.hfListViewTrade.Size = new System.Drawing.Size(682, 200);
            this.hfListViewTrade.SortColumn = 0;
            this.hfListViewTrade.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.hfListViewTrade.TabIndex = 0;
            this.hfListViewTrade.UseCompatibleStateImageBehavior = false;
            this.hfListViewTrade.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader45
            // 
            this.columnHeader45.Text = "合约";
            this.columnHeader45.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader45.Width = 65;
            // 
            // columnHeader53
            // 
            this.columnHeader53.Text = "成交时间";
            this.columnHeader53.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader53.Width = 71;
            // 
            // columnHeader46
            // 
            this.columnHeader46.Text = "交易所";
            this.columnHeader46.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader47
            // 
            this.columnHeader47.Text = "报单编号";
            this.columnHeader47.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader47.Width = 66;
            // 
            // columnHeader48
            // 
            this.columnHeader48.Text = "买卖";
            this.columnHeader48.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader49
            // 
            this.columnHeader49.Text = "开平";
            this.columnHeader49.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader50
            // 
            this.columnHeader50.Text = "价格";
            this.columnHeader50.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader50.Width = 77;
            // 
            // columnHeader51
            // 
            this.columnHeader51.Text = "数量";
            this.columnHeader51.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader54
            // 
            this.columnHeader54.Text = "经纪公司报单编号";
            this.columnHeader54.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader54.Width = 128;
            // 
            // tabPageQrySettleInfo
            // 
            this.tabPageQrySettleInfo.Controls.Add(this.label42);
            this.tabPageQrySettleInfo.Controls.Add(this.buttonSaveInfo);
            this.tabPageQrySettleInfo.Controls.Add(this.buttonQrySettleInfo);
            this.tabPageQrySettleInfo.Controls.Add(this.dateTimePickerQrySettleInfo);
            this.tabPageQrySettleInfo.Controls.Add(this.richTextBoxSettleInfo);
            this.tabPageQrySettleInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageQrySettleInfo.Name = "tabPageQrySettleInfo";
            this.tabPageQrySettleInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQrySettleInfo.Size = new System.Drawing.Size(688, 247);
            this.tabPageQrySettleInfo.TabIndex = 13;
            this.tabPageQrySettleInfo.Text = "结算单";
            this.tabPageQrySettleInfo.UseVisualStyleBackColor = true;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(181, 17);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(53, 12);
            this.label42.TabIndex = 5;
            this.label42.Text = "结算日期";
            // 
            // buttonSaveInfo
            // 
            this.buttonSaveInfo.Location = new System.Drawing.Point(458, 12);
            this.buttonSaveInfo.Name = "buttonSaveInfo";
            this.buttonSaveInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveInfo.TabIndex = 3;
            this.buttonSaveInfo.Text = "保  存";
            this.buttonSaveInfo.UseVisualStyleBackColor = true;
            this.buttonSaveInfo.Click += new System.EventHandler(this.buttonSaveInfo_Click);
            // 
            // buttonQrySettleInfo
            // 
            this.buttonQrySettleInfo.Location = new System.Drawing.Point(372, 12);
            this.buttonQrySettleInfo.Name = "buttonQrySettleInfo";
            this.buttonQrySettleInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonQrySettleInfo.TabIndex = 2;
            this.buttonQrySettleInfo.Text = "查  询";
            this.buttonQrySettleInfo.UseVisualStyleBackColor = true;
            this.buttonQrySettleInfo.Click += new System.EventHandler(this.buttonQrySettleInfo_Click);
            // 
            // dateTimePickerQrySettleInfo
            // 
            this.dateTimePickerQrySettleInfo.CustomFormat = "yyyy年MM月dd日";
            this.dateTimePickerQrySettleInfo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerQrySettleInfo.Location = new System.Drawing.Point(241, 13);
            this.dateTimePickerQrySettleInfo.Name = "dateTimePickerQrySettleInfo";
            this.dateTimePickerQrySettleInfo.Size = new System.Drawing.Size(120, 21);
            this.dateTimePickerQrySettleInfo.TabIndex = 1;
            // 
            // richTextBoxSettleInfo
            // 
            this.richTextBoxSettleInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBoxSettleInfo.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxSettleInfo.Location = new System.Drawing.Point(3, 44);
            this.richTextBoxSettleInfo.Name = "richTextBoxSettleInfo";
            this.richTextBoxSettleInfo.Size = new System.Drawing.Size(682, 200);
            this.richTextBoxSettleInfo.TabIndex = 0;
            this.richTextBoxSettleInfo.Text = "";
            this.richTextBoxSettleInfo.WordWrap = false;
            // 
            // splitContainerOrder
            // 
            this.splitContainerOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOrder.Location = new System.Drawing.Point(0, 0);
            this.splitContainerOrder.Name = "splitContainerOrder";
            // 
            // splitContainerOrder.Panel1
            // 
            this.splitContainerOrder.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainerOrder.Panel2
            // 
            this.splitContainerOrder.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainerOrder.Panel2.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainerOrder.Size = new System.Drawing.Size(382, 275);
            this.splitContainerOrder.SplitterDistance = 186;
            this.splitContainerOrder.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelAskPrice, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelAskVolume, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label16, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label20, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelBidPrice, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label18, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelBidVolume, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelInstrumentName, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelLastPrice, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label21, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label22, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label25, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label26, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label27, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelUpperLimit, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label24, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelLowerLimit, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.label28, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label29, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label30, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label31, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.label32, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label33, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label36, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.label37, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.labelWeiBi, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelWeiCha, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelSettlementPrice, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelPreSettlementPrice, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelUpDown, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelTotalVolume, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelOpenPrice, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelHighest, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelLowest, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.labelVolume, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelOpenInstetest, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.labelPreOI, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.labelOIDiff, 3, 10);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(184, 256);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "卖价";
            // 
            // labelAskPrice
            // 
            this.labelAskPrice.AutoEllipsis = true;
            this.labelAskPrice.AutoSize = true;
            this.labelAskPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAskPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelAskPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAskPrice.Location = new System.Drawing.Point(35, 23);
            this.labelAskPrice.Margin = new System.Windows.Forms.Padding(0);
            this.labelAskPrice.Name = "labelAskPrice";
            this.labelAskPrice.Size = new System.Drawing.Size(57, 23);
            this.labelAskPrice.TabIndex = 2;
            this.labelAskPrice.Text = "-";
            this.labelAskPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAskVolume
            // 
            this.labelAskVolume.AutoEllipsis = true;
            this.labelAskVolume.AutoSize = true;
            this.labelAskVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAskVolume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelAskVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAskVolume.Location = new System.Drawing.Point(130, 23);
            this.labelAskVolume.Name = "labelAskVolume";
            this.labelAskVolume.Size = new System.Drawing.Size(51, 23);
            this.labelAskVolume.TabIndex = 1;
            this.labelAskVolume.Text = "-";
            this.labelAskVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(95, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 3;
            this.label16.Text = "卖量";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 6;
            this.label20.Text = "买价";
            // 
            // labelBidPrice
            // 
            this.labelBidPrice.AutoEllipsis = true;
            this.labelBidPrice.AutoSize = true;
            this.labelBidPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBidPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelBidPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBidPrice.Location = new System.Drawing.Point(38, 46);
            this.labelBidPrice.Name = "labelBidPrice";
            this.labelBidPrice.Size = new System.Drawing.Size(51, 23);
            this.labelBidPrice.TabIndex = 0;
            this.labelBidPrice.Text = "-";
            this.labelBidPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(95, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 6;
            this.label18.Text = "买量";
            // 
            // labelBidVolume
            // 
            this.labelBidVolume.AutoEllipsis = true;
            this.labelBidVolume.AutoSize = true;
            this.labelBidVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBidVolume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelBidVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBidVolume.Location = new System.Drawing.Point(130, 46);
            this.labelBidVolume.Name = "labelBidVolume";
            this.labelBidVolume.Size = new System.Drawing.Size(51, 23);
            this.labelBidVolume.TabIndex = 0;
            this.labelBidVolume.Text = "-";
            this.labelBidVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInstrumentName
            // 
            this.labelInstrumentName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelInstrumentName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelInstrumentName, 2);
            this.labelInstrumentName.Location = new System.Drawing.Point(129, 5);
            this.labelInstrumentName.Name = "labelInstrumentName";
            this.labelInstrumentName.Size = new System.Drawing.Size(17, 12);
            this.labelInstrumentName.TabIndex = 3;
            this.labelInstrumentName.Text = "--";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "最新";
            // 
            // labelLastPrice
            // 
            this.labelLastPrice.AutoEllipsis = true;
            this.labelLastPrice.AutoSize = true;
            this.labelLastPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLastPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelLastPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastPrice.Location = new System.Drawing.Point(38, 92);
            this.labelLastPrice.Name = "labelLastPrice";
            this.labelLastPrice.Size = new System.Drawing.Size(51, 23);
            this.labelLastPrice.TabIndex = 5;
            this.labelLastPrice.Text = "-";
            this.labelLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 74);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 0;
            this.label21.Text = "委比";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(95, 74);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 0;
            this.label22.Text = "委差";
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(95, 97);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(29, 12);
            this.label25.TabIndex = 0;
            this.label25.Text = "均价";
            // 
            // label26
            // 
            this.label26.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 120);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 12);
            this.label26.TabIndex = 0;
            this.label26.Text = "涨跌";
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(95, 120);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(29, 12);
            this.label27.TabIndex = 0;
            this.label27.Text = "昨结";
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 166);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 6;
            this.label23.Text = "涨板";
            // 
            // labelUpperLimit
            // 
            this.labelUpperLimit.AutoEllipsis = true;
            this.labelUpperLimit.AutoSize = true;
            this.labelUpperLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUpperLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelUpperLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpperLimit.ForeColor = System.Drawing.Color.Red;
            this.labelUpperLimit.Location = new System.Drawing.Point(38, 161);
            this.labelUpperLimit.Name = "labelUpperLimit";
            this.labelUpperLimit.Size = new System.Drawing.Size(51, 23);
            this.labelUpperLimit.TabIndex = 5;
            this.labelUpperLimit.Text = "-";
            this.labelUpperLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(95, 166);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 12);
            this.label24.TabIndex = 6;
            this.label24.Text = "跌板";
            // 
            // labelLowerLimit
            // 
            this.labelLowerLimit.AutoEllipsis = true;
            this.labelLowerLimit.AutoSize = true;
            this.labelLowerLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLowerLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelLowerLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLowerLimit.ForeColor = System.Drawing.Color.Green;
            this.labelLowerLimit.Location = new System.Drawing.Point(130, 161);
            this.labelLowerLimit.Name = "labelLowerLimit";
            this.labelLowerLimit.Size = new System.Drawing.Size(51, 23);
            this.labelLowerLimit.TabIndex = 5;
            this.labelLowerLimit.Text = "-";
            this.labelLowerLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(3, 143);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(29, 12);
            this.label28.TabIndex = 0;
            this.label28.Text = "总手";
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(95, 143);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 12);
            this.label29.TabIndex = 0;
            this.label29.Text = "开盘";
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(3, 189);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(29, 12);
            this.label30.TabIndex = 6;
            this.label30.Text = "现手";
            // 
            // label31
            // 
            this.label31.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(95, 189);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(29, 12);
            this.label31.TabIndex = 6;
            this.label31.Text = "最高";
            // 
            // label32
            // 
            this.label32.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(3, 212);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(29, 12);
            this.label32.TabIndex = 6;
            this.label32.Text = "持仓";
            // 
            // label33
            // 
            this.label33.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 237);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(29, 12);
            this.label33.TabIndex = 6;
            this.label33.Text = "昨仓";
            // 
            // label36
            // 
            this.label36.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(95, 212);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(29, 12);
            this.label36.TabIndex = 6;
            this.label36.Text = "最低";
            // 
            // label37
            // 
            this.label37.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(95, 237);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(29, 12);
            this.label37.TabIndex = 6;
            this.label37.Text = "仓差";
            // 
            // labelWeiBi
            // 
            this.labelWeiBi.AutoEllipsis = true;
            this.labelWeiBi.AutoSize = true;
            this.labelWeiBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeiBi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelWeiBi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWeiBi.Location = new System.Drawing.Point(38, 69);
            this.labelWeiBi.Name = "labelWeiBi";
            this.labelWeiBi.Size = new System.Drawing.Size(51, 23);
            this.labelWeiBi.TabIndex = 0;
            this.labelWeiBi.Text = "-";
            this.labelWeiBi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWeiCha
            // 
            this.labelWeiCha.AutoEllipsis = true;
            this.labelWeiCha.AutoSize = true;
            this.labelWeiCha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeiCha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelWeiCha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWeiCha.Location = new System.Drawing.Point(130, 69);
            this.labelWeiCha.Name = "labelWeiCha";
            this.labelWeiCha.Size = new System.Drawing.Size(51, 23);
            this.labelWeiCha.TabIndex = 0;
            this.labelWeiCha.Text = "-";
            this.labelWeiCha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSettlementPrice
            // 
            this.labelSettlementPrice.AutoEllipsis = true;
            this.labelSettlementPrice.AutoSize = true;
            this.labelSettlementPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSettlementPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelSettlementPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettlementPrice.Location = new System.Drawing.Point(130, 92);
            this.labelSettlementPrice.Name = "labelSettlementPrice";
            this.labelSettlementPrice.Size = new System.Drawing.Size(51, 23);
            this.labelSettlementPrice.TabIndex = 0;
            this.labelSettlementPrice.Text = "-";
            this.labelSettlementPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPreSettlementPrice
            // 
            this.labelPreSettlementPrice.AutoEllipsis = true;
            this.labelPreSettlementPrice.AutoSize = true;
            this.labelPreSettlementPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPreSettlementPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelPreSettlementPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreSettlementPrice.Location = new System.Drawing.Point(130, 115);
            this.labelPreSettlementPrice.Name = "labelPreSettlementPrice";
            this.labelPreSettlementPrice.Size = new System.Drawing.Size(51, 23);
            this.labelPreSettlementPrice.TabIndex = 0;
            this.labelPreSettlementPrice.Text = "-";
            this.labelPreSettlementPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUpDown
            // 
            this.labelUpDown.AutoEllipsis = true;
            this.labelUpDown.AutoSize = true;
            this.labelUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUpDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpDown.Location = new System.Drawing.Point(38, 115);
            this.labelUpDown.Name = "labelUpDown";
            this.labelUpDown.Size = new System.Drawing.Size(51, 23);
            this.labelUpDown.TabIndex = 5;
            this.labelUpDown.Text = "-";
            this.labelUpDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTotalVolume
            // 
            this.labelTotalVolume.AutoEllipsis = true;
            this.labelTotalVolume.AutoSize = true;
            this.labelTotalVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTotalVolume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTotalVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalVolume.Location = new System.Drawing.Point(38, 138);
            this.labelTotalVolume.Name = "labelTotalVolume";
            this.labelTotalVolume.Size = new System.Drawing.Size(51, 23);
            this.labelTotalVolume.TabIndex = 5;
            this.labelTotalVolume.Text = "-";
            this.labelTotalVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOpenPrice
            // 
            this.labelOpenPrice.AutoEllipsis = true;
            this.labelOpenPrice.AutoSize = true;
            this.labelOpenPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOpenPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelOpenPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOpenPrice.Location = new System.Drawing.Point(130, 138);
            this.labelOpenPrice.Name = "labelOpenPrice";
            this.labelOpenPrice.Size = new System.Drawing.Size(51, 23);
            this.labelOpenPrice.TabIndex = 0;
            this.labelOpenPrice.Text = "-";
            this.labelOpenPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHighest
            // 
            this.labelHighest.AutoEllipsis = true;
            this.labelHighest.AutoSize = true;
            this.labelHighest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelHighest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHighest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHighest.Location = new System.Drawing.Point(130, 184);
            this.labelHighest.Name = "labelHighest";
            this.labelHighest.Size = new System.Drawing.Size(51, 23);
            this.labelHighest.TabIndex = 5;
            this.labelHighest.Text = "-";
            this.labelHighest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLowest
            // 
            this.labelLowest.AutoEllipsis = true;
            this.labelLowest.AutoSize = true;
            this.labelLowest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLowest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelLowest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLowest.Location = new System.Drawing.Point(130, 207);
            this.labelLowest.Name = "labelLowest";
            this.labelLowest.Size = new System.Drawing.Size(51, 23);
            this.labelLowest.TabIndex = 5;
            this.labelLowest.Text = "-";
            this.labelLowest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVolume
            // 
            this.labelVolume.AutoEllipsis = true;
            this.labelVolume.AutoSize = true;
            this.labelVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVolume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVolume.Location = new System.Drawing.Point(38, 184);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(51, 23);
            this.labelVolume.TabIndex = 5;
            this.labelVolume.Text = "-";
            this.labelVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOpenInstetest
            // 
            this.labelOpenInstetest.AutoEllipsis = true;
            this.labelOpenInstetest.AutoSize = true;
            this.labelOpenInstetest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOpenInstetest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelOpenInstetest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOpenInstetest.Location = new System.Drawing.Point(38, 207);
            this.labelOpenInstetest.Name = "labelOpenInstetest";
            this.labelOpenInstetest.Size = new System.Drawing.Size(51, 23);
            this.labelOpenInstetest.TabIndex = 5;
            this.labelOpenInstetest.Text = "-";
            this.labelOpenInstetest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPreOI
            // 
            this.labelPreOI.AutoEllipsis = true;
            this.labelPreOI.AutoSize = true;
            this.labelPreOI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPreOI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelPreOI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreOI.Location = new System.Drawing.Point(38, 230);
            this.labelPreOI.Name = "labelPreOI";
            this.labelPreOI.Size = new System.Drawing.Size(51, 26);
            this.labelPreOI.TabIndex = 5;
            this.labelPreOI.Text = "-";
            this.labelPreOI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOIDiff
            // 
            this.labelOIDiff.AutoEllipsis = true;
            this.labelOIDiff.AutoSize = true;
            this.labelOIDiff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOIDiff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelOIDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOIDiff.Location = new System.Drawing.Point(130, 230);
            this.labelOIDiff.Name = "labelOIDiff";
            this.labelOIDiff.Size = new System.Drawing.Size(51, 26);
            this.labelOIDiff.TabIndex = 5;
            this.labelOIDiff.Text = "-";
            this.labelOIDiff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label19.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label19, 2);
            this.label19.Location = new System.Drawing.Point(3, 5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 3;
            this.label19.Text = "盘口明细";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.comboBoxInstrument);
            this.splitContainer3.Panel1.Controls.Add(this.numericUpDownVolume);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.numericUpDownPrice);
            this.splitContainer3.Panel1.Controls.Add(this.buttonPrice);
            this.splitContainer3.Panel1.Controls.Add(this.comboBoxOffset);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.labelUpper);
            this.splitContainer3.Panel1.Controls.Add(this.labelLower);
            this.splitContainer3.Panel1.Controls.Add(this.label5);
            this.splitContainer3.Panel1.Controls.Add(this.labelVolumeMax);
            this.splitContainer3.Panel1.Controls.Add(this.label15);
            this.splitContainer3.Panel1.Controls.Add(this.comboBoxDirector);
            this.splitContainer3.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.button1);
            this.splitContainer3.Panel2.Controls.Add(this.buttonOrder);
            this.splitContainer3.Panel2.Controls.Add(this.buttonMarketPrice);
            this.splitContainer3.Panel2.Controls.Add(this.buttonCancel);
            this.splitContainer3.Panel2.Controls.Add(this.btnParked);
            this.splitContainer3.Size = new System.Drawing.Size(192, 275);
            this.splitContainer3.SplitterDistance = 167;
            this.splitContainer3.TabIndex = 0;
            // 
            // comboBoxInstrument
            // 
            this.comboBoxInstrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxInstrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBoxInstrument.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxInstrument.FormattingEnabled = true;
            this.comboBoxInstrument.Location = new System.Drawing.Point(49, 18);
            this.comboBoxInstrument.Name = "comboBoxInstrument";
            this.comboBoxInstrument.Size = new System.Drawing.Size(117, 20);
            this.comboBoxInstrument.TabIndex = 10;
            this.comboBoxInstrument.SelectedIndexChanged += new System.EventHandler(this.comboBoxInstrument_SelectedIndexChanged);
            // 
            // numericUpDownVolume
            // 
            this.numericUpDownVolume.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDownVolume.Location = new System.Drawing.Point(66, 135);
            this.numericUpDownVolume.Name = "numericUpDownVolume";
            this.numericUpDownVolume.Size = new System.Drawing.Size(77, 26);
            this.numericUpDownVolume.TabIndex = 20;
            this.numericUpDownVolume.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "合约";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownPrice
            // 
            this.numericUpDownPrice.Enabled = false;
            this.numericUpDownPrice.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDownPrice.Location = new System.Drawing.Point(66, 102);
            this.numericUpDownPrice.Name = "numericUpDownPrice";
            this.numericUpDownPrice.Size = new System.Drawing.Size(77, 26);
            this.numericUpDownPrice.TabIndex = 19;
            this.numericUpDownPrice.ValueChanged += new System.EventHandler(this.numericUpDownPrice_ValueChanged);
            // 
            // buttonPrice
            // 
            this.buttonPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrice.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonPrice.Location = new System.Drawing.Point(6, 105);
            this.buttonPrice.Name = "buttonPrice";
            this.buttonPrice.Size = new System.Drawing.Size(54, 23);
            this.buttonPrice.TabIndex = 18;
            this.buttonPrice.Text = "跟盘价";
            this.buttonPrice.UseVisualStyleBackColor = false;
            this.buttonPrice.Click += new System.EventHandler(this.buttonPrice_Click);
            // 
            // comboBoxOffset
            // 
            this.comboBoxOffset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOffset.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxOffset.FormattingEnabled = true;
            this.comboBoxOffset.Items.AddRange(new object[] {
            "开仓",
            "平仓",
            "平今"});
            this.comboBoxOffset.Location = new System.Drawing.Point(49, 74);
            this.comboBoxOffset.Name = "comboBoxOffset";
            this.comboBoxOffset.Size = new System.Drawing.Size(117, 20);
            this.comboBoxOffset.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(-6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "买卖";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelUpper
            // 
            this.labelUpper.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelUpper.AutoSize = true;
            this.labelUpper.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpper.ForeColor = System.Drawing.Color.Red;
            this.labelUpper.Location = new System.Drawing.Point(145, 102);
            this.labelUpper.Name = "labelUpper";
            this.labelUpper.Size = new System.Drawing.Size(11, 14);
            this.labelUpper.TabIndex = 5;
            this.labelUpper.Text = "-";
            // 
            // labelLower
            // 
            this.labelLower.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLower.AutoSize = true;
            this.labelLower.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLower.ForeColor = System.Drawing.Color.Green;
            this.labelLower.Location = new System.Drawing.Point(145, 116);
            this.labelLower.Name = "labelLower";
            this.labelLower.Size = new System.Drawing.Size(11, 14);
            this.labelLower.TabIndex = 5;
            this.labelLower.Text = "-";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(8, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "手 数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelVolumeMax
            // 
            this.labelVolumeMax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVolumeMax.AutoSize = true;
            this.labelVolumeMax.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelVolumeMax.Location = new System.Drawing.Point(156, 140);
            this.labelVolumeMax.Name = "labelVolumeMax";
            this.labelVolumeMax.Size = new System.Drawing.Size(14, 14);
            this.labelVolumeMax.TabIndex = 0;
            this.labelVolumeMax.Text = "-";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(143, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "≤";
            // 
            // comboBoxDirector
            // 
            this.comboBoxDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDirector.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxDirector.FormattingEnabled = true;
            this.comboBoxDirector.Items.AddRange(new object[] {
            "买入",
            "卖出"});
            this.comboBoxDirector.Location = new System.Drawing.Point(49, 46);
            this.comboBoxDirector.Name = "comboBoxDirector";
            this.comboBoxDirector.Size = new System.Drawing.Size(117, 20);
            this.comboBoxDirector.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(-6, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "开平";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(81, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonOrder
            // 
            this.buttonOrder.Location = new System.Drawing.Point(6, 21);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(54, 58);
            this.buttonOrder.TabIndex = 0;
            this.buttonOrder.Text = "下 单";
            this.buttonOrder.UseVisualStyleBackColor = true;
            this.buttonOrder.Click += new System.EventHandler(this.buttonOrder_Click);
            // 
            // buttonMarketPrice
            // 
            this.buttonMarketPrice.Location = new System.Drawing.Point(131, 72);
            this.buttonMarketPrice.Name = "buttonMarketPrice";
            this.buttonMarketPrice.Size = new System.Drawing.Size(50, 23);
            this.buttonMarketPrice.TabIndex = 3;
            this.buttonMarketPrice.Text = "市价";
            this.buttonMarketPrice.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(73, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(106, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // btnParked
            // 
            this.btnParked.Location = new System.Drawing.Point(73, 72);
            this.btnParked.Name = "btnParked";
            this.btnParked.Size = new System.Drawing.Size(52, 23);
            this.btnParked.TabIndex = 2;
            this.btnParked.Text = "预埋";
            this.btnParked.UseVisualStyleBackColor = true;
            this.btnParked.Click += new System.EventHandler(this.btnParked_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1084, 562);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::CTP_交易.Properties.Settings.Default, "FormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::CTP_交易.Properties.Settings.Default.FormLocation;
            this.Name = "FormTrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "交易终端";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTrade_FormClosed);
            this.Load += new System.EventHandler(this.FormTrade_Load);
            this.LocationChanged += new System.EventHandler(this.FormTrade_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.FormTrade_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSHFE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSHFE)).EndInit();
            this.tabPageCZCE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCZCE)).EndInit();
            this.tabPageDCE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDCE)).EndInit();
            this.tabPageCFFEX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCFFEX)).EndInit();
            this.tabPageSelected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelected)).EndInit();
            this.tabPageArbitrage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArbitrage)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControSystem.ResumeLayout(false);
            this.tabPageOrder.ResumeLayout(false);
            this.tabPagePosition.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabPageParked.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.tabPageAccount.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.tabPageInstrument.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstruments)).EndInit();
            this.tabPageInstrumentSelf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstrumentsSelected)).EndInit();
            this.tabPageBankFuture.ResumeLayout(false);
            this.tabPageBankFuture.PerformLayout();
            this.tabPageSystem.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageSetting.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomVolume)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriceTick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlowPrice)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPageHelp.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageQryTrade.ResumeLayout(false);
            this.tabPageQryTrade.PerformLayout();
            this.tabPageQrySettleInfo.ResumeLayout(false);
            this.tabPageQrySettleInfo.PerformLayout();
            this.splitContainerOrder.Panel1.ResumeLayout(false);
            this.splitContainerOrder.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOrder)).EndInit();
            this.splitContainerOrder.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControSystem;
		private System.Windows.Forms.TabPage tabPageOrder;
		private System.Windows.Forms.TabPage tabPagePosition;
		private System.Windows.Forms.TabPage tabPageParked;
		private System.Windows.Forms.TabPage tabPageAccount;
		private System.Windows.Forms.TabPage tabPageInstrument;
		private System.Windows.Forms.SplitContainer splitContainerOrder;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.ComboBox comboBoxInstrument;
		private System.Windows.Forms.NumericUpDown numericUpDownVolume;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownPrice;
		private System.Windows.Forms.Button buttonPrice;
		/// <summary>
		/// 开平
		/// </summary>
		private System.Windows.Forms.ComboBox comboBoxOffset;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		/// <summary>
		/// 买卖
		/// </summary>
		private System.Windows.Forms.ComboBox comboBoxDirector;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonOrder;
		private System.Windows.Forms.Button buttonMarketPrice;
		private System.Windows.Forms.Button btnParked;
		private System.Windows.Forms.Button buttonCancel;
		private HFListView listViewOrder;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button buttonQuickLock;
		private System.Windows.Forms.Button buttonCovert;
		private System.Windows.Forms.Button buttonQuickClose;
		private System.Windows.Forms.SplitContainer splitContainer7;
		private System.Windows.Forms.Button buttonQryAccount;
		private System.Windows.Forms.ComboBox comboBoxErrMsg;
		private HFListView listViewPosition;
		private System.Windows.Forms.TabPage tabPageInstrumentSelf;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TabPage tabPageSystem;
		private System.Windows.Forms.Button buttonChangePwd;
		private System.Windows.Forms.TextBox textBoxNewPassWord;
		private System.Windows.Forms.TextBox textBoxOldPassword;
		private System.Windows.Forms.RadioButton radioButtonAccount;
		private System.Windows.Forms.RadioButton radioButtonUser;
		private System.Windows.Forms.TabPage tabPageBankFuture;
		private HFListView listViewSeries;
		private HFListView listViewBank;
		private System.Windows.Forms.Button buttonTransfer;
		private System.Windows.Forms.Button buttonQryTransferSeries;
		private System.Windows.Forms.ComboBox comboBoxTransferType;
		private System.Windows.Forms.TextBox textBoxBankName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxTradeAmount;
		private System.Windows.Forms.TextBox textBoxBankPwd;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxAccountPwd;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBoxFutureFetchAmount;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxBankID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private UserControlTradeAccount userControlTradeAccount1;
		private HFListView hfListViewParkedOrder;
		private System.Windows.Forms.SplitContainer splitContainer5;
		private HFListView hfListViewParkedAction;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private System.Windows.Forms.ColumnHeader columnHeader31;
		private System.Windows.Forms.ColumnHeader columnHeader32;
		private System.Windows.Forms.ColumnHeader columnHeader33;
		private System.Windows.Forms.ColumnHeader columnHeader35;
		private System.Windows.Forms.ColumnHeader columnHeader36;
		private System.Windows.Forms.ColumnHeader columnHeader37;
		private System.Windows.Forms.ColumnHeader columnHeader38;
		private System.Windows.Forms.ColumnHeader columnHeader39;
		private System.Windows.Forms.ColumnHeader columnHeader40;
		private System.Windows.Forms.ColumnHeader columnHeader41;
		private System.Windows.Forms.ColumnHeader columnHeader42;
		private System.Windows.Forms.ColumnHeader columnHeader43;
		private System.Windows.Forms.ColumnHeader columnHeader34;
		private System.Windows.Forms.ColumnHeader columnHeader44;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label labelBidPrice;
		private System.Windows.Forms.Label labelBidVolume;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label labelAskPrice;
		private System.Windows.Forms.Label labelAskVolume;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label labelLastPrice;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label labelUpperLimit;
		private System.Windows.Forms.Label labelLowerLimit;
		private System.Windows.Forms.Label labelInstrumentName;
		private System.Windows.Forms.Label labelUpper;
		private System.Windows.Forms.Label labelLower;
		private System.Windows.Forms.Label labelVolumeMax;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button buttonShowLog;
		private HFListView hfListViewLog;
		private System.Windows.Forms.ColumnHeader columnHeader67;
		private System.Windows.Forms.ColumnHeader columnHeader68;
		private System.Windows.Forms.Button buttonClearLog;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label labelWeiBi;
		private System.Windows.Forms.Label labelWeiCha;
		private System.Windows.Forms.Label labelSettlementPrice;
		private System.Windows.Forms.Label labelPreSettlementPrice;
		private System.Windows.Forms.Label labelUpDown;
		private System.Windows.Forms.Label labelTotalVolume;
		private System.Windows.Forms.Label labelOpenPrice;
		private System.Windows.Forms.Label labelHighest;
		private System.Windows.Forms.Label labelLowest;
		private System.Windows.Forms.Label labelVolume;
		private System.Windows.Forms.Label labelOpenInstetest;
		private System.Windows.Forms.Label labelPreOI;
		private System.Windows.Forms.Label labelOIDiff;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelSHFE;
		private System.Windows.Forms.Label labelCZCE;
		private System.Windows.Forms.Label labelDCE;
		private System.Windows.Forms.Label labelFFEX;
		private System.Windows.Forms.RadioButton radioButtonTrade;
		private System.Windows.Forms.RadioButton radioButtonMd;
		private System.Windows.Forms.TabPage tabPageHelp;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.TabPage tabPageAbout;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TextBox textBoxNewPwdConfirm;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.TabPage tabPageSetting;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.NumericUpDown numericUpDownFlowPrice;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown numericUpDownPriceTick;
		private System.Windows.Forms.Button buttonResetSetting;
		private System.Windows.Forms.CheckBox checkBoxFlowPrice;
		private System.Windows.Forms.CheckBox checkBoxPlaySound;
		private System.Windows.Forms.CheckBox checkBoxFastCloseAddTick;
		private System.Windows.Forms.CheckBox checkBoxShowTootip;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBoxKeepInstrument;
		private DoubleBufferDGV dataGridViewInstruments;
        private DoubleBufferDGV dataGridViewInstrumentsSelected;
		private DoubleBufferDGV dataGridViewCustomVolume;
		private System.Windows.Forms.DataGridViewTextBoxColumn 合约;
		private System.Windows.Forms.DataGridViewTextBoxColumn 手数;
		private System.Windows.Forms.TabPage tabPageQryTrade;
		private System.Windows.Forms.TabPage tabPageQrySettleInfo;
		private HFListView hfListViewTrade;
		private System.Windows.Forms.ColumnHeader columnHeader45;
		private System.Windows.Forms.ColumnHeader columnHeader46;
		private System.Windows.Forms.ColumnHeader columnHeader47;
		private System.Windows.Forms.ColumnHeader columnHeader48;
		private System.Windows.Forms.ColumnHeader columnHeader49;
		private System.Windows.Forms.ColumnHeader columnHeader50;
		private System.Windows.Forms.ColumnHeader columnHeader51;
		private System.Windows.Forms.Button buttonQryPosition;
		private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
		private System.Windows.Forms.DateTimePicker dateTimePickerStart;
		private System.Windows.Forms.ColumnHeader columnHeader53;
		private System.Windows.Forms.ColumnHeader columnHeader54;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Button buttonQrySettleInfo;
		private System.Windows.Forms.DateTimePicker dateTimePickerQrySettleInfo;
		private System.Windows.Forms.RichTextBox richTextBoxSettleInfo;
		private System.Windows.Forms.Button buttonSaveTrade;
		private System.Windows.Forms.Button buttonSaveInfo;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.ToolTip toolTipInfo;
		private System.Windows.Forms.Button buttonSaveLog;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.CheckBox checkBoxClearSelected;
		private System.Windows.Forms.CheckBox checkBoxClearCosumVolume;
		private System.Windows.Forms.CheckBox checkBoxClearServers;
		private System.Windows.Forms.CheckBox checkBoxResetAll;
		private System.Windows.Forms.CheckBox checkBoxClearLog;
        private System.Windows.Forms.Button buttonExportRate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSHFE;
        private DoubleBufferDGV dataGridViewSHFE;
        private System.Windows.Forms.TabPage tabPageCZCE;
        private DoubleBufferDGV dataGridViewCZCE;
        private System.Windows.Forms.TabPage tabPageDCE;
        private DoubleBufferDGV dataGridViewDCE;
        private System.Windows.Forms.TabPage tabPageCFFEX;
        private DoubleBufferDGV dataGridViewCFFEX;
        private System.Windows.Forms.TabPage tabPageSelected;
        private DoubleBufferDGV dataGridViewSelected;
        private System.Windows.Forms.TabPage tabPageArbitrage;
        private DoubleBufferDGV dataGridViewArbitrage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
       
	}
}

