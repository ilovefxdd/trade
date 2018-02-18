
// CTP_mfcDlg.cpp : 实现文件
//

#include "MdSpi.h"
#include "TdSpi.h"
#include <stdio.h>
#include <direct.h>
#include "ThostFtdcMdApi.h"
#include "ThostFtdcTraderApi.h"
#include <iostream>
#include <sstream>
#include "stdafx.h"
#include "CTP_mfc.h"
#include "CTP_mfcDlg.h"
#include "afxdialogex.h"
#pragma comment(lib,"thostmduserapi.lib")
#pragma comment(lib,"thosttraderapi.lib")
#ifdef _DEBUG
#define new DEBUG_NEW
#endif
using namespace std;

// 用于应用程序“关于”菜单项的 CAboutDlg 对话框

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

	// 对话框数据
	enum { IDD = IDD_ABOUTBOX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	// 实现
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CCTP_mfcDlg 对话框



CCTP_mfcDlg::CCTP_mfcDlg(CWnd* pParent /*=NULL*/)
: CDialogEx(CCTP_mfcDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}
MdSpi *CCTP_mfcDlg::msp = NULL;
void CCTP_mfcDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT1, mask);
	DDX_Control(pDX, IDC_EDIT2, l_cou);
	DDX_Control(pDX, IDC_EDIT3, edit_bl);
	DDX_Control(pDX, IDOK, cbu);
	DDX_Control(pDX, IDC_COMBO1, run_msg);
}

BEGIN_MESSAGE_MAP(CCTP_mfcDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	//	ON_WM_CLOSE()
	ON_WM_QUERYDRAGICON()
	ON_MESSAGE(WM_MY_MESSAGE, MyMessage)
	ON_MESSAGE(WM_MY_count, Mycount)
	ON_MESSAGE(WM_MY_bl, blcount)
	ON_WM_TIMER()
	ON_BN_CLICKED(IDOK, &CCTP_mfcDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDC_BUTTON1, &CCTP_mfcDlg::OnBnClickedButton1)
END_MESSAGE_MAP()


int CaculateWeekDay(int y, int m, int d)
{
	int week = 0;
	if (m == 1){ m = 13; y--; }
	if (m == 2) { m = 14; y--; }
	week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7; //1752年9月3日之后的公式
	int weekstr = 0;
	switch (week)
	{
	case 0: {weekstr = 1; break; }
	case 1: {weekstr = 2; break; }
	case 2: {weekstr = 3; break; }
	case 3: {weekstr = 4; break; }
	case 4: {weekstr = 5; break; }
	case 5: {weekstr = 6; break; }
	case 6: {weekstr = 7; break; }
	}
	return weekstr;
}
// CCTP_mfcDlg 消息处理程序
void CCTP_mfcDlg::OnTimer(UINT_PTR nIDEvent)
{
	SYSTEMTIME st;
	CString strDate, strTime;
	GetLocalTime(&st);
	int d = CaculateWeekDay(st.wYear, st.wMonth, st.wDay);
	int s = st.wHour * 60 + st.wMinute;
	bool jy = false;

	if ((s > 166 && s<520) || (s>920 && s < 1150) || (s>690 && s<800)) jy = false; else jy = true;
	if (!jy || d>5)
	{
		if (s == 692 || s == 922 || s == 168)
		{
			if (msp != NULL && !wt)
			{
				for (int i = 0; i < 20; i++)
				{
					msp->pi[i]->ts->write(msp->pi[i]->tik, msp->pi[i]->tik_f);
				}
				wt = true;
			}
		}
		if ((s > 170 && s<520) || (s>924 && s < 1250) || (s>694 && s < 800))
			AfxGetMainWnd()->SendMessage(WM_CLOSE);
		/*	if (thread_stat)
			{
			//SuspendThread(hThread);

			TerminateThread(hThread, 0);
			thread_stat = false;
			hThread = NULL;
			}*/
	}
	else
	{
		//quotethread();
		//	order();
		if (mThread == NULL)
		{
			mThread = CreateThread(NULL, 0, quotethread, this, 0, NULL);
			//	tThread = CreateThread(NULL, 0,order, this, 0, NULL);
			thread_stat = true;
			wt = false;
		}
	}

	CDialogEx::OnTimer(nIDEvent);
}
BOOL CCTP_mfcDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。  当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO:  在此添加额外的初始化代码
	SetTimer(1, 10000, NULL);
	cbu.EnableWindow(FALSE);
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CCTP_mfcDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。  对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CCTP_mfcDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文
		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);
		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;
		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CCTP_mfcDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CCTP_mfcDlg::OnBnClickedOk()
{

}
MdSpi *CCTP_mfcDlg::md_op = NULL;
CThostFtdcTraderApi *CCTP_mfcDlg::tdapi = NULL;
DWORD WINAPI  CCTP_mfcDlg::quotethread(LPVOID lParam)
{
	HWND hw = AfxGetApp()->GetMainWnd()->GetSafeHwnd();
	CThostFtdcMdApi *mdapi = CThostFtdcMdApi::CreateFtdcMdApi();
	MdSpi *mdspi = new MdSpi(mdapi);
	md_op = mdspi;
	msp = mdspi;
	mdspi->mainh = hw;
	//注册事件处理对象
	mdapi->RegisterSpi(mdspi);
	////和前置机连接
	mdapi->RegisterFront("tcp://116.236.239.136:41213");
	mdapi->Init();
	mdapi->Join();
	return 1;
}
//	mdapi->RegisterFront("tcp://180.168.146.187:10010");

