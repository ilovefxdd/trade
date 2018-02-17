#include "kwidget.h"

KWidget::KWidget(QWidget *parent)
: QWidget(parent)
{
	setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);
}

void KWidget::paintEvent(QPaintEvent *event)
{
	QPainter p(this);
	p.begin(this); //msvc²»ÐèÒª
	QPen pen;

	pen.setColor(QColor(0, 100, 0));
	QBrush brush;
	brush.setColor(QColor(0, 0,0));
	brush.setStyle(Qt::SolidPattern);
	pen.setWidth(3);
	pen.setColor(QColor(255, 0, 0));
	p.setPen(pen);
	p.setBrush(brush);
	p.setPen(pen);
	p.drawRect(0,0, 1000,this->height()-20);
	
	
	
	p.end();
}

void KWidget::mousePressEvent(QMouseEvent *event)
{
	
}

void KWidget::mouseMoveEvent(QMouseEvent *event)
{
}

void KWidget::mouseReleaseEvent(QMouseEvent *event)
{
	
}
void KWidget::draw(){}