#ifndef MDSPI_H
#define MDSPI_H
#include "ThostFtdcMdApi.h"
#include <map>
#include <string>
#include <list>
#include <iostream>
#include <fstream>
#include"ThostFtdcUserApiStruct.h"
#include "ThostFtdcTraderApi.h"
#include "kserial.h"
#include <vector>
#define WM_MY_MESSAGE WM_USER+100 
#define WM_MY_count WM_USER+101 
#define WM_MY_bl WM_USER+102
#define DEBUG

using namespace std;
typedef enum 结构 { 顶, 底, 中继, 起点 }jg;
typedef struct 笔
{
	double high;
	double low;
	double o;
	bar ba;
	jg zd;
	int index;
};
typedef vector<笔*> blist;
typedef vector<bar *> barvect;
typedef struct scp
{
	bar_list *bs;
	blist bl;
	barvect tmpbs;
};
class Pzid
{
public:
	Pzid(char *pzid);
	void makek();
	void k_write();

	HWND mainh;
	void make_k(const mytick *tick);
	void make_b(CThostFtdcDepthMarketDataField *tick);
	int js_tick(mytick *tick);
	int split(char dst[][3], char* str, const char* spl);
	int c_h, c_m;//小时和分钟
	double k_h;//
	double k_l;
	int c_vol;//k量
	double c_je;//Ｋ金额
	bar * p_bar;
	bar_vol *p_bv;
	tick_list *ts;//tick　list
	bar_list *bs;//k_Bar list
	blist bl;//
	barvect tmpbs;//
	jg_list *jl; //
	bool wed;
	
	char tik[38];//文件名
	char tik_f[38];//csv
	mytick *pretick;//前一tick
	char id[6];

	static DWORD WINAPI  生成(LPVOID lParam);
	static tick_ptr *wp;//静态tuck头
	static DWORD WINAPI 包含计算(scp * t);
	//	static DWORD WINAPI  w(LPVOID lParam);
	int askv;
	int bibv;
	void writed();
private:
	void process(mytick  *tick);
	void of_p(char *pzid);
	HANDLE sch;
	ofstream fk;
	ofstream ck;
	ofstream fb;
	void gettick();
	void getbar(char* str);
	ifstream itick, ibar;

};
class MdSpi :public CThostFtdcMdSpi{
public:
	MdSpi(CThostFtdcMdApi *mdapi);
	//建立连接时触发
	void OnFrontConnected();
	///登录请求响应
	void OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo,
		int nRequestID, bool bIsLast);
	///登出请求响应
	void OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo,
		int nRequestID, bool bIsLast);
	///订阅行情应答
	void OnRspSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo,
		int nRequestID, bool bIsLast);
	///取消订阅行情应答
	void OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo,
		int nRequestID, bool bIsLast);
	///深度行情通知
	void OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData);

	///生成分钟线
	void trading(int ss, int zz);
	CThostFtdcTraderApi *tdapi;
	HWND mainh;
	Pzid *pi[20];
private:
	CThostFtdcMdApi *mdapi;
	CThostFtdcReqUserLoginField *loginField;
	int loginRequestID;

};

#endif