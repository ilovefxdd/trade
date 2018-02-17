#ifndef WRITED_H
#define WRITED_H
#include <QObject>
#include <QThread>


#include "kserial.h"
class Wdthread : public QThread
{
	Q_OBJECT
public:
	explicit Wdthread(QObject *parent = 0);
	mytick *tick;
	QString filen;
	bar * p_bar;
	int len;
	bool t_b;
protected:
	void run();//run


};

#endif // WRITED_H