#include "ThostFtdcMdApi.h"
#include <map>
#include <string>
#include <list>
#include <iostream>

#include"ThostFtdcUserApiStruct.h"


 struct mytick
{

	TThostFtdcPriceType	LastPrice;
	///上次结算价
	TThostFtdcPriceType	PreSettlementPrice;
	///昨收盘
	TThostFtdcPriceType	PreClosePrice;
	///昨持仓量
	TThostFtdcLargeVolumeType	PreOpenInterest;
	///今开盘
	TThostFtdcPriceType	OpenPrice;
	///最高价
	TThostFtdcPriceType	HighestPrice;
	///最低价
	TThostFtdcPriceType	LowestPrice;
	///数量
	TThostFtdcVolumeType	Volume;
	///成交金额
	TThostFtdcMoneyType	Turnover;
	///持仓量
	TThostFtdcLargeVolumeType	OpenInterest;
	///今收盘
	TThostFtdcPriceType	ClosePrice;

	///最后修改时间
	TThostFtdcTimeType	UpdateTime;
	///最后修改毫秒
	TThostFtdcMillisecType	UpdateMillisec;
	///申买价一
	TThostFtdcPriceType	BidPrice1;
	///申买量一
	TThostFtdcVolumeType	BidVolume1;
	///申卖价一
	TThostFtdcPriceType	AskPrice1;
	///申卖量一
	TThostFtdcVolumeType	AskVolume1;
	//TThostFtdcVolumeType	AskBidV;
	TThostFtdcInstrumentIDType	InstrumentID;
	///当日均价
	TThostFtdcPriceType	AveragePrice;
};

 struct bar
{
	char bar_date[9];
	char bar_time[9];
	double high;
	double low;
	double open;
	double close;
	int vol;
	int openinst;
	double je;
};
 struct bar_vol
{
	int df;
	int kf;
};
struct tick_ptr{
	mytick *tick;
	tick_ptr *next;
};
 struct list_p
{
	tick_ptr * h;
	FILE *fop;
	char tik_s[38];
	char tik_f[38];
};
 
class tick_list
{
private	:
	 tick_ptr *head;
public:
	static tick_ptr *wp;
	list_p * lp;
	tick_ptr * cou;
	FILE * tft;
	
	int n;
	tick_list();
	static char tik_s[38];
	static char tik_f[38];
	//static DWORD WINAPI w(LPVOID lParam);
	void add(mytick * tick);
	void write(char tik[38],char t[38]);
	double high();
	double low();
	double zf();
	double open();
	double close();
	double zd();
	
//	ofstream csv;
//	fstream _file;
//	ofstream ft;
};
struct bar_ptr //k指针
{ 
	bar *b;
	bar_vol *bv;
	bar_ptr *next;
};
class k
{
	k();

};
class bar_list
{
public:
	bar_list();
	int n;
	int bar_count;
	bar_ptr * bars_h;
	bar_ptr * cou;
	bar_ptr * today;
	void add(bar *b, bar_vol *bv);
	int get_bar(int n);
	double ma(int n, int p);
	double mav(int n, int p);
	double macd_d(int a, int b, int c, int p);
	double macd_e(int a, int b, int c, int p);
	double vvh(int n, int p);
	double vvl(int n, int p);
};

