#include "stdafx.h"
#include "CTP_mfc.h"
#include "CTP_mfcDlg.h"
#include "afxdialogex.h"
#ifdef _DEBUG
#define new DEBUG_NEW
#endif

bar_list::bar_list()
{
	this->today = NULL;
	this->bars_h = NULL;
	this->cou = NULL;
	this->bar_count = 0;
};
void bar_list::add(bar *b,bar_vol *bv)
{
	bar_ptr *tmp = new bar_ptr();
	bar *t_b=new bar();
	memcpy(t_b, b, sizeof(bar));
	bar_vol *t_bv = new bar_vol();
	memcpy(t_bv, bv, sizeof(bar_vol));
	tmp->b =t_b;
	tmp->bv = t_bv;
	tmp->next =NULL;
	if (this->bars_h == NULL)
	{
		this->cou = tmp;
		this->bars_h =cou;
	}
	else
	{
		cou->next = tmp;
		cou = tmp;
	}
	bar_count++;
}
tick_list::tick_list()
{
	this->n = 0;
	head = NULL;
}
jg_list::jg_list()
{
	aj = new fjb_adom();
	bj = new fjb_adom();
}
void jg_list::add(mytick * tick)
{
	jgdy * p;
	jgdy * tmp;
	fjb_adom * j;
	bool find = false;
	if (jg_list_head == NULL)
	{
		tmp = new jgdy();
		j = new fjb_adom();
		j->j = tick->LastPrice;
		j->askv = tick->AskVolume1;
		j->bidv = tick->BidVolume1;
		tmp->jg = j;
		jg_list_head = tmp;

	}
	p = this->jg_list_head;
	while (p != NULL)
	{
		if (tick->LastPrice == p->jg->j)
		{
			p->jg->v += tick->Volume;
			find = true;
			break;
		}
		else
			p = p->next;
	}
	if (!find)
	{
		tmp = new jgdy();
		j = new fjb_adom();
		j->j = tick->LastPrice;
		j->askv = 0;
		j->bidv = 0;
		tmp->jg = j;
		tmp->next = this->jg_list_head;
		this->jg_list_head = tmp;
	}

	p = this->jg_list_head;
	find = false;

	while (p != NULL)
	{
		if (tick->AskPrice1 == p->jg->j)
		{
			p->jg->askv += tick->AskVolume1;
			find = true;
			break;
		}
		else
			p = p->next;
	}
	if (!find)
	{
		tmp = new jgdy();
		j = new fjb_adom();
		j->j = tick->AskPrice1;
		j->askv = tick->AskVolume1;
		j->bidv = 0;
		tmp->jg = j;
		tmp->next = this->jg_list_head;
		this->jg_list_head = tmp;
	}

	p = this->jg_list_head;
	find = false;

	while (p != NULL)
	{
		if (tick->BidPrice1 == p->jg->j)
		{
			p->jg->bidv += tick->BidVolume1;
			find = true;
			break;
		}
		else
			p = p->next;
	}
	if (!find)
	{
		tmp = new jgdy();
		j = new fjb_adom();
		j->j = tick->BidPrice1;
		j->askv = 0;
		j->bidv = tick->BidVolume1;
		tmp->jg = j;
		tmp->next = this->jg_list_head;
		this->jg_list_head = tmp;
	}
}

void jg_list::getpk(double jg)
{
}
char tick_list::tik_s[38] = "";
char tick_list::tik_f[38] = "";
tick_ptr * tick_list::wp = NULL;
void tick_list::add(mytick *tick)
{
	tick_ptr *tmp = new tick_ptr();
	tick_ptr *del;
	mytick *t = new mytick();
	memcpy(t, tick, sizeof(mytick));
	tmp->tick = t;
	tmp->next = NULL;
	if (head == NULL)
	{
		head = tmp;
		cou = tmp;
	}
	else
	{
		cou->next = tmp;
		cou = tmp;
	}
	n++;
}
DWORD WINAPI  tick_list::w(LPVOID lParam)
{
	list_p * pp = (list_p *)lParam;
	tick_ptr *tmp = new tick_ptr();
	tmp = pp->h;
	fstream _file;
	ofstream ft;
	bool cz = false;
	ofstream csv;
	_file.open(pp->tik_s, ios::in);
	if (!_file)
		cz = false; else cz = true;
	if (!cz)
	{
		ft.open(pp->tik_s, ios::out | ios::binary);
	}
	else
	{
		_file.close();
		ft.open(pp->tik_s, ios::app | ios::binary);
	}
	_file.open(pp->tik_f, ios::in);
	if (!_file)
		cz = false; else cz = true;
	if (!cz)
	{
		csv.open(pp->tik_f, ios::out );
	}
	else
	{
		_file.close();
		csv.open(pp->tik_f, ios::app );
	}
	while (tmp != NULL)
	{
		ft.write((char *)tmp->tick, sizeof(mytick));

		csv 
			<< tmp->tick->InstrumentID << "|"
			<< tmp->tick->LastPrice <<"|"
			<< tmp->tick->PreSettlementPrice <<"|"
			<< tmp->tick->PreClosePrice << "|"
			<< tmp->tick->Volume <<"|"
			<< tmp->tick->PreOpenInterest <<"|"
			<< tmp->tick->UpdateTime << "|"
			<< tmp->tick->UpdateMillisec << "|"
			<< tmp->tick->BidPrice1 << "|"
			<< tmp->tick->BidVolume1 << "|"
			<< tmp->tick->AskPrice1 << "|"
			<< tmp->tick->AskVolume1 << "|"
			<< tmp->tick->Turnover << "|"
			<< tmp->tick->UpdateTime << "|"
			<< tmp->tick->OpenInterest << "|" << endl;
		tmp = tmp->next;
	}
	ft.close();
	csv.close();
	//ft.close();
	return 0;
}
void tick_list::write(char tik[38], char t[38])
{
	lp = new list_p();
	strcpy(lp->tik_s, tik);
	strcpy(lp->tik_f, t);
	lp->h = head;
	CreateThread(NULL, 0, w,lp, 0, NULL);
}
tj::tj()
{

}
void tj::ontick(CThostFtdcDepthMarketDataField *tick)
{
	mxs.ticks++;
	mxs.bars;
	mxs.ml;
	mxs.mc;
	mxs.qt;
	mxs.mll;
	mxs.mcl;
	mxs.h_l;
	mxs.cou;
}

