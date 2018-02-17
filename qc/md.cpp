#include "md.h"

#include <cstring>
#include <string>
#include <QDebug>
#include <QTime>
#include <QFile>
#include <QDatastream>
#include <QDir>
#include <QDateTime>
#include "writed.h"
#pragma comment(lib,"thostmduserapi.lib")
#pragma comment(lib,"thosttraderapi.lib")
const QString BROKER_ID = "7070";
const QString NULL_STR = "";

MdSpi::MdSpi(CThostFtdcMdApi *mdapi){
	this->mdapi = mdapi;
	loginRequestID = 10;
}

void MdSpi::OnFrontConnected(){
	//	cout << "已连接上，请求登录" << endl;
	loginField = new CThostFtdcReqUserLoginField();
	strcpy(loginField->BrokerID, BROKER_ID.toLatin1());
	strcpy(loginField->UserID, NULL_STR.toLatin1());
	strcpy(loginField->Password, NULL_STR.toLatin1());

	mdapi->ReqUserLogin(loginField, loginRequestID);
	qDebug() << "logining.....";
}

void MdSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo,
	int nRequestID, bool bIsLast){
	//cout << "登陆成功\n";
	//cout << "交易日：" << mdapi->GetTradingDay() << endl;
	if (pRspInfo->ErrorID == 0){
		qDebug() << "login";
		char *instrumentID[] = { "ru1805", "rb1805", "zn1804", "al1804", "ni1805","ru1809","zn1803","al1803","cu1804","cu1803","RM805","CF805","j1805",
		"ni1809","p1805","y1805","SR805","MA805","bu1805","ZC805"};

		//订阅一个合约所以数量为1
		mdapi->SubscribeMarketData(instrumentID, 20);
		char id[7];
		Pzid * ptmp;
		for (int i = 0; i < 20; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				id[j] = instrumentID[i][j];
			}
			ptmp = new Pzid(id);

			pi[i] = ptmp;
		}
	}
	else{ qDebug() << "login fial"; }
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
void MdSpi::OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData){

	char tmp[7];
	int zq = 0;
	for (int j = 0; j < 7; j++)
	{
		tmp[j] = pDepthMarketData->InstrumentID[j];
	}
	for (int i = 0; i < 20; i++)
	{
		for (int vr = 0; vr < 6; vr++)
		{
			if (tmp[vr] == pi[i]->id[vr])
			{
				zq++;
			}
		}
		if (zq == 6)
		{
			pi[i]->make_b(pDepthMarketData);
		}
		zq = 0;
	}
	emit  MsgSignal(pDepthMarketData);

	//
}

void  Pzid::make_b(CThostFtdcDepthMarketDataField *tick)
{
	int h, m, s;
	char t[3][3];
	int n = split(t, tick->UpdateTime, ":");

	for (int v = 0; v < 3; v++){
		for (int c = 0; c < 3; c++)
		{
			t[v][c] = tick->UpdateTime[v * 3 + c];
		}
	}
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
		bs->add(p_bar, p_bv);
		Wdthread * mtd = new Wdthread();
		mtd->tick = NULL;
		mtd->filen = b_Path + ".bar";
		mtd->p_bar = p_bar;
		mtd->t_b = false;
		mtd->len = sizeof(bar)*bs->bar_count;
		mtd->start();
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

int Pzid::js_tick(mytick *t)
{

	int zq = 0;
	for (int vr = 0; vr < 6; vr++)
	{
		if (t->InstrumentID[vr] == id[vr])
		{
			zq++;
		}
	}
	if (zq == 6)
	{
		ts->add(t);
		Wdthread * mt = new Wdthread();
		mt->tick = t;
		mt->filen = t_Path + ".tik";
		mt->p_bar = NULL;
		mt->t_b = true;
		mt->len = sizeof(mytick)*ts->n;
		mt->start();

	}

	return 0;
}

void Pzid::writed()
{
	//	ts->write(tik, tik_f);
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


void Pzid::of_p(char *pzid)
{
	for (int i = 0; i < 7; i++)
	{
		id[i] = pzid[i];
	}
	id[7] = '\0';
	t_Path = "D:/DATA/tik";
	b_Path = "D:/DATA/1m";
	QDir kDir;

	QTime t = QTime::currentTime();
	int hh = t.hour();
	//	qDebug() << t;
	//	qDebug() << hh;
	QDate d = QDate::currentDate();
	//	qDebug() << d;
	QString  dt = d.toString("yyyy-MM-dd");
	//	qDebug() <<dt;
	if (hh > 20)
	{
		dt = d.addDays(1).toString("yyyy-MM-dd");
	}
	//	qDebug() << this->id;
	t_Path = t_Path + "/" + QString(QLatin1String(this->id));// +"/" + d
	//	qDebug() << t_Path;
	if (!kDir.exists(t_Path))
	{
		kDir.mkpath(t_Path);
	}
	b_Path = b_Path + "/" + QString(QLatin1String(this->id));// +"/" + d
	if (!kDir.exists(b_Path))
	{
		kDir.mkpath(b_Path);
	}
	//CString strTemp;

	t_Path = t_Path + "/" + dt ;
	b_Path = b_Path + "/" + dt ;
	ts = new tick_list();
	bs = new bar_list();
	//建立tik文件、流

	//建立bar文件、流

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
	tik_saved = false;
	bar_saved = false;
}
/*
void  mdThread::run()
{
CThostFtdcMdApi *mdapi = CThostFtdcMdApi::CreateFtdcMdApi();
MdSpi *mdspi = new MdSpi(mdapi);
mdapi->RegisterSpi(mdspi);
////和前置机连接
mdapi->RegisterFront("tcp://116.236.239.136:41213");

mdapi->Init();
mdapi->Join();
}*/
