
#ifndef MAINWINDOW_H
#define MAINWINDOW_H
#include <QMainWindow>
#include <QLineEdit>
#include "md.h"
#include <QDebug>
#include <QStandarditemmodel>
#include <QList>
#include <QHeaderView>
#include "kwidget.h"
namespace Ui {
	class MainWindow;
}

class MainWindow : public QMainWindow
{
	Q_OBJECT

public:
	explicit MainWindow(QWidget *parent = 0);
	~MainWindow();
	MdSpi * mdspi;
	CThostFtdcMdApi *mdapi;
	QStandardItemModel  *model;
	QList<QString> ls;
	KWidget * kw;
signals:
	void bs();
	public slots:
	void mSlot(CThostFtdcDepthMarketDataField* tep);
protected:
	void paintEvent(QPaintEvent *);
	//	void bSlot();
	private slots:

	//void drawklist(bar_list bl);
	void drawjg();
	void on_pushButton_3_clicked();

private:
	Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H
