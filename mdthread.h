#ifndef MDTHREAD_H
#define MDTHREAD_H
#include "kwidget.h"
#include <QThread>
#include "md.h"
#include <QObject>
#include  "ThostFtdcUserApiStruct.h"
class mdthread : public QThread
{
	Q_OBJECT
public:
	explicit mdthread(QObject *parent = 0);
	MdSpi * tspi;
	CThostFtdcMdApi *tapi;
protected:
	void run();//run


};

#endif // MDTHREAD_H
