#include "stdafx.h"
#include "MdSpi.h"
#include "ThostFtdcUserApiStruct.h"
#include "ThostFtdcTraderApi.h"
#include <iostream>
#include <cstring>
#include <string>
#include <fstream>
#include <list>
#include<vector>


using namespace std;

const string BROKER_ID = "7070";
const string NULL_STR = "";


MdSpi::MdSpi(CThostFtdcMdApi *mdapi){
	this->mdapi = mdapi;
	loginRequestID = 10;
}

void MdSpi::OnFrontConnected(){
	//	cout << "已连接上，请求登录" << endl;
	loginField = new CThostFtdcReqUserLoginField();
	strcpy(loginField->BrokerID, BROKER_ID.c_str());
	strcpy(loginField->UserID, NULL_STR.c_str());
	strcpy(loginField->Password, NULL_STR.c_str());
	mdapi->ReqUserLogin(loginField, loginRequestID);
}

void MdSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo,
	int nRequestID, bool bIsLast){
	//cout << "登陆成功\n";
	//cout << "交易日：" << mdapi->GetTradingDay() << endl;
	if (pRspInfo->ErrorID == 0){
		//	cout << "请求的登陆成功," << "请求ID为" << loginRequestID << endl;
		/***************************************************************/
		//	cout << "尝试订阅行情" << endl;

		char *instrumentID[] = { "zn1802", "ru1805", "ru1809", "RM805", "SR805", "al1802", "rb1805", "CF805", "i1805", "ni1805", "m1805", "jm1805", "al1802", "pp1805", "l1805", "MA805", "TA805", "ag1802", "au1802", "y1805" };
	
	//订阅一个合约所以数量为1
		mdapi->SubscribeMarketData(instrumentID,20);

		Pzid * ptmp;
		for (int i = 0; i <20; i++)
		{
			ptmp = new Pzid(instrumentID[i]);
			ptmp->mainh = mainh;
			pi[i] = ptmp;
		}
	}
}

void MdSpi::OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo,
	int nRequestID, bool bIsLast){

}

//订阅行情应答

void MdSpi::OnRspSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo,
	int nRequestID, bool bIsLast){
}

//取消订阅行情应答
void MdSpi::OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo,
	int nRequestID, bool bIsLast){

}

void Pzid::gettick()
{
	CThostFtdcDepthMarketDataField *tick = new CThostFtdcDepthMarketDataField();
	
}
void Pzid::getbar(char* str)
{
	ifstream	ibar = ifstream(str, ios::in | ios::binary | ios::ate);
	bar * rbar = new bar();
	ibar.seekg(0, ios::beg);
	bar_ptr * tmp;
	while (!ibar.eof())
	{
	//	ibar.read((char *)rbar, sizeof(bar));
	//	bs->add(rbar);
		/*
		if (bs->bar_count > 1000){
			tmp = bs->bars_h;
			bs->bars_h = tmp->next;
			bs->bar_count--;
			delete tmp;
		}
		*/
	}
	ibar.close();
	//if (bs.size() > 0)
	{
		//		PostMessage(mainh, WM_MY_count, (WPARAM)bs.size(), 0);
	}
}

