
#ifndef MD_H
#define MD_H
#include "ThostFtdcMdApi.h"
#include "ThostFtdcUserApiDataType.h"
#include "ThostFtdcUserApiStruct.h"
#include <QObject>
#include "writed.h"
#include <QFile>
#include <QDataStream>

class Pzid
{
public:
	Pzid(char *pzid);
	void makek();
	void k_write();
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
	bool wed;
	QString t_Path;// = "D:/DATA/tik";
	QString b_Path;


	mytick *pretick;//前一tick
	char id[7];
	int askv;
	int bibv;
	bool tik_saved;
	bool bar_saved;
	void writed();
private:
	void of_p(char *pzid);
};
class MdSpi :public QObject, public CThostFtdcMdSpi{
	Q_OBJECT
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
    bool showed;
Pzid * pi[20];
public: signals:
	void MsgSignal(CThostFtdcDepthMarketDataField* tep);
		
private:
    CThostFtdcMdApi *mdapi;
    CThostFtdcReqUserLoginField *loginField;
    int loginRequestID;

};
#endif // MD_H

