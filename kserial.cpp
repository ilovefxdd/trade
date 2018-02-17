#include "kserial.h"

bar_list::bar_list()
{
	this->today = NULL;
	this->bars_h = NULL;
	this->cou = NULL;
	this->bar_count = 0;
};
void bar_list::add(bar *b, bar_vol *bv)
{
	bar_ptr *tmp = new bar_ptr();
	bar *t_b = new bar();
	memcpy(t_b, b, sizeof(bar));
	bar_vol *t_bv = new bar_vol();
	memcpy(t_bv, bv, sizeof(bar_vol));
	tmp->b = t_b;
	tmp->bv = t_bv;
	tmp->next = NULL;
	if (this->bars_h == NULL)
	{
		this->cou = tmp;
		this->bars_h = cou;
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


char tick_list::tik_s[38] = "";
char tick_list::tik_f[38] = "";
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
	lp = new list_p();
	lp->fop = tft;
	lp->h = tmp;
	//	HANDLE h = CreateThread(NULL, 0, wr, lp, 0, NULL);
	//	CloseHandle(h);
}