void  Pzid::make_b(CThostFtdcDepthMarketDataField *tick)
{
	int h, m, s;
	char t[3][3];
	int n = split(t, tick->UpdateTime, ":");
	/*
	for (int v = 0; v < 3; v++){
	for (int c = 0; c < 3; c++)
	{
	t[v][c] = tick->UpdateTime[v * 3 + c];
	}
	}*/
	mytick *tt = new mytick();
	//生成tick数据
	tt->AskPrice1 = tick->AskPrice1;
	tt->AskVolume1 = tick->AskVolume1;
	tt->BidPrice1 = tick->BidPrice1;
	tt->BidVolume1 = tick->BidVolume1;
	tt->ClosePrice = 0;
	tt->HighestPrice = tick->HighestPrice;
	tt->LastPrice = tick->LastPrice;
	tt->LowestPrice = tick->LowestPrice;
	tt->OpenInterest = tick->OpenInterest;
	tt->OpenPrice = tick->OpenPrice;
	tt->PreClosePrice = tick->PreClosePrice;
	tt->PreOpenInterest = tick->PreOpenInterest;
	tt->PreSettlementPrice = tick->PreSettlementPrice;
	tt->Turnover = tick->Turnover;
	tt->UpdateMillisec = tick->UpdateMillisec;
	strcat(tt->UpdateTime, t[0]);
	strcat(tt->UpdateTime, ":");
	strcat(tt->UpdateTime, t[1]);
	strcat(tt->UpdateTime, ":");
	strcat(tt->UpdateTime, t[2]);
	strcpy(tt->InstrumentID, tick->InstrumentID);

	tt->Volume = tick->Volume;
	js_tick(tt);
	bool firsttick = false;
	if (tick->UpdateMillisec < 100)		firsttick = true;	else firsttick = false;
	sscanf(t[0], "%d", &h);
	sscanf(t[1], "%d", &m);
	sscanf(t[2], "%d", &s);
	if (h == c_h && m == c_m)
	{
		if (tick->LastPrice > k_h) k_h = tick->LastPrice;
		if (tick->LastPrice < k_l) k_l = tick->LastPrice;
		p_bar->vol = tick->Volume - c_vol;
		p_bar->openinst = tick->OpenInterest;
		p_bar->je = tick->Turnover - c_je;
	}
	if (h > c_h || m > c_m)
	{
		if (p_bar != NULL)
		{
			fk.write((char *)p_bar, sizeof(bar));
			ck<< p_bar->bar_date << "|"
				<<p_bar->bar_time<< "|"
				<< p_bar->close << "|"
				<< p_bar->high << "|"
				<<p_bar->je << "|"
				<< p_bar->low << "|"
				<<p_bar->open << "|"
				<< p_bar->openinst<< "|"
				<< p_bar->vol<< "|"
				 << endl;
			int ss = bl.size();
			PostMessage(mainh, WM_MY_count, (WPARAM)ss, 0);

			if (sch != NULL)
			{
				TerminateThread(sch, 0);
				CloseHandle(sch);
			}
	
		}

		p_bar = new bar();
		p_bv = new bar_vol();
		c_h = h;
		c_m = m;

		strcat(p_bar->bar_time, t[0]);
		strcat(p_bar->bar_time, ":");
		strcat(p_bar->bar_time, t[1]);
		strcpy(p_bar->bar_date, tick->ActionDay);
		p_bar->high = tick->LastPrice;
		p_bar->close = tick->LastPrice;
		p_bar->open = tick->LastPrice;
		p_bar->low = tick->LastPrice;
		c_vol = tick->Volume;
		p_bar->vol = tick->Volume;
		p_bar->openinst = tick->OpenInterest;
		p_bar->je = tick->Turnover;
		c_je = tick->Turnover;
	}
};
int Pzid::split(char dst[][3], char* str, const char* spl)
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

DWORD WINAPI  Pzid::生成(LPVOID lParam)
{
	bar_ptr *tmp;
	bar_ptr *head;
	scp *t = new scp();
	t = (scp *)lParam;
	if (t->bl.size() > 0)
		t->bl.clear();
	if (t->bs->n < 3)return 0;
	tmp = t->bs->bars_h;
	while (tmp->next != NULL)
	{
		if (tmp == t->bs->bars_h)
		{
		}
		else
		{
			包含计算(t);
		}
	}
	return 0;
}