DWORD WINAPI  CCTP_mfcDlg::order(LPVOID lParam)
{
	tdapi = CThostFtdcTraderApi::CreateFtdcTraderApi();
	TdSpi *tdspi = new TdSpi(tdapi);
	HWND hw = AfxGetApp()->GetMainWnd()->GetSafeHwnd();
	//注册事件处理对象
	tdapi->RegisterSpi(tdspi);
	//订阅共有流和私有流
	tdspi->mainh = hw;
	tdapi->SubscribePublicTopic(THOST_TERT_RESTART);
	tdapi->SubscribePrivateTopic(THOST_TERT_RESTART);
	//注册前置机
	tdapi->RegisterFront("tcp://180.168.146.187:10000");	//模拟
	//和前置机连接
	tdapi->Init();
	tdapi->Join();
	return 0;
}
int  CCTP_mfcDlg::split(char dst[][3], char* str, const char* spl)
{
	int n = 0;
	char *result = NULL;
	result = strtok(str, spl);
	while (result != NULL)
	{
		strcpy(dst[n++], result);
		result = strtok(NULL, spl);
	}
	return n;
}

LRESULT  CCTP_mfcDlg::MyMessage(WPARAM wParam, LPARAM lParam)
{
	CString str;
	CThostFtdcDepthMarketDataField *info;
	info = (CThostFtdcDepthMarketDataField *)wParam;
	CString clog;
	int h, m, s;
	char t[3][3];
	for (int v = 0; v < 3; v++){
		for (int c = 0; c < 3; c++)
		{
			t[v][c] = info->UpdateTime[v * 3 + c];
		}
	}
	//	mask.GetWindowTextW(clog);
	clog = "";
	clog += info->InstrumentID;
	str.Format(_T("%d"), info->Volume);
	clog += "成交量：";
	clog += str;
	clog += "\r\n";
	str.Format(_T("%.2f"), info->OpenInterest);
	clog += "持仓量：";
	clog += str;
	clog += "\r\n";
	str.Format(_T("%.2f"), info->LastPrice);
	clog += "现  价：";
	clog += str;
	clog += "\r\n";
	str.Format(_T("%.2f"), info->BidPrice1);
	clog += "买  价：";
	clog += str;
	clog += "\r\n";
	str.Format(_T("%.2f"), info->AskPrice1);
	clog += "卖  价：";
	clog += str;
	clog += "\r\n";
	str.Format(_T("%d"), info->BidVolume1);
	clog += "委买量：";
	clog += str;
	clog += "\r\n";
	str.Format(_T("%d"), info->AskVolume1);
	clog += "量卖量：";
	clog += str;
	clog += "\r\n";
	clog += info->ActionDay;
	clog += "\r\n";
	clog += t[0];
	clog += ":";
	clog += t[1];
	clog += ":";
	clog += t[2];
	clog += "\r\n";
	str.Format(_T("%d"), info->UpdateMillisec);
	clog += str;
	clog += "\r\n";
	SYSTEMTIME st;
	CString strDate, strTime;
	GetLocalTime(&st);
	char year[5], mon[5], day[5], mi[5];
	char datec[28] = "";
	sprintf(year, "%d", st.wHour);
	sprintf(mon, "%d", st.wMinute);
	sprintf(day, "%d", st.wSecond);
	sprintf(mi, "%d", st.wMilliseconds);
	strcat(datec, "h:");
	strcat(datec, year);
	strcat(datec, "m:");
	strcat(datec, mon);
	strcat(datec, "s:");
	strcat(datec, day);
	strcat(datec, "mi:");
	strcat(datec, mi);
	clog += datec;
	mask.SetWindowTextW(clog);
	return 0;
}
LRESULT  CCTP_mfcDlg::Mycount(WPARAM wParam, LPARAM lParam)
{
	CString str;
	str.Format(_T("%d"), wParam);
	run_msg.SetWindowTextW(str);
	return 0;
}
LRESULT  CCTP_mfcDlg::blcount(WPARAM wParam, LPARAM lParam)
{
	return 0;
}
void CCTP_mfcDlg::OnBnClickedButton1()
{
}
