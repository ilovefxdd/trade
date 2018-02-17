#include "mdthread.h"
#include <QThread>
#include <QObject>
#include  "ThostFtdcUserApiStruct.h"
#include "md.h"
#include <QDebug>
#pragma comment(lib,"thostmduserapi.lib")
#pragma comment(lib,"thosttraderapi.lib")
mdthread::mdthread(QObject *parent):
    QThread(parent)
{
	
}

void mdthread::run()
{
	
	tapi->RegisterSpi(tspi);
	tapi->RegisterFront("tcp://116.236.239.136:41213");

	tapi->Init();
	tapi->Join();
    ////和前置机连接
  
}
