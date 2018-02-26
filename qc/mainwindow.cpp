#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "md.h"
#include "mdthread.h"
#include <QDebug>
#include <QWidget>
#include <QPainter>
#include <QPaintEvent>
#include <QPen>
#include <QBrush>
#include <QFile>
#include <QDatastream>
#include <QDir>
#include <QDateTime>
#include <QTextCodec>

MainWindow::MainWindow(QWidget *parent) :
QMainWindow(parent),
ui(new Ui::MainWindow)
{
	ui->setupUi(this);
	QTextCodec::setCodecForLocale(QTextCodec::codecForName("GB2312"));
	model = new QStandardItemModel();
	model->setColumnCount(7);
	model->setHeaderData(0, Qt::Horizontal, QString::fromLocal8Bit(""));
	model->setHeaderData(1, Qt::Horizontal, QString::fromLocal8Bit(""));
	model->setHeaderData(2, Qt::Horizontal, QString::fromLocal8Bit(""));
	model->setHeaderData(3, Qt::Horizontal, QString::fromLocal8Bit(""));
	model->setHeaderData(4, Qt::Horizontal, QString::fromLocal8Bit(""));
	model->setHeaderData(5, Qt::Horizontal, QString::fromLocal8Bit(""));

	kw = new KWidget();
	kw->setParent(ui->tab_3);
	kw->setMinimumSize(1820, 1080);
	//	kw->adjustSize
	//kw->setGeometry(10, 10, ui->centralWidget->width() - 20, ui->centralWidget->width() - 20);
	qDebug() << kw->width();
	QSizePolicy policy = kw->sizePolicy();
	//	kw->setwidt = ui->tab_3.width() - 10;
	policy.setHorizontalStretch(3);
	policy.setVerticalStretch(3);
	policy.setHorizontalPolicy(QSizePolicy::Maximum);
	kw->show();

	//	model->setHeaderData(1, Qt::Horizontal, QString::fromLocal8Bit(""));
	ui->tableView->setModel(model);

	ui->tableView->horizontalHeader()->setDefaultAlignment(Qt::AlignLeft);

	//	ui->tableView->horizontalHeader()->setFixedSize(0, QHeaderView::Fixed);
	//	ui->tableView->horizontalHeader()->setResizeMode(1, QHeaderView::Fixed);
	ui->tableView->setColumnWidth(0, 101);
	ui->tableView->setColumnWidth(1, 102);

	ls << "ru1805" << "rb1805" << "zn1804" << "al1804" << "ni1805" << "ru1809" << "zn1803" << "al1803" << "cu1804" << "cu1803" << "RM805" << "CF805" << "j1805" <<
		"ni1809" << "p1805" << "y1805" << "SR805" << "MA805" << "bu1805" << "ZC805";
	for (int i = 0; i < 20; i++)
	{
		model->setItem(i, 0, new QStandardItem(ls[i]));// ui->tw->item(i, 0)->text()));
		for (int c = 1; c < 7; c++)
		{
			model->setItem(i, c, new QStandardItem(""));
		}

		model->item(i, 0)->setForeground(QBrush(QColor(255, 0, 0)));

		model->item(i, 0)->setTextAlignment(Qt::AlignCenter);
		for (int c = 0; c < 7; c++)
		{
			model->item(i, c)->setBackground(QBrush(QColor(50, 55, 100)));
		}
	}

	mdthread * mt = new mdthread();
	mdapi = CThostFtdcMdApi::CreateFtdcMdApi();
	mdspi = new MdSpi(mdapi);
	//	QPushButton* b1 = new QPushButton(this);	
	connect(mdspi, SIGNAL(MsgSignal(CThostFtdcDepthMarketDataField*)), this, SLOT(mSlot(CThostFtdcDepthMarketDataField*)));
	//	connect(b1, &QPushButton::pressed,this, &MainWindow::bSlot);
	mt->tapi = mdapi;
	mt->tspi = mdspi;
	mt->start();
}

void MainWindow::mSlot(CThostFtdcDepthMarketDataField* tep)
{
	//double zxj, mj, mj;
	//	int vol, zc;
	for (int it = 0; it < 20; it++)
	if (tep->InstrumentID == ls[it])//ui-tableView->indexAt(QPoint(it, 0)).data())
	{

		model->setItem(it, 1, new QStandardItem(QString("%1").arg(tep->LastPrice)));
		model->setItem(it, 2, new QStandardItem(QString("%1").arg(tep->AskPrice1)));
		model->setItem(it, 3, new QStandardItem(QString("%1").arg(tep->BidPrice1)));
		model->setItem(it, 4, new QStandardItem(QString("%1").arg(tep->Volume, 10)));
		model->setItem(it, 5, new QStandardItem(QString::number(long(tep->OpenInterest), 10)));
		model->item(it, 0)->setForeground(QBrush(QColor(255, 0, 0)));

		model->item(it, 0)->setTextAlignment(Qt::AlignCenter);
		for (int c = 0; c < 7; c++)
		{
			model->item(it, c)->setBackground(QBrush(QColor(155, 155, 100)));
		}
	}

}
MainWindow::~MainWindow()
{
	delete ui;
}
/*
void MainWindow::bSlot()
{
lin2->setText(QString("%1").arg(156.36));
}
for (int it = 0; it < 5; it++)//tablewidget
{
if (tep->InstrumentID == ui->tw->item(it, 0)->text())//tablewidget
{
ui->tw->setItem(it, 1, new QTableWidgetItem(QString("%1").arg(tep->LastPrice)));
ui->tw->setItem(it, 2, new QTableWidgetItem(QString("%1").arg(tep->AskPrice1)));
ui->tw->setItem(it, 3, new QTableWidgetItem(QString("%1").arg(tep->BidPrice1)));
ui->tw->setItem(it, 4, new QTableWidgetItem(QString("%1").arg(tep->Volume, 10)));
ui->tw->setItem(it, 5, new QTableWidgetItem(QString::number(long(tep->OpenInterest), 10)));
}
}
*/
void MainWindow::paintEvent(QPaintEvent *)
{

}

void MainWindow::on_pushButton_3_clicked()
{

}
void MainWindow::drawjg()
{
}