DWORD WINAPI Pzid::包含计算(scp * t)
{
	bar bar1, bar2;
	bar_ptr * tmp = t->bs->bars_h;
	t->tmpbs.clear();
	while (tmp !=NULL)
	{
		t->tmpbs.push_back(tmp->b);
		tmp = tmp->next;
	}
	int ind = t->tmpbs.size() - 1;
	int bh1, bh2, bh3;
	int bl1, bl2, bl3;
	barvect::iterator rep;
	if (ind > 2)
	{
		bar1 = *(t->tmpbs[ind]);
		bar2 = *(t->tmpbs[ind - 1]);
		bh1 = bar1.high;
		bh2 = bar2.high;
		bl1 = bar1.low;
		bl2 = bar2.low;
		if ((bh1 >= bh2) && (bl1 <= bl2))
		{
			if (t->bl.size() == 0) { return 0; }
			if (t->bl[t->bl.size() - 1]->zd == 底)
			{
				bar1.low = bar2.low;
				rep = t->tmpbs.end();
				t->tmpbs.erase(rep - 1);
			}
			if (t->bl[t->bl.size() - 1]->zd == 顶)
			{
				bar1.high = bar2.high;
				rep = t->tmpbs.end();
				t->tmpbs.erase(rep - 1);
			}
			return 0;
		}
		if ((bh1 <= bh2 && (int)bl1 >= bl2))
		{
			if (t->bl.size() == 0) { return 0; }
			if (t->bl[t->bl.size() - 1]->zd == 底)
			{
				bar2.low = bar1.low;
				rep = t->tmpbs.end();
				t->tmpbs.erase(rep);
			}
			if (t->bl[t->bl.size() - 1]->zd == 顶)
			{
				bar2.high = bar1.high;
				rep = t->tmpbs.end();
				t->tmpbs.erase(rep);
			}
		}
	}
	bar ba1, ba2, ba3;
	笔 *bi = new 笔();
	blist::iterator j;
	ind = t->tmpbs.size() - 1;
	if (ind > 7)
	{
		ba1 = *(t->tmpbs[ind - 2]);
		ba2 = *(t->tmpbs[ind - 1]);
		ba3 = *(t->tmpbs[ind]);
		bh1 = ba1.high;
		bh2 = ba2.high;
		bh3 = ba3.high;
		bl1 = ba1.low;
		bl2 = ba2.low;
		bl3 = ba3.low;
		if ((bh2 > bh1 && bh2 > bh3) && (bl2 > bl1 && bl2 > bl3))
		{
			if (t->bl.size() == 0)
			{
				bi->zd = 顶;
				bi->o = 0;
				bi->high = bh2;
				bi->low = bl2;
				bi->ba = ba2;
				bi->index = ind - 1;
				t->bl.push_back(bi);
				
			}
			if (t->bl[t->bl.size() - 1]->zd == 顶 && bh1 > t->bl[t->bl.size() - 1]->high)
			{
				t->bl.pop_back();
				bi->zd = 顶;
				bi->o = 0;
				bi->high = bh2;
				bi->low = bl2;
				bi->ba = ba2;
				bi->index = ind - 1;
				t->bl.push_back(bi);
			
			}
			if (t->bl[t->bl.size() - 1]->zd == 底 && ind > t->bl[t->bl.size() - 1]->index + 4 && bh1 > t->bl[t->bl.size() - 1]->high)
			{
				bi->zd = 顶;
				bi->o = 0;
				bi->high = bh2;
				bi->low = bl2;
				bi->ba = ba2;
				bi->index = ind - 1;
				t->bl.push_back(bi);
			
			}
		}
		if ((bh2 < bh1 && bh2 < bh3) && (bl2 < bl1 && bl2 < bl3))
		{
			if (t->bl.size() == 0)
			{
				bi->zd = 底;
				bi->o = 0;
				bi->high = bh2;
				bi->low = bl2;
				bi->ba = ba2;
				bi->index = ind - 1;
				t->bl.push_back(bi);
			
			}
			if (t->bl[t->bl.size() - 1]->zd == 底 && bl1 < t->bl[t->bl.size() - 1]->low)
			{

				t->bl.pop_back();
				bi->zd = 底;
				bi->o = 0;
				bi->high = bh2;
				bi->low = bl2;
				bi->ba = ba2;
				bi->index = ind - 1;
				t->bl.push_back(bi);
			
			}
			if (t->bl[t->bl.size() - 1]->zd == 顶 && ind > t->bl[t->bl.size() - 1]->index + 4 && bl1 < t->bl[t->bl.size() - 1]->low)
			{
				bi->zd = 底;
				bi->o = 0;
				bi->high = bh1;
				bi->low = bl1;
				bi->ba = ba1;
				bi->index = ind - 1;
				t->bl.push_back(bi);
				//fb.write((char *)bi, sizeof(笔));
			//	PostMessage(mh, WM_MY_bl, (WPARAM)bi, 0);
			}
		}
	}
	return 0;
}
int Pzid::js_tick(mytick *t)
{
	if (strcmp(t->InstrumentID, id) == 0)
	{
		ts->add(t);
		process(t);
		jl->add(t);
	}

	return 0;
}
void MdSpi::trading(int ss, int zz)
{

}
void Pzid::process(mytick* tick)
{
	jgdy *jgp;
	jgp = jl->jg_list_head;
	while (jgp != NULL)
	{
		if (jgp->jg->j == tick->LastPrice)
		{
			break;
		}
		jgp = jgp->next;
	}
}

