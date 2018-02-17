#include "writed.h"
#include <QFile>
#include <QDataStream>
Wdthread::Wdthread(QObject *parent) :
QThread(parent)
{
	t_b = false;
}
void Wdthread::run()
{
	if (t_b)
	{
		QString t_f = filen;
		QFile  tik_f(t_f);
		if (!tik_f.open(QIODevice::ReadWrite))
		{
			return;
		}

		QDataStream tik_df(&tik_f);
		tik_df.skipRawData(len);
		tik_df.writeRawData((char *)tick, sizeof(mytick));
		tik_f.close();
		
	}else
	{
		QString t_f = filen;
		QFile  tik_f(t_f);
		if (!tik_f.open(QIODevice::ReadWrite))
		{
			return;
		}

		QDataStream tik_df(&tik_f);
		tik_df.skipRawData(len);
		tik_df.writeRawData((char *)p_bar, sizeof(bar));
		tik_f.close();
	}
}