void Pzid::writed()
{
	ts->write(tik,tik_f);
}

int split(char dst[][3], char* str, const char* spl)
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


void MdSpi::OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData){
	for (int i = 0; i <20; i++)
	{
		if (strcmp(pDepthMarketData->InstrumentID, pi[i]->id) == 0)
		{
			pi[i]->make_b(pDepthMarketData);
		}
	}
	
}
void Pzid::of_p(char *pzid)
{
	SYSTEMTIME st;
	CString strDate, strTime;
	GetLocalTime(&st);
	char year[5], mon[5], day[5];
	char datec[9] = "";
	sprintf(mon, "%d", st.wMonth);
	sprintf(day, "%d", st.wDay);
	sprintf(year, "%d", st.wYear);
	strcat(datec, year);
	strcat(datec, mon);
	strcat(datec, day);
	char tick_p[17] = "c:\\data\\tick\\";
	char bar_p[15] = "c:\\data\\1m\\";

	CString strTemp;
	strcpy(id, pzid);

	strcpy(tik, tick_p);
	strcat(tik, datec);
	strTemp = tik; //char[] to CString //strTemp.Format("%s",tik); char * to CString
	if (!PathFileExists(strTemp))//文件夹不存在则创建
	{
		CreateDirectory(strTemp, NULL);
	}
	strcat(tik, "\\");
	strcat(tik, id);
	strcpy(tik_f, tik);
	strcat(tik_f, ".txt");
	strcat(tik, ".tik");

	char mk[36] = "";
	char csk[36] = "";
	strcat(mk, bar_p);

	strTemp = mk; //char[] to CString //strTemp.Format("%s",tik); char * to CString
	if (!PathFileExists(strTemp))//文件夹不存在则创建
	{
		CreateDirectory(strTemp, NULL);
	}

	strcat(mk, id);
	strcat(csk, mk);
	strcat(mk, ".mk");
	strcat(csk, ".txt");
	ts = new tick_list();
	bs = new bar_list();
	jl = new jg_list();
	//	itick = ifstream(tik, ios::in|ios::binary|ios::ate);
	//getbar(mk);
	bool cz;
	fstream _f;

	_f.open(mk, ios::in);
	
	if (!_f)
		cz = false; else cz = true;
	if (!cz)
	{
		fk.open(mk, ios::out | ios::binary);
	}
	else
	{
		_f.close();
		fk.open(mk, ios::app | ios::binary);
	}
	_f.open(csk, ios::in);

	if (!_f)
		cz = false; else cz = true;
	if (!cz)
	{
		ck.open(csk, ios::out | ios::binary);
	}
	else
	{
		_f.close();
		ck.open(csk, ios::app | ios::binary);
	}
}
Pzid::Pzid(char *pzid)
{
	c_h = 0;
	c_m = 0;
	c_vol = 0;
	p_bar = NULL;
	askv = 0;
	bibv = 0;
	of_p(pzid);
	pretick = NULL;
	wed = false;
	//	gettick();
		
}
